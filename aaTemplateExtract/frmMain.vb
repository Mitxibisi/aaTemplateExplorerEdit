'The MIT License (MIT)
'
'Copyright (c) 2014 Eliot Landrum
'
'Permission is hereby granted, free of charge, to any person obtaining a copy of
'this software and associated documentation files (the "Software"), to deal in
'the Software without restriction, including without limitation the rights to
'use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
'the Software, and to permit persons to whom the Software is furnished to do so,
'subject to the following conditions:
'
'The above copyright notice and this permission notice shall be included in all
'copies or substantial portions of the Software.
'
'THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
'IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
'FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
'COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
'IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
'CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Imports System.IO
Imports System.Text
Imports System.Xml.Serialization
Imports System.Xml.Linq

Public Class frmMain

    Dim aaTemplateExtract As aaTemplateExtract
    Dim aaExcelData As aaExcelData
    Dim AuthenticationMode As String
    Dim ExportFolder As String
    Dim ImportExcel As String
    Public LoneAttributes As String() = {}
    Public ArrayAttributes As String() = {}

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        lblStatus.Text = "Initializing"

        UpdateAttr()

        ' set up the new GRAccess client
        aaTemplateExtract = New aaTemplateExtract


        aaExcelData = New aaExcelData

        ' fill in a default node name
        txtNodeName.Text = Environment.MachineName

        ' get the list of Galaxies from the local node and fill the combo box with the collection
        cmboGalaxyList.DataSource = aaTemplateExtract.getGalaxies(txtNodeName.Text)

        ' do any UI clean up work that might be needed when the galaxy name changes
        refreshGalaxyInfo(cmboGalaxyList.SelectedValue)

        Refres_Instances(1)

        btnExport.Enabled = False

        lblStatus.Text = ""

    End Sub

    Private Sub cmboGalaxyList_SelectionChangeCommitted(ByVal sender As Object, ByVal e As EventArgs) Handles cmboGalaxyList.SelectionChangeCommitted
        Dim senderComboBox As ComboBox = CType(sender, ComboBox)

        If (senderComboBox.SelectedValue IsNot Nothing) Then
            refreshGalaxyInfo(senderComboBox.SelectedValue)
        Else
            clearGalaxyInfo()
        End If
    End Sub

    Public Sub refreshGalaxyInfo(ByVal GalaxyName)
        lblStatus.Text = "Refreshing Galaxy Info"
        aaTemplateExtract.setGalaxy(GalaxyName)
        grpLoginInput.Visible = aaTemplateExtract.showLogin
        lblSecurityType.Text = aaTemplateExtract.authMode

        If aaTemplateExtract.loggedIn Then
            ' this would happen if there's no security and we automatically login
            lstTemplates.DataSource = aaTemplateExtract.getTemplates()
        Else
            lstTemplates.Items.Clear()
        End If
        lblStatus.Text = ""
    End Sub

    Public Sub clearGalaxyInfo()
        lstTemplates.DataSource = Nothing
        txtUserInput.Clear()
        txtPwdInput.Clear()
        btnExport.Enabled = False
    End Sub

    Public Sub ExportTemplatesToFile(ByVal filePath As String, ByVal templateNames As String(), ByVal progressBar As ProgressBar)
        Dim outputFile As String = Path.Combine(filePath, "atributos_extraidos.csv")

        Try
            ' Configurar la barra de progreso
            progressBar.Minimum = 0
            progressBar.Maximum = templateNames.Length
            progressBar.Value = 0
            progressBar.Step = 1

            ' Usar StreamWriter para mejor rendimiento en escritura de archivos
            Using writer As New StreamWriter(outputFile, False, Encoding.UTF8)
                writer.WriteLine("Plantilla,Nombre,Plantilla derivada,Descripción,Historizado,Eventos,Alarm,Unidad") ' Encabezado CSV

                For Each templateName In templateNames
                    Dim templateData = aaTemplateExtract.getTemplateData(templateName)

                    ' Procesar atributos discretos
                    For Each attr As aaFieldAttributeDiscrete In templateData.FieldAttributesDiscrete
                        writer.WriteLine($"{templateName},{attr.Name},{attr.TemplateName},{attr.Description},{attr.Historized},{attr.Events},{attr.Alarm},{attr.EngUnits}")
                    Next

                    ' Procesar atributos analógicos
                    For Each attr As aaFieldAttributeAnalog In templateData.FieldAttributesAnalog
                        writer.WriteLine($"{templateName},{attr.Name},{attr.TemplateName},{attr.Description},{attr.Historized},{attr.Events},{attr.Alarm},{attr.EngUnits}")
                    Next

                    ' Incrementar la barra de progreso
                    progressBar.PerformStep()
                Next
            End Using

            ' Restablecer barra de progreso
            progressBar.Value = 0

            MessageBox.Show($"Exportación completada: {templateNames.Length} plantilla(s) exportada(s).", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As IOException
            MessageBox.Show($"Error de archivo: {ex.Message}", "Error de E/S", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As UnauthorizedAccessException
            MessageBox.Show("Acceso denegado. Verifique los permisos del archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        Catch ex As Exception
            MessageBox.Show($"Error inesperado: {ex.Message}{vbCrLf}{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub linkLblSelectAll_Click(sender As Object, e As EventArgs) Handles linkLblSelectAll.Click
        For x = 1 To (lstTemplates.Items.Count - 1)
            lstTemplates.SetSelected(x, True)
        Next x
    End Sub


    Private Sub linkLblSelectNone_Click(sender As Object, e As EventArgs) Handles linkLblSelectNone.Click
        For x = 1 To (lstTemplates.Items.Count - 1)
            lstTemplates.SetSelected(x, False)
        Next x
    End Sub


    Private Sub btnBrowseFolders_Click(sender As Object, e As EventArgs) Handles btnBrowseFolders.Click
        If dlgFolderBrowser.ShowDialog() = DialogResult.OK Then
            ExportFolder = dlgFolderBrowser.SelectedPath
            lblFolderPath.Text = ExportFolder

            If lstTemplates.SelectedItems.Count > 0 And ExportFolder IsNot Nothing And aaTemplateExtract.loggedIn Then
                btnExport.Enabled = True
            Else
                btnExport.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnRefreshGalaxies_Click(sender As Object, e As EventArgs) Handles btnRefreshGalaxies.Click
        lblStatus.Text = "Refreshing Available Galaxies"
        clearGalaxyInfo()

        ' get the list of Galaxies from the local node and fill the combo box with the collection
        cmboGalaxyList.DataSource = aaTemplateExtract.getGalaxies(txtNodeName.Text)

        ' do any UI clean up work that might be needed when the galaxy name changes
        refreshGalaxyInfo(cmboGalaxyList.SelectedValue)
        lblStatus.Text = ""
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click

        Dim y As Integer
        y = lstTemplates.SelectedItems.Count - 1
        Dim TemplateNames(0 To y) As String

        Dim x As Integer = 0
        For Each Template In lstTemplates.SelectedItems
            TemplateNames(x) = Template
            x += 1
        Next

        lblStatus.Text = "Exporting " & lstTemplates.SelectedItems.Count.ToString & " Templates"

        exportTemplatesToFile(ExportFolder, TemplateNames, ProgressBar1)

        lblStatus.Text = "Archivo CSV generado"

    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        lblStatus.Text = "Logging in"

        refreshGalaxyInfo(cmboGalaxyList.SelectedValue)

        If aaTemplateExtract.login(txtUserInput.Text, txtPwdInput.Text) >= 0 Then
            lblStatus.Text = "Logged In " & cmboGalaxyList.Text & " Wich User " & txtUserInput.Text
        Else
            lblStatus.Text = "Error logging in"
        End If

    End Sub


    Private Sub btnRefreshTemplates_Click(sender As Object, e As EventArgs) Handles btnRefreshTemplates.Click
        lblStatus.Text = "Refreshing Template List"
        lstTemplates.DataSource = aaTemplateExtract.getTemplates(chkHideBaseTemplates.CheckState)
        lblStatus.Text = ""
    End Sub

    Private Sub btnCreateInstance_Click() Handles btnNewInstance.Click
        ' Contar el total de GroupBox en TabPage2
        Dim totalGroupBoxes As Integer = TabPage2.Controls.OfType(Of GroupBox)().Count()
        For Each grupo As GroupBox In TabPage2.Controls.OfType(Of GroupBox)()
            Dim i As Integer
            i = i + 1
            lblStatus.Text = "Creating Instance Number " & i & "/" & totalGroupBoxes
            ' Buscar los TextBox dentro de cada GroupBox por Tag
            Dim txtPlantilla As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = "Plantilla")
            Dim txtNombreInstancia As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = "NombreInstancia")
            Dim txtArea As TextBox = TabPage2.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = "Area")
            Dim txtCodigoObjeto As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = LoneAttributes(0))
            Dim txtDesObjeto As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = LoneAttributes(1))
            Dim txtMapeado As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = ArrayAttributes(0))

            ' Verificar que todos los campos existen antes de llamar a la función
            If txtPlantilla IsNot Nothing AndAlso txtNombreInstancia IsNot Nothing Then
                aaTemplateExtract.createInstance(txtPlantilla.Text, txtNombreInstancia.Text, txtArea?.Text, txtCodigoObjeto?.Text, txtDesObjeto?.Text, txtMapeado?.Text, LoneAttributes, ArrayAttributes)
            End If
        Next
        lblStatus.Text = ""
        If aaTemplateExtract.loggedIn Then
            MessageBox.Show("New Instances created")
        End If
    End Sub

    Private Sub btnCreateInstances_Click(sender As Object, e As EventArgs) Handles btnCreateInstance.Click
        Refres_Instances()
    End Sub

    Private Sub Refres_Instances(Optional ByVal instances As Integer = 0)
        Dim lastTextBoxMultiple As Boolean = False
        Dim numInstancias As Integer

        UpdateAttr()

        ' Limpiar campos previos, pero NO el NumericUpDown
        For Each control As Control In TabPage2.Controls
            If Not control.Name = "NumericUpDown2" Then
                control.Dispose() ' Eliminar todos los controles excepto el NumericUpDown
            End If
        Next

        ' Verificar si el NumericUpDown ya existe
        Dim numInstancesSelector As NumericUpDown = TryCast(TabPage2.Controls("NumericUpDown2"), NumericUpDown)

        ' Si no existe, crear uno nuevo
        If numInstancesSelector Is Nothing Then
            numInstancesSelector = New NumericUpDown()
            numInstancesSelector.Name = "NumericUpDown2"
            numInstancesSelector.Location = New Point(170, 10)
            numInstancesSelector.Width = 50
            numInstancesSelector.Minimum = 1
            numInstancesSelector.Maximum = 100
            numInstancesSelector.Value = 1 ' Valor inicial si es necesario

        End If

        ' Crear botón superior
        Dim btnSetInstances As New Button()
        btnSetInstances.Name = "btnCreateInstance"
        btnSetInstances.Text = "Generar instancias"
        btnSetInstances.Location = New Point(10, 10)
        btnSetInstances.Width = 150

        ' Agregar evento al botón para regenerar instancias
        AddHandler btnSetInstances.Click, AddressOf btnCreateInstances_Click

        If instances > 0 Then
            ' Obtener número de instancias desde el NumericUpDown
            numInstancias = instances
        Else
            ' Obtener número de instancias desde el NumericUpDown
            numInstancias = numInstancesSelector.Value
        End If

        ' Crear Label
        Dim areaEtiqueta As New Label()
        areaEtiqueta.Text = "Area"
        areaEtiqueta.Location = New Point(10, 43)
        areaEtiqueta.AutoSize = True

        ' Crear TextBox
        Dim areaTextBox As New TextBox()
        areaTextBox.Width = 150
        areaTextBox.Location = New Point(50, 40)
        areaTextBox.Tag = "Area"

        TabPage2.Controls.Clear()

        TabPage2.Controls.Add(btnSetInstances)
        TabPage2.Controls.Add(numInstancesSelector)
        TabPage2.Controls.Add(areaEtiqueta)
        TabPage2.Controls.Add(areaTextBox)

        ' Definir nombres de los campos y sus Tags
        Dim nombresAtributes As List(Of String) = New List(Of String) From {"Plantilla", "Nombre Instancia"}

        Dim tagsCamposIntermedios As List(Of String) = New List(Of String) From {"Plantilla", "NombreInstancia"}

        If Not String.IsNullOrWhiteSpace(LoneAttributes(0)) Then
            nombresAtributes.AddRange(LoneAttributes)
            tagsCamposIntermedios.AddRange(LoneAttributes)
        End If
        If Not String.IsNullOrWhiteSpace(ArrayAttributes(0)) Then
            nombresAtributes.AddRange(ArrayAttributes)
            tagsCamposIntermedios.AddRange(ArrayAttributes)
        End If

        ' Convertir a array
        Dim nombresCampos As String() = nombresAtributes.ToArray()
        ' Convertir a array
        Dim tagsCampos As String() = tagsCamposIntermedios.ToArray()

        ' Espaciado inicial (debajo del botón y NumericUpDown)
        Dim yOffset As Integer = 65

        For i As Integer = 0 To numInstancias - 1
            ' Crear un GroupBox para cada instancia
            Dim grupo As New GroupBox()
            Dim Height As Integer = 84
            grupo.Text = "Instancia " & (i + 1)
            grupo.Width = TabPage2.Width - 20
            If Not String.IsNullOrWhiteSpace(LoneAttributes(0)) Then
                Height = Height + (LoneAttributes.Length * 30)
            End If
            If Not String.IsNullOrWhiteSpace(ArrayAttributes(0)) Then
                Height = Height + (ArrayAttributes.Length * 60)
            End If
            grupo.Height = Height
            grupo.Location = New Point(10, yOffset)

            yOffset += grupo.Height + 10 ' Espaciado entre grupos

            ' Añadir los campos dentro del grupo
            Dim campoOffset As Integer = 20

            For j As Integer = 0 To nombresCampos.Count - 1

                ' Crear Label
                Dim nuevaEtiqueta As New Label()
                nuevaEtiqueta.Text = nombresCampos(j)
                nuevaEtiqueta.Location = New Point(10, campoOffset)
                nuevaEtiqueta.AutoSize = True

                ' Crear TextBox
                Dim nuevoTextBox As New TextBox()
                nuevoTextBox.Width = 150
                nuevoTextBox.Location = New Point(150, campoOffset)
                nuevoTextBox.Tag = tagsCampos(j) ' ASIGNAR EL TAG CORRECTAMENTE
                If Array.IndexOf(ArrayAttributes, tagsCampos(j)) >= 0 Then
                    nuevoTextBox.Height = 50
                    nuevoTextBox.Multiline = True
                    nuevoTextBox.ScrollBars = ScrollBars.Vertical
                    lastTextBoxMultiple = True
                End If
                ' Agregar controles al GroupBox
                grupo.Controls.Add(nuevaEtiqueta)
                grupo.Controls.Add(nuevoTextBox)

                ' Si el TextBox anterior fue multilínea, ajustar solo si es necesario
                If lastTextBoxMultiple Then
                    campoOffset += 10 + nuevoTextBox.Height ' Ajuste basado en la altura
                    lastTextBoxMultiple = False
                Else
                    ' Ajustar campoOffset basado en el tamaño real del TextBox
                    campoOffset += nuevoTextBox.Height + 10 ' Espaciado base entre campos
                End If
            Next

            ' Agregar el GroupBox al TabPage
            TabPage2.Controls.Add(grupo)
        Next

        ' Crear botón inferior para procesar instancias
        Dim btnProcessInstances As New Button()
        btnProcessInstances.Name = "btnNewInstance"
        btnProcessInstances.Text = "Crear instancias"
        btnProcessInstances.Location = New Point(10, yOffset)
        btnProcessInstances.Width = 200

        ' Agregar evento al botón para procesar instancias
        AddHandler btnProcessInstances.Click, AddressOf btnCreateInstance_Click

        ' Agregar botón a TabPage
        TabPage2.Controls.Add(btnProcessInstances)
    End Sub

    Public Sub UpdateAttr()
        LoneAttributes = txtAtributes.Text.Split(",")
        ArrayAttributes = txtArrayAtributes.Text.Split(",")
    End Sub

    Private Sub btnLoadData_Click(sender As Object, e As EventArgs) Handles btnLoadData.Click
        Try
            Dim openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "Archivos MLSM y XLSX (*.mlsm;*.xlsx)|*.mlsm;*.xlsx"
            openFileDialog.Title = "Seleccionar un archivo"

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                ImportExcel = openFileDialog.FileName

                Dim InstancesData = aaExcelData.CargarDatosMapeado(ImportExcel, Integer.Parse(txtIname.Text), Integer.Parse(txtItemplate.Text), Integer.Parse(txtIMap.Text))

                añadir_Mapeado(InstancesData)
            End If
        Catch y As Exception
            Debug.WriteLine(y)
        End Try
    End Sub

Private Sub añadir_Mapeado(InstancesData)
        Dim i As Integer = 0

        Refres_Instances(InstancesData.Count)

        For Each grupo As GroupBox In TabPage2.Controls.OfType(Of GroupBox)()
            ' Verificar que el índice i no exceda el tamaño de InstancesData
            If i >= InstancesData.Count Then Exit For

            ' Buscar los TextBox dentro de cada GroupBox por Tag
            Dim txtNombreInstancia As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = "NombreInstancia")
            Dim txtPlantilla As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = "Plantilla")

            If Not String.IsNullOrWhiteSpace(InstancesData(i).InstanceName) Then
                txtNombreInstancia.Text = InstancesData(i).InstanceName
            End If
            If Not String.IsNullOrWhiteSpace(InstancesData(i).InstanceTemplate) Then
                txtPlantilla.Text = InstancesData(i).InstanceTemplate
            End If

            For Each attr In InstancesData(i).InstanceAloneAttr
                For Each att In LoneAttributes
                    Dim txtbox As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = att)
                    txtbox.Text = attr
                Next
            Next

            For Each attr In InstancesData(i).InstanceArrayAttr
                For Each att In ArrayAttributes
                    Dim txtbox As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = att)
                    For Each lblattr In attr
                        txtbox.AppendText(lblattr & Environment.NewLine)
                    Next
                Next
            Next
            i = i + 1
        Next
    End Sub
End Class

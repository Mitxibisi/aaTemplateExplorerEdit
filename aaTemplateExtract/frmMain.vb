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
    Dim AuthenticationMode As String
    Dim ExportFolder As String

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        lblStatus.Text = "Initializing"

        ' set up the new GRAccess client
        aaTemplateExtract = New aaTemplateExtract

        ' fill in a default node name
        txtNodeName.Text = Environment.MachineName

        ' get the list of Galaxies from the local node and fill the combo box with the collection
        cmboGalaxyList.DataSource = aaTemplateExtract.getGalaxies(txtNodeName.Text)

        ' do any UI clean up work that might be needed when the galaxy name changes
        refreshGalaxyInfo(cmboGalaxyList.SelectedValue)

        Refres_Instances()

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
        For Each grupo As GroupBox In TabPage2.Controls.OfType(Of GroupBox)()
            Dim i As Integer
            i = i + 1
            lblStatus.Text = "Creating instance number " & i
            ' Buscar los TextBox dentro de cada GroupBox por Tag
            Dim txtPlantilla As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = "Plantilla")
            Dim txtNombreInstancia As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = "NombreInstancia")
            Dim txtArea As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = "Area")
            Dim txtCodigoObjeto As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = "CodigoObjeto")
            Dim txtDesObjeto As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = "DesObjeto")

            ' Verificar que todos los campos existen antes de llamar a la función
            If txtPlantilla IsNot Nothing AndAlso txtNombreInstancia IsNot Nothing Then
                aaTemplateExtract.createInstance(txtPlantilla.Text, txtNombreInstancia.Text, txtArea?.Text, txtCodigoObjeto?.Text, txtDesObjeto?.Text)
            End If
        Next
        lblStatus.Text = ""
        MessageBox.Show("New Instances created")
    End Sub

    Private Sub btnCreateInstances_Click(sender As Object, e As EventArgs) Handles btnCreateInstance.Click
        Refres_Instances()
    End Sub

    Private Sub Refres_Instances()
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

            ' Agregarlo a la página
            TabPage2.Controls.Add(numInstancesSelector)
        End If

        ' Crear botón superior
        Dim btnSetInstances As New Button()
        btnSetInstances.Name = "btnCreateInstance"
        btnSetInstances.Text = "Generar instancias"
        btnSetInstances.Location = New Point(10, 10)
        btnSetInstances.Width = 150

        ' Agregar evento al botón para regenerar instancias
        AddHandler btnSetInstances.Click, AddressOf btnCreateInstances_Click

        ' Agregar controles al TabPage
        TabPage2.Controls.Add(btnSetInstances)

        ' Obtener número de instancias desde el NumericUpDown
        Dim numInstancias As Integer = numInstancesSelector.Value

        ' Definir nombres de los campos y sus Tags
        Dim nombresCampos As String() = {"Plantilla", "Nombre Instancia", "Área", "Descripción Objeto", "Código Objeto"}
        Dim tagsCampos As String() = {"Plantilla", "NombreInstancia", "Area", "DesObjeto", "CodigoObjeto"}

        ' Espaciado inicial (debajo del botón y NumericUpDown)
        Dim yOffset As Integer = 50

        For i As Integer = 0 To numInstancias - 1
            ' Crear un GroupBox para cada instancia
            Dim grupo As New GroupBox()
            grupo.Text = "Instancia " & (i + 1)
            grupo.Width = TabPage2.Width - 20
            grupo.Height = 170
            grupo.Location = New Point(10, yOffset)
            yOffset += grupo.Height + 10 ' Espaciado entre grupos

            ' Añadir los campos dentro del grupo
            Dim campoOffset As Integer = 20

            For j As Integer = 0 To nombresCampos.Length - 1
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

                ' Agregar controles al GroupBox
                grupo.Controls.Add(nuevaEtiqueta)
                grupo.Controls.Add(nuevoTextBox)

                campoOffset += 30 ' Espaciado entre campos
            Next

            ' Agregar el GroupBox al TabPage
            TabPage2.Controls.Add(grupo)
        Next

        ' Crear botón inferior para procesar instancias
        Dim btnProcessInstances As New Button()
        btnProcessInstances.Name = "btnNewInstance"
        btnProcessInstances.Text = "Procesar instancias"
        btnProcessInstances.Location = New Point(10, yOffset)
        btnProcessInstances.Width = 200

        ' Agregar evento al botón para procesar instancias
        AddHandler btnProcessInstances.Click, AddressOf btnCreateInstance_Click

        ' Agregar botón a TabPage
        TabPage2.Controls.Add(btnProcessInstances)
    End Sub

End Class
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
    Public LogBox As ListBox

    ' ------- Generales ----------
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        lblStatus.Text = "Initializing"

        UpdateAttr()
        addExcelData(LoneAttributes, ArrayAttributes)

        ' set up the new GRAccess client
        aaTemplateExtract = New aaTemplateExtract


        aaExcelData = New aaExcelData

        ' fill in a default node name
        txtNodeName.Text = Environment.MachineName

        LogBox = ErrorLog

        ' get the list of Galaxies from the local node and fill the combo box with the collection
        cmboGalaxyList.DataSource = aaTemplateExtract.getGalaxies(txtNodeName.Text)

        ' do any UI clean up work that might be needed when the galaxy name changes
        refreshGalaxyInfo(cmboGalaxyList.SelectedValue)

        Refres_Instances(1)
        Refresh_TemplateAttr(NUPdfa.Value, NUPafa.Value)

        restore_configdata()

        btnExport.Enabled = False

        lblStatus.Text = ""

    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        My.Settings.CSVDir = txtDirecc.Text
        My.Settings.CSVTAGS = txtTags.Text
        My.Settings.InstanceNameIndex = txtIname.Text
        My.Settings.InstanceTemplateIndex = txtItemplate.Text
        My.Settings.AloneAttributes = txtAtributes.Text
        My.Settings.ArrayAttributes = txtArrayAtributes.Text
        My.Settings.Save()
        My.Settings.Reload()
    End Sub

    Private Sub restore_configdata()
        txtItemplate.Text = My.Settings.InstanceTemplateIndex
        txtTags.Text = My.Settings.CSVTAGS
        txtIname.Text = My.Settings.InstanceNameIndex
        txtArrayAtributes.Text = My.Settings.ArrayAttributes
        txtAtributes.Text = My.Settings.AloneAttributes
        txtDirecc.Text = My.Settings.CSVDir
    End Sub


    ' ------- Pantalla Exportar Plantillas -------
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

        ExportTemplatesToFile(ExportFolder, TemplateNames, ProgressBar1)

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


    ' ------- Pantalla Exportar Plantillas Funciones -------
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

    ' ------- Pantallas de Instancias y Configuraciones Instancias -------
    ' ------- Pantallas Configuraciones Instancias -------
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        My.Settings.Reset()
        My.Settings.Save()
        restore_configdata()
    End Sub

    Private Sub btnLoadData_Click(sender As Object, e As EventArgs) Handles btnLoadData.Click
        Try
            Dim openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "Archivos MLSM y XLSX (*.mlsm;*.xlsx)|*.mlsm;*.xlsx"
            openFileDialog.Title = "Seleccionar un archivo"

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                ImportExcel = openFileDialog.FileName

                Dim LoneAttributesInteger As New List(Of Integer)()
                Dim ArrayAttributesInteger As New List(Of Integer)()

                For Each attr In LoneAttributes
                    Dim txtloneattr As TextBox = EditAttributes.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Name.ToString() = "txt_" & attr)
                    Dim value As Integer
                    If Integer.TryParse(txtloneattr?.Text, value) Then
                        LoneAttributesInteger.Add(value.ToString())
                    Else
                        LoneAttributesInteger.Add("10000")
                    End If
                Next

                For Each attr In ArrayAttributes
                    Dim txtloneattr As TextBox = EditAttributes.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Name.ToString() = "txt_" & attr)
                    Dim value As Integer
                    If Integer.TryParse(txtloneattr?.Text, value) Then
                        ArrayAttributesInteger.Add(value.ToString())
                    Else
                        ArrayAttributesInteger.Add("10000")
                    End If
                Next

                Dim InstancesData = aaExcelData.CargarDatosMapeado(ImportExcel, Integer.Parse(txtIname.Text), Integer.Parse(txtItemplate.Text), LoneAttributesInteger, ArrayAttributesInteger, txtTags.Text, txtDirecc.Text, CheckBox1.Checked, CheckBox2.Checked, txtestados.Text, txtordenes.Text)
                añadir_Mapeado(InstancesData)
            End If
        Catch y As Exception
            LogBox.Items.Add(y)
        End Try
    End Sub

    Private Sub txtAtributes_TextChanged(sender As Object, e As EventArgs) Handles txtArrayAtributes.TextChanged, txtAtributes.TextChanged
        Refres_Instances()
        addExcelData(LoneAttributes, ArrayAttributes)
    End Sub

    Private Sub txtTemplateData_TextChanged(sender As Object, e As EventArgs) Handles txtTemplateData.TextChanged
        Dim AttrList As New List(Of String)()
        Dim template = txtTemplateData.Text

        AttrList = aaTemplateExtract.getTemplateAttributes(template)

        ' Limpiar ComboBox antes de añadir nuevos datos
        ComboBox1.Items.Clear()

        If AttrList IsNot Nothing AndAlso AttrList.Count > 0 AndAlso AttrList(0) <> "BadTemplate" Then
            For Each Attr In AttrList
                If Not ComboBox1.Items.Contains(Attr) Then
                    ComboBox1.Items.Add(Attr)
                End If
            Next
        End If
    End Sub


    ' ------- Pantallas Instancias -------
    Private Sub btnDeploy_Click(sender As Object, e As EventArgs) Handles btnDeploy.Click
        Dim Cascade As Boolean = False
        ' Contar el total de GroupBox en TabPage2
        Dim totalGroupBoxes As Integer = TPInstances.Controls.OfType(Of GroupBox)().Count()
        For Each grupo As GroupBox In TPInstances.Controls.OfType(Of GroupBox)()
            Dim i As Integer
            i = i + 1
            lblStatus.Text = "Deploy Instance Number " & i & "/" & totalGroupBoxes
            ' Buscar los TextBox dentro de cada GroupBox por Tag
            Dim txtNombreInstancia As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = "NombreInstancia")
            Dim CCascade As CheckBox = TPInstances.Controls.OfType(Of CheckBox)().FirstOrDefault(Function(txt) txt.Name.ToString() = "cbCascade")
            Cascade = (CCascade IsNot Nothing AndAlso CCascade.Checked)

            ' Verificar que todos los campos existen antes de llamar a la función
            If txtNombreInstancia IsNot Nothing Then
                aaTemplateExtract.deployUndeployInstance(txtNombreInstancia.Text, Cascade, 0)
            End If
        Next
        lblStatus.Text = ""
        MessageBox.Show("Operacion Finalizada")
    End Sub

    Private Sub btnUndeploy_Click(sender As Object, e As EventArgs) Handles btnUndeploy.Click
        Dim totalGroupBoxes As Integer = TPInstances.Controls.OfType(Of GroupBox)().Count()
        For Each grupo As GroupBox In TPInstances.Controls.OfType(Of GroupBox)()
            Dim i As Integer
            i = i + 1
            lblStatus.Text = "UnDeploy Instance Number " & i & "/" & totalGroupBoxes
            ' Buscar los TextBox dentro de cada GroupBox por Tag
            Dim txtNombreInstancia As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = "NombreInstancia")

            ' Verificar que todos los campos existen antes de llamar a la función
            If txtNombreInstancia IsNot Nothing Then
                aaTemplateExtract.deployUndeployInstance(txtNombreInstancia.Text, False, 1)
            End If
        Next
        lblStatus.Text = ""
        MessageBox.Show("Operacion Finalizada")
    End Sub

    Private Sub btnCreateInstance_Click(sender As Object, e As EventArgs) Handles btnNewInstance.Click
        Dim LoneAttributesText As New List(Of String)()
        Dim ArrayAttributesText As New List(Of String)()

        ' Contar el total de GroupBox en TPInstances
        Dim totalGroupBoxes As Integer = TPInstances.Controls.OfType(Of GroupBox)().Count()
        For Each grupo As GroupBox In TPInstances.Controls.OfType(Of GroupBox)()
            LoneAttributesText.Clear()
            ArrayAttributesText.Clear()
            Dim i As Integer
            i = i + 1
            lblStatus.Text = "Creating Instance Number " & i & "/" & totalGroupBoxes
            ' Buscar los TextBox dentro de cada GroupBox por Tag
            Dim txtPlantilla As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = "Plantilla")
            Dim txtNombreInstancia As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = "NombreInstancia")
            Dim txtArea As TextBox = TPInstances.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = "Area")

            For Each attr In LoneAttributes
                Dim txtloneattr As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = attr)
                LoneAttributesText.Add(txtloneattr?.Text)
            Next

            For Each attr In ArrayAttributes
                Dim txtloneattr As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = attr)
                ArrayAttributesText.Add(txtloneattr?.Text)
            Next

            ' Verificar que todos los campos existen antes de llamar a la función
            If txtPlantilla IsNot Nothing AndAlso txtNombreInstancia IsNot Nothing Then
                aaTemplateExtract.createInstance(txtPlantilla.Text, txtNombreInstancia.Text, txtArea?.Text, LoneAttributesText, ArrayAttributesText, LoneAttributes, ArrayAttributes)
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


    ' ------- Pantallas de Instancias Funciones -------
    Private Sub Refres_Instances(Optional ByVal instances As Integer = 0)
        Dim lastTextBoxMultiple As Boolean = False
        Dim numInstancias As Integer

        UpdateAttr()

        ' Limpiar campos previos, pero NO el NumericUpDown
        For Each control As Control In TPInstances.Controls
            If Not control.Name = "NumericUpDown2" Then
                control.Dispose() ' Eliminar todos los controles excepto el NumericUpDown
            End If
        Next

        ' Verificar si el NumericUpDown ya existe
        Dim numInstancesSelector As NumericUpDown = TryCast(TPInstances.Controls("NumericUpDown2"), NumericUpDown)

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

        TPInstances.Controls.Clear()

        TPInstances.Controls.Add(btnSetInstances)
        TPInstances.Controls.Add(numInstancesSelector)
        TPInstances.Controls.Add(areaEtiqueta)
        TPInstances.Controls.Add(areaTextBox)

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
            grupo.Width = TPInstances.Width - 20
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
            TPInstances.Controls.Add(grupo)
        Next

        ' Crear botón inferior para procesar instancias
        Dim btnProcessInstances As New Button()
        btnProcessInstances.Name = "btnNewInstance"
        btnProcessInstances.Text = "Crear instancias"
        btnProcessInstances.Location = New Point(10, yOffset)
        btnProcessInstances.Width = 100

        ' Agregar evento al botón para procesar instancias
        AddHandler btnProcessInstances.Click, AddressOf btnCreateInstance_Click

        ' Agregar botón a TabPage
        TPInstances.Controls.Add(btnProcessInstances)

        ' Crear botón inferior para procesar instancias
        Dim btnDeployInstances As New Button()
        btnDeployInstances.Name = "btnDeploy"
        btnDeployInstances.Text = "Deploy"
        btnDeployInstances.Location = New Point(10, btnProcessInstances.Height + 6 + yOffset)
        btnDeployInstances.Width = 100

        ' Agregar evento al botón para procesar instancias
        AddHandler btnDeployInstances.Click, AddressOf btnDeploy_Click
        TPInstances.Controls.Add(btnDeployInstances)

        ' Crear botón inferior para procesar instancias
        Dim btnUnDeployInstances As New Button()
        btnUnDeployInstances.Name = "btnUndeploy"
        btnUnDeployInstances.Text = "UnDeploy"
        btnUnDeployInstances.Location = New Point(10, btnDeployInstances.Height + 35 + yOffset)
        btnUnDeployInstances.Width = 100

        Dim chcmark As New CheckBox()
        chcmark.Name = "cbCascade"
        chcmark.Text = "Modo Cascada"
        chcmark.Tag = "ActivarCascada"
        chcmark.Location = New Point(120, yOffset)

        AddHandler btnUnDeployInstances.Click, AddressOf btnUndeploy_Click

        TPInstances.Controls.Add(btnUnDeployInstances)
        TPInstances.Controls.Add(chcmark)
    End Sub

    Public Sub UpdateAttr()
        LoneAttributes = txtAtributes.Text.Split(",")
        ArrayAttributes = txtArrayAtributes.Text.Split(",")
    End Sub

    Private Sub añadir_Mapeado(InstancesData)
        Dim i As Integer = 0

        Refres_Instances(InstancesData.Count)

        For Each grupo As GroupBox In TPInstances.Controls.OfType(Of GroupBox)()
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

            For index As Integer = 0 To Math.Min(InstancesData(i).InstanceAloneAttr.Count, LoneAttributes.Count) - 1
                Dim att As String = LoneAttributes(index)
                Dim attr As String = InstancesData(i).InstanceAloneAttr(index)

                ' Buscar el TextBox por el Tag correspondiente
                Dim txtbox As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = att)
                If txtbox IsNot Nothing Then
                    If attr <> "Valor Nulo" Then
                        txtbox.Text = attr
                    Else
                        txtbox.Text = ""
                    End If
                End If
            Next

            For index As Integer = 0 To Math.Min(InstancesData(i).InstanceArrayAttr.Count, ArrayAttributes.Count) - 1
                Dim att As String = ArrayAttributes(index)
                Dim attrList As List(Of String) = InstancesData(i).InstanceArrayAttr(index)

                ' Buscar el TextBox correspondiente por Tag
                Dim txtbox As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = att)
                If txtbox IsNot Nothing Then
                    txtbox.Clear() ' Limpiar el contenido anterior
                    For Each lblattr In attrList
                        If lblattr <> "Valor Nulo" Then
                            txtbox.AppendText(lblattr & Environment.NewLine)
                        Else
                            txtbox.AppendText("")
                        End If
                    Next
                End If
            Next
            i = i + 1
        Next
    End Sub

    Private Sub addExcelData(Mylist1 As String(), Mylist2 As String())
        ' Limpiar los controles previos en el Panel
        EditAttributes.Controls.Clear()

        ' Variables para la posición inicial
        Dim startY As Integer = 10
        Dim spacing As Integer = 30 ' Espacio entre cada control

        ' Añadir datos de la primera lista
        startY = AddControlsFromList(Mylist1, startY, spacing)

        ' Añadir datos de la segunda lista
        AddControlsFromList(Mylist2, startY, spacing)

        ' Habilitar el scroll si es necesario
        EditAttributes.AutoScroll = True
    End Sub

    Function AddControlsFromList(MyList As String(), startY As Integer, spacing As Integer)
        For i As Integer = 0 To MyList.Length - 1
            ' Crear el Label
            Dim lbl As New Label()
            lbl.Text = MyList(i)
            lbl.Location = New Point(10, startY)
            lbl.AutoSize = True

            ' Crear el TextBox
            Dim txt As New TextBox()
            txt.Name = "txt_" & MyList(i) ' Nombre único para cada TextBox
            txt.Location = New Point(lbl.Width + 10, startY)
            txt.Width = 100
            txt.Text = ""

            ' Añadir los controles al Panel
            EditAttributes.Controls.Add(lbl)
            EditAttributes.Controls.Add(txt)

            ' Incrementar la posición Y para el siguiente control
            startY += spacing
        Next
        Return startY
    End Function


    ' ------- Pantalla Crear Plantilla -------
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnNewTemplate.Click
        ' Atributos Discretos
        Dim DiscFANames As New List(Of String)()
        Dim DiscFADesc As New List(Of String)()
        Dim DiscFAAlarmed As New List(Of Boolean)()
        Dim DiscFAHistorized As New List(Of Boolean)()
        Dim DiscFAEvents As New List(Of Boolean)()
        Dim DiscLockFA As New List(Of List(Of Boolean))()

        ' Atributos Analogicos
        Dim AnalogFANames As New List(Of String)()
        Dim AnalogFADesc As New List(Of String)()
        Dim AnalogFAEngUnits As New List(Of String)()
        Dim AnalogFAHistorized As New List(Of Boolean)()
        Dim AnalogFAEvents As New List(Of Boolean)()
        Dim AnalogFAEscalado As New List(Of Boolean)()
        Dim AnalogLockFA As New List(Of List(Of Boolean))()

        ' Extraccion Discretos (Se cambiara pero un mejor codigo mas expandible y manejable)
        For Each grupo As GroupBox In TPDiscFA.Controls.OfType(Of GroupBox)()
            Dim DiscLock As New List(Of Boolean)()
            DiscLock.Add(False)
            DiscLock.Add(GetCheckBox(grupo, "Disc_L_Descripcion"))
            DiscLock.Add(GetCheckBox(grupo, "Disc_L_Alarmed"))
            DiscLock.Add(GetCheckBox(grupo, "Disc_L_Historizado"))
            DiscLock.Add(GetCheckBox(grupo, "Disc_L_Evento"))

            DiscLockFA.Add(DiscLock)

            DiscFANames.Add(GetTextBox(grupo, "Disc_Nombre"))
            DiscFADesc.Add(GetTextBox(grupo, "Disc_Descripcion"))

            DiscFAAlarmed.Add(GetCheckBox(grupo, "Disc_Alarmed"))
            DiscFAHistorized.Add(GetCheckBox(grupo, "Disc_Historizado"))
            DiscFAEvents.Add(GetCheckBox(grupo, "Disc_Evento"))
        Next

        ' Extraccion Analogicos (Se cambiara pero un mejor codigo mas expandible y manejable)
        For Each grupo As GroupBox In TPFAAnalog.Controls.OfType(Of GroupBox)()
            Dim AnalogLock As New List(Of Boolean)()
            AnalogLock.Add(False)
            AnalogLock.Add(GetCheckBox(grupo, "Analog_L_Descripcion"))
            AnalogLock.Add(GetCheckBox(grupo, "Analog_L_Unidad"))
            AnalogLock.Add(GetCheckBox(grupo, "Analog_L_Historizado"))
            AnalogLock.Add(GetCheckBox(grupo, "Analog_L_Evento"))
            AnalogLock.Add(GetCheckBox(grupo, "Analog_L_Escalado"))

            AnalogLockFA.Add(AnalogLock)

            AnalogFANames.Add(GetTextBox(grupo, "Analog_Nombre"))
            AnalogFADesc.Add(GetTextBox(grupo, "Analog_Descripcion"))
            AnalogFAEngUnits.Add(GetTextBox(grupo, "Analog_Unidad"))

            AnalogFAHistorized.Add(GetCheckBox(grupo, "Analog_Historizado"))
            AnalogFAEvents.Add(GetCheckBox(grupo, "Analog_Evento"))
            AnalogFAEscalado.Add(GetCheckBox(grupo, "Analog_Escalado"))
        Next

        Dim UdasNames = txtValueName.Text.Split(",")
        Dim UdasValue = txtValor1.Text.Split(",")

        aaTemplateExtract.CreateTemplate(txtDTemplate.Text,
                                         txtNewTName.Text,
                                         UdasNames,
                                         UdasValue,
                                         DiscFANames,
                                         AnalogFANames,
                                         DiscFADesc,
                                         AnalogFADesc,
                                         AnalogFAEngUnits,
                                         DiscFAAlarmed,
                                         DiscFAHistorized,
                                         AnalogFAHistorized,
                                         DiscFAEvents,
                                         AnalogFAEvents,
                                         DiscLockFA,
                                         AnalogFAEscalado,
                                         AnalogLockFA)

    End Sub

    Private Sub btnUploadTemplateAttr_Click(sender As Object, e As EventArgs) Handles btnUploadTemplateAttr.Click
        Refresh_TemplateAttr(NUPdfa.Value, NUPafa.Value)
    End Sub

    Private Sub Refresh_TemplateAttr(DiscValue As Integer, AnalogValue As Integer)
        addControlsToTab("Disc", DiscValue, TPDiscFA, {"Nombre", "Descripcion"}, {"Alarmed", "Historizado", "Evento"})
        addControlsToTab("Analog", AnalogValue, TPFAAnalog, {"Nombre", "Descripcion", "Unidad"}, {"Historizado", "Evento", "Escalado"})
    End Sub

    Private Sub addControlsToTab(ByVal str As String, ByVal numInstancias As Integer, ByVal Page As TabPage, ByVal txtCampos As String(), ByVal txtCheckBox As String())
        Page.Controls.Clear()

        Dim nombresCampos As String() = txtCampos
        Dim tagsCampos As String() = txtCampos
        Dim tagsCheckbox As String() = txtCheckBox

        ' Espaciado inicial
        Dim yOffset As Integer = 10

        For i As Integer = 0 To numInstancias - 1
            ' Crear un GroupBox para cada instancia
            Dim grupo As New GroupBox()
            grupo.Text = "Attributo " & str & (i + 1)
            grupo.Width = Page.Width - 20
            grupo.Height = (((txtCampos.Count + txtCheckBox.Count) * 32))
            grupo.Location = New Point(10, yOffset)

            yOffset += grupo.Height + 10 ' Espaciado entre grupos

            ' Añadir los campos dentro del grupo
            Dim campoOffset As Integer = 20

            For j As Integer = 0 To nombresCampos.Count - 1

                ' Crear Label
                Dim nuevaEtiqueta As New Label()
                nuevaEtiqueta.Text = str & "_" & nombresCampos(j)
                nuevaEtiqueta.Location = New Point(10, campoOffset)
                nuevaEtiqueta.AutoSize = True

                ' Crear TextBox
                Dim nuevoTextBox As New TextBox()
                nuevoTextBox.Width = 150
                nuevoTextBox.Location = New Point(15 + nuevaEtiqueta.Width, campoOffset)
                nuevoTextBox.Tag = str & "_" & tagsCampos(j)

                ' Crear CheckBox
                Dim nuevoCheckBox As New CheckBox()
                nuevoCheckBox.Width = 150
                nuevoCheckBox.Text = "Lock"
                nuevoCheckBox.Location = New Point(25 + nuevaEtiqueta.Width + nuevoTextBox.Width, campoOffset)
                nuevoCheckBox.Tag = str & "_L_" & tagsCampos(j)

                grupo.Controls.Add(nuevoCheckBox)

                ' Agregar controles al GroupBox
                grupo.Controls.Add(nuevaEtiqueta)
                grupo.Controls.Add(nuevoTextBox)

                ' Ajustar campoOffset basado en el tamaño real del TextBox
                campoOffset += nuevoTextBox.Height + 10 ' Espaciado base entre campos
            Next

            For j As Integer = 0 To tagsCheckbox.Count - 1

                ' Crear TextBox
                Dim nuevoCheckBox As New CheckBox()
                nuevoCheckBox.Width = 150
                nuevoCheckBox.Text = tagsCheckbox(j)
                nuevoCheckBox.Location = New Point(150, campoOffset)
                nuevoCheckBox.Tag = str & "_" & tagsCheckbox(j) ' ASIGNAR EL TAG CORRECTAMENTE

                Dim nuevoLockCheckBox As New CheckBox()
                nuevoLockCheckBox.Width = 150
                nuevoLockCheckBox.Text = "Lock"
                nuevoLockCheckBox.Location = New Point(nuevoCheckBox.Width + 125, campoOffset)
                nuevoLockCheckBox.Tag = str & "_L_" & tagsCheckbox(j) ' ASIGNAR EL TAG CORRECTAMENTE

                grupo.Controls.Add(nuevoLockCheckBox)
                grupo.Controls.Add(nuevoCheckBox)

                ' Ajustar campoOffset basado en el tamaño real del TextBox
                campoOffset += nuevoCheckBox.Height + 1 ' Espaciado base entre campos
            Next

            ' Agregar el GroupBox al TabPage
            Page.Controls.Add(grupo)
        Next
    End Sub


    ' ------- Get Data -------
    Private Function GetTextBox(ByVal grupo As GroupBox, ByVal Tag As String)
        Dim txtValue As TextBox = grupo.Controls.OfType(Of TextBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = Tag)
        Return txtValue.Text
    End Function

    Private Function GetCheckBox(ByVal grupo As GroupBox, ByVal Tag As String)
        Dim cbValue As CheckBox = grupo.Controls.OfType(Of CheckBox)().FirstOrDefault(Function(txt) txt.Tag.ToString() = Tag)
        Return cbValue.Checked
    End Function
End Class
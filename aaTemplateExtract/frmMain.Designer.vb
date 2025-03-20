<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Me.dlgFolderBrowser = New System.Windows.Forms.FolderBrowserDialog()
		Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
		Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
		Me.DirectoryEntry1 = New System.DirectoryServices.DirectoryEntry()
		Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
		Me.TabPage4 = New System.Windows.Forms.TabPage()
		Me.ErrorLog = New System.Windows.Forms.ListBox()
		Me.TabPage5 = New System.Windows.Forms.TabPage()
		Me.TPTemplates = New System.Windows.Forms.TabControl()
		Me.TPDiscFA = New System.Windows.Forms.TabPage()
		Me.TPFAAnalog = New System.Windows.Forms.TabPage()
		Me.TPUDA = New System.Windows.Forms.TabPage()
		Me.txtValor1 = New System.Windows.Forms.TextBox()
		Me.Label23 = New System.Windows.Forms.Label()
		Me.Label20 = New System.Windows.Forms.Label()
		Me.txtValueName = New System.Windows.Forms.TextBox()
		Me.TabPage3 = New System.Windows.Forms.TabPage()
		Me.TabPage6 = New System.Windows.Forms.TabPage()
		Me.btnUploadTemplateAttr = New System.Windows.Forms.Button()
		Me.Label27 = New System.Windows.Forms.Label()
		Me.btnNewTemplate = New System.Windows.Forms.Button()
		Me.txtNewTName = New System.Windows.Forms.TextBox()
		Me.Label26 = New System.Windows.Forms.Label()
		Me.txtDTemplate = New System.Windows.Forms.TextBox()
		Me.Label25 = New System.Windows.Forms.Label()
		Me.Label19 = New System.Windows.Forms.Label()
		Me.Label24 = New System.Windows.Forms.Label()
		Me.Label18 = New System.Windows.Forms.Label()
		Me.NUPscr = New System.Windows.Forms.NumericUpDown()
		Me.NUPdfa = New System.Windows.Forms.NumericUpDown()
		Me.NUPuda = New System.Windows.Forms.NumericUpDown()
		Me.NUPafa = New System.Windows.Forms.NumericUpDown()
		Me.TPInstancesC = New System.Windows.Forms.TabPage()
		Me.TPInstancesData = New System.Windows.Forms.TabControl()
		Me.TPInstances = New System.Windows.Forms.TabPage()
		Me.numInstancesSelector = New System.Windows.Forms.NumericUpDown()
		Me.btnUndeploy = New System.Windows.Forms.Button()
		Me.btnCreateInstance = New System.Windows.Forms.Button()
		Me.cbCascade = New System.Windows.Forms.CheckBox()
		Me.btnNewInstance = New System.Windows.Forms.Button()
		Me.btnDeploy = New System.Windows.Forms.Button()
		Me.TPConfigurations = New System.Windows.Forms.TabPage()
		Me.GroupBox3 = New System.Windows.Forms.GroupBox()
		Me.Label15 = New System.Windows.Forms.Label()
		Me.Button2 = New System.Windows.Forms.Button()
		Me.txtTemplateData = New System.Windows.Forms.TextBox()
		Me.Label17 = New System.Windows.Forms.Label()
		Me.Label16 = New System.Windows.Forms.Label()
		Me.ComboBox1 = New System.Windows.Forms.ComboBox()
		Me.txtAtributes = New System.Windows.Forms.TextBox()
		Me.txtArrayAtributes = New System.Windows.Forms.TextBox()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.Label22 = New System.Windows.Forms.Label()
		Me.Label21 = New System.Windows.Forms.Label()
		Me.txtordenes = New System.Windows.Forms.TextBox()
		Me.txtestados = New System.Windows.Forms.TextBox()
		Me.CheckBox2 = New System.Windows.Forms.CheckBox()
		Me.CheckBox1 = New System.Windows.Forms.CheckBox()
		Me.Label14 = New System.Windows.Forms.Label()
		Me.Label13 = New System.Windows.Forms.Label()
		Me.txtDirecc = New System.Windows.Forms.TextBox()
		Me.txtTags = New System.Windows.Forms.TextBox()
		Me.GroupBox2 = New System.Windows.Forms.GroupBox()
		Me.GroupBox4 = New System.Windows.Forms.GroupBox()
		Me.EditAttributes = New System.Windows.Forms.Panel()
		Me.Label12 = New System.Windows.Forms.Label()
		Me.Label11 = New System.Windows.Forms.Label()
		Me.txtItemplate = New System.Windows.Forms.TextBox()
		Me.txtIname = New System.Windows.Forms.TextBox()
		Me.btnLoadData = New System.Windows.Forms.Button()
		Me.TabPage1 = New System.Windows.Forms.TabPage()
		Me.GroupBox6 = New System.Windows.Forms.GroupBox()
		Me.lstTemplates = New System.Windows.Forms.ListBox()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
		Me.btnExport = New System.Windows.Forms.Button()
		Me.chkHideBaseTemplates = New System.Windows.Forms.CheckBox()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.btnRefreshTemplates = New System.Windows.Forms.Button()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.lblFolderPath = New System.Windows.Forms.Label()
		Me.linkLblSelectAll = New System.Windows.Forms.LinkLabel()
		Me.btnBrowseFolders = New System.Windows.Forms.Button()
		Me.linkLblSelectNone = New System.Windows.Forms.LinkLabel()
		Me.Label9 = New System.Windows.Forms.Label()
		Me.GroupBox5 = New System.Windows.Forms.GroupBox()
		Me.cmboGalaxyList = New System.Windows.Forms.ComboBox()
		Me.lblSecurityType = New System.Windows.Forms.Label()
		Me.Label10 = New System.Windows.Forms.Label()
		Me.grpLoginInput = New System.Windows.Forms.GroupBox()
		Me.btnLogin = New System.Windows.Forms.Button()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.txtPwdInput = New System.Windows.Forms.TextBox()
		Me.txtUserInput = New System.Windows.Forms.TextBox()
		Me.txtNodeName = New System.Windows.Forms.TextBox()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.btnRefreshGalaxies = New System.Windows.Forms.Button()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.Configuraciones = New System.Windows.Forms.TabControl()
		Me.StatusStrip1.SuspendLayout()
		CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.TabPage4.SuspendLayout()
		Me.TabPage5.SuspendLayout()
		Me.TPTemplates.SuspendLayout()
		Me.TPUDA.SuspendLayout()
		Me.TabPage6.SuspendLayout()
		CType(Me.NUPscr, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.NUPdfa, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.NUPuda, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.NUPafa, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.TPInstancesC.SuspendLayout()
		Me.TPInstancesData.SuspendLayout()
		Me.TPInstances.SuspendLayout()
		CType(Me.numInstancesSelector, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.TPConfigurations.SuspendLayout()
		Me.GroupBox3.SuspendLayout()
		Me.GroupBox1.SuspendLayout()
		Me.GroupBox2.SuspendLayout()
		Me.GroupBox4.SuspendLayout()
		Me.TabPage1.SuspendLayout()
		Me.GroupBox6.SuspendLayout()
		Me.GroupBox5.SuspendLayout()
		Me.grpLoginInput.SuspendLayout()
		Me.Configuraciones.SuspendLayout()
		Me.SuspendLayout()
		'
		'dlgFolderBrowser
		'
		Me.dlgFolderBrowser.RootFolder = System.Environment.SpecialFolder.MyDocuments
		'
		'StatusStrip1
		'
		Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
		Me.StatusStrip1.Location = New System.Drawing.Point(0, 503)
		Me.StatusStrip1.Name = "StatusStrip1"
		Me.StatusStrip1.Size = New System.Drawing.Size(405, 22)
		Me.StatusStrip1.TabIndex = 29
		Me.StatusStrip1.Text = "StatusStrip1"
		'
		'lblStatus
		'
		Me.lblStatus.Name = "lblStatus"
		Me.lblStatus.Size = New System.Drawing.Size(0, 17)
		'
		'FileSystemWatcher1
		'
		Me.FileSystemWatcher1.EnableRaisingEvents = True
		Me.FileSystemWatcher1.SynchronizingObject = Me
		'
		'TabPage4
		'
		Me.TabPage4.Controls.Add(Me.ErrorLog)
		Me.TabPage4.Location = New System.Drawing.Point(4, 22)
		Me.TabPage4.Name = "TabPage4"
		Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage4.Size = New System.Drawing.Size(397, 477)
		Me.TabPage4.TabIndex = 5
		Me.TabPage4.Text = "Events Log"
		Me.TabPage4.UseVisualStyleBackColor = True
		'
		'ErrorLog
		'
		Me.ErrorLog.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ErrorLog.FormattingEnabled = True
		Me.ErrorLog.Location = New System.Drawing.Point(3, 3)
		Me.ErrorLog.Name = "ErrorLog"
		Me.ErrorLog.Size = New System.Drawing.Size(391, 471)
		Me.ErrorLog.TabIndex = 0
		'
		'TabPage5
		'
		Me.TabPage5.Controls.Add(Me.TPTemplates)
		Me.TabPage5.Location = New System.Drawing.Point(4, 22)
		Me.TabPage5.Name = "TabPage5"
		Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage5.Size = New System.Drawing.Size(397, 477)
		Me.TabPage5.TabIndex = 4
		Me.TabPage5.Text = "Template"
		Me.TabPage5.UseVisualStyleBackColor = True
		'
		'TPTemplates
		'
		Me.TPTemplates.Controls.Add(Me.TPDiscFA)
		Me.TPTemplates.Controls.Add(Me.TPFAAnalog)
		Me.TPTemplates.Controls.Add(Me.TPUDA)
		Me.TPTemplates.Controls.Add(Me.TabPage3)
		Me.TPTemplates.Controls.Add(Me.TabPage6)
		Me.TPTemplates.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TPTemplates.Location = New System.Drawing.Point(3, 3)
		Me.TPTemplates.Name = "TPTemplates"
		Me.TPTemplates.SelectedIndex = 0
		Me.TPTemplates.Size = New System.Drawing.Size(391, 471)
		Me.TPTemplates.TabIndex = 12
		'
		'TPDiscFA
		'
		Me.TPDiscFA.AutoScroll = True
		Me.TPDiscFA.Location = New System.Drawing.Point(4, 22)
		Me.TPDiscFA.Name = "TPDiscFA"
		Me.TPDiscFA.Padding = New System.Windows.Forms.Padding(3)
		Me.TPDiscFA.Size = New System.Drawing.Size(383, 445)
		Me.TPDiscFA.TabIndex = 0
		Me.TPDiscFA.Text = "Discrete Attributes"
		Me.TPDiscFA.UseVisualStyleBackColor = True
		'
		'TPFAAnalog
		'
		Me.TPFAAnalog.AutoScroll = True
		Me.TPFAAnalog.Location = New System.Drawing.Point(4, 22)
		Me.TPFAAnalog.Name = "TPFAAnalog"
		Me.TPFAAnalog.Padding = New System.Windows.Forms.Padding(3)
		Me.TPFAAnalog.Size = New System.Drawing.Size(383, 445)
		Me.TPFAAnalog.TabIndex = 1
		Me.TPFAAnalog.Text = "Analog Attributes"
		Me.TPFAAnalog.UseVisualStyleBackColor = True
		'
		'TPUDA
		'
		Me.TPUDA.Controls.Add(Me.txtValor1)
		Me.TPUDA.Controls.Add(Me.Label23)
		Me.TPUDA.Controls.Add(Me.Label20)
		Me.TPUDA.Controls.Add(Me.txtValueName)
		Me.TPUDA.Location = New System.Drawing.Point(4, 22)
		Me.TPUDA.Name = "TPUDA"
		Me.TPUDA.Padding = New System.Windows.Forms.Padding(3)
		Me.TPUDA.Size = New System.Drawing.Size(383, 445)
		Me.TPUDA.TabIndex = 2
		Me.TPUDA.Text = "UDa's"
		Me.TPUDA.UseVisualStyleBackColor = True
		'
		'txtValor1
		'
		Me.txtValor1.Location = New System.Drawing.Point(9, 58)
		Me.txtValor1.Name = "txtValor1"
		Me.txtValor1.Size = New System.Drawing.Size(312, 20)
		Me.txtValor1.TabIndex = 7
		Me.txtValor1.Text = "Valor 1,Valor 2"
		'
		'Label23
		'
		Me.Label23.AutoSize = True
		Me.Label23.Location = New System.Drawing.Point(6, 3)
		Me.Label23.Name = "Label23"
		Me.Label23.Size = New System.Drawing.Size(52, 13)
		Me.Label23.TabIndex = 10
		Me.Label23.Text = "Tagname"
		'
		'Label20
		'
		Me.Label20.AutoSize = True
		Me.Label20.Location = New System.Drawing.Point(6, 42)
		Me.Label20.Name = "Label20"
		Me.Label20.Size = New System.Drawing.Size(31, 13)
		Me.Label20.TabIndex = 8
		Me.Label20.Text = "Valor"
		'
		'txtValueName
		'
		Me.txtValueName.Location = New System.Drawing.Point(9, 19)
		Me.txtValueName.Name = "txtValueName"
		Me.txtValueName.Size = New System.Drawing.Size(312, 20)
		Me.txtValueName.TabIndex = 9
		Me.txtValueName.Text = "Valor1,Valor2"
		'
		'TabPage3
		'
		Me.TabPage3.Location = New System.Drawing.Point(4, 22)
		Me.TabPage3.Name = "TabPage3"
		Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage3.Size = New System.Drawing.Size(383, 445)
		Me.TabPage3.TabIndex = 3
		Me.TabPage3.Text = "Scripts"
		Me.TabPage3.UseVisualStyleBackColor = True
		'
		'TabPage6
		'
		Me.TabPage6.Controls.Add(Me.btnUploadTemplateAttr)
		Me.TabPage6.Controls.Add(Me.Label27)
		Me.TabPage6.Controls.Add(Me.btnNewTemplate)
		Me.TabPage6.Controls.Add(Me.txtNewTName)
		Me.TabPage6.Controls.Add(Me.Label26)
		Me.TabPage6.Controls.Add(Me.txtDTemplate)
		Me.TabPage6.Controls.Add(Me.Label25)
		Me.TabPage6.Controls.Add(Me.Label19)
		Me.TabPage6.Controls.Add(Me.Label24)
		Me.TabPage6.Controls.Add(Me.Label18)
		Me.TabPage6.Controls.Add(Me.NUPscr)
		Me.TabPage6.Controls.Add(Me.NUPdfa)
		Me.TabPage6.Controls.Add(Me.NUPuda)
		Me.TabPage6.Controls.Add(Me.NUPafa)
		Me.TabPage6.Location = New System.Drawing.Point(4, 22)
		Me.TabPage6.Name = "TabPage6"
		Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage6.Size = New System.Drawing.Size(383, 445)
		Me.TabPage6.TabIndex = 4
		Me.TabPage6.Text = "Configuration"
		Me.TabPage6.UseVisualStyleBackColor = True
		'
		'btnUploadTemplateAttr
		'
		Me.btnUploadTemplateAttr.Location = New System.Drawing.Point(262, 168)
		Me.btnUploadTemplateAttr.Name = "btnUploadTemplateAttr"
		Me.btnUploadTemplateAttr.Size = New System.Drawing.Size(92, 23)
		Me.btnUploadTemplateAttr.TabIndex = 21
		Me.btnUploadTemplateAttr.Text = "Update"
		Me.btnUploadTemplateAttr.UseVisualStyleBackColor = True
		'
		'Label27
		'
		Me.Label27.AutoSize = True
		Me.Label27.Location = New System.Drawing.Point(11, 171)
		Me.Label27.Name = "Label27"
		Me.Label27.Size = New System.Drawing.Size(91, 13)
		Me.Label27.TabIndex = 20
		Me.Label27.Text = "Number of Scripts"
		'
		'btnNewTemplate
		'
		Me.btnNewTemplate.Location = New System.Drawing.Point(262, 20)
		Me.btnNewTemplate.Name = "btnNewTemplate"
		Me.btnNewTemplate.Size = New System.Drawing.Size(92, 23)
		Me.btnNewTemplate.TabIndex = 6
		Me.btnNewTemplate.Text = "Create"
		Me.btnNewTemplate.UseVisualStyleBackColor = True
		'
		'txtNewTName
		'
		Me.txtNewTName.Location = New System.Drawing.Point(123, 51)
		Me.txtNewTName.Name = "txtNewTName"
		Me.txtNewTName.Size = New System.Drawing.Size(121, 20)
		Me.txtNewTName.TabIndex = 3
		'
		'Label26
		'
		Me.Label26.AutoSize = True
		Me.Label26.Location = New System.Drawing.Point(11, 147)
		Me.Label26.Name = "Label26"
		Me.Label26.Size = New System.Drawing.Size(89, 13)
		Me.Label26.TabIndex = 19
		Me.Label26.Text = "Number of UDA's"
		'
		'txtDTemplate
		'
		Me.txtDTemplate.Location = New System.Drawing.Point(123, 16)
		Me.txtDTemplate.Name = "txtDTemplate"
		Me.txtDTemplate.Size = New System.Drawing.Size(121, 20)
		Me.txtDTemplate.TabIndex = 2
		'
		'Label25
		'
		Me.Label25.AutoSize = True
		Me.Label25.Location = New System.Drawing.Point(11, 121)
		Me.Label25.Name = "Label25"
		Me.Label25.Size = New System.Drawing.Size(139, 13)
		Me.Label25.TabIndex = 18
		Me.Label25.Text = "Number of Analog Attributes"
		'
		'Label19
		'
		Me.Label19.AutoSize = True
		Me.Label19.Location = New System.Drawing.Point(10, 55)
		Me.Label19.Name = "Label19"
		Me.Label19.Size = New System.Drawing.Size(107, 13)
		Me.Label19.TabIndex = 5
		Me.Label19.Text = "New Template Name"
		'
		'Label24
		'
		Me.Label24.AutoSize = True
		Me.Label24.Location = New System.Drawing.Point(11, 93)
		Me.Label24.Name = "Label24"
		Me.Label24.Size = New System.Drawing.Size(145, 13)
		Me.Label24.TabIndex = 17
		Me.Label24.Text = "Number of Discrete Attributes"
		'
		'Label18
		'
		Me.Label18.AutoSize = True
		Me.Label18.Location = New System.Drawing.Point(11, 20)
		Me.Label18.Name = "Label18"
		Me.Label18.Size = New System.Drawing.Size(86, 13)
		Me.Label18.TabIndex = 4
		Me.Label18.Text = "Master Template"
		'
		'NUPscr
		'
		Me.NUPscr.Location = New System.Drawing.Point(205, 171)
		Me.NUPscr.Name = "NUPscr"
		Me.NUPscr.Size = New System.Drawing.Size(42, 20)
		Me.NUPscr.TabIndex = 16
		Me.NUPscr.Value = New Decimal(New Integer() {1, 0, 0, 0})
		'
		'NUPdfa
		'
		Me.NUPdfa.Location = New System.Drawing.Point(205, 93)
		Me.NUPdfa.Name = "NUPdfa"
		Me.NUPdfa.Size = New System.Drawing.Size(42, 20)
		Me.NUPdfa.TabIndex = 13
		Me.NUPdfa.Value = New Decimal(New Integer() {1, 0, 0, 0})
		'
		'NUPuda
		'
		Me.NUPuda.Location = New System.Drawing.Point(205, 145)
		Me.NUPuda.Name = "NUPuda"
		Me.NUPuda.Size = New System.Drawing.Size(42, 20)
		Me.NUPuda.TabIndex = 15
		Me.NUPuda.Value = New Decimal(New Integer() {1, 0, 0, 0})
		'
		'NUPafa
		'
		Me.NUPafa.Location = New System.Drawing.Point(205, 119)
		Me.NUPafa.Name = "NUPafa"
		Me.NUPafa.Size = New System.Drawing.Size(42, 20)
		Me.NUPafa.TabIndex = 14
		Me.NUPafa.Value = New Decimal(New Integer() {1, 0, 0, 0})
		'
		'TPInstancesC
		'
		Me.TPInstancesC.AutoScroll = True
		Me.TPInstancesC.Controls.Add(Me.TPInstancesData)
		Me.TPInstancesC.Location = New System.Drawing.Point(4, 22)
		Me.TPInstancesC.Name = "TPInstancesC"
		Me.TPInstancesC.Padding = New System.Windows.Forms.Padding(3)
		Me.TPInstancesC.Size = New System.Drawing.Size(397, 477)
		Me.TPInstancesC.TabIndex = 1
		Me.TPInstancesC.Text = "Instances"
		Me.TPInstancesC.UseVisualStyleBackColor = True
		'
		'TPInstancesData
		'
		Me.TPInstancesData.Controls.Add(Me.TPInstances)
		Me.TPInstancesData.Controls.Add(Me.TPConfigurations)
		Me.TPInstancesData.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TPInstancesData.Location = New System.Drawing.Point(3, 3)
		Me.TPInstancesData.Name = "TPInstancesData"
		Me.TPInstancesData.SelectedIndex = 0
		Me.TPInstancesData.Size = New System.Drawing.Size(391, 471)
		Me.TPInstancesData.TabIndex = 18
		'
		'TPInstances
		'
		Me.TPInstances.AutoScroll = True
		Me.TPInstances.Controls.Add(Me.numInstancesSelector)
		Me.TPInstances.Controls.Add(Me.btnUndeploy)
		Me.TPInstances.Controls.Add(Me.btnCreateInstance)
		Me.TPInstances.Controls.Add(Me.cbCascade)
		Me.TPInstances.Controls.Add(Me.btnNewInstance)
		Me.TPInstances.Controls.Add(Me.btnDeploy)
		Me.TPInstances.Location = New System.Drawing.Point(4, 22)
		Me.TPInstances.Name = "TPInstances"
		Me.TPInstances.Padding = New System.Windows.Forms.Padding(3)
		Me.TPInstances.Size = New System.Drawing.Size(383, 445)
		Me.TPInstances.TabIndex = 0
		Me.TPInstances.Text = "Instances Atributes"
		Me.TPInstances.UseVisualStyleBackColor = True
		'
		'numInstancesSelector
		'
		Me.numInstancesSelector.Location = New System.Drawing.Point(251, 100)
		Me.numInstancesSelector.Name = "numInstancesSelector"
		Me.numInstancesSelector.Size = New System.Drawing.Size(37, 20)
		Me.numInstancesSelector.TabIndex = 13
		Me.numInstancesSelector.Value = New Decimal(New Integer() {1, 0, 0, 0})
		'
		'btnUndeploy
		'
		Me.btnUndeploy.Location = New System.Drawing.Point(170, 140)
		Me.btnUndeploy.Name = "btnUndeploy"
		Me.btnUndeploy.Size = New System.Drawing.Size(75, 23)
		Me.btnUndeploy.TabIndex = 17
		Me.btnUndeploy.Text = "Button1"
		Me.btnUndeploy.UseVisualStyleBackColor = True
		'
		'btnCreateInstance
		'
		Me.btnCreateInstance.Location = New System.Drawing.Point(170, 100)
		Me.btnCreateInstance.Name = "btnCreateInstance"
		Me.btnCreateInstance.Size = New System.Drawing.Size(75, 23)
		Me.btnCreateInstance.TabIndex = 14
		Me.btnCreateInstance.Text = "Generate"
		Me.btnCreateInstance.UseVisualStyleBackColor = True
		'
		'cbCascade
		'
		Me.cbCascade.AutoSize = True
		Me.cbCascade.Location = New System.Drawing.Point(177, 169)
		Me.cbCascade.Name = "cbCascade"
		Me.cbCascade.Size = New System.Drawing.Size(68, 17)
		Me.cbCascade.TabIndex = 16
		Me.cbCascade.Text = "Cascada"
		Me.cbCascade.UseVisualStyleBackColor = True
		'
		'btnNewInstance
		'
		Me.btnNewInstance.Location = New System.Drawing.Point(31, 97)
		Me.btnNewInstance.Name = "btnNewInstance"
		Me.btnNewInstance.Size = New System.Drawing.Size(118, 23)
		Me.btnNewInstance.TabIndex = 0
		Me.btnNewInstance.Text = "Create Instance"
		Me.btnNewInstance.UseVisualStyleBackColor = True
		'
		'btnDeploy
		'
		Me.btnDeploy.Location = New System.Drawing.Point(74, 140)
		Me.btnDeploy.Name = "btnDeploy"
		Me.btnDeploy.Size = New System.Drawing.Size(75, 23)
		Me.btnDeploy.TabIndex = 15
		Me.btnDeploy.Text = "Button1"
		Me.btnDeploy.UseVisualStyleBackColor = True
		'
		'TPConfigurations
		'
		Me.TPConfigurations.Controls.Add(Me.GroupBox3)
		Me.TPConfigurations.Controls.Add(Me.GroupBox1)
		Me.TPConfigurations.Location = New System.Drawing.Point(4, 22)
		Me.TPConfigurations.Name = "TPConfigurations"
		Me.TPConfigurations.Padding = New System.Windows.Forms.Padding(3)
		Me.TPConfigurations.Size = New System.Drawing.Size(383, 445)
		Me.TPConfigurations.TabIndex = 1
		Me.TPConfigurations.Text = "Configurations"
		Me.TPConfigurations.UseVisualStyleBackColor = True
		'
		'GroupBox3
		'
		Me.GroupBox3.Controls.Add(Me.Label15)
		Me.GroupBox3.Controls.Add(Me.Button2)
		Me.GroupBox3.Controls.Add(Me.txtTemplateData)
		Me.GroupBox3.Controls.Add(Me.Label17)
		Me.GroupBox3.Controls.Add(Me.Label16)
		Me.GroupBox3.Controls.Add(Me.ComboBox1)
		Me.GroupBox3.Controls.Add(Me.txtAtributes)
		Me.GroupBox3.Controls.Add(Me.txtArrayAtributes)
		Me.GroupBox3.Location = New System.Drawing.Point(4, 6)
		Me.GroupBox3.Name = "GroupBox3"
		Me.GroupBox3.Size = New System.Drawing.Size(374, 134)
		Me.GroupBox3.TabIndex = 39
		Me.GroupBox3.TabStop = False
		Me.GroupBox3.Text = "New Instance Attributes"
		'
		'Label15
		'
		Me.Label15.AutoSize = True
		Me.Label15.Location = New System.Drawing.Point(254, 14)
		Me.Label15.Name = "Label15"
		Me.Label15.Size = New System.Drawing.Size(70, 13)
		Me.Label15.TabIndex = 41
		Me.Label15.Text = "Reset Values"
		'
		'Button2
		'
		Me.Button2.Font = New System.Drawing.Font("Webdings", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
		Me.Button2.Location = New System.Drawing.Point(323, 10)
		Me.Button2.Name = "Button2"
		Me.Button2.Size = New System.Drawing.Size(25, 25)
		Me.Button2.TabIndex = 31
		Me.Button2.Text = "q"
		Me.Button2.UseVisualStyleBackColor = True
		'
		'txtTemplateData
		'
		Me.txtTemplateData.Location = New System.Drawing.Point(7, 105)
		Me.txtTemplateData.Name = "txtTemplateData"
		Me.txtTemplateData.Size = New System.Drawing.Size(139, 20)
		Me.txtTemplateData.TabIndex = 36
		'
		'Label17
		'
		Me.Label17.AutoSize = True
		Me.Label17.Location = New System.Drawing.Point(4, 60)
		Me.Label17.Name = "Label17"
		Me.Label17.Size = New System.Drawing.Size(78, 13)
		Me.Label17.TabIndex = 38
		Me.Label17.Text = "Array Attributes"
		'
		'Label16
		'
		Me.Label16.AutoSize = True
		Me.Label16.Location = New System.Drawing.Point(4, 21)
		Me.Label16.Name = "Label16"
		Me.Label16.Size = New System.Drawing.Size(81, 13)
		Me.Label16.TabIndex = 37
		Me.Label16.Text = "Alone Attributes"
		'
		'ComboBox1
		'
		Me.ComboBox1.FormattingEnabled = True
		Me.ComboBox1.Location = New System.Drawing.Point(152, 105)
		Me.ComboBox1.Name = "ComboBox1"
		Me.ComboBox1.Size = New System.Drawing.Size(216, 21)
		Me.ComboBox1.TabIndex = 31
		'
		'txtAtributes
		'
		Me.txtAtributes.Location = New System.Drawing.Point(7, 38)
		Me.txtAtributes.Name = "txtAtributes"
		Me.txtAtributes.Size = New System.Drawing.Size(361, 20)
		Me.txtAtributes.TabIndex = 32
		'
		'txtArrayAtributes
		'
		Me.txtArrayAtributes.Location = New System.Drawing.Point(7, 76)
		Me.txtArrayAtributes.Name = "txtArrayAtributes"
		Me.txtArrayAtributes.Size = New System.Drawing.Size(361, 20)
		Me.txtArrayAtributes.TabIndex = 33
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.Label22)
		Me.GroupBox1.Controls.Add(Me.Label21)
		Me.GroupBox1.Controls.Add(Me.txtordenes)
		Me.GroupBox1.Controls.Add(Me.txtestados)
		Me.GroupBox1.Controls.Add(Me.CheckBox2)
		Me.GroupBox1.Controls.Add(Me.CheckBox1)
		Me.GroupBox1.Controls.Add(Me.Label14)
		Me.GroupBox1.Controls.Add(Me.Label13)
		Me.GroupBox1.Controls.Add(Me.txtDirecc)
		Me.GroupBox1.Controls.Add(Me.txtTags)
		Me.GroupBox1.Controls.Add(Me.GroupBox2)
		Me.GroupBox1.Controls.Add(Me.btnLoadData)
		Me.GroupBox1.Location = New System.Drawing.Point(4, 143)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(374, 296)
		Me.GroupBox1.TabIndex = 38
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Excel Import Data"
		'
		'Label22
		'
		Me.Label22.AutoSize = True
		Me.Label22.Location = New System.Drawing.Point(279, 155)
		Me.Label22.Name = "Label22"
		Me.Label22.Size = New System.Drawing.Size(68, 13)
		Me.Label22.TabIndex = 45
		Me.Label22.Text = "DB_Ordenes"
		'
		'Label21
		'
		Me.Label21.AutoSize = True
		Me.Label21.Location = New System.Drawing.Point(282, 114)
		Me.Label21.Name = "Label21"
		Me.Label21.Size = New System.Drawing.Size(66, 13)
		Me.Label21.TabIndex = 44
		Me.Label21.Text = "DB_Estados"
		'
		'txtordenes
		'
		Me.txtordenes.Location = New System.Drawing.Point(285, 173)
		Me.txtordenes.Name = "txtordenes"
		Me.txtordenes.Size = New System.Drawing.Size(76, 20)
		Me.txtordenes.TabIndex = 43
		Me.txtordenes.Text = "DB101"
		'
		'txtestados
		'
		Me.txtestados.Location = New System.Drawing.Point(285, 132)
		Me.txtestados.Name = "txtestados"
		Me.txtestados.Size = New System.Drawing.Size(77, 20)
		Me.txtestados.TabIndex = 42
		Me.txtestados.Text = "DB100"
		'
		'CheckBox2
		'
		Me.CheckBox2.AutoSize = True
		Me.CheckBox2.Location = New System.Drawing.Point(285, 222)
		Me.CheckBox2.Name = "CheckBox2"
		Me.CheckBox2.Size = New System.Drawing.Size(89, 17)
		Me.CheckBox2.TabIndex = 41
		Me.CheckBox2.Text = "2 ScanGroup"
		Me.CheckBox2.UseVisualStyleBackColor = True
		'
		'CheckBox1
		'
		Me.CheckBox1.AutoSize = True
		Me.CheckBox1.Location = New System.Drawing.Point(286, 199)
		Me.CheckBox1.Name = "CheckBox1"
		Me.CheckBox1.Size = New System.Drawing.Size(84, 17)
		Me.CheckBox1.TabIndex = 40
		Me.CheckBox1.Text = "ScanGroup*"
		Me.CheckBox1.UseVisualStyleBackColor = True
		'
		'Label14
		'
		Me.Label14.AutoSize = True
		Me.Label14.Location = New System.Drawing.Point(285, 73)
		Me.Label14.Name = "Label14"
		Me.Label14.Size = New System.Drawing.Size(63, 13)
		Me.Label14.TabIndex = 39
		Me.Label14.Text = "Direcciones"
		'
		'Label13
		'
		Me.Label13.AutoSize = True
		Me.Label13.Location = New System.Drawing.Point(285, 28)
		Me.Label13.Name = "Label13"
		Me.Label13.Size = New System.Drawing.Size(31, 13)
		Me.Label13.TabIndex = 38
		Me.Label13.Text = "Tags"
		'
		'txtDirecc
		'
		Me.txtDirecc.Location = New System.Drawing.Point(285, 91)
		Me.txtDirecc.Name = "txtDirecc"
		Me.txtDirecc.Size = New System.Drawing.Size(77, 20)
		Me.txtDirecc.TabIndex = 37
		'
		'txtTags
		'
		Me.txtTags.Location = New System.Drawing.Point(285, 47)
		Me.txtTags.Name = "txtTags"
		Me.txtTags.Size = New System.Drawing.Size(77, 20)
		Me.txtTags.TabIndex = 36
		'
		'GroupBox2
		'
		Me.GroupBox2.Controls.Add(Me.GroupBox4)
		Me.GroupBox2.Controls.Add(Me.Label12)
		Me.GroupBox2.Controls.Add(Me.Label11)
		Me.GroupBox2.Controls.Add(Me.txtItemplate)
		Me.GroupBox2.Controls.Add(Me.txtIname)
		Me.GroupBox2.Location = New System.Drawing.Point(6, 19)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Size = New System.Drawing.Size(265, 269)
		Me.GroupBox2.TabIndex = 35
		Me.GroupBox2.TabStop = False
		Me.GroupBox2.Text = "Excel Configuration"
		'
		'GroupBox4
		'
		Me.GroupBox4.Controls.Add(Me.EditAttributes)
		Me.GroupBox4.Location = New System.Drawing.Point(6, 60)
		Me.GroupBox4.Name = "GroupBox4"
		Me.GroupBox4.Size = New System.Drawing.Size(250, 203)
		Me.GroupBox4.TabIndex = 36
		Me.GroupBox4.TabStop = False
		Me.GroupBox4.Tag = "EditAttributes"
		Me.GroupBox4.Text = "Excel Attributes"
		'
		'EditAttributes
		'
		Me.EditAttributes.AutoScroll = True
		Me.EditAttributes.Dock = System.Windows.Forms.DockStyle.Fill
		Me.EditAttributes.Location = New System.Drawing.Point(3, 16)
		Me.EditAttributes.Name = "EditAttributes"
		Me.EditAttributes.Size = New System.Drawing.Size(244, 184)
		Me.EditAttributes.TabIndex = 36
		'
		'Label12
		'
		Me.Label12.AutoSize = True
		Me.Label12.Location = New System.Drawing.Point(13, 41)
		Me.Label12.Name = "Label12"
		Me.Label12.Size = New System.Drawing.Size(95, 13)
		Me.Label12.TabIndex = 43
		Me.Label12.Text = "Instance Template"
		'
		'Label11
		'
		Me.Label11.AutoSize = True
		Me.Label11.Location = New System.Drawing.Point(17, 19)
		Me.Label11.Name = "Label11"
		Me.Label11.Size = New System.Drawing.Size(79, 13)
		Me.Label11.TabIndex = 42
		Me.Label11.Text = "Instance Name"
		'
		'txtItemplate
		'
		Me.txtItemplate.Location = New System.Drawing.Point(125, 38)
		Me.txtItemplate.Name = "txtItemplate"
		Me.txtItemplate.Size = New System.Drawing.Size(128, 20)
		Me.txtItemplate.TabIndex = 39
		'
		'txtIname
		'
		Me.txtIname.Location = New System.Drawing.Point(125, 16)
		Me.txtIname.Name = "txtIname"
		Me.txtIname.Size = New System.Drawing.Size(128, 20)
		Me.txtIname.TabIndex = 36
		'
		'btnLoadData
		'
		Me.btnLoadData.Location = New System.Drawing.Point(282, 260)
		Me.btnLoadData.Name = "btnLoadData"
		Me.btnLoadData.Size = New System.Drawing.Size(88, 28)
		Me.btnLoadData.TabIndex = 34
		Me.btnLoadData.Text = "Load Excel"
		Me.btnLoadData.UseVisualStyleBackColor = True
		'
		'TabPage1
		'
		Me.TabPage1.Controls.Add(Me.GroupBox6)
		Me.TabPage1.Controls.Add(Me.GroupBox5)
		Me.TabPage1.Location = New System.Drawing.Point(4, 22)
		Me.TabPage1.Name = "TabPage1"
		Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage1.Size = New System.Drawing.Size(397, 477)
		Me.TabPage1.TabIndex = 0
		Me.TabPage1.Text = "Galaxy"
		Me.TabPage1.UseVisualStyleBackColor = True
		'
		'GroupBox6
		'
		Me.GroupBox6.Controls.Add(Me.lstTemplates)
		Me.GroupBox6.Controls.Add(Me.Label5)
		Me.GroupBox6.Controls.Add(Me.ProgressBar1)
		Me.GroupBox6.Controls.Add(Me.btnExport)
		Me.GroupBox6.Controls.Add(Me.chkHideBaseTemplates)
		Me.GroupBox6.Controls.Add(Me.Label4)
		Me.GroupBox6.Controls.Add(Me.btnRefreshTemplates)
		Me.GroupBox6.Controls.Add(Me.Label6)
		Me.GroupBox6.Controls.Add(Me.lblFolderPath)
		Me.GroupBox6.Controls.Add(Me.linkLblSelectAll)
		Me.GroupBox6.Controls.Add(Me.btnBrowseFolders)
		Me.GroupBox6.Controls.Add(Me.linkLblSelectNone)
		Me.GroupBox6.Controls.Add(Me.Label9)
		Me.GroupBox6.Location = New System.Drawing.Point(9, 213)
		Me.GroupBox6.Name = "GroupBox6"
		Me.GroupBox6.Size = New System.Drawing.Size(378, 258)
		Me.GroupBox6.TabIndex = 32
		Me.GroupBox6.TabStop = False
		Me.GroupBox6.Text = "Template Extract"
		'
		'lstTemplates
		'
		Me.lstTemplates.FormattingEnabled = True
		Me.lstTemplates.Location = New System.Drawing.Point(80, 17)
		Me.lstTemplates.Name = "lstTemplates"
		Me.lstTemplates.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
		Me.lstTemplates.Size = New System.Drawing.Size(273, 121)
		Me.lstTemplates.Sorted = True
		Me.lstTemplates.TabIndex = 15
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label5.Location = New System.Drawing.Point(19, 198)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(73, 15)
		Me.Label5.TabIndex = 9
		Me.Label5.Text = "Export Path:"
		'
		'ProgressBar1
		'
		Me.ProgressBar1.Location = New System.Drawing.Point(100, 238)
		Me.ProgressBar1.Maximum = 2000
		Me.ProgressBar1.Name = "ProgressBar1"
		Me.ProgressBar1.Size = New System.Drawing.Size(250, 5)
		Me.ProgressBar1.TabIndex = 30
		'
		'btnExport
		'
		Me.btnExport.Location = New System.Drawing.Point(20, 220)
		Me.btnExport.Name = "btnExport"
		Me.btnExport.Size = New System.Drawing.Size(75, 23)
		Me.btnExport.TabIndex = 10
		Me.btnExport.Text = "Export"
		Me.btnExport.UseVisualStyleBackColor = True
		'
		'chkHideBaseTemplates
		'
		Me.chkHideBaseTemplates.AutoSize = True
		Me.chkHideBaseTemplates.Checked = True
		Me.chkHideBaseTemplates.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkHideBaseTemplates.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.chkHideBaseTemplates.Location = New System.Drawing.Point(84, 145)
		Me.chkHideBaseTemplates.Name = "chkHideBaseTemplates"
		Me.chkHideBaseTemplates.Size = New System.Drawing.Size(144, 19)
		Me.chkHideBaseTemplates.TabIndex = 28
		Me.chkHideBaseTemplates.Text = "Hide Base Templates"
		Me.chkHideBaseTemplates.UseVisualStyleBackColor = True
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.Location = New System.Drawing.Point(7, 15)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(68, 15)
		Me.Label4.TabIndex = 8
		Me.Label4.Text = "Templates:"
		'
		'btnRefreshTemplates
		'
		Me.btnRefreshTemplates.Font = New System.Drawing.Font("Webdings", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
		Me.btnRefreshTemplates.Location = New System.Drawing.Point(32, 36)
		Me.btnRefreshTemplates.Name = "btnRefreshTemplates"
		Me.btnRefreshTemplates.Size = New System.Drawing.Size(30, 25)
		Me.btnRefreshTemplates.TabIndex = 27
		Me.btnRefreshTemplates.Text = "q"
		Me.btnRefreshTemplates.UseVisualStyleBackColor = True
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label6.ForeColor = System.Drawing.Color.Red
		Me.Label6.Location = New System.Drawing.Point(102, 220)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(247, 15)
		Me.Label6.TabIndex = 11
		Me.Label6.Text = "Warning! May overwrite existing files in path."
		'
		'lblFolderPath
		'
		Me.lblFolderPath.AutoSize = True
		Me.lblFolderPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblFolderPath.Location = New System.Drawing.Point(94, 198)
		Me.lblFolderPath.Name = "lblFolderPath"
		Me.lblFolderPath.Size = New System.Drawing.Size(10, 15)
		Me.lblFolderPath.TabIndex = 20
		Me.lblFolderPath.Text = " "
		'
		'linkLblSelectAll
		'
		Me.linkLblSelectAll.AutoSize = True
		Me.linkLblSelectAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.linkLblSelectAll.Location = New System.Drawing.Point(277, 147)
		Me.linkLblSelectAll.Name = "linkLblSelectAll"
		Me.linkLblSelectAll.Size = New System.Drawing.Size(20, 15)
		Me.linkLblSelectAll.TabIndex = 16
		Me.linkLblSelectAll.TabStop = True
		Me.linkLblSelectAll.Text = "All"
		'
		'btnBrowseFolders
		'
		Me.btnBrowseFolders.Location = New System.Drawing.Point(20, 168)
		Me.btnBrowseFolders.Name = "btnBrowseFolders"
		Me.btnBrowseFolders.Size = New System.Drawing.Size(104, 23)
		Me.btnBrowseFolders.TabIndex = 19
		Me.btnBrowseFolders.Text = "Select Export Path"
		Me.btnBrowseFolders.UseVisualStyleBackColor = True
		'
		'linkLblSelectNone
		'
		Me.linkLblSelectNone.AutoSize = True
		Me.linkLblSelectNone.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.linkLblSelectNone.Location = New System.Drawing.Point(301, 147)
		Me.linkLblSelectNone.Name = "linkLblSelectNone"
		Me.linkLblSelectNone.Size = New System.Drawing.Size(37, 15)
		Me.linkLblSelectNone.TabIndex = 17
		Me.linkLblSelectNone.TabStop = True
		Me.linkLblSelectNone.Text = "None"
		'
		'Label9
		'
		Me.Label9.AutoSize = True
		Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label9.Location = New System.Drawing.Point(231, 147)
		Me.Label9.Name = "Label9"
		Me.Label9.Size = New System.Drawing.Size(44, 15)
		Me.Label9.TabIndex = 18
		Me.Label9.Text = "Select:"
		'
		'GroupBox5
		'
		Me.GroupBox5.Controls.Add(Me.cmboGalaxyList)
		Me.GroupBox5.Controls.Add(Me.lblSecurityType)
		Me.GroupBox5.Controls.Add(Me.Label10)
		Me.GroupBox5.Controls.Add(Me.grpLoginInput)
		Me.GroupBox5.Controls.Add(Me.txtNodeName)
		Me.GroupBox5.Controls.Add(Me.Label8)
		Me.GroupBox5.Controls.Add(Me.Label1)
		Me.GroupBox5.Controls.Add(Me.btnRefreshGalaxies)
		Me.GroupBox5.Controls.Add(Me.Label7)
		Me.GroupBox5.Location = New System.Drawing.Point(8, 6)
		Me.GroupBox5.Name = "GroupBox5"
		Me.GroupBox5.Size = New System.Drawing.Size(379, 201)
		Me.GroupBox5.TabIndex = 31
		Me.GroupBox5.TabStop = False
		Me.GroupBox5.Text = "Galaxy Login"
		'
		'cmboGalaxyList
		'
		Me.cmboGalaxyList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmboGalaxyList.FormattingEnabled = True
		Me.cmboGalaxyList.Location = New System.Drawing.Point(81, 46)
		Me.cmboGalaxyList.Name = "cmboGalaxyList"
		Me.cmboGalaxyList.Size = New System.Drawing.Size(273, 21)
		Me.cmboGalaxyList.TabIndex = 1
		'
		'lblSecurityType
		'
		Me.lblSecurityType.AutoSize = True
		Me.lblSecurityType.Location = New System.Drawing.Point(95, 75)
		Me.lblSecurityType.Name = "lblSecurityType"
		Me.lblSecurityType.Size = New System.Drawing.Size(10, 13)
		Me.lblSecurityType.TabIndex = 21
		Me.lblSecurityType.Text = " "
		'
		'Label10
		'
		Me.Label10.AutoSize = True
		Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label10.Location = New System.Drawing.Point(11, 15)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(40, 15)
		Me.Label10.TabIndex = 22
		Me.Label10.Text = "Node:"
		'
		'grpLoginInput
		'
		Me.grpLoginInput.Controls.Add(Me.btnLogin)
		Me.grpLoginInput.Controls.Add(Me.Label3)
		Me.grpLoginInput.Controls.Add(Me.Label2)
		Me.grpLoginInput.Controls.Add(Me.txtPwdInput)
		Me.grpLoginInput.Controls.Add(Me.txtUserInput)
		Me.grpLoginInput.Location = New System.Drawing.Point(91, 88)
		Me.grpLoginInput.Name = "grpLoginInput"
		Me.grpLoginInput.Size = New System.Drawing.Size(263, 99)
		Me.grpLoginInput.TabIndex = 14
		Me.grpLoginInput.TabStop = False
		'
		'btnLogin
		'
		Me.btnLogin.Location = New System.Drawing.Point(132, 68)
		Me.btnLogin.Name = "btnLogin"
		Me.btnLogin.Size = New System.Drawing.Size(105, 21)
		Me.btnLogin.TabIndex = 11
		Me.btnLogin.Text = "Login"
		Me.btnLogin.UseVisualStyleBackColor = True
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Location = New System.Drawing.Point(12, 45)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(64, 15)
		Me.Label3.TabIndex = 10
		Me.Label3.Text = "Password:"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Location = New System.Drawing.Point(12, 18)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(36, 15)
		Me.Label2.TabIndex = 9
		Me.Label2.Text = "User:"
		'
		'txtPwdInput
		'
		Me.txtPwdInput.Location = New System.Drawing.Point(90, 42)
		Me.txtPwdInput.Name = "txtPwdInput"
		Me.txtPwdInput.Size = New System.Drawing.Size(147, 20)
		Me.txtPwdInput.TabIndex = 8
		Me.txtPwdInput.UseSystemPasswordChar = True
		'
		'txtUserInput
		'
		Me.txtUserInput.Location = New System.Drawing.Point(90, 13)
		Me.txtUserInput.Name = "txtUserInput"
		Me.txtUserInput.Size = New System.Drawing.Size(147, 20)
		Me.txtUserInput.TabIndex = 7
		'
		'txtNodeName
		'
		Me.txtNodeName.Location = New System.Drawing.Point(81, 15)
		Me.txtNodeName.Name = "txtNodeName"
		Me.txtNodeName.Size = New System.Drawing.Size(237, 20)
		Me.txtNodeName.TabIndex = 23
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Location = New System.Drawing.Point(98, 88)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(0, 13)
		Me.Label8.TabIndex = 13
		Me.Label8.Tag = "authenticationMode"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(11, 49)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(47, 15)
		Me.Label1.TabIndex = 2
		Me.Label1.Text = "Galaxy:"
		'
		'btnRefreshGalaxies
		'
		Me.btnRefreshGalaxies.Font = New System.Drawing.Font("Webdings", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
		Me.btnRefreshGalaxies.Location = New System.Drawing.Point(324, 11)
		Me.btnRefreshGalaxies.Name = "btnRefreshGalaxies"
		Me.btnRefreshGalaxies.Size = New System.Drawing.Size(30, 25)
		Me.btnRefreshGalaxies.TabIndex = 24
		Me.btnRefreshGalaxies.Text = "q"
		Me.btnRefreshGalaxies.UseVisualStyleBackColor = True
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label7.Location = New System.Drawing.Point(11, 72)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(78, 15)
		Me.Label7.TabIndex = 12
		Me.Label7.Text = "Security type:"
		'
		'Configuraciones
		'
		Me.Configuraciones.Controls.Add(Me.TabPage1)
		Me.Configuraciones.Controls.Add(Me.TPInstancesC)
		Me.Configuraciones.Controls.Add(Me.TabPage5)
		Me.Configuraciones.Controls.Add(Me.TabPage4)
		Me.Configuraciones.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Configuraciones.Location = New System.Drawing.Point(0, 0)
		Me.Configuraciones.Name = "Configuraciones"
		Me.Configuraciones.SelectedIndex = 0
		Me.Configuraciones.Size = New System.Drawing.Size(405, 503)
		Me.Configuraciones.TabIndex = 30
		'
		'frmMain
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(405, 525)
		Me.Controls.Add(Me.Configuraciones)
		Me.Controls.Add(Me.StatusStrip1)
		Me.Name = "frmMain"
		Me.Text = "aaGRToolKit"
		Me.StatusStrip1.ResumeLayout(False)
		Me.StatusStrip1.PerformLayout()
		CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.TabPage4.ResumeLayout(False)
		Me.TabPage5.ResumeLayout(False)
		Me.TPTemplates.ResumeLayout(False)
		Me.TPUDA.ResumeLayout(False)
		Me.TPUDA.PerformLayout()
		Me.TabPage6.ResumeLayout(False)
		Me.TabPage6.PerformLayout()
		CType(Me.NUPscr, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.NUPdfa, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.NUPuda, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.NUPafa, System.ComponentModel.ISupportInitialize).EndInit()
		Me.TPInstancesC.ResumeLayout(False)
		Me.TPInstancesData.ResumeLayout(False)
		Me.TPInstances.ResumeLayout(False)
		Me.TPInstances.PerformLayout()
		CType(Me.numInstancesSelector, System.ComponentModel.ISupportInitialize).EndInit()
		Me.TPConfigurations.ResumeLayout(False)
		Me.GroupBox3.ResumeLayout(False)
		Me.GroupBox3.PerformLayout()
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.GroupBox2.ResumeLayout(False)
		Me.GroupBox2.PerformLayout()
		Me.GroupBox4.ResumeLayout(False)
		Me.TabPage1.ResumeLayout(False)
		Me.GroupBox6.ResumeLayout(False)
		Me.GroupBox6.PerformLayout()
		Me.GroupBox5.ResumeLayout(False)
		Me.GroupBox5.PerformLayout()
		Me.grpLoginInput.ResumeLayout(False)
		Me.grpLoginInput.PerformLayout()
		Me.Configuraciones.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents dlgFolderBrowser As System.Windows.Forms.FolderBrowserDialog
	Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
	Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents DirectoryEntry1 As DirectoryServices.DirectoryEntry
	Friend WithEvents FileSystemWatcher1 As IO.FileSystemWatcher
	Friend WithEvents Configuraciones As TabControl
	Friend WithEvents TabPage1 As TabPage
	Friend WithEvents ProgressBar1 As ProgressBar
	Friend WithEvents Label10 As Label
	Friend WithEvents cmboGalaxyList As ComboBox
	Friend WithEvents chkHideBaseTemplates As CheckBox
	Friend WithEvents Label1 As Label
	Friend WithEvents btnRefreshTemplates As Button
	Friend WithEvents Label7 As Label
	Friend WithEvents lblFolderPath As Label
	Friend WithEvents btnRefreshGalaxies As Button
	Friend WithEvents btnBrowseFolders As Button
	Friend WithEvents Label8 As Label
	Friend WithEvents Label9 As Label
	Friend WithEvents txtNodeName As TextBox
	Friend WithEvents linkLblSelectNone As LinkLabel
	Friend WithEvents grpLoginInput As GroupBox
	Friend WithEvents btnLogin As Button
	Friend WithEvents Label3 As Label
	Friend WithEvents Label2 As Label
	Friend WithEvents txtPwdInput As TextBox
	Friend WithEvents txtUserInput As TextBox
	Friend WithEvents linkLblSelectAll As LinkLabel
	Friend WithEvents lblSecurityType As Label
	Friend WithEvents lstTemplates As ListBox
	Friend WithEvents Label6 As Label
	Friend WithEvents Label4 As Label
	Friend WithEvents btnExport As Button
	Friend WithEvents Label5 As Label
	Friend WithEvents TPInstancesC As TabPage
	Friend WithEvents numInstancesSelector As NumericUpDown
	Friend WithEvents btnUndeploy As Button
	Friend WithEvents btnCreateInstance As Button
	Friend WithEvents cbCascade As CheckBox
	Friend WithEvents btnNewInstance As Button
	Friend WithEvents btnDeploy As Button
	Friend WithEvents TabPage5 As TabPage
	Friend WithEvents TPTemplates As TabControl
	Friend WithEvents TPDiscFA As TabPage
	Friend WithEvents TPFAAnalog As TabPage
	Friend WithEvents TPUDA As TabPage
	Friend WithEvents txtValor1 As TextBox
	Friend WithEvents Label23 As Label
	Friend WithEvents Label20 As Label
	Friend WithEvents txtValueName As TextBox
	Friend WithEvents TabPage3 As TabPage
	Friend WithEvents TabPage6 As TabPage
	Friend WithEvents btnUploadTemplateAttr As Button
	Friend WithEvents Label27 As Label
	Friend WithEvents btnNewTemplate As Button
	Friend WithEvents txtNewTName As TextBox
	Friend WithEvents Label26 As Label
	Friend WithEvents txtDTemplate As TextBox
	Friend WithEvents Label25 As Label
	Friend WithEvents Label19 As Label
	Friend WithEvents Label24 As Label
	Friend WithEvents Label18 As Label
	Friend WithEvents NUPscr As NumericUpDown
	Friend WithEvents NUPdfa As NumericUpDown
	Friend WithEvents NUPuda As NumericUpDown
	Friend WithEvents NUPafa As NumericUpDown
	Friend WithEvents TabPage4 As TabPage
	Friend WithEvents ErrorLog As ListBox
	Friend WithEvents TPInstancesData As TabControl
	Friend WithEvents TPInstances As TabPage
	Friend WithEvents TPConfigurations As TabPage
	Friend WithEvents GroupBox6 As GroupBox
	Friend WithEvents GroupBox5 As GroupBox
	Friend WithEvents GroupBox3 As GroupBox
	Friend WithEvents Label15 As Label
	Friend WithEvents Button2 As Button
	Friend WithEvents txtTemplateData As TextBox
	Friend WithEvents Label17 As Label
	Friend WithEvents Label16 As Label
	Friend WithEvents ComboBox1 As ComboBox
	Friend WithEvents txtAtributes As TextBox
	Friend WithEvents txtArrayAtributes As TextBox
	Friend WithEvents GroupBox1 As GroupBox
	Friend WithEvents Label22 As Label
	Friend WithEvents Label21 As Label
	Friend WithEvents txtordenes As TextBox
	Friend WithEvents txtestados As TextBox
	Friend WithEvents CheckBox2 As CheckBox
	Friend WithEvents CheckBox1 As CheckBox
	Friend WithEvents Label14 As Label
	Friend WithEvents Label13 As Label
	Friend WithEvents txtDirecc As TextBox
	Friend WithEvents txtTags As TextBox
	Friend WithEvents GroupBox2 As GroupBox
	Friend WithEvents GroupBox4 As GroupBox
	Friend WithEvents EditAttributes As Panel
	Friend WithEvents Label12 As Label
	Friend WithEvents Label11 As Label
	Friend WithEvents txtItemplate As TextBox
	Friend WithEvents txtIname As TextBox
	Friend WithEvents btnLoadData As Button
End Class

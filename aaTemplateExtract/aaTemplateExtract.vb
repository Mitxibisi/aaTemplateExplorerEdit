﻿Imports System.IO
Imports aaGRAccessApp

''' <summary>
''' The aaTemplateExtract class interfaces with GRAccess to connect, collect, and parse data from the Galaxy.
''' All the hard work is in this class, but aaTemplateData contains the actual data structure.
''' </summary>
Public Class aaTemplateExtract
    Public authMode As String
    Public showLogin As Boolean
    Public errorMessage As String
    Public loggedIn As Boolean

    Private resultStatus As Boolean
    Private resultCount As Integer
    Private galaxyNode As String
    Private galaxyName As String
    Private templateName As String
    Private grAccess As aaGRAccessApp.GRAccessApp
    Private myGalaxy As aaGRAccessApp.IGalaxy

    ' ------- Galaxy ----------
    ''' <summary>
    ''' Initializes and sets up our GRAccess client app.
    ''' </summary>
    Public Sub New()
        grAccess = New aaGRAccessApp.GRAccessApp
        loggedIn = False
        showLogin = True
    End Sub

    ''' <summary>
    ''' Queries for all the galaxy names on this node.
    ''' </summary>
    ''' <param name="NodeName">The node name as a string (e.g. "localhost").</param>
    ''' <returns>A collection of galaxy names as strings.</returns>
    Public Function getGalaxies(ByVal NodeName As String) As Collection
        Dim galaxies As aaGRAccessApp.IGalaxies
        Dim galaxy As aaGRAccessApp.IGalaxy
        Dim galaxyList As New Collection

        galaxyNode = NodeName

        Try
            galaxies = grAccess.QueryGalaxies(galaxyNode)
            resultStatus = grAccess.CommandResult.Successful
            If resultStatus And (galaxies IsNot Nothing) And galaxies.count > 0 Then
                For Each galaxy In galaxies
                    galaxyList.Add(galaxy.Name)
                Next
            Else
                Throw New ApplicationException("No Galaxies detected on this node")
            End If

        Catch e As Exception
            MessageBox.Show("Error occurred: " & e.Message)
        End Try
        Return galaxyList
    End Function

    ''' <summary>
    ''' Given a Galaxy Name, sets our Client to that Galaxy.
    ''' </summary>
    ''' <param name="galaxyName">A string that is the Galaxy name.</param>
    ''' <returns>Nothing</returns>
    Public Function setGalaxy(ByVal galaxyName)
        showLogin = True
        loggedIn = False
        Try
            If (galaxyNode IsNot Nothing) And (galaxyName IsNot Nothing) Then
                myGalaxy = grAccess.QueryGalaxies(galaxyNode)(galaxyName)
                authMode = getAuthType()
            Else
                Throw New ApplicationException("No Node or Galaxy Selected")
            End If
        Catch e As Exception
            MessageBox.Show("Error occurred: " & e.Message)
        End Try

        Return 0
    End Function

    ''' <summary>
    ''' Determines what authentication type is required for this Galaxy.
    ''' </summary>
    ''' <returns>The authentication mode.</returns>
    ''' <remarks>Not currently working.</remarks>
    Private Function getAuthType() As String
        Dim mode As String
        'Dim galaxySecurity As aaGRAccessApp.IGalaxySecurity
        Dim authMode As New aaGRAccessApp.EAUTHMODE
        mode = ""

        mode = "Unknown. Click Login with blank User/Pass if no security."

        Return mode
    End Function

    ''' <summary>
    ''' Tries to login to the Galaxy.
    ''' </summary>
    ''' <param name="user">User name, include Domain (e.g. "Domain\user") if authenticating against a domain.</param>
    ''' <param name="password">Password</param>
    ''' <returns>Status of login attempt.</returns>
    Public Function login(ByVal user, ByVal password) As Integer
        Try
            myGalaxy.Login(user, password)
            If myGalaxy.CommandResult.Successful Then
                loggedIn = True
                Return 0
            Else
                loggedIn = False
                Return -1
            End If
        Catch e As Exception
            loggedIn = False
            MessageBox.Show("Error occurred: " & e.Message)
            Return -2
        End Try

    End Function


    ' ------- Templates ----------
    ''' <summary>
    ''' Discovers all of the templates in this Galaxy.
    ''' </summary>
    ''' <param name="HideBaseTemplates">If set, will not return the base templates that are in every Galaxy.</param>
    ''' <returns>A collection of strings with the template names listed.</returns>
    ''' <remarks>Does not return instances or checked in templates.</remarks>
    Public Function getTemplates(Optional ByVal HideBaseTemplates As Boolean = False) As Collection
        Dim templateList As New Collection
        Dim gTemplates As aaGRAccessApp.IgObjects
        Dim gTemplate As aaGRAccessApp.IgObject

        Dim BaseTemplates() As String = New String() {"$Boolean", "$Integer", "$Double", "$Float", "$String", "$FieldReference", "$UserDefined", "$AnalogDevice", "$AppEngine", "$Area", "$DDESuiteLinkClient", "$DiscreteDevice", "$InTouchProxy", "$InTouchViewApp", "$OPCClient", "$RedundantDIObject", "$Sequencer", "$SQLData", "$Switch", "$ViewEngine", "$WinPlatform"}

        Try
            If loggedIn Then
                gTemplates = myGalaxy.QueryObjects(aaGRAccessApp.EgObjectIsTemplateOrInstance.gObjectIsTemplate,
                                                aaGRAccessApp.EConditionType.checkoutStatusIs,
                                                aaGRAccessApp.ECheckoutStatus.notCheckedOut,
                                                aaGRAccessApp.EMatch.MatchCondition)
                resultStatus = myGalaxy.CommandResult.Successful
                If resultStatus And (gTemplates IsNot Nothing) And (gTemplates.count > 0) Then
                    For Each gTemplate In gTemplates
                        If Not (HideBaseTemplates And BaseTemplates.Contains(gTemplate.Tagname)) Then
                            templateList.Add(gTemplate.Tagname)
                        End If
                    Next
                Else
                    Throw New ApplicationException("No templates found")
                End If
            End If

        Catch e As Exception
            MessageBox.Show("Error occurred: " & e.Message)
        End Try

        Return templateList
    End Function

    ''' <summary>
    ''' This is the master function that is initiated for each template. Sub-functions will gather all of the attributes and scripts.
    ''' </summary>
    ''' <param name="TemplateName">The template name that is desired to get data from.</param>
    ''' <returns>An aaTemplate class of all of the template data (scripts, UDAs, field attributes).</returns>
    Public Function getTemplateData(ByVal TemplateName As String) As aaTemplate
        Dim templateList(1) As String
        Dim gTemplates As aaGRAccessApp.IgObjects
        Dim gTemplate As aaGRAccessApp.ITemplate
        Dim templateData As New aaTemplate()
        Dim gDerivedTemplate As String

        Try
            If loggedIn Then
                ' Convert the individual template name to an array. 
                templateList(0) = TemplateName
                ' query the galaxy for this template's data
                gTemplates = myGalaxy.QueryObjectsByName(aaGRAccessApp.EgObjectIsTemplateOrInstance.gObjectIsTemplate, templateList)
                resultStatus = myGalaxy.CommandResult.Successful
                If resultStatus And (gTemplates IsNot Nothing) And (gTemplates.count > 0) Then
                    gTemplate = gTemplates(1)

                    ' get all of the Configurable Attributes
                    Dim gAttributes = gTemplate.Attributes

                    ' Get the current template data
                    templateData = New aaTemplate(gTemplate.Tagname,
                                              GetAttrInteger("ConfigVersion", gAttributes),
                                              GetFieldAttributesDiscrete(gAttributes),
                                              GetFieldAttributesAnalog(gAttributes))

                    ' Check for derived templates and recursively get their data
                    gDerivedTemplate = gTemplate.DerivedFrom
                    If Not String.IsNullOrEmpty(gDerivedTemplate) Then
                        Dim derivedTemplateData As aaTemplate = getTemplateData(gDerivedTemplate)
                        ' Sumar los FieldAttributes de la plantilla derivada
                        templateData.AddAttributes(derivedTemplateData)
                    End If
                End If
            Else
                Throw New ApplicationException("Not Logged In")
            End If

        Catch e As Exception
            frmMain.LogBox.Items.Add("Error occurred: " & e.Message)
        End Try

        Return templateData
    End Function

    Public Sub CreateTemplate(ByVal TemplateName As String,
                              ByVal NewTemplateName As String,
                              ByVal AttrName As String(),
                              ByVal Valor As String(),
                              ByVal DiscFANames As List(Of String),
                              ByVal AnalogFANames As List(Of String),
                              ByVal DiscFADesc As List(Of String),
                              ByVal AnalogFADesc As List(Of String),
                              ByVal AnalogFAEngUnits As List(Of String),
                              ByVal DiscFAAlarmed As List(Of Boolean),
                              ByVal DiscFAHistorized As List(Of Boolean),
                              ByVal AnalogFAHistorized As List(Of Boolean),
                              ByVal DiscFAEvents As List(Of Boolean),
                              ByVal AnalogFAEvents As List(Of Boolean),
                              ByVal DiscFALocks As List(Of List(Of Boolean)),
                              ByVal AnalogFAScaled As List(Of Boolean),
                              ByVal AnalogFALocks As List(Of List(Of Boolean)))

        Try
            If loggedIn Then
                Dim templateList(0) As String
                templateList(0) = TemplateName

                ' Consultar plantilla
                Dim gTemplates As aaGRAccessApp.IgObjects = myGalaxy.QueryObjectsByName(aaGRAccessApp.EgObjectIsTemplateOrInstance.gObjectIsTemplate, templateList)
                If gTemplates Is Nothing OrElse gTemplates.count = 0 Then
                    frmMain.LogBox.Items.Add("Error: Plantilla '" & TemplateName & "' no encontrada.")
                    Exit Sub
                End If

                Dim baseTemplate As aaGRAccessApp.ITemplate = CType(gTemplates(1), aaGRAccessApp.ITemplate)
                Dim newTemplate As aaGRAccessApp.ITemplate = baseTemplate.CreateTemplate(NewTemplateName, True)

                AddNewTemAttr(newTemplate,
                                AttrName,
                                Valor,
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
                                DiscFALocks,
                                AnalogFAScaled,
                                AnalogFALocks)

                frmMain.LogBox.Items.Add("Plantilla y atributos guardados y check-in realizado.")
            Else
                Throw New ApplicationException("No se ha iniciado sesión en la galaxia.")
            End If
        Catch ex As Exception
            frmMain.LogBox.Items.Add("Error general: " & ex.Message)
        End Try
    End Sub


    ' ------- Instances ----------
    Public Sub createInstance(ByVal TemplateName As String, ByVal InstanceName As String, ByVal AreaName As String, ByVal LoneAttrText As List(Of String), ByVal ArrayAttrText As List(Of String), AloneAttributes As String(), arrayAttributes As String())
        Dim templateList(1) As String
        Dim gTemplates As aaGRAccessApp.IgObjects
        Dim gTemplate As aaGRAccessApp.ITemplate
        Dim instance As aaGRAccessApp.IInstance
        Dim templateData As New aaTemplate()
        Dim FinalMap As String() = {}

        Try
            If loggedIn Then
                ' Convert the individual template name to an array. 
                templateList(0) = TemplateName
                ' query the galaxy for this template's data
                gTemplates = myGalaxy.QueryObjectsByName(aaGRAccessApp.EgObjectIsTemplateOrInstance.gObjectIsTemplate, templateList)
                resultStatus = myGalaxy.CommandResult.Successful
                If resultStatus And (gTemplates IsNot Nothing) And (gTemplates.count > 0) Then
                    gTemplate = gTemplates(1)
                    instance = gTemplate.CreateInstance(InstanceName, True)
                    instance.CheckOut()
                    EditarCfg(LoneAttrText, ArrayAttrText, instance, AloneAttributes, arrayAttributes)
                    instance.Save()
                    instance.Area = AreaName
                Else
                    MessageBox.Show("Error: Template" & TemplateName & "not found")
                End If
            Else
                Throw New ApplicationException("Not Logged In")
            End If

        Catch e As Exception
            MessageBox.Show("Error: " & e.Message)
        End Try
    End Sub

    Public Sub deployUndeployInstance(ByVal InstanceName As String, Cascade As Boolean, mode As Integer)
        Dim InstanceList(1) As String
        Dim gInstances As aaGRAccessApp.IgObjects
        Dim instance As aaGRAccessApp.IInstance

        Try
            If loggedIn Then

                InstanceList(0) = InstanceName
                gInstances = myGalaxy.QueryObjectsByName(aaGRAccessApp.EgObjectIsTemplateOrInstance.gObjectIsInstance, InstanceList)
                resultStatus = myGalaxy.CommandResult.Successful

                If mode = 1 Then
                    If resultStatus Then
                        instance = gInstances(1)
                        instance.Undeploy(EForceOffScan.doForceOffScan, ECascade.doCascade, markAsUndeployedOnFailure:=False)
                    End If
                Else
                    If resultStatus Then
                        instance = gInstances(1)
                        If Cascade Then
                            instance.Deploy(EActionForCurrentlyDeployedObjects.deployChanges, ESkipIfCurrentlyUndeployed.dontSkipIfCurrentlyUndeployed, EDeployOnScan.doDeployOnScan, EForceOffScan.doForceOffScan, ECascade.doCascade, True)
                        Else
                            instance.Deploy(EActionForCurrentlyDeployedObjects.deployChanges, ESkipIfCurrentlyUndeployed.dontSkipIfCurrentlyUndeployed, EDeployOnScan.doDeployOnScan, EForceOffScan.doForceOffScan, ECascade.dontCascade, True)
                        End If
                    Else
                        MessageBox.Show("Error: Instance" & InstanceName & "not found")
                    End If
                End If
            Else
                Throw New ApplicationException("Not Logged In")
            End If

        Catch e As Exception
            MessageBox.Show("Error: " & e.Message)
        End Try
    End Sub


    ' ------- Instance Attributes ----------
    Private Sub AgregarElementosACfgMapeado(gAttributes As aaGRAccess.IAttributes, valores As String(), ArrayAttributes As String)
        Try
            Dim Attr As aaGRAccess.IAttribute = gAttributes.Item(ArrayAttributes)

            If Attr Is Nothing Then
                frmMain.LogBox.Items.Add($"El atributo {ArrayAttributes} no existe.")
                Return
            End If

            ' Crear un nuevo MxValue para el array
            Dim MXVal_ As New aaGRAccess.MxValue()

            ' Iniciar el array estableciendo el tipo de datos, por ejemplo, String
            If valores.Length > 0 Then
                MXVal_.PutString(valores(0)) ' Establecer el tipo de datos usando el primer valor
            End If

            ' Obtener el tamaño máximo del array
            Dim maxItems As Integer = 50

            ' Asegurarse de no agregar más de 50 elementos
            Dim elementosAAgregar As Integer = Math.Min(valores.Length, maxItems)

            ' Recorrer los valores y agregar cada uno al array utilizando PutElement
            For i As Integer = 0 To elementosAAgregar - 1 ' Asegurarse de no exceder el tamaño de 'valores'
                Dim Elemento As New aaGRAccess.MxValue()

                ' Verificar si el valor está vacío o nulo
                If String.IsNullOrEmpty(valores(i)) Then
                    ' Establecer el valor vacío
                    Elemento.PutString("")
                Else
                    ' Establecer el valor real
                    Elemento.PutString(valores(i))
                End If

                ' Usar i + 1 para asegurar índices secuenciales (1-based)
                MXVal_.PutElement(i + 1, Elemento)
            Next

            ' Si el tamaño de 'valores' es menor que 'maxItems', completa el array con valores vacíos
            For i As Integer = elementosAAgregar To maxItems - 1
                Dim Elemento As New aaGRAccess.MxValue()
                Elemento.PutString("") ' Valor vacío
                MXVal_.PutElement(i + 1, Elemento) ' Usar i + 1 para asegurar índices secuenciales
            Next

            ' Asignar el array al atributo Cfg_Mapeado
            Attr.SetValue(MXVal_)

            frmMain.LogBox.Items.Add("Los elementos se han agregado correctamente al atributo " & ArrayAttributes)
        Catch ex As Exception
            frmMain.LogBox.Items.Add("Error al agregar los elementos: " & ex.Message)
        End Try
    End Sub

    Private Sub EditarCfg(Aloneattr As List(Of String), arrayeattr As List(Of String), Instance As aaGRAccessApp.IInstance, ConfigurableAttr As String(), ArrayAttributes As String())
        Dim gAttributes = Instance.Attributes

        Try
            For j As Integer = 0 To ConfigurableAttr.Length - 1
                ToMxValue(Aloneattr(j), gAttributes, Instance, ConfigurableAttr(j))
            Next

            For i As Integer = 0 To ArrayAttributes.Length - 1
                Dim FinalMap = arrayeattr(i).Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                AgregarElementosACfgMapeado(gAttributes, FinalMap, ArrayAttributes(i))
            Next

            Instance.Save()
            Instance.CheckIn()
        Catch ex As Exception
            frmMain.LogBox.Items.Add("Error " & ex.Message)
        End Try
    End Sub


    ' ------- Template Attributes ----------
    ''' <summary>
    ''' Gets all of the Analog Field Attributes for a given template. 
    ''' </summary>
    ''' <param name="gAttributes">All of the Configurable Attributes that were found for this template.</param>
    ''' <returns>A collection of all of the Analog Field Attributes using the aaFieldAttributeAnalog class.</returns>
    ''' <remarks></remarks>
    Private Function GetFieldAttributesAnalog(gAttributes As aaGRAccess.IAttributes) As Collection
        Dim FieldAttributes As New Collection()

        ' A list of the Field Attributes are stored in an XML fragment in the UserAttrData attribute
        Dim UserAttrData As XElement = XElement.Parse(gAttributes.Item("UserAttrData").value.GetString)

        Dim attrList = UserAttrData.<DiscreteAttr>.Attributes("Name")

        For Each attr In attrList
            Dim attrName = attr.Value

            ' Now, put all of the info together into one data set for this attribute
            Dim AnalogAttrData = New aaFieldAttributeAnalog(attrName,
                GetAttrString("Tagname", gAttributes),
                GetAttrString(attrName + ".Desc", gAttributes),
                GetAttrBoolean(attrName + ".Historized", gAttributes),
                GetAttrBoolean(attrName + ".LogDataChangeEvent", gAttributes),
                GetAttrBoolean(attrName + ".Alarmed", gAttributes),
                GetAttrString(attrName + ".EngUnits", gAttributes))

            ' Finally, add it to a (growing) collection of field attributes
            FieldAttributes.Add(AnalogAttrData)
        Next

        Return FieldAttributes
    End Function

    ''' <summary>
    ''' Gets all of the Discrete Field Attributes for a given template. 
    ''' </summary>
    ''' <param name="gAttributes">All of the Configurable Attributes that were found for this template.</param>
    ''' <returns>A collection of all of the Discrete Field Attributes using the aaFieldAttributeDiscrete class.</returns>
    ''' <remarks></remarks>
    Private Function GetFieldAttributesDiscrete(gAttributes As aaGRAccess.IAttributes) As Collection
        Dim FieldAttributes As New Collection()

        ' A list of the Field Attributes are stored in an XML fragment in the UserAttrData attribute
        Dim UserAttrData As XElement = XElement.Parse(gAttributes.Item("UserAttrData").value.GetString)

        Dim attrList = UserAttrData.<AnalogAttr>.Attributes("Name")

        For Each attr In attrList
            Dim attrName = attr.Value

            ' Now, put all of the info together into one data set for this attribute
            Dim DiscreteAttrData = New aaFieldAttributeDiscrete(attrName,
                GetAttrString("Tagname", gAttributes),
                GetAttrString(attrName + ".Desc", gAttributes),
                GetAttrBoolean(attrName + ".Historized", gAttributes),
                GetAttrBoolean(attrName + ".LogDataChangeEvent", gAttributes),
                GetAttrBoolean(attrName + ".Alarmed", gAttributes),
                GetAttrString(attrName + ".EngUnits", gAttributes))

            ' Finally, add it to a (growing) collection of field attributes
            FieldAttributes.Add(DiscreteAttrData)
        Next

        Return FieldAttributes
    End Function

    Public Function getTemplateAttributes(templatename As String) As List(Of String)
        Dim templateList(0) As String
        Dim gTemplates As aaGRAccessApp.IgObjects
        Dim gTemplate As aaGRAccessApp.ITemplate
        Dim gAttributes As aaGRAccess.IAttributes
        Dim attrList As New List(Of String)()

        Try
            If loggedIn Then
                ' Convert the individual template name to an array. 
                templateList(0) = templatename

                ' Query the galaxy for this template's data
                gTemplates = myGalaxy.QueryObjectsByName(aaGRAccessApp.EgObjectIsTemplateOrInstance.gObjectIsTemplate, templateList)
                resultStatus = myGalaxy.CommandResult.Successful

                If resultStatus AndAlso gTemplates IsNot Nothing AndAlso gTemplates.count > 0 Then
                    gTemplate = CType(gTemplates(1), aaGRAccessApp.ITemplate)

                    ' Get all of the Configurable Attributes
                    gAttributes = gTemplate.Attributes

                    ' Iterate correctly over attributes using index
                    Dim prefixes As String() = {"Scr_" & "Src_"}
                    For i As Integer = 1 To gAttributes.count
                        Dim attr = gAttributes(i)
                        If Not prefixes.Any(Function(p) attr.Name.StartsWith(p)) Then
                            attrList.Add(attr.Name)
                        End If
                    Next

                    Return attrList
                Else
                    attrList.Add("BadTemplate")
                    Return attrList
                End If
            Else
                attrList.Add("NotLoggedIn")
                Return attrList
            End If
        Catch ex As Exception
            MessageBox.Show("Error Ocurred: " & ex.Message & vbCrLf & ex.StackTrace)
            attrList.Add("Error")
            Return attrList
        End Try
    End Function

    Public Sub AddNewTemAttr(ByVal newTemplate As aaGRAccessApp.ITemplate,
                             ByVal AttrName As String(), ByVal Valor As String(),
                             ByVal DiscFANames As List(Of String),
                             ByVal AnalogFANames As List(Of String),
                             ByVal DiscFADesc As List(Of String),
                             ByVal AnalogFADesc As List(Of String),
                             ByVal AnalogFAEngUnits As List(Of String),
                             ByVal DiscFAAlarmed As List(Of Boolean),
                             ByVal DiscFAHistorized As List(Of Boolean),
                             ByVal AnalogFAHistorized As List(Of Boolean),
                             ByVal DiscFAEvents As List(Of Boolean),
                             ByVal AnalogFAEvents As List(Of Boolean),
                             ByVal DiscFALocks As List(Of List(Of Boolean)),
                             ByVal AnalogFAScaled As List(Of Boolean),
                             ByVal AnalogFALocks As List(Of List(Of Boolean)))

        ' Check-Out para editar
        newTemplate.CheckOut()

        ' El orden de escritura es importante si intentas crear los UDA despues de los FA dara fallo, desconozco la razon pero al seguir el orden no da problemas.
        createNewUDA(newTemplate, AttrName, Valor)

        Dim UDOAttributes As IAttributes = newTemplate.Attributes
        Dim UDOUserAttrDataAttribute As IAttribute = UDOAttributes("UserAttrData")
        Dim MxVal As New MxValueClass()

        Dim xmlString As String = "<AttrXML>"

        For Each name In DiscFANames
            xmlString &= "<DiscreteAttr Name=""" & name & """ />"
        Next

        For Each name In AnalogFANames
            xmlString &= "<AnalogAttr Name=""" & name & """ />"
        Next

        xmlString &= "</AttrXML>"

        MxVal.PutString(xmlString)
        UDOUserAttrDataAttribute.SetValue(MxVal)

        newTemplate.Save()

        Dim I As Integer = 0

        If DiscFANames.Count <> 0 And DiscFANames(0) IsNot String.Empty Then
            For Each name In DiscFANames
                createNewDiscreteFA(newTemplate, name, "---", DiscFADesc(I), DiscFAAlarmed(I), DiscFAHistorized(I), DiscFAEvents(I), DiscFALocks(I))
                I = I + 1
            Next
        End If

        I = 0

        If AnalogFANames.Count <> 0 And AnalogFANames(0) IsNot String.Empty Then
            For Each name In AnalogFANames
                createNewAnalogFA(newTemplate, name, "---", AnalogFADesc(I), AnalogFAEngUnits(I), AnalogFAHistorized(I), AnalogFAEvents(I), AnalogFAScaled(I), AnalogFALocks(I))
                I = I + 1
            Next
        End If

        ' ---- Guardar y Check-In ----
        newTemplate.Save()
        newTemplate.CheckIn("Plantilla y atributos creados correctamente.")
    End Sub

    Private Sub createNewDiscreteFA(ByVal NewTemplate As aaGRAccessApp.ITemplate,
                                    ByVal FAName As String,
                                    ByVal FAValue As String,
                                    ByVal FADesc As String,
                                    ByVal Alarmed As Boolean,
                                    ByVal Historized As Boolean,
                                    ByVal GenerateEvent As Boolean,
                                    ByVal Locked As List(Of Boolean))

        ' Configurar los atributos como de costumbre
        Dim DiscreteFieldAttribute As IAttribute
        Dim MxVal As New MxValueClass()
        Dim UDOAttributes As IAttributes = NewTemplate.Attributes

        Dim UDOAttributesList As String() = {".Input.InputSource", ".Desc", ".Alarmed", ".Historized", ".LogDataChangeEvent"}

        Dim i As Integer = 0

        For Each Name In UDOAttributesList
            DiscreteFieldAttribute = UDOAttributes(FAName & Name)
            Select Case i
                Case 0
                    Dim MXRef As IMxReference
                    MXRef = DiscreteFieldAttribute.value.GetMxReference()
                    MXRef.FullReferenceString = FAValue
                    MxVal.PutMxReference(MXRef)

                Case 1
                    MxVal.PutInternationalString(1033, FADesc)

                Case 2
                    MxVal.PutBoolean(Alarmed)

                Case 3
                    MxVal.PutBoolean(Historized)

                Case 4
                    MxVal.PutBoolean(GenerateEvent)
            End Select
            DiscreteFieldAttribute.SetValue(MxVal)
            If Locked(i) Then
                Try
                    DiscreteFieldAttribute.SetLocked(MxPropertyLockedEnum.MxPropertyLockedEnumEND)
                Catch Ex As Exception
                    frmMain.LogBox.Items.Add("Error general: " & Ex.Message)
                End Try
            End If
            i = i + 1
        Next
    End Sub

    Private Sub createNewAnalogFA(ByVal NewTemplate As aaGRAccessApp.ITemplate,
                                  ByVal FAName As String,
                                  ByVal FAValue As String,
                                  ByVal FADesc As String,
                                  ByVal FAEngUnits As String,
                                  ByVal Historized As Boolean,
                                  ByVal GenerateEvent As Boolean,
                                  ByVal Scaled As Boolean,
                                  ByVal Locked As List(Of Boolean))

        ' Configurar los atributos como de costumbre
        Dim AnalogFieldAttribute As IAttribute
        Dim MxVal As New MxValueClass()
        Dim UDOAttributes As IAttributes = NewTemplate.Attributes

        Dim UDOAttributesList As String() = {".Input.InputSource", ".Desc", ".EngUnits", ".Historized", ".LogDataChangeEvent", ".Scaled"}

        Dim i As Integer = 0

        For Each Name In UDOAttributesList
            AnalogFieldAttribute = UDOAttributes(FAName & Name)
            Select Case i
                Case 0
                    Dim MXRef As IMxReference
                    MXRef = AnalogFieldAttribute.value.GetMxReference()
                    MXRef.FullReferenceString = FAValue
                    MxVal.PutMxReference(MXRef)

                Case 1
                    MxVal.PutInternationalString(1033, FADesc)

                Case 2
                    MxVal.PutString(FAEngUnits)

                Case 3
                    MxVal.PutBoolean(Historized)

                Case 4
                    MxVal.PutBoolean(GenerateEvent)

                Case 5
                    MxVal.PutBoolean(Scaled)

            End Select
            AnalogFieldAttribute.SetValue(MxVal)
            If Locked(i) Then
                Try
                    AnalogFieldAttribute.SetLocked(MxPropertyLockedEnum.MxPropertyLockedEnumEND)
                Catch Ex As Exception
                    frmMain.LogBox.Items.Add("Error general: " & Ex.Message)
                End Try
            End If
            i = i + 1
        Next
    End Sub

    Private Sub createNewUDA(ByVal template As aaGRAccessApp.ITemplate, UDAName As String(), UDAValue As String())
        Try
            Dim Index As Integer = 0
            For Each UDA In UDAName
                ' Añadir el nuevo UDA
                template.AddUDA(
                UDAName(Index),
                MxDataType.MxString,
                MxAttributeCategory.MxCategoryWriteable_USC_Lockable,
                MxSecurityClassification.MxSecurityOperate,
                False,
                0)

                template.Save()

                ' Configurar el valor del nuevo atributo
                Dim newAttribute As aaGRAccessApp.IAttribute = template.Attributes(UDAName(Index))

                If newAttribute IsNot Nothing Then
                    Dim attrValue As New aaGRAccessApp.MxValueClass()
                    attrValue.PutString(UDAValue(Index))
                    newAttribute.SetValue(attrValue)
                Else
                    frmMain.LogBox.Items.Add("No se encontró el atributo recién creado: " & newAttribute.ToString)
                End If

                ' Guardar los cambios en la plantilla
                template.Save()

                Index += 1
            Next
            frmMain.LogBox.Items.Add($"{Index} UDA(s) añadido(s) y configurado(s) correctamente, en la nueva plantilla " & template.ToString)
        Catch ex As Exception
            frmMain.LogBox.Items.Add("Error: " & ex.Message)
        End Try
    End Sub


    ' ------- Data Conversion ----------
    Private Sub ToMxValue(Atribute As String, gAttributes As aaGRAccess.IAttributes, Instance As aaGRAccessApp.IInstance, COMAtribute As String)
        Try
            Dim MXVal_ As aaGRAccess.MxValue = New aaGRAccess.MxValue()
            Dim Attr As aaGRAccess.IAttribute = gAttributes.Item(COMAtribute)

            If gAttributes.Item(COMAtribute) Is Nothing Then
                frmMain.LogBox.Items.Add("El atributo " & COMAtribute & " no existe.")
                Return
            End If
            MXVal_.PutString(Atribute)

            Attr.SetValue(MXVal_)
        Catch ex As Exception
            frmMain.LogBox.Items.Add(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Since the attribute may not exist, need to provide a safe, concise way to get it without crashing the program.
    ''' </summary>
    ''' <param name="AttributeName">The Attribute Name that exists in the template.</param>
    ''' <param name="gAttributes">All of the Configurable Attributes that were found for this template.</param>
    ''' <returns>Either the value of the Attribute or a boolean false.</returns>
    ''' <remarks></remarks>
    Private Function GetAttrBoolean(ByVal AttributeName As String, gAttributes As aaGRAccess.IAttributes) As Boolean
        ' Since the attribute may not exist, need to provide a safe, concise way to get it without crashing the program
        Try
            Return gAttributes.Item(AttributeName).value.GetBoolean()
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Since the attribute may not exist, need to provide a safe, concise way to get it without crashing the program.
    ''' </summary>
    ''' <param name="AttributeName">The Attribute Name that exists in the template.</param>
    ''' <param name="gAttributes">All of the Configurable Attributes that were found for this template.</param>
    ''' <returns>Either the value of the Attribute or a blank string.</returns>
    ''' <remarks></remarks>
    Private Function GetAttrString(ByVal AttributeName As String, gAttributes As aaGRAccess.IAttributes) As String
        ' 
        Try
            Return gAttributes.Item(AttributeName).value.GetString()
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Since the attribute may not exist, need to provide a safe, concise way to get it without crashing the program.
    ''' </summary>
    ''' <param name="AttributeName">The Attribute Name that exists in the template.</param>
    ''' <param name="gAttributes">All of the Configurable Attributes that were found for this template.</param>
    ''' <returns>Either the value of the Attribute or a zero.</returns>
    ''' <remarks></remarks>
    Private Function GetAttrInteger(ByVal AttributeName As String, gAttributes As aaGRAccess.IAttributes) As Integer
        ' Since the attribute may not exist, need to provide a safe, concise way to get it without crashing the program
        Try
            Return gAttributes.Item(AttributeName).value.GetInteger()
        Catch ex As Exception
            Return 0
        End Try
    End Function
End Class
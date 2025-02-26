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

Imports System.Xml.Serialization

''' <summary>
''' Stores ArchestrA Galaxy template data in an XML serializable format.
''' </summary>
Public Module aaTemplateData

    ''' <summary>
    ''' The current XML schema version. This is useful in exports to determine 
    ''' if data may be missing from an older version export.
    ''' </summary>
    ''' <history>
    ''' 1.0 - Scripts only
    ''' 1.1 - Scripts and Attributes to two separate directories; added Discrete Field Attributes
    ''' 1.2 - Added version numbering to schema
    '''     - Added Analog Field Attributes
    '''     - Added script Declarations and Aliases
    '''     - Added template name and template revision number to each top level XML
    ''' </history>
    Public SchemaVersion As Double = 1.2

    ''' <summary>
    ''' The template itself. Contains scripts, field attributes, and user defined attributes.
    ''' </summary>
    <Serializable()>
    Public Class aaTemplate
        <XmlAttribute("name")> Public Name As String
        <XmlAttribute("revision")> Public Revision As Integer
        <XmlAttribute("XmlVersion")> Public XmlVersion As Double
        Public FieldAttributesDiscrete As New Collection()
        Public FieldAttributesAnalog As New Collection()

        Public Sub New()

        End Sub

        Public Sub New(ByVal Name As String, ByVal Revision As Integer, ByVal FieldAttributesDiscrete As Collection, ByVal FieldAttributesAnalog As Collection)
            Me.Name = Name
            Me.Revision = Revision
            Me.XmlVersion = SchemaVersion
            Me.FieldAttributesDiscrete = FieldAttributesDiscrete
            Me.FieldAttributesAnalog = FieldAttributesAnalog
        End Sub

        ' Método para agregar los atributos de otra instancia de aaTemplate
        Public Sub AddAttributes(ByVal otherTemplate As aaTemplate)
            For Each item In otherTemplate.FieldAttributesDiscrete
                FieldAttributesDiscrete.Add(item)
            Next
            For Each item In otherTemplate.FieldAttributesAnalog
                FieldAttributesAnalog.Add(item)
            Next
        End Sub

    End Class


    ''' <summary>
    ''' Discrete Field Attribute data structure.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class aaFieldAttributeDiscrete
        <XmlAttribute("name")> Public Name As String
        <XmlAttribute("TemplateName")> Public TemplateName As String
        Public Description As String
        Public Historized As String
        Public Events As String
        Public Alarm As Boolean

        Public Sub New()

        End Sub

        Public Sub New(ByVal Name As String, ByVal TemplateName As String,
                       ByVal Description As String, ByVal Historized As Boolean, ByVal Events As Boolean, ByVal Alarm As Boolean)

            Me.Name = Name
            Me.TemplateName = TemplateName
            Me.Description = Description
            Me.Historized = Historized
            Me.Events = Events
            Me.Alarm = Alarm

        End Sub

    End Class

    ''' <summary>
    ''' Analog Field Attribute data structure.
    ''' </summary>
    <Serializable()>
    Public Class aaFieldAttributeAnalog
        <XmlAttribute("name")> Public Name As String
        <XmlAttribute("TemplateName")> Public TemplateName As String
        Public Description As String
        Public Historized As String
        Public Events As String
        Public Alarm As Boolean

        Public Sub New()

        End Sub

        Public Sub New(ByVal Name As String, ByVal TemplateName As String,
                       ByVal Description As String, ByVal Historized As Boolean, ByVal Events As Boolean, ByVal Alarm As Boolean)

            Me.Name = Name
            Me.TemplateName = TemplateName
            Me.Description = Description
            Me.Historized = Historized
            Me.Events = Events
            Me.Alarm = Alarm

        End Sub

    End Class

    <XmlRoot("FieldAttributes")>
    Public Class FieldAttributesContainer
        <XmlArray("DiscreteAttributes")>
        <XmlArrayItem("Attribute")>
        Public Property DiscreteAttributes As List(Of aaFieldAttributeDiscrete)

        <XmlArray("AnalogAttributes")>
        <XmlArrayItem("Attribute")>
        Public Property AnalogAttributes As List(Of aaFieldAttributeAnalog)

        Public Sub New()
            DiscreteAttributes = New List(Of aaFieldAttributeDiscrete)()
            AnalogAttributes = New List(Of aaFieldAttributeAnalog)()
        End Sub
    End Class
End Module
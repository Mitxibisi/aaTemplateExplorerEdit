Imports System
Imports System.IO
Imports OfficeOpenXml

Public Class aaExcelData

    <Serializable()>
    Public Class aaInstanceData
        Public InstanceName As String
        Public InstanceTemplate As String
        Public InstanceAloneAttr As New List(Of String)
        Public InstanceArrayAttr As New List(Of List(Of String))

        Public Sub New()

        End Sub

        Public Sub New(ByVal InstanceName As String, ByVal InstanceTemplate As String, InstanceArrayAttr As List(Of List(Of String)), InstanceAloneAttr As List(Of String))

            Me.InstanceName = InstanceName
            Me.InstanceTemplate = InstanceTemplate
            Me.InstanceArrayAttr = InstanceArrayAttr ' Convertir array en lista de Strings
            Me.InstanceAloneAttr = InstanceAloneAttr

        End Sub
    End Class

    Public Function CargarDatosMapeado(FilPath As String, InstanceNameColumnIndex As Integer, InstanceTemplateColumnIndex As Integer, EditableAloneElementsIndex As List(Of Integer), EditableArrayElementsIndex As List(Of Integer))
        Dim filePath As String
        Dim package As ExcelPackage
        Dim worksheet As ExcelWorksheet
        Dim InstanceData As New List(Of aaInstanceData)
        Dim InstancesNames As New List(Of String)()
        Dim NewInstanceData As New aaInstanceData

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial
        filePath = FilPath ' Archivo con macros
        package = New ExcelPackage(New FileInfo(filePath))
        worksheet = package.Workbook.Worksheets(0)
        InstancesNames = ObtenerDatosInstancesName(InstanceNameColumnIndex, worksheet)

        For Each name In InstancesNames
            Dim strname As String = name.ToString
            Dim Template = ObtenerDatosInstance(InstanceNameColumnIndex, worksheet, strname, InstanceTemplateColumnIndex)

            Dim NewArrayElementList As New List(Of List(Of String))

            For Each column As Integer In EditableArrayElementsIndex
                Dim attrList = ObtenerDatosColumna(InstanceNameColumnIndex, worksheet, strname, column)

                NewArrayElementList.Add(attrList)
            Next

            Dim NewAloneElementList As New List(Of String)

            For Each column As Integer In EditableAloneElementsIndex
                Dim attrList = ObtenerDatosInstance(InstanceNameColumnIndex, worksheet, strname, column)

                NewAloneElementList.Add(attrList)
            Next

            If Template.StartsWith("$") Then
                NewInstanceData = New aaInstanceData(strname,
                                             Template,
                                             NewArrayElementList,
                                             NewAloneElementList)
                InstanceData.Add(NewInstanceData)
            End If
        Next

        Return InstanceData
    End Function

    Private Function ObtenerDatosInstancesName(columnIndex As Integer, worksheet As ExcelWorksheet)
        Dim value As Object
        Dim InstancesNames As New List(Of String)() ' Cambio de String() a String

        For row As Integer = 1 To worksheet.Dimension.End.Row
            value = worksheet.Cells(row, columnIndex).Value
            If value IsNot Nothing Then
                ' Eliminar espacios vacíos
                Dim trimmedValue As String = value.ToString().Trim()
                ' Agregar solo si no está vacío
                If Not String.IsNullOrEmpty(trimmedValue) Then
                    instancesNames.Add(trimmedValue)
                End If
            End If
        Next

        instancesNames = instancesNames.Distinct().ToList

        Return instancesNames
    End Function

    Private Function ObtenerDatosInstance(columnIndex As Integer, worksheet As ExcelWorksheet, instanceFilter As String, templatecolumnIndex As Integer) As String
        Dim value As Object
        Dim filteredValues As String = String.Empty ' Inicializamos la variable como una cadena vacía
        Dim columnValue As Object = "Valor Nulo"

        For row As Integer = 1 To worksheet.Dimension.End.Row
            value = worksheet.Cells(row, columnIndex).Value

            If value IsNot Nothing Then
                Dim trimmedValue As String = value.ToString().Trim()

                ' Verificar si la celda contiene el filtro
                If Not String.IsNullOrEmpty(trimmedValue) AndAlso trimmedValue = instanceFilter Then
                    If templatecolumnIndex <> 10000 Then
                        columnValue = worksheet.Cells(row, templatecolumnIndex).Value
                    End If

                    If columnValue IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(columnValue.ToString()) Then
                        filteredValues = columnValue.ToString().Trim() ' Asignamos el valor de la columna
                        Return filteredValues ' Salimos de la función y devolvemos el valor encontrado
                    End If
                End If
            End If
        Next

        Return filteredValues ' Retorna una cadena vacía si no encuentra el filtro
    End Function

    Private Function ObtenerDatosColumna(columnIndex As Integer, worksheet As ExcelWorksheet, instanceFilter As String, mapcolumnIndex As Integer) As List(Of String)
        Dim value As Object
        Dim filteredValues As New List(Of String)() ' Inicializamos la lista
        Dim columnValue As Object = "Valor Nulo"

        For row As Integer = 1 To worksheet.Dimension.End.Row
            value = worksheet.Cells(row, columnIndex).Value

            If value IsNot Nothing Then
                Dim trimmedValue As String = value.ToString().Trim()

                ' Verificar si la celda contiene el filtro
                If Not String.IsNullOrEmpty(trimmedValue) AndAlso trimmedValue = instanceFilter Then
                    ' Obtener el valor de la columna de la misma fila
                    If mapcolumnIndex <> 10000 Then
                        columnValue = worksheet.Cells(row, mapcolumnIndex).Value
                    End If

                    If columnValue IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(columnValue.ToString()) Then
                        filteredValues.Add(columnValue.ToString().Trim()) ' Añadir el valor a la lista
                    End If
                End If
            End If
        Next

        Return filteredValues
    End Function
End Class
Imports System
Imports System.IO
Imports OfficeOpenXml

Public Class aaExcelData

    <Serializable()>
    Public Class aaInstanceData
        Public InstanceName As String
        Public InstanceTemplate As String
        Public InstanceMap As New List(Of String) ' Ahora es una lista de Strings

        Public Sub New()

        End Sub

        Public Sub New(ByVal InstanceName As String, ByVal InstanceTemplate As String, InstanceMap As List(Of String))

            Me.InstanceName = InstanceName
            Me.InstanceTemplate = InstanceTemplate
            Me.InstanceMap = New List(Of String)(InstanceMap) ' Convertir array en lista de Strings

        End Sub
    End Class

    Public Function CargarDatosMapeado(FilPath As String, InstanceNameColumnIndex As Integer, InstanceTemplateColumnIndex As Integer, InstanceMapColumnIndex As Integer)
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
            Dim Template = ObtenerDatosInstanceTemplate(InstanceNameColumnIndex, worksheet, strname, InstanceTemplateColumnIndex)
            Dim Map = ObtenerDatosColumna(InstanceNameColumnIndex, worksheet, strname, InstanceMapColumnIndex)

            If Template.StartsWith("$") Then
                NewInstanceData = New aaInstanceData(strname,
                                                 Template,
                                                 Map)
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

    Private Function ObtenerDatosInstanceTemplate(columnIndex As Integer, worksheet As ExcelWorksheet, instanceFilter As String, templatecolumnIndex As Integer) As String
        Dim value As Object
        Dim filteredValues As String = String.Empty ' Inicializamos la variable como una cadena vacía

        For row As Integer = 1 To worksheet.Dimension.End.Row
            value = worksheet.Cells(row, columnIndex).Value

            If value IsNot Nothing Then
                Dim trimmedValue As String = value.ToString().Trim()

                ' Verificar si la celda contiene el filtro
                If Not String.IsNullOrEmpty(trimmedValue) AndAlso trimmedValue.Contains(instanceFilter) Then
                    Dim columnValue As Object = worksheet.Cells(row, templatecolumnIndex).Value

                    If columnValue IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(columnValue.ToString()) Then
                        filteredValues = columnValue.ToString().Trim() ' Asignamos el valor de la columna 7
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

        For row As Integer = 1 To worksheet.Dimension.End.Row
            value = worksheet.Cells(row, columnIndex).Value

            If value IsNot Nothing Then
                Dim trimmedValue As String = value.ToString().Trim()

                ' Verificar si la celda contiene el filtro
                If Not String.IsNullOrEmpty(trimmedValue) AndAlso trimmedValue.Contains(instanceFilter) Then
                    ' Obtener el valor de la columna 7 de la misma fila
                    Dim column8Value As Object = worksheet.Cells(row, mapcolumnIndex).Value

                    If column8Value IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(column8Value.ToString()) Then
                        filteredValues.Add(column8Value.ToString().Trim()) ' Añadir el valor a la lista
                    End If
                End If
            End If
        Next

        Return filteredValues
    End Function
End Class
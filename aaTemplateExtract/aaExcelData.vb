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

    Public Function CargarDatosMapeado(FilPath As String, InstanceNameColumnIndex As Integer, InstanceTemplateColumnIndex As Integer, EditableAloneElementsIndex As List(Of Integer), EditableArrayElementsIndex As List(Of Integer), csvIndex As String, csv1index As String, generateCSV As Boolean, generateDoubleCSV As Boolean, DoubleText1 As String, DoubleText2 As String)
        Dim filePath As String
        Dim package As ExcelPackage
        Dim worksheet As ExcelWorksheet
        Dim InstanceData As New List(Of aaInstanceData)
        Dim InstancesNames As New List(Of String)()
        Dim NewInstanceData As New aaInstanceData
        Dim CSV As New List(Of String)()

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

            If generateCSV Then
                Dim tags = ObtenerDatosColumna(InstanceNameColumnIndex, worksheet, strname, csvIndex)
                Dim direcciones = ObtenerDatosColumna(InstanceNameColumnIndex, worksheet, strname, csv1index)

                For I As Integer = 1 To tags.Count - 1
                    Dim original As String = direcciones(I)

                    ' Aplicar las sustituciones como en la fórmula de Excel
                    original = original.Replace(".DBX", ",X") _
                                   .Replace(".DBDINT", ",DINT") _
                                   .Replace(".DBW", ",INT") _
                                   .Replace(".DBB", ",BYTE") _
                                   .Replace(".DBD", ",REAL")

                    CSV.Add("""" & tags(I) & """" & "," & """" & original & """")
                Next
            End If

            If Template.StartsWith("$") Then
                NewInstanceData = New aaInstanceData(strname,
                                             Template,
                                             NewArrayElementList,
                                             NewAloneElementList)
                InstanceData.Add(NewInstanceData)
            End If
        Next

        If generateCSV Then
            If generateDoubleCSV Then
                If generateCSV Then
                    ' Obtener la ruta del escritorio del usuario actual
                    Dim desktopPath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

                    ' Inicializar listas para cada tipo de DB
                    Dim db100List As New List(Of String)()
                    Dim db101List As New List(Of String)()

                    ' Clasificar las líneas según el tipo de DB
                    For Each linea As String In CSV
                        If linea.Contains(DoubleText1) Then
                            db100List.Add(linea)
                        ElseIf linea.Contains(DoubleText2) Then
                            db101List.Add(linea)
                        End If
                    Next

                    ' Guardar DB100 en su archivo
                    Dim rutaCSV_DB100 As String = Path.Combine(desktopPath, "resultado_" & DoubleText1 & ".csv")
                    Using writer As New StreamWriter(rutaCSV_DB100, False, System.Text.Encoding.UTF8)
                        For Each linea As String In db100List
                            writer.WriteLine(linea)
                        Next
                    End Using

                    ' Guardar DB101 en su archivo
                    Dim rutaCSV_DB101 As String = Path.Combine(desktopPath, "resultado_" & DoubleText2 & ".csv")
                    Using writer As New StreamWriter(rutaCSV_DB101, False, System.Text.Encoding.UTF8)
                        For Each linea As String In db101List
                            writer.WriteLine(linea)
                        Next
                    End Using

                    ' Mensaje de confirmación
                    MessageBox.Show("CSV generado correctamente en: " & vbCrLf &
                    rutaCSV_DB100 & vbCrLf & rutaCSV_DB101)
                End If

            Else
                ' Obtener la ruta del escritorio del usuario actual
                Dim desktopPath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                Dim rutaCSV As String = Path.Combine(desktopPath, "resultado.csv")

                ' Guardar la lista CSV en el archivo
                Using writer As New StreamWriter(rutaCSV, False, System.Text.Encoding.UTF8)
                    For Each linea As String In CSV
                        writer.WriteLine(linea)
                    Next
                End Using

                ' Mensaje de confirmación
                MessageBox.Show("CSV generado correctamente en: " & rutaCSV)
            End If
        End If

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
                    InstancesNames.Add(trimmedValue)
                End If
            End If
        Next

        InstancesNames = InstancesNames.Distinct().ToList

        Return InstancesNames
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
'--------------------------------------------------------------------- 
'THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY 
'KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
'IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A 
'PARTICULAR PURPOSE. 
'---------------------------------------------------------------------


Imports System
Imports System.Data
Imports DataGridCustomColumns

Public Class CustomDataGrid

    Private dataSource As DataSet

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        Me.Close()
    End Sub

    Private Sub CustomDataGrid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateDataTable(100)                                ' Create DataSet with some bogus data
        SetupTableStyles()                                  ' Setup custom table styles and bind to grid
    End Sub

    Private Sub SetupTableStyles()
        Dim alternatingColor As Color = SystemColors.ControlDark

        Dim vehicle As DataTable = Me.dataSource.Tables(1)

        ' ID Column 

        Dim dataGridCustomColumn0 As New DataGridCustomTextBoxColumn

        With dataGridCustomColumn0
            .Owner = Me.DataGrid1
            .Format = "0##"
            .FormatInfo = Nothing
            .HeaderText = vehicle.Columns(0).ColumnName
            .MappingName = vehicle.Columns(0).ColumnName
            .Width = DataGrid1.Width * 10 / 100         ' 10% of the grid size
            .AlternatingBackColor = alternatingColor
            .ReadOnly = True
        End With

        DataGridTableStyle1.GridColumnStyles.Add(dataGridCustomColumn0)

        ' Make column

        Dim dataGridCustomColumn1 As New DataGridCustomTextBoxColumn()

        With dataGridCustomColumn1
            .Owner = Me.DataGrid1
            .HeaderText = vehicle.Columns(1).ColumnName
            .MappingName = vehicle.Columns(1).ColumnName
            .NullText = "-Probably Ford-"
            .Width = Me.DataGrid1.Width * 40 / 100 ' 40%
            .Alignment = HorizontalAlignment.Right
            .AlternatingBackColor = alternatingColor
        End With

        Me.DataGridTableStyle1.GridColumnStyles.Add(dataGridCustomColumn1)

        ' Mileage column

        Dim dataGridCustomColumn2 As New DataGridCustomUpDownColumn()

        With dataGridCustomColumn2
            .Owner = Me.DataGrid1
            .HeaderText = vehicle.Columns(2).ColumnName
            .MappingName = vehicle.Columns(2).ColumnName
            .NullText = "-Unknown-"
            .Width = Me.DataGrid1.Width * 20 / 100 ' 15%
            .Alignment = HorizontalAlignment.Left
            .AlternatingBackColor = alternatingColor
        End With

        Me.DataGridTableStyle1.GridColumnStyles.Add(dataGridCustomColumn2)

        ' Availability clumn 

        Dim dataGridCustomColumn3 As New DataGridCustomCheckBoxColumn()

        With dataGridCustomColumn3
            .Owner = Me.DataGrid1
            .HeaderText = vehicle.Columns(3).ColumnName
            .MappingName = vehicle.Columns(3).ColumnName
            .NullText = "-Unknown-"
            .Width = Me.DataGrid1.Width * 10 / 100 ' 10%
            .Alignment = HorizontalAlignment.Left
            .AlternatingBackColor = alternatingColor
        End With

        Me.DataGridTableStyle1.GridColumnStyles.Add(dataGridCustomColumn3)

        ' Fuel Level column

        Dim dataGridCustomColumn4 As New DataGridCustomComboBoxColumn()

        With dataGridCustomColumn4
            .Owner = Me.DataGrid1
            .HeaderText = vehicle.Columns(4).ColumnName
            .MappingName = vehicle.Columns(4).ColumnName
            .NullText = "-Unknown-"
            .Width = Me.DataGrid1.Width * 30 / 100 ' 30%
            .Alignment = HorizontalAlignment.Left
            .AlternatingBackColor = alternatingColor
        End With

        Me.DataGridTableStyle1.GridColumnStyles.Add(dataGridCustomColumn4)

        ' Last Used column

        Dim dataGridCustomColumn5 As New DataGridCustomDateTimePickerColumn()

        With dataGridCustomColumn5
            .Owner = Me.DataGrid1
            .HeaderText = vehicle.Columns(5).ColumnName
            .MappingName = vehicle.Columns(5).ColumnName
            .NullText = "-Unknown-"
            .Width = Me.DataGrid1.Width * 30 / 100 ' 30%
            .Alignment = HorizontalAlignment.Left
            .AlternatingBackColor = alternatingColor
        End With

        Me.DataGridTableStyle1.GridColumnStyles.Add(dataGridCustomColumn5)

        Me.DataGridTableStyle1.MappingName = vehicle.TableName                  ' Setup table mapping name

        Me.DataGrid1.DataSource = vehicle                                       ' Setup grid's data source

        Dim cb As ComboBox = CType(dataGridCustomColumn4.HostedControl, ComboBox)

        Dim fuel As DataTable = Me.dataSource.Tables(0)                         ' Set up data source

        cb.DataSource = fuel                                                    ' For combo box column
        cb.DisplayMember = fuel.Columns(0).ColumnName
        cb.ValueMember = fuel.Columns(0).ColumnName

        Me.DataGrid1.CurrentRowIndex = 50                                       ' Move to the middle of the table

    End Sub

    Private Sub CreateDataTable(ByVal records As Integer)
        Me.dataSource = New DataSet                             ' Create DataSet

        Dim fuel As New DataTable("Fuel")                       ' Create 'Items' table - that would be list of items to choose from for ComboBox column

        fuel.Columns.Add("Level", GetType(String))              ' Add 'Choice' column 

        fuel.Rows.Add("-Unknown-")                              ' And some rows - that would be user's choice
        fuel.Rows.Add("Empty")
        fuel.Rows.Add("Half")
        fuel.Rows.Add("Full")

        Me.dataSource.Tables.Add(fuel)                          ' Add 'Fuel' table to DataSet

        Dim vehicle As New DataTable("Vehicle")                 ' Create 'Vehicle' table - that's the one to show in the grid

        ' Add some columns to show in the grid

        vehicle.Columns.Add("ID", GetType(Int32))               ' Primary key
        vehicle.Columns.Add("Make", GetType(String))            ' Vehicle make
        vehicle.Columns.Add("Mileage", GetType(Int32))          ' Car's Mileage
        vehicle.Columns.Add("Availability", GetType(Boolean))   ' Availability
        vehicle.Columns.Add("Fuel Level", GetType(String))      ' Fuel Level
        vehicle.Columns.Add("Last Used", GetType(DateTime))     ' Last used date

        For i As Integer = 0 To records                         ' Add some rows
            Dim availible As Object

            Select Case i Mod 3
                Case 0
                    availible = False
                Case 1
                    availible = True
                Case Else
                    availible = DBNull.Value
            End Select

            vehicle.Rows.Add( _
                        i, _
                        String.Format("Make #{0}", i), _
                        i, _
                        availible, _
                        DBNull.Value, _
                        DateTime.Now)
        Next

        Me.dataSource.Tables.Add(vehicle)                     ' Add table to DataSet


    End Sub


End Class

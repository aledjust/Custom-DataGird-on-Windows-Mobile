using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DataGridCustomColumns;

namespace CustomDataGridCs
{
	public partial class CustomDataGrid : Form
	{
		private DataSet dataSource;

		public CustomDataGrid()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
            GetChecked();
		}

		private void CustomDataGrid_Load(object sender, EventArgs e)
		{
			CreateDataTable(100);      // Create DataSet with some bogus data
			SetupTableStyles();        // Setup custom table styles and bind to grid
		}


		private void SetupTableStyles()
		{
			Color alternatingColor = SystemColors.ControlDark;
			DataTable vehicle = dataSource.Tables[1];
 
			// ID Column 
			DataGridCustomTextBoxColumn dataGridCustomColumn0 = new DataGridCustomTextBoxColumn();
			dataGridCustomColumn0.Owner = this.dataGrid1;
			dataGridCustomColumn0.Format = "0##";
			dataGridCustomColumn0.FormatInfo = null;
			dataGridCustomColumn0.HeaderText = vehicle.Columns[0].ColumnName;
			dataGridCustomColumn0.MappingName = vehicle.Columns[0].ColumnName;
			dataGridCustomColumn0.Width = dataGrid1.Width * 10 / 100;				// 10% of grid size
			dataGridCustomColumn0.AlternatingBackColor = alternatingColor;
			dataGridCustomColumn0.ReadOnly = true;
			dataGridTableStyle1.GridColumnStyles.Add(dataGridCustomColumn0);

			// Make column
			DataGridCustomTextBoxColumn dataGridCustomColumn1 = new DataGridCustomTextBoxColumn();
			dataGridCustomColumn1.Owner = this.dataGrid1;
			dataGridCustomColumn1.HeaderText = vehicle.Columns[1].ColumnName;
			dataGridCustomColumn1.MappingName = vehicle.Columns[1].ColumnName;
			dataGridCustomColumn1.NullText = "-Probably Ford-";
			dataGridCustomColumn1.Width = dataGrid1.Width * 40 / 100;				// 40% of grid size
			dataGridCustomColumn1.Alignment = HorizontalAlignment.Right;
			dataGridCustomColumn1.AlternatingBackColor = alternatingColor;
			dataGridTableStyle1.GridColumnStyles.Add(dataGridCustomColumn1);

			// Mileage column
			DataGridCustomUpDownColumn dataGridCustomColumn2 = new DataGridCustomUpDownColumn();
			dataGridCustomColumn2.Owner = this.dataGrid1;
			dataGridCustomColumn2.HeaderText = vehicle.Columns[2].ColumnName;
			dataGridCustomColumn2.MappingName = vehicle.Columns[2].ColumnName;
			dataGridCustomColumn2.NullText = "-Unknown-";
			dataGridCustomColumn2.Width = dataGrid1.Width * 20 / 100;				// 20% of grid size
			dataGridCustomColumn2.Alignment = HorizontalAlignment.Left;
			dataGridCustomColumn2.AlternatingBackColor = alternatingColor;
			dataGridTableStyle1.GridColumnStyles.Add(dataGridCustomColumn2);

			// Availability column 
			DataGridCustomCheckBoxColumn dataGridCustomColumn3 = new DataGridCustomCheckBoxColumn();
			dataGridCustomColumn3.Owner = this.dataGrid1;
			dataGridCustomColumn3.HeaderText = vehicle.Columns[3].ColumnName;
			dataGridCustomColumn3.MappingName = vehicle.Columns[3].ColumnName;
			dataGridCustomColumn3.NullText = "-Unknown-";
			dataGridCustomColumn3.Width = dataGrid1.Width * 10 / 100;				// 10% of grid size
			dataGridCustomColumn3.Alignment = HorizontalAlignment.Left;
			dataGridCustomColumn3.AlternatingBackColor = alternatingColor;
			dataGridTableStyle1.GridColumnStyles.Add(dataGridCustomColumn3);

			// Fuel Level column
			DataGridCustomComboBoxColumn dataGridCustomColumn4 = new DataGridCustomComboBoxColumn();
			dataGridCustomColumn4.Owner = this.dataGrid1;
			dataGridCustomColumn4.HeaderText = vehicle.Columns[4].ColumnName;
			dataGridCustomColumn4.MappingName = vehicle.Columns[4].ColumnName;
			dataGridCustomColumn4.NullText = "-Unknown-";
			dataGridCustomColumn4.Width = dataGrid1.Width * 30 / 100;				// 30% of grid size
			dataGridCustomColumn4.Alignment = HorizontalAlignment.Left;
			dataGridCustomColumn4.AlternatingBackColor = alternatingColor;
			dataGridTableStyle1.GridColumnStyles.Add(dataGridCustomColumn4);

			// Last Used column
			DataGridCustomDateTimePickerColumn dataGridCustomColumn5 = new DataGridCustomDateTimePickerColumn();
			dataGridCustomColumn5.Owner = this.dataGrid1;
			dataGridCustomColumn5.HeaderText = vehicle.Columns[5].ColumnName;
			dataGridCustomColumn5.MappingName = vehicle.Columns[5].ColumnName;
			dataGridCustomColumn5.NullText = "-Unknown-";
			dataGridCustomColumn5.Width = dataGrid1.Width * 30 / 100;				// 30% of grid size
			dataGridCustomColumn5.Alignment = HorizontalAlignment.Left;
			dataGridCustomColumn5.AlternatingBackColor = alternatingColor;
			dataGridTableStyle1.GridColumnStyles.Add(dataGridCustomColumn5);

			// Grid, mapping

			dataGridTableStyle1.MappingName = vehicle.TableName;				// Setup table mapping name
			dataGrid1.DataSource = vehicle;										// Setup grid's data source

			ComboBox cb = (ComboBox)dataGridCustomColumn4.HostedControl;

			DataTable fuel = dataSource.Tables[0];								// Set up data source
			cb.DataSource = fuel;												// For combo box column
			cb.DisplayMember = fuel.Columns[0].ColumnName;
			cb.ValueMember = fuel.Columns[0].ColumnName;

			dataGrid1.CurrentRowIndex = 50;										// Move to the middle of the table
		}

		private void CreateDataTable(int records)
		{
			this.dataSource = new DataSet();									// Create DataSet

			DataTable fuel = new DataTable("Fuel");								// Create 'Items' table - that would be list of items to choose from for ComboBox column
			fuel.Columns.Add("Level", System.Type.GetType("System.String"));	// Add 'Choice' column 
			fuel.Rows.Add("-Unknown-");											// And some rows - that would be user's choice
			fuel.Rows.Add("Empty");
			fuel.Rows.Add("Half");
			fuel.Rows.Add("Full");

			dataSource.Tables.Add(fuel);										// Add 'Fuel' table to DataSet

			DataTable vehicle = new DataTable("Vehicle");						// Create 'Vehicle' table - that's the one to show in the grid

			// Add some columns to show in the grid

			vehicle.Columns.Add("ID", System.Type.GetType("System.Int32"));				// Primary key
			vehicle.Columns.Add("Make", System.Type.GetType("System.String"));			// Vehicle make
			vehicle.Columns.Add("Mileage", System.Type.GetType("System.Int32"));		// Car's Mileage
			vehicle.Columns.Add("Availability", System.Type.GetType("System.Boolean"));	// Availability
			vehicle.Columns.Add("Fuel Level", System.Type.GetType("System.String"));	// Fuel Level
			vehicle.Columns.Add("Last Used", System.Type.GetType("System.DateTime"));	// Last used date

			for (int i = 0; i < records; i++ )
			{
				Object available;
				switch (i % 3)
				{
					case 0: available = false; break;
					case 1: available = true; break;
					default: available = DBNull.Value; break;
				}
				vehicle.Rows.Add(i, String.Format("Make #{0}", i), i, available, DBNull.Value, DateTime.Now);
			}

			dataSource.Tables.Add(vehicle);
		}

        private void GetChecked()
        {
            DataTable dt1 = new DataTable("TABLEAPA");


        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Hides the ID column
            this.dataGridView1.Columns[0].Visible = false;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'employeeDb_2020_DataSet.Employees' table. You can move, or remove it, as needed.
            this.employeesTableAdapter.Fill(this.employeeDb_2020_DataSet.Employees);

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string msg = "Do you want to exit the application?";
            string caption = "Exit Application: Database interface";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Question;

            DialogResult result;
            result = MessageBox.Show(this, msg, caption, buttons, icon);

            if(result == DialogResult.Yes)
            {
                //MessageBox.Show("Goodbye.", "Database interface", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            this.employeesBindingSource.MovePrevious();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            this.employeesBindingSource.MoveNext();
        }

        private void buttonAddNew_Click(object sender, EventArgs e)
        {
            this.employeesBindingSource.AddNew();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = "Do you want to save | update the selected record?";
                string caption = "Database interface";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                MessageBoxIcon icon = MessageBoxIcon.Question;

                DialogResult result;

                result = MessageBox.Show(this, msg, caption, buttons, icon);

                if(result == DialogResult.Yes)
                {
                    // Save | Update
                    this.employeesBindingSource.EndEdit();
                    this.employeesTableAdapter.Update(this.employeeDb_2020_DataSet.Employees);

                    // Refresh
                    this.employeesTableAdapter.Fill(this.employeeDb_2020_DataSet.Employees);

                    MessageBox.Show("The record has been saved succesfully.", "Database interface", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Save | Update data failed: " + ex.Message.ToString(), "Database interface", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = "Warning: Record(s) will be permanently deleted.\nContinue?";
                string caption = "Database interface";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                MessageBoxIcon icon = MessageBoxIcon.Question;

                DialogResult result;

                result = MessageBox.Show(this, msg, caption, buttons, icon);

                if (result == DialogResult.Yes)
                {
                    // Delete the record
                    this.employeesBindingSource.RemoveCurrent();

                    // Save the changes
                    this.employeesBindingSource.EndEdit();
                    this.employeesTableAdapter.Update(this.employeeDb_2020_DataSet.Employees);

                    // Refresh data
                    this.employeesTableAdapter.Fill(this.employeeDb_2020_DataSet.Employees);

                    MessageBox.Show("The record has been deleted succesfully.", "Database interface", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Save | Update data failed: " + ex.Message.ToString(), "Database interface", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            buttonAddNew.PerformClick();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            buttonDelete.PerformClick();
        }

        private void toolStripButtonUpdateItem_Click(object sender, EventArgs e)
        {
            buttonSave.PerformClick();
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }
    }
}

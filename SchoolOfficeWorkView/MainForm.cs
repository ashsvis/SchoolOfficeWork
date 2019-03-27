using Model;
using System;
using System.Windows.Forms;
using ViewGenerator;

namespace SchoolOfficeWorkView
{
    public partial class MainForm : Form
    {
        SchoolOffice _school = new SchoolOffice();
        public MainForm()
        {
            InitializeComponent();
            GridPanelBuilder.Error += GridPanelBuilder_Error;
        }

        private void GridPanelBuilder_Error(string message, string caption)
        {
            MessageBox.Show(this, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void tsmiPartitions_Click(object sender, EventArgs e)
        {
            pnlContainer.Controls.Clear();
            pnlContainer.Controls.Add(GridPanelBuilder.BuildPropertyPanel(_school, new Partition(), _school.Partitions));
        }

        private void tsmiEmployees_Click(object sender, EventArgs e)
        {
            pnlContainer.Controls.Clear();
            pnlContainer.Controls.Add(GridPanelBuilder.BuildPropertyPanel(_school, new Employee(), _school.Employees));
        }

        private void tsmiApproiments_Click(object sender, EventArgs e)
        {
            pnlContainer.Controls.Clear();
            pnlContainer.Controls.Add(GridPanelBuilder.BuildPropertyPanel(_school, new Appointment(), _school.Appointments));
        }
    }
}

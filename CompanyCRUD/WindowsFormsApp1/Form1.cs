using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UI
{
    public partial class Form1 : Form
    {
        private Func<object, EmployeeEventHandler, bool> EmployeeDeleted;
        private Func<object, DepartmentEventHandler, List<Employee>> DepartmentSelected;
        private Func<object, EmployeeEventHandler, bool> EmployeeUpdated;
        private Func<object, EmployeeEventHandler, bool> EmployeeInserted;

        public Form1()
        {
            InitializeComponent();
            FillDepartmentsCompo();
            EmployeeDeleted += Services.DeleteEmployee;
            DepartmentSelected += Services.GetEmployeeByDepartment;
            EmployeeUpdated += Services.UpdateEmployee;
            EmployeeInserted += Services.InsertEmployee;
        }



        private void delBtn_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1 && EmployeeDeleted == null)
            {
                return;
            }

            bool isSucceeded = EmployeeDeleted(this, GetEmployeeEvenTHandler());

            listView1.SelectedItems[0].Remove();

            if (isSucceeded)
            {
                MessageBox.Show("Employee Deleted Succesfully");
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1 && EmployeeUpdated == null)
            {
                return;
            }

            bool isSucceeded = EmployeeUpdated(this, GetEmployeeEvenTHandler());

            if (isSucceeded)
            {
                UpdateEmployeeInListView();
                MessageBox.Show("Employee udated Succesfully");
            }
        }        

        private void insertBtn_Click(object sender, EventArgs e)
        {
            if(!CheckTextBoxNotEmpty() && EmployeeInserted == null)
            {
                return;
            }

            bool isSucceeded = EmployeeInserted(this, GetEmployeeEvenTHandler());

            if (isSucceeded)
            {
                DisplayEmployeeList();
                MessageBox.Show("Employee Added Succesfully");
            }
        }

        private void FillDepartmentsCompo()
        {
            deptComp.DisplayMember = "Name";
            deptComp.ValueMember = "ID";
            deptComp.DataSource = Services.GetDepartments();
        }

        private void deptComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayEmployeeList();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1)
            {
                return;
            }

            InsertListViewItemIntoTextBox();
        }

        private void InsertListViewItemIntoTextBox()
        {
            nameBox.Text = listView1.SelectedItems[0].SubItems[1].Text;
            BDdateTimePicker.Value = Convert.ToDateTime(listView1.SelectedItems[0].SubItems[2].Text);
            salaryBox.Text = listView1.SelectedItems[0].SubItems[3].Text;
        }

        private bool RetrieveEmployeeByDepatment(out List<Employee> empList)
        {
            empList = null;

            if (DepartmentSelected != null)
            {
                int id = Convert.ToInt32(deptComp.SelectedValue);
                empList = DepartmentSelected(this, new DepartmentEventHandler() { Id = id });
                return true;
            }

            return false;
        }

        private void FillEmployeeViewList(List<Employee> empList)
        {
            listView1.Items.Clear();

            foreach (Employee emp in empList)
            {
                CreateEmployeeListViewItem(emp);
            }
        }

        private void CreateEmployeeListViewItem(Employee emp)
        {
            ListViewItem item = new ListViewItem(emp.Id.ToString());
            item.SubItems.Add(emp.Name.ToString());
            item.SubItems.Add(emp.BirthDate.ToString());
            item.SubItems.Add(emp.Salary.ToString());

            listView1.Items.Add(item);
        }

        private void DisplayEmployeeList()
        {
            if(RetrieveEmployeeByDepatment(out List<Employee> empList))
            {
                FillEmployeeViewList(empList);
            }
        }

        private void UpdateEmployeeInListView()
        {
            listView1.SelectedItems[0].SubItems[1].Text = nameBox.Text;
            listView1.SelectedItems[0].SubItems[2].Text = BDdateTimePicker.Value.ToString();
            listView1.SelectedItems[0].SubItems[3].Text = salaryBox.Text;
        }

        private bool CheckTextBoxNotEmpty()
        {
            return nameBox.Text != "" && salaryBox.Text != "";
        }

        private EmployeeEventHandler GetEmployeeEvenTHandler()
        {
            int id = -1;
            try
            {
                id = Convert.ToInt32(listView1.SelectedItems[0].Text);

            }
            catch (System.ArgumentOutOfRangeException)
            {
                id = -1;
            }

            EmployeeEventHandler emp = new EmployeeEventHandler()
            {
                Id = id,
                Name = nameBox.Text,
                BirthDate = BDdateTimePicker.Value,
                Salary = Convert.ToDecimal(salaryBox.Text),
                Depno = Convert.ToInt32(deptComp.SelectedValue),
            };

            return emp;
        }
    }
}

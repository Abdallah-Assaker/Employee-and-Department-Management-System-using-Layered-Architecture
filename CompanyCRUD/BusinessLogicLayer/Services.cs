using DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public static class Services
    {
        public static List<Department> GetDepartments()
        {
            DataTable dt = DataBaseLayer.DB.GetDepartments();
            List<Department> list = new List<Department>();

            FillDepartmentListFromDataTable(dt, list);

            return list;
        }

        private static void FillDepartmentListFromDataTable(DataTable dt, List<Department> list)
        {
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new Department()
                {
                    Id = (int)dr["ID"],
                    Name = (string)dr["Name"],
                });
            }
        }

        public static List<Employee> GetEmployeeByDepartment(object obj, DepartmentEventHandler e)
        {
            DataTable dt = DataBaseLayer.DB.GetEmployeesByDepID(e.Id);
            List<Employee> list = new List<Employee>();

            FillEmployeeListFromDataTable(dt, list);

            return list;
        }

        private static void FillEmployeeListFromDataTable(DataTable dt, List<Employee> list)
        {
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new Employee()
                {
                    Id = (int)dr["ID"],
                    Name = (string)dr["Name"],
                    BirthDate = Convert.ToDateTime(dr["BirthDate"]),
                    Salary = Convert.ToDecimal(dr["Salary"]),
                });
            }
        }

        public static bool DeleteEmployee(object sender, EmployeeEventHandler e)
        {
            bool isDeleted = DB.DeleteEmpolyeeByID(e.Id);

            return isDeleted;
        }

        public static bool UpdateEmployee(object sender, EmployeeEventHandler e)
        {
            bool isUpdated = DB.UpdateEmplyee(e.Id, e.Name, e.BirthDate, e.Salary);

            return isUpdated;
        }

        public static bool InsertEmployee(object sender, EmployeeEventHandler e)
        {
            bool isAdded = DB.CreateNewEmployee(e.Depno, e.Name, e.BirthDate, e.Salary);
            return isAdded;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataBaseLayer
{
    public static class DB
    {
        static SqlConnection connection;
        static SqlCommand command;
        static SqlDataAdapter adapter;

        static DB()
        {
            connection = new SqlConnection("Data Source=DESKTOP-M0HL8AK;Initial Catalog=Company;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            command = new SqlCommand();
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
        }

 
        public static DataTable GetDepartments()
        {
            string cmdString = "SELECT ID, Name FROM Department";
            return ExecuteDisconnectedModeQuery(cmdString);
        }

        public static DataTable GetEmployees()
        {
            string cmdString = "SELECT ID, Name, BirthDate, Salary FROM HR.Employee";
            return ExecuteDisconnectedModeQuery(cmdString);
        }

        public static DataTable GetEmployeesByDepID(int id)
        {
            string cmdString = $"SELECT ID, Name, BirthDate, Salary FROM HR.Employee WHERE DeptID = {id}";
            return ExecuteDisconnectedModeQuery(cmdString);
        }

        private static DataTable ExecuteDisconnectedModeQuery(string cmdString)
        {
            command.CommandText = cmdString;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public static bool UpdateEmplyee(int id, string name, DateTime birthDate, decimal salary)
        {
            string cmdString = $"UPDATE HR.Employee SET Name = '{name}', BirthDate = '{birthDate.ToString("yyyy-MM-dd")}', Salary = {salary} WHERE ID = {id}";
            return ExecuteConnectedModeQuery(cmdString);
        }

        public static bool CreateNewEmployee(int depNom, string name, DateTime birthDate, decimal salary)
        {
            string cmdString = $"INSERT INTO HR.Employee (Name, BirthDate, Salary, DeptID) VALUES ('{name}', '{birthDate.ToString("yyyy-MM-dd")}', {salary}, {depNom})";
            return ExecuteConnectedModeQuery(cmdString);

        }
        public static bool DeleteEmpolyeeByID(int id)
        {
            string cmdString = $"DELETE FROM HR.Employee WHERE ID = {id}";
            return ExecuteConnectedModeQuery(cmdString);
        }

        private static bool ExecuteConnectedModeQuery(string cmdString)
        {
            connection.Open();
            command.CommandText = cmdString;
            int rowsAffectedCount = command.ExecuteNonQuery();
            connection.Close();

            if (rowsAffectedCount > 0)
            {
                return true;
            }
            return false;
        }
    }
}

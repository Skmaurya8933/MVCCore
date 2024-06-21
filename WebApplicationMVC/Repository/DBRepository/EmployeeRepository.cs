using System.Data.SqlClient;
using System.Xml.Linq;
using WebApplicationMVC.Models;
using WebApplicationMVC.Repository.IDBRepository;

namespace WebApplicationMVC.Repository.DBRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;
        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("EmployeeDB");
        }
        public BaseResponseModel<List<Employee>> GetAll()
        {
            BaseResponseModel<List<Employee>> baseResponseModel= new BaseResponseModel<List<Employee>>();   
            var employees = new List<Employee>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("SELECT * FROM Emp_tbl", connection);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employee
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Position = reader.GetString(reader.GetOrdinal("Position")),
                                Department = reader.GetString(reader.GetOrdinal("Department"))
                            });
                        }
                    }
                }
                baseResponseModel.Value= employees;
                baseResponseModel.Success=true;
                baseResponseModel.ResultMessage = "Success";
            }
            catch(Exception ex)
            {
                baseResponseModel.Value = employees;
                baseResponseModel.Success = false;
                baseResponseModel.ResultMessage = "Error";

            }                    
            return baseResponseModel;
        }

        public BaseResponseModel<Employee> GetById(int id)
        {
            BaseResponseModel<Employee> baseResponseModel= new BaseResponseModel<Employee>();   
            Employee employee = new Employee();
            try
            {              
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("SELECT * FROM Emp_tbl WHERE Id = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            employee.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            employee.Name = reader.GetString(reader.GetOrdinal("Name"));
                            employee.Position = reader.GetString(reader.GetOrdinal("Position"));
                            employee.Department = reader.GetString(reader.GetOrdinal("Department"));
                        }
                    }
                }
                baseResponseModel.Value = employee;
                baseResponseModel.Success = true;
                baseResponseModel.ResultMessage = "Success";
            }
            catch (Exception ex)
            {
                baseResponseModel.Value = employee;
                baseResponseModel.Success = false;
                baseResponseModel.ResultMessage = "Error";

            }          
            return baseResponseModel;
        }

        public BaseResponseModel AddEmployee(Employee employee)
        {
            int a = 0;
            BaseResponseModel<bool> baseResponseModel= new BaseResponseModel<bool>();   
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Emp_tbl (Name, Position,Department) VALUES (@Name, @Position,@Department)", conn);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Position", employee.Position);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    conn.Open();
                    a=cmd.ExecuteNonQuery();
                }
                if (a > 0)
                {
                    baseResponseModel.Success = true;
                    baseResponseModel.ResultMessage = "Employee added successfully!";
                }
                
            }
            catch (Exception ex)
            {
                baseResponseModel.Success = false;
                baseResponseModel.ResultMessage = "Error";
            }
            return baseResponseModel;


        }
        public BaseResponseModel UpdateEmployee(Employee employee)
        {
            int a = 0;
            BaseResponseModel<bool> baseResponseModel = new BaseResponseModel<bool>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Update Emp_tbl set Name=@Name ,Position=@Position,Department=@Department where Id=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", employee.Id);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Position", employee.Position);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    conn.Open();
                    a = cmd.ExecuteNonQuery();
                }
                if (a > 0)
                {
                    baseResponseModel.Success = true;
                    baseResponseModel.ResultMessage = "Success";
                }

            }
            catch (Exception ex)
            {
                baseResponseModel.Success = false;
                baseResponseModel.ResultMessage = "Error";
            }
            return baseResponseModel;
        }
        public BaseResponseModel DeleteEmployee(int id)
        {
            int a = 0;
            BaseResponseModel<bool> baseResponseModel = new BaseResponseModel<bool>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Delete from Emp_tbl where Id=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id",id);
                    conn.Open();
                    a = cmd.ExecuteNonQuery();
                }
                if (a > 0)              
                {
                    baseResponseModel.Success = true;
                    baseResponseModel.ResultMessage = "Success";
                }
            }
            catch (Exception ex)
            {
                baseResponseModel.Success = false;
                baseResponseModel.ResultMessage = "Error";
            }
            return baseResponseModel;
        }

    }
}

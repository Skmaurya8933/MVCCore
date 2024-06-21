using WebApplicationMVC.Models;

namespace WebApplicationMVC.Repository.IDBRepository
{
    public interface IEmployeeRepository
    {
        BaseResponseModel<List<Employee>> GetAll();
        BaseResponseModel<Employee> GetById(int id);
        BaseResponseModel AddEmployee(Employee employee);
        BaseResponseModel UpdateEmployee(Employee employee);
        BaseResponseModel DeleteEmployee(int id);
    }
}

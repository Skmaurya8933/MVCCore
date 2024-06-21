using Microsoft.AspNetCore.Mvc;
using WebApplicationMVC.Models;
using WebApplicationMVC.Repository.IDBRepository;

namespace WebApplicationMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var response = _employeeRepository.GetAll();
            if (response.Success)
            {
                return View(response.Value);
            }
            return View("Error", response.ResultMessage);

        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var response = _employeeRepository.GetById(id);
            if (response.Success)
            {
                return View(response.Value);
            }
            return View("Error", response.ResultMessage);
        }
        public IActionResult Add(int id)
        {
            ViewBag.PageName = id == 0 ? "Create Employee" : "Edit Employee";
            ViewBag.Action = id == 0 ? "Create" : "Edit";
            ViewBag.IsEdit =id == 0 ? false : true;
            if (id == 0)
            {
                return View();
            }
            else
            {
                var employee =_employeeRepository.GetById(id);
                if (employee.Success && employee.Value != null)
                {
                    return View(employee.Value);
                }
                return View("Error", employee.ResultMessage);
            }
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var response = _employeeRepository.AddEmployee(employee);
                if (response.Success)
                {
                    return RedirectToAction("Index");
                }
                return View("Error", response.ResultMessage);
            }
            else { return RedirectToAction("Add"); }
           
        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {   
            if (ModelState.IsValid)
            {
                var response = _employeeRepository.UpdateEmployee(employee);
                if (response.Success)
                {
                    return RedirectToAction("Index");
                }
                return View("Error", response.ResultMessage);
            }
            else { return RedirectToAction("Add"); }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id!=0)
            {
                var response = _employeeRepository.DeleteEmployee(id);
                if (response.Success)
                {
                    return RedirectToAction("Index");
                }
                return View("Error", response.ResultMessage);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}

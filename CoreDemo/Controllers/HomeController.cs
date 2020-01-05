using CoreDemo.Models;
using CoreDemo.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CoreDemo.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        private IHostingEnvironment _hostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        [AllowAnonymous]
        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployee();
            return View(model);
        }

        [AllowAnonymous]
        public ViewResult Details(int Id)
        {
            var employee = _employeeRepository.GetEmployee(Id);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", Id);
            }

            EmployeeDetailsViewModel employeeDetailsViewModel = new EmployeeDetailsViewModel()
            {
                Employee = employee,
                PageTitle = "Employee Details"
            };
            return View(employeeDetailsViewModel);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public RedirectToActionResult Create(EmployeeCreateViewModel model)
        {
            string uniqueFineName = ProcessUplodedFile(model);
            Employee newEmployee = new Employee()
            {
                Name = model.Name,
                Department = model.Department,
                Email = model.Email,
                PhotoPath = uniqueFineName
            };
            _employeeRepository.AddEmployee(newEmployee);
            return RedirectToAction("Details", new { newEmployee.Id });
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel()
            {
                EmployeeId = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Email = employee.Email,
                EmployeePhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }
        [HttpPost]
        public RedirectToActionResult Edit(EmployeeEditViewModel model)
        {

            var employee = _employeeRepository.GetEmployee(model.EmployeeId);
            employee.Name = model.Name;
            employee.Department = model.Department;
            employee.Email = model.Email;
            if (model.Photo != null)
            {
                if (model.EmployeePhotoPath != null)
                {
                    var delfilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Image", model.EmployeePhotoPath);
                    System.IO.File.Delete(delfilePath);
                }
                employee.PhotoPath = ProcessUplodedFile(model);
            }        

            
            _employeeRepository.UpdateEmployee(employee);
            return RedirectToAction("Index");
        }

        private string ProcessUplodedFile(EmployeeCreateViewModel model)
        {
            string uniqueFineName = null;
            if (model.Photo != null)
            {
                var rootPath = Path.Combine(_hostingEnvironment.WebRootPath, "Image");
                uniqueFineName = Guid.NewGuid() + "_" + model.Photo.FileName;
                var filePath = Path.Combine(rootPath, uniqueFineName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
                    
            }

            return uniqueFineName;
        }
    }
}

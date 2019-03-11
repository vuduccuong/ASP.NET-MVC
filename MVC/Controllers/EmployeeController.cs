using Data;
using MODEL;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DataDbContext _context;

        public EmployeeController()
        {
            _context = new DataDbContext();
        }

        // GET: Employee
        [HttpGet]
        public JsonResult GetListEmployees()
        {
            var model = from e in _context.Employees
                        select e;
            return Json(new
            {
                data = model,
                status = true,
            }, JsonRequestBehavior.AllowGet);
        }

        //Update: Employee
        [HttpPost]
        public JsonResult UpdateEmployee(string model)
        {
            string statusResult;
            string message = string.Empty;
            //Chuyển đổi dữ liệu từ string Json
            //Sau đó map sang kiểu dữ liệu của mình
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Employee employee = serializer.Deserialize<Employee>(model);

            //Dùng Find() để tìm kiếm dữ liệu trong DB
            var entity = _context.Employees.Find(employee.Id);
            if (entity != null)
            {
                entity.Salary = employee.Salary;
            }

            try
            {
                _context.SaveChanges();
                statusResult = "OK";
            }
            catch (Exception ex)
            {
                statusResult = "Err";
                message = ex.Message;
            }
            return Json(new
            {
                status = statusResult,
                err = message
            });
        }

        //Create new Employee
        [HttpPost]
        public JsonResult CreateEmployee(string employee)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Employee inputData = serializer.Deserialize<Employee>(employee);
            bool status = true;
            string message = string.Empty;
            try
            {
                if (inputData.Id.Equals(0))
                {
                    _context.Employees.Add(inputData);
                    _context.SaveChanges();
                }
                else
                {
                    var entity = _context.Employees.Find(inputData.Id);
                    if (entity != null)
                    {
                        entity.Name = inputData.Name;
                        entity.Salary = inputData.Salary;
                        entity.Status = inputData.Status;
                    }

                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                status = false;
                message = e.Message;
            }

            return Json(new
            {
                status,
                message
            });
        }

        //Get Employee By Id
        [HttpPost]
        public JsonResult GetEmployeeById(int id)
        {
            var model = _context.Employees.Find(id);
            return Json(new
            {
                data = model,
                status = "OK"
            });
        }

        //Search Employee
        [HttpGet]
        public JsonResult SearchEmployee(string keyword)
        {
            var model = _context.Employees.Where(x => x.Name.Contains(keyword.Trim()));
            return Json(new
            {
                data = model,
                status = "OK"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
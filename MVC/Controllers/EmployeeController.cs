using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Data;
using MODEL;

namespace MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private DataDbContext _context;

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
            },JsonRequestBehavior.AllowGet);
        }

        //Update: Employee
        [HttpPost]
        public JsonResult UpdateEmployee(string model)
        {
            var statusResult = "";
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
            catch(Exception ex)
            {
                statusResult = "Err";
            }
            return Json(new
            {
                status = statusResult,
            });
        }
    }
}
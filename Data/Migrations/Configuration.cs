using System.Collections.Generic;
using MODEL;

namespace Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.DataDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.DataDbContext context)
        {
            AddEmployee(context);
        }

        private void AddEmployee(DataDbContext context)
        {
            if (context.Employees.Count() == 0)
            {
                List<Employee> lstEmployees = new List<Employee>()
                {
                    new Employee() {Name = "Đinh Bằng Thép", Salary = 10000000, Status = true},
                    new Employee() {Name = "Võ Văn An", Salary = 12000000, Status = false},
                    new Employee() {Name = "Doãn Chí Bình", Salary = 150000000, Status = true},
                    new Employee() {Name = "Tiểu Long Nữ", Salary = 15000000, Status = true},
                    new Employee() {Name = "Dương Quá", Salary = 200000000, Status = true},
                    new Employee() {Name = "Âu Dương Phong", Salary = 10000000, Status = true},
                    new Employee() {Name = "Chu Chỉ Nhược", Salary = 11000000, Status = true},
                    new Employee() {Name = "Quách Tĩnh", Salary = 120000000, Status = true},
                    new Employee() {Name = "Hoàng Dung", Salary = 10000000, Status = true},
                    new Employee() {Name = "Quách Tương", Salary = 1000000, Status = true},
                    new Employee() {Name = "Quách Phù", Salary = 1500000, Status = true},
                };
                context.Employees.AddRange(lstEmployees);
                context.SaveChanges();
            }
        }
    }
}

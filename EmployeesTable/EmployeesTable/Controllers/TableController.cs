using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using EmployeesContext;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;

namespace EmployeesTable.Controllers
{
    public class TableController : Controller
    {
        public EmployeeRep _context { get; private set; }
        private readonly string[] positions = new string[4] { "Стажер", "Джуниор", "Мидл", "Сеньор" };
        public TableController(EmployeeRep context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Show()
        {
            return View(_context.GetEmployees());
        }
        [HttpGet]
        public IActionResult Add()
        {
            
            SelectList selectList = new SelectList(positions);
            ViewBag.SelectItems = selectList;
            return View();
        }
        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            if (employee.Name == null)
            {
                ModelState.AddModelError(nameof(employee.Name), "Формат ФИО: Васильев Василий Васильевич");
            }
            else
            {
                Regex regex = new Regex("[А-Я]{1}[а-я]{2,20} [А-Я]{1}[а-я]{2,20} [А-Я]{1}[а-я]{2,20}$");
                bool isNameValid = regex.IsMatch(employee.Name);
                if (employee.Name == " " || employee.Name.Length > 50 || !isNameValid)
                {
                    ModelState.AddModelError(nameof(employee.Name), "Формат ФИО: Васильев Василий Васильевич");
                }
            }          
             if (DateTime.Now < employee.HB || employee.HB==DateTime.MinValue)
            {
                ModelState.AddModelError(nameof(employee.HB), "Некорректная дата");
            }
             if (employee.Position != positions[0] && employee.Position != positions[1] && employee.Position != positions[2] && employee.Position != positions[3])
            {
                ModelState.AddModelError(nameof(employee.Position), "Некорректная должность");
            }
             if (employee.Seniority > 50 || employee.Seniority < 0)
            {
                ModelState.AddModelError(nameof(employee.Seniority), "Некорректный стаж");
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.AddEmployee(employee);
                }
                catch (Exception)
                {
                    return BadRequest("Данные невозможно сохранить");
                }
                return RedirectToAction("Show");
            }
            else
            {
                SelectList selectList = new SelectList(positions);
                ViewBag.SelectItems = selectList;
                return View(employee); 
            }
        }
    }
}

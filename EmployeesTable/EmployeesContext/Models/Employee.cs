using System;
using System.ComponentModel.DataAnnotations;


namespace EmployeesContext
{
    public class Employee
    {
        public long Id { get; set; }
        [RegularExpression("([А-Я]{1}[а-я]{2,20} [А-Я]{1}[а-я]{2,20} [А-Я]{1}[а-я]{2,20}$)", ErrorMessage = "Формат ФИО: Васильев Василий Васильевич")]
        public string Name { get; set; }
        
        public string Position { get; set; }
        
        public DateTime HB { get; set; }
       
        public int Seniority { get; set; }       
        public bool MaternityLeave { get; set; }
    }
}

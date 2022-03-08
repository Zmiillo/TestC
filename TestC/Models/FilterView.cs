using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestC.Models
{
    public class FilterView
    {
        public FilterView(List<Department> departments, int? department)
        {
            departments.Insert(0, new Department { Name = "Все", Id = 0 });
            Departments = new SelectList(departments, "Id", "Name", department);
            SelectedDepartment = department;
        }
        public SelectList Departments { get; private set; } 
        public int? SelectedDepartment { get; private set; }   
    }
}
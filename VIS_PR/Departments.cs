using System;

namespace VIS_PR
{
    public class Departments
    {

        public int DepartmentId { get; set; }
        public string Name { get; set; }

        Departments(int departmentId, string name)
        {
            DepartmentId = departmentId;
            Name = name;
        }

        public Departments()
        {
            
        }
    }
}
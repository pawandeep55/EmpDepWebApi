using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpDepWebApi.Models
{
    public class EmployeeCustomClass
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string DeptName { get; set; }

        public override string ToString()
        {
            return "id: " + EmpId + " ename:" + EmpName + " dname:" + DeptName;
        }
    }
}
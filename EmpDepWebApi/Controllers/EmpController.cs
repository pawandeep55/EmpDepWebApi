using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmpDepWebApi.Models;
using EmployeeService;

namespace EmpDepWebApi.Controllers
{

    [RoutePrefix("api/Emp")]
    public class EmpController : ApiController
    {
        TestDBEntities empDb = new EmployeeService.TestDBEntities();

        // GET: api/Emp
        [Route("get")]
        public IEnumerable<EmployeeCustomClass> Get()
        {
            //var emp = empDb.tbEmployees.Include(e => e.e_d_id);
            //return  emp.ToList();
            List<tbEmployee> list = empDb.tbEmployees.ToList();
            List<EmployeeCustomClass> empCustomList = new List<EmployeeCustomClass>();
            foreach (tbEmployee e in list)
            {
                EmployeeCustomClass emp = new EmployeeCustomClass();
                emp.EmpId = e.e_id;
                emp.EmpName = e.e_name;
                emp.DeptName = e.tbDepartment.d_name;
                empCustomList.Add(emp);
            }
            return empCustomList;
        }

        // GET: api/Emp/5
        public EmployeeCustomClass Get(int id)
        {
            tbEmployee emp = empDb.tbEmployees.FirstOrDefault(e => e.e_id == id);
            EmployeeCustomClass empCustom = new EmployeeCustomClass();
            empCustom.EmpId = emp.e_id;
            empCustom.EmpName = emp.e_name;
            empCustom.DeptName = emp.tbDepartment.d_name;
            return empCustom;
        }

        // POST: api/Emp
        public void Post([FromBody]EmployeeCustomClass emp)
        {
            Console.WriteLine(emp);

            tbDepartment dep = empDb.tbDepartments.FirstOrDefault(e => e.d_name.Equals(emp.DeptName));
            tbEmployee newEmp = new tbEmployee();
            newEmp.e_name = emp.EmpName;
            newEmp.tbDepartment = dep;
            //ef
            empDb.tbEmployees.Add(newEmp);
            empDb.SaveChanges();


        }

        // PUT: api/Emp/5

        public void Put(int id, [FromBody]EmployeeCustomClass updateEmp)
        {
            if (updateEmp != null)
            {

                tbEmployee emp = empDb.tbEmployees.FirstOrDefault(e => e.e_id == updateEmp.EmpId);

                tbDepartment dep = empDb.tbDepartments.FirstOrDefault(e => e.d_name.Equals(updateEmp.DeptName));

                emp.e_name = updateEmp.EmpName;
                emp.tbDepartment = dep;


                empDb.SaveChanges();
            }

        }

        // DELETE: api/Emp/5
        public void Delete(int id)
        {
            tbEmployee emp = empDb.tbEmployees.FirstOrDefault(e => e.e_id == id);
            empDb.tbEmployees.Remove(emp);
            empDb.SaveChanges();
        }
    }
}

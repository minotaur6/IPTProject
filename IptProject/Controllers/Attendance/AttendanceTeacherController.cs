using IptProject.Models.Attendance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;


namespace IptProject.Controllers.Attendance
{
    public class AttendanceTeacherController : Controller
    {
        // GET: AttendanceTeacher
        public async System.Threading.Tasks.Task<ActionResult> MarkAttendance()
        {
            string emp ="" ;
            Employee employee = new Employee(); 
            List<Employee> employees = new List<Employee>();
            List<Course> courses = new List<Course>();
            List<Section> sections = new List<Section>();
            List<Semester> semesters = new List<Semester>();
            

            //StudentCourseAttendance studentCourseAttendance = new StudentCourseAttendance();
            MarkAttendanceVM markAttendanceVM = new MarkAttendanceVM();


            using (var client = new HttpClient())
            {
                int empId = 11; // teacher login id
                //int courseid = 7; //pass student's course id

                client.BaseAddress = new Uri("https://localhost:44380/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET teacher name
                HttpResponseMessage result0 = await client.GetAsync("AttendanceTeacher/GetTeacherName/" + empId);
                //HTTP GET course
                HttpResponseMessage result = await client.GetAsync("AttendanceTeacher/GetTeacherCourses/" + empId);
                //HTTP GET section
                HttpResponseMessage result3 = await client.GetAsync("AttendanceTeacher/GetTeacherCourseSection/" + empId);

                //HttpResponseMessage result2 = await client.GetAsync("AttendanceStudent/GetStudentAttendance/" + courseid);
                if (result0.IsSuccessStatusCode)
                {
                    var response0 = result0.Content.ReadAsStringAsync().Result;
                    employees = JsonConvert.DeserializeObject<List<Employee>>(response0);
                    //employee.EmpName = JsonConvert.DeserializeObject<string>(response0).ToString();
                   // emp = JsonConvert.DeserializeObject<string>(response0).ToString();


                }
                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsStringAsync().Result;                   
                    courses = JsonConvert.DeserializeObject<List<Course>>(response);
                }

                if (result3.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsStringAsync().Result;
                    sections = JsonConvert.DeserializeObject<List<Section>>(response);
                }

                /*if (result2.IsSuccessStatusCode)
                {
                    var response = result2.Content.ReadAsStringAsync().Result;
                    // var re = r.Content.ReadAsStringAsync().Result;
                    studentCourseAttendances = JsonConvert.DeserializeObject<List<StudentCoursesAttendance>>(response);
                }*/
                markAttendanceVM.EmpName = emp.ToString();
                markAttendanceVM.EmpName = employee.EmpName;
                markAttendanceVM.employees = employees;
                markAttendanceVM.courses = courses;
                markAttendanceVM.sections = sections;

                //courseVM.studentcourseattendances = studentCourseAttendances;
                //studentCourseAttendance.CourseCode = checkAttendance;

                return View(markAttendanceVM);
            }


       
        }

        public ActionResult AddAttendance()
        {
            return View();
            //


        }

        public ActionResult EditAttendance()
        {
            return View();
        }

        /*public void Delete DeleteAttendance()
        {
            
        }*/
    }
}
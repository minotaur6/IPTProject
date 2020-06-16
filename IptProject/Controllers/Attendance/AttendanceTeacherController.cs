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
                //HTTP GET semester
                HttpResponseMessage result2 = await client.GetAsync("AttendanceTeacher/GetTeacherSemester/" + empId);

                //HTTP GET section
                //HttpResponseMessage result2 = await client.GetAsync("AttendanceStudent/GetStudentAttendance/" + courseid);

                if (result0.IsSuccessStatusCode)
                {
                    var response = result0.Content.ReadAsStringAsync().Result;
                    employees = JsonConvert.DeserializeObject<List<Employee>>(response);
                    
                }
                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsStringAsync().Result;                   
                    courses = JsonConvert.DeserializeObject<List<Course>>(response);
                }
                if (result2.IsSuccessStatusCode)
                {
                    var response = result2.Content.ReadAsStringAsync().Result;
                    semesters = JsonConvert.DeserializeObject<List<Semester>>(response);
                }

                /*if (result2.IsSuccessStatusCode)
                {
                    var response = result2.Content.ReadAsStringAsync().Result;
                    // var re = r.Content.ReadAsStringAsync().Result;
                    studentCourseAttendances = JsonConvert.DeserializeObject<List<StudentCoursesAttendance>>(response);
                }*/


                markAttendanceVM.employees = employees;
                markAttendanceVM.courses = courses;
                markAttendanceVM.semesters = semesters;
                markAttendanceVM.sections = sections;

               

                return View(markAttendanceVM);
            }


       
        }

        public async System.Threading.Tasks.Task<ActionResult> AddAttendance()
        {
            AddAttendanceVM addAttendanceVM = new AddAttendanceVM();

            List<Student> students = new List<Student>();




            using (var client = new HttpClient())
            {
                int empId = 11; // teacher login id
                //int courseid = 7; //pass student's course id

                client.BaseAddress = new Uri("https://localhost:44380/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                
                //HTTP GET course
                HttpResponseMessage result = await client.GetAsync("AttendanceTeacher/GetTeacherStudents/" + empId);
                
                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsStringAsync().Result;
                    students = JsonConvert.DeserializeObject<List<Student>>(response);
                }
               

                /*if (result2.IsSuccessStatusCode)
                {
                    var response = result2.Content.ReadAsStringAsync().Result;
                    // var re = r.Content.ReadAsStringAsync().Result;
                    studentCourseAttendances = JsonConvert.DeserializeObject<List<StudentCoursesAttendance>>(response);
                }*/


                addAttendanceVM.students = students;
                



                return View(addAttendanceVM);
            }

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
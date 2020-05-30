using IptProject.Models.Attendance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IptProject.Controllers.Attendance
{
    public class AttendanceStudentController : Controller
    {
        // GET: AttendanceStudent
        public async Task<ActionResult> ViewAttendance()
        {
            List<AllStudentCourses> checkAttendance = new List<AllStudentCourses>();
            List<StudentCoursesAttendance> studentCourseAttendances= new List<StudentCoursesAttendance>();

            //StudentCourseAttendance studentCourseAttendance = new StudentCourseAttendance();
            CoursesVM courseVM = new CoursesVM();
            using (var client = new HttpClient())
            {
                int studentId = 10;
                int courseid = 27; //pass student's course id

                client.BaseAddress = new Uri("https://localhost:44380/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET
                HttpResponseMessage result = await client.GetAsync("AttendanceStudent/GetStudentCourse/" + studentId);
                HttpResponseMessage result2 = await client.GetAsync("AttendanceStudent/GetStudentAttendance/" + courseid);

   


                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsStringAsync().Result;
                   // var re = r.Content.ReadAsStringAsync().Result;
                    checkAttendance = JsonConvert.DeserializeObject<List<AllStudentCourses>>(response);
                                   
                }

                if (result2.IsSuccessStatusCode)
                {
                    var response = result2.Content.ReadAsStringAsync().Result;
                    // var re = r.Content.ReadAsStringAsync().Result;
                    studentCourseAttendances = JsonConvert.DeserializeObject<List<StudentCoursesAttendance>>(response);

                }
                courseVM.allStudentCourses = checkAttendance;
                courseVM.studentcourseattendances = studentCourseAttendances;
                //studentCourseAttendance.CourseCode = checkAttendance;

                return View(courseVM);
                
            }

           




        }
    }
}
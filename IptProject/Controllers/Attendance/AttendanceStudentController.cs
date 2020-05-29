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

            //StudentCourseAttendance studentCourseAttendance = new StudentCourseAttendance();
            //CoursesVM courseVM = new CoursesVM();
            using (var client = new HttpClient())
            {
                int studentId = 10;

                client.BaseAddress = new Uri("https://localhost:44380/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET
                HttpResponseMessage result = await client.GetAsync("AttendanceStudent/GetStudentCourse/" + studentId);
             
                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsStringAsync().Result;
                    checkAttendance = JsonConvert.DeserializeObject<List<AllStudentCourses>>(response);
                                   
                }
                //courseVM.allStudentCourses = checkAttendance;
                //studentCourseAttendance.CourseCode = checkAttendance;

                return View(checkAttendance);
            }
        }
    }
}
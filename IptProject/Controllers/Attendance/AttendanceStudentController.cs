using IptProject.Models.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace IptProject.Controllers.Attendance
{
    public class AttendanceStudentController : Controller
    {
        // GET: AttendanceStudent
        public ActionResult ViewAttendance()
        {
            List<StudentCourseAttendance> checkAttendance = new List<StudentCourseAttendance>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380/api/");
                //HTTP GET
                int studentId = 10;
                var responseTask = client.GetAsync("AttendanceStudent/ViewAttendance/" + studentId);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<StudentCourseAttendance[]>();
                    readTask.Wait();

                    var studentAttendance = readTask.Result;
                    foreach (var stud in studentAttendance)
                    {
                        checkAttendance.Add(stud);
                    }

                    //var fooditems = readTask.Result;

                    //}
                }

                return View(checkAttendance);
            }
        }
    }
}
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

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380/api/");
                //HTTP GET








                //var responseTask = client.GetAsync("attendance/get");
                //responseTask.Wait();

                //var result = responseTask.Result;
                //if (result.IsSuccessStatusCode)
                //{

                    //var readTask = result.Content.ReadAsAsync<FoodItem[]>();
                    //readTask.Wait();

                    //var fooditems = readTask.Result;

                    
                //}
            }

            return View();
        }
    }
}
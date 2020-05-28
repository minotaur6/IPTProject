using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IptProject.Controllers.Attendance
{
    public class AttendanceStudentController : Controller
    {
        // GET: AttendanceStudent
        public ActionResult ViewAttendance()
        {
            return View();
        }
    }
}
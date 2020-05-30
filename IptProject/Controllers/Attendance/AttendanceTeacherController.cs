using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IptProject.Controllers.Attendance
{
    public class AttendanceTeacherController : Controller
    {
        // GET: AttendanceTeacher
        public ActionResult MarkAttendance()
        {
            return View();
        }

        public ActionResult AddAttendance()
        {
            return View();
        }

        public ActionResult EditAttendance()
        {
            return View();
        }
    }
}
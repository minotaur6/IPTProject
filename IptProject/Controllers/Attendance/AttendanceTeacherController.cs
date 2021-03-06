﻿using IptProject.Models.Attendance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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

                //HttpResponseMessage result3 = await client.GetAsync("AttendanceTeacher/GetTeacherSemester/" + empId);

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
                //markAttendanceVM.sections = sections;

                return View(markAttendanceVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> MarkAttendance(FormCollection formCollection)
        {
            SectionVM section = new SectionVM();
            List<Section> sections = new List<Section>();
            string EmpName = formCollection[1];
            Int32.TryParse(formCollection[2], out int courseID);
            section.EmpName = EmpName;
            section.CourseID = courseID;
            var json = JsonConvert.SerializeObject(new DataVM { courseID = courseID, empName = EmpName });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage result = await client.PostAsync("AttendanceTeacher/GetTeacherSections/", data);
                var response = result.Content.ReadAsStringAsync().Result;

                sections = JsonConvert.DeserializeObject<List<Section>>(response);
            }
            section.sections = sections;
            return View("ChooseSection", section);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> ChooseSection(FormCollection formCollection)
        {
            string teacherName = formCollection[1];
            string courseID = formCollection[2];
            string sectionName = formCollection[3];
            AddAttendanceVM addAttendanceVM = new AddAttendanceVM { CourseID = courseID, EmpName = teacherName, SectionName = sectionName };
            var json = JsonConvert.SerializeObject(addAttendanceVM);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage result = await client.PostAsync("AttendanceTeacher/GetTeacherStudents/", data);
                var response = result.Content.ReadAsStringAsync().Result;
                addAttendanceVM.students = JsonConvert.DeserializeObject<List<Student>>(response);
            }
            return View("AddAttendance", addAttendanceVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> SaveAttendence(FormCollection formCollection)
        {
            string Date = formCollection[1];
            string Duration = formCollection[2];
            string[] EnrollmentID = formCollection[3].Split(',');
            string[] Presence = formCollection[4].Split(',');
            List<Attend> attendances = new List<Attend>();
            for (int i = 0; i < EnrollmentID.Count(); i++)
            {
                attendances.Add(new Attend { EnrollmentID = Int32.Parse(EnrollmentID[i]), AttendanceStatus = Presence[i], ClassDuration = Int32.Parse(Duration), AttendanceDate = Date });
            }

            var json = JsonConvert.SerializeObject(attendances);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage result = await client.PostAsync("AttendanceTeacher/AddStudentAttendance/", data);
                var response = result.Content.ReadAsStringAsync().Result;
            }

            return RedirectToAction("MarkAttendance");
        }

        public ActionResult EditAttendance()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAttendance(FormCollection form)
        {
            string[] test = form[1].Split(',');
            string Date = test[test.Length - 2];

            return Content("Here");
        }
    }
}
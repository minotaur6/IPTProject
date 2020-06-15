﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IptProject.Models.Attendance
{
    public class MarkAttendanceVM
    {
        public string EmpName { get; set; }
        public List<Employee> employees { get; set; }
        public List<Course> courses { get; set; }

        public List<Section> sections { get; set; }

        public List<Semester> semesters { get; set; }

    }
}
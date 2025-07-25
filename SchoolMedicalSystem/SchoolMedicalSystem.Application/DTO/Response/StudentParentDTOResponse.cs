﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Response
{
    public class StudentParentDTOResponse
    {
        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ParentPhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public int? UserId { get; set; }
        public int? ClassId { get; set; }
    }
}

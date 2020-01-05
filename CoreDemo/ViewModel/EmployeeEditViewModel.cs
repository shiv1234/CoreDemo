using CoreDemo.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.ViewModel
{
    public class EmployeeEditViewModel : EmployeeCreateViewModel
    {       
        public int EmployeeId { get; set; }
        public string EmployeePhotoPath { get; set; }
    }
}

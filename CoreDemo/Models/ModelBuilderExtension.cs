using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
            new Employee
            {
                Id = 1,
                Name = "Shiv",
                Department = Dept.Admin,
                Email = "shiv@gmail.com"
            },
            new Employee
            {
                Id = 2,
                Name = "Gunjan",
                Department = Dept.HR,
                Email = "gunjan@gmail.com"
            }
         );

        }
    }
}

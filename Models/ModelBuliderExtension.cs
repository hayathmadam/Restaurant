using Microsoft.EntityFrameworkCore;

namespace EmployeeManagment.Models
{
    public static class ModelBuliderExtension
    {

        public static void seed(this  ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Employee>().HasData(

                    new Employee()
                    {
                        Id = 8,
                        Name = "Hait",
                        Email = "hayathm1994@gmail.com",
                        Department = Dept.ECO,


                    },
                     new Employee()
                     {
                         Id = 9,
                         Name = "Hait",
                         Email = "hayathm1994@gmail.com",
                         Department = Dept.ECO,


                     });


        }
    }
}

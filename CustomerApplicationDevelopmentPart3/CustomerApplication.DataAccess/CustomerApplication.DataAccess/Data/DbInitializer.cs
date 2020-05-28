using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerApplication.Model.Models;


namespace CustomerApplication.DataAccess.Data
{
    public class DbInitializer
    {
        public static void Initialize(DataContext context)
        {

            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            byte[] passwordHash, passwordSalt;
            //UserService.CreatePasswordHash("123", out passwordHash, out passwordSalt);

            int input = 123;


            var students = new Employee[]
            {

                new Employee {Id = 1, FirstName = "test", LastName = "test", Username = "test", PasswordHash = BitConverter.GetBytes(input), PasswordSalt = BitConverter.GetBytes(input) }
                


        };
            foreach (Employee s in students)
            { 
             
                context.Users.Add(s);
            }
            context.SaveChanges();


        }
    }
}
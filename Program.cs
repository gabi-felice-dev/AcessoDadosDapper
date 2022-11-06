using System;
using Blog.Models;
using Blog.Repositories;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog
{
    class Program
    {
        private const string CONNECTION_STRING = @"Data Source=DESKTOP-LQVQMLG;Initial Catalog=Blog;Persist Security Info=True;User ID=sa;Password=425879;TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            using var connection = new SqlConnection(CONNECTION_STRING);
            var repository = new Repository<User>(connection);

            // CreateUser(repository);
            // UpdateUser(repository);
            // DeleteUser(repository);
            // ReadUser(repository);
            ReadUsers(repository);
            //ReadWithRoles(connection);
        }

        private static void CreateUser(Repository<User> repository)
        {
            var user = new User
            {
                Bio = "Dev Pleno",
                Email = "gabriele_felice@hotmail.com",
                Image = "",
                Name = "Gabriele Felice",
                Slug = "gabriele-felice",
                PasswordHash = Guid.NewGuid().ToString()
            };

            repository.Create(user);
        }

        private static void ReadUsers(Repository<User> repository)
        {
            var users = repository.ReadAll();
            foreach (var item in users)
                Console.WriteLine(item.Name);
        }

        private static void ReadUser(Repository<User> repository)
        {
            var user = repository.Read(2);
            Console.WriteLine(user?.Email);
        }

        private static void UpdateUser(Repository<User> repository)
        {
            var user = repository.Read(2);
            user.Email = "hello@balta.io";
            repository.Update(user);

            Console.WriteLine(user?.Email);
        }

        private static void DeleteUser(Repository<User> repository)
        {
            var user = repository.Read(2);
            repository.Delete(user);
        }

        private static void ReadWithRoles(SqlConnection connection)
        {
            var repository = new UserRepository(connection);
            var users = repository.ReadWithRole();

            foreach (var user in users)
            {
                Console.WriteLine(user.Email);
                foreach (var role in user.Roles) Console.WriteLine($" - {role.Slug}");
            }
        }
    }
}

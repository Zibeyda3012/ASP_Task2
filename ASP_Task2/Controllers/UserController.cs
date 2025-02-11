using ASP_Task2.Entities;
using ASP_Task2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ASP_Task2.Controllers
{
    public class UserController : Controller
    {

        public List<Person> Users { get; set; }
        string path = "JsonFiles/UserData.json";

        public UserController()
        {
            Users = new List<Person>();
            ReadDataFromFile();
        }


        public List<Person> ReadDataFromFile()
        {
            if (System.IO.File.Exists(path))
            {
                string jsonString = System.IO.File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<Person>>(jsonString);
            }

            else
                return null;
        }

        public void WriteDataToFile()
        {

            var jsonString = JsonSerializer.Serialize(Users, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(path, jsonString);
        }

        public void Delete(string id)
        {
            Users = ReadDataFromFile();
            var user = Users.FirstOrDefault(x => x.Id == id);
            Users.Remove(user);
            WriteDataToFile();
        }


        [HttpGet]
        public IActionResult Index1()
        {
            Users = ReadDataFromFile();
            return View(Users);
        }

        public IActionResult DeleteUser(string id)
        {
            Delete(id);
            return RedirectToAction("Index1");
        }

        public IActionResult EditUser(string id)
        {
            Users = ReadDataFromFile();
            var user = Users.FirstOrDefault(x => x.Id == id);
            return View(user);
        }

        public IActionResult Details(string id)
        {
            Users = ReadDataFromFile();
            var user = Users.FirstOrDefault(x => x.Id == id);
            return View(user);
        }

        public IActionResult AddUser()
        {
            var vm = new UserAddViewModel()
            {
                Person = new Person()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult AddUser(UserAddViewModel model, IFormFile image)
        {


            if (ModelState.IsValid)
            {
                var user = new Person()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = model.Person.Name,
                    Surname = model.Person.Surname,
                    Age = model.Person.Age
                };

                if (image is not null)
                {
                    string fileExtension = Path.GetExtension(model.Person.Image).ToLower();
                    string fileName = $"{Guid.NewGuid()}{fileExtension}";
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                        image.CopyTo(stream);

                    user.Image = fileName;
                }

                Users = ReadDataFromFile();
                Users.Add(user);
                WriteDataToFile();
                return Redirect("Index1");
            }

            return View(model);

        }

        [HttpPost]
        public IActionResult EditUser(Person person, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                Users = ReadDataFromFile();
                var user = Users.FirstOrDefault(x => x.Id == person.Id);

                if (user is not null)
                {
                    user.Name = person.Name;
                    user.Surname = person.Surname;
                    user.Age = person.Age;

                    if (image is not null)
                    {
                        string fileExtension = Path.GetExtension(person.Image).ToLower();
                        string fileName = $"{Guid.NewGuid()}{fileExtension}";
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                            image.CopyTo(stream);

                        user.Image = fileName;
                    }

                   
                    WriteDataToFile();
                    return RedirectToAction("Index1");
                }

                else
                    return Redirect($"/user/EditUser/{person.Id}");
            }

            else
                return Redirect("Index1");
        }
    }

}

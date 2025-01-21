using ASP_Task2.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace ASP_Task2.Controllers
{
    public class HomeController : Controller
    {
        public List<Person> Users { get; set; }
        string path = "JsonFiles/UserData.json";
        public HomeController()
        {
            Users = ReadDataFromFile(path);
        }
        public List<Person> ReadDataFromFile(string path)
        {
            string jsonString = System.IO.File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<Person>>(jsonString);

        }

        [HttpGet]
        public IActionResult Index1()
        {


            //List<Person> Users = new List<Person>()
            //{
            //    new Person()
            //    {
            //        Id = 1,
            //        Name="Zibeyda",
            //        Surname= "Musayeva",
            //        Age=21,
            //        Image=" "
            //    },

            //    new Person()
            //    {
            //        Id = 1,
            //        Name="Zibeyda",
            //        Surname= "Musayeva",
            //        Age=21,
            //        Image=" "
            //    },
            //};

            Users = ReadDataFromFile(path);

            return View(Users);
        }

        //public async Task WriteDataToFile(string path, List<Person> Users)
        //{

        //}

        //public async Task<List<Person>> ReadDataFromFile(string path)
        //{
        //    string jsonString = System.IO.File.ReadAllText(path);
        //    return JsonSerializer.Deserialize<List<Person>>(jsonString);

        //}

        public IActionResult Delete()
        {

            return RedirectToAction("/home/Index1");
        }

        //public IActionResult Edit()
        //{

        //}

        //public IActionResult Details()
        //{

        //}



    }
}

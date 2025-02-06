using System;
using System.ComponentModel.DataAnnotations;

namespace ASP_Task2.Entities
{
    public class Person
    {

        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public string Image { get; set; } = "img1.jpg";

        [Required]
        [Range(10, 75)]
        public short Age { get; set; }
    }
}

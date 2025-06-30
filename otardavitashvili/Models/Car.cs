// MODEL: Car.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace otardavitashvili.Models
{
    public class Car
    {
        public Guid CarId { get; set; }

        [Required]
        public string Model { get; set; } = string.Empty;

        [Range(1900, 2100)]
        public int ReleaseDate { get; set; }

        [Range(1, 1000)]
        public int Speed { get; set; }

        [Required]
        public Guid PersonId { get; set; }

        public Person Person { get; set; } = null!;
    }
}
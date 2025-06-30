using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace otardavitashvili.Models
{
    public class Person
    {
        public Guid PersonId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(30)]
        public string PersonName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(30)]
        public string PersonLastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birth Date is required")]
        public DateTime BirthDate { get; set; }

        public List<Car> Cars { get; set; } = new();
    }
}

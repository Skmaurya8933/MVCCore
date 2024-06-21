using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Position is required")]
        public string Position { get; set; } = string.Empty;

        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; } = string.Empty;
    }
}

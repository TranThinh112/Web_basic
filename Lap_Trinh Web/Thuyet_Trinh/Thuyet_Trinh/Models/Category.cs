using System.ComponentModel.DataAnnotations;


namespace Web_buoi_7.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }
}
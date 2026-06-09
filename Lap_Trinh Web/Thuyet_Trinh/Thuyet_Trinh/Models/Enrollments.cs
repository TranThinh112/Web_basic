using System.ComponentModel.DataAnnotations.Schema;

namespace Web_buoi_7.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int CourseId { get; set; }

        public DateTime EnrollDate { get; set; }

        [ForeignKey("UserId")]
        public AppUser? User { get; set; }

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }
    }
}
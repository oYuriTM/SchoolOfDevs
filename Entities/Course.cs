using System.Text.Json.Serialization;

namespace SchoolOfDevs.Entities
{
    public class Course : BaseEntity
    {     
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public User Teacher { get; set; }
        [JsonIgnore]
        public ICollection<User> Students { get; set; }
        [JsonIgnore]
        public List<StudentCourse> StudentCourses { get; set; }
    }
}

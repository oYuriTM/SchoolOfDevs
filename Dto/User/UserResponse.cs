using SchoolOfDevs.Entities;
using SchoolOfDevs.Enums;

namespace SchoolOfDevs.Dto.User
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string UserName { get; set; }
        public TypeUser TypeUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Course> CoursesTeaching { get; set; } // TypeUser == Teacher
        public List<Course> CoursesStuding { get; set; } // TypeUser == Student
    }
}

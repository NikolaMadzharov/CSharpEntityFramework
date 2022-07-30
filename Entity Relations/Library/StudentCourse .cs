using System.ComponentModel.DataAnnotations.Schema;

namespace P01_StudentSystem.Data.Models
{
    public class StudentCourse
    {
        [ForeignKey(nameof(Students))]
        public int StudentId { get; set; }

        public virtual Student Students { get; set; }

        [ForeignKey(nameof(Courses))]
        public int  CourseId { get; set; }

        public  virtual Course Courses { get; set; }



        
    }
}
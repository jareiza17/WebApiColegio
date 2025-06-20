namespace ModelsLayer.Entities
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }

        public int StudentID { get; set; }
        public Student Student { get; set; }

        public int SubjectID { get; set; }
        public Subject Subject { get; set; }
    }
}

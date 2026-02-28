namespace mvc2.Models
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }

        public double AgeSquared => Math.Pow(Age, 2);
    }
}
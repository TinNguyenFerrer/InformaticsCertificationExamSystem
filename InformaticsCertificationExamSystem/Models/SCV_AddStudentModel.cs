namespace InformaticsCertificationExamSystem.Models
{
    public class SCV_AddStudentModel
    {
        public string Name { get; set; }
        //public string BirthPlace { get; set; }
        public DateTime BirthDay { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? IdentifierCode { get; set; }
    }
}



namespace DomainLayer.EntitiesDTOS
{
    public class DoctorDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int departmentId { get; set; }
    }
}



using Microsoft.AspNetCore.Http;

namespace DomainLayer.EntitiesDTOS
{
    public class StudentDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDay { get; set; }
        public int departmentId { get; set; }
        public IFormFile Form { get; set; }

    }
}

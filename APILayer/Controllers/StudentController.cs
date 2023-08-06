using DomainLayer.Entities;
using DomainLayer.EntitiesDTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiddelLayer.Interfaces;

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IGenericRepository<Student> _studentRepository;

        public StudentController(IGenericRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var query = await _studentRepository.GetAll(new[] { "department" });

            if(query is null)
                return NotFound("not exist students");

            return Ok(query);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var query = await _studentRepository.GetById(id);

            if (query is null)
                return NotFound("student is not exist");

            return Ok(query);
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetStudentByName(string name)
        {
            var query = await _studentRepository.FindBy(x=>x.Name==name, new[] { "department" });

            if (query is null)
                return NotFound("student is not exist");

            return Ok(query);
        }

        [HttpGet("GetByPhone")]
        public async Task<IActionResult> GetStudentByPhone(string phone)
        {
            var query = await _studentRepository.FindBy(x => x.Phone == phone, new[] { "department" });

            if (query is null)
                return NotFound("student is not exist");

            return Ok(query);
        }

        [HttpGet("GetByEmail")]
        public async Task<IActionResult> GetStudentByEmail(string email)
        {
            var query = await _studentRepository.FindBy(x => x.Email == email, new[] { "department" });

            if (query is null)
                return NotFound("student is not exist");

            return Ok(query);
        }


        [HttpGet("GetByDepartmentId")]
        public async Task<IActionResult> GetStudentByName(int departmentId)
        {
            var query = await _studentRepository.FindBy(x => x.departmentId == departmentId, new[] { "department" });

            if (query is null)
                return NotFound("student is not exist");

            return Ok(query);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromForm] StudentDTO studentDto)
        {
            if (studentDto == null)
                return BadRequest("student is null");

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");

            using var DataStream = new MemoryStream();
            await studentDto.Form.CopyToAsync(DataStream);

            Student student = new Student()
            {
                BirthDay = studentDto.BirthDay,
                departmentId = studentDto.departmentId,
                Email = studentDto.Email,
                Phone = studentDto.Phone,
                Form = DataStream.ToArray(),
                Name = studentDto.Name
            };

             _studentRepository.Add(student);
            _studentRepository.Save();

            return Created("",student);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent([FromForm] StudentDTO studentDto,int id)
        {
            var search = await _studentRepository.GetById(id);

            if (search is null)
                return BadRequest("this student is not exist");

            if (studentDto == null)
                return BadRequest("student is null");

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");

            using var DataStream = new MemoryStream();
            studentDto.Form.CopyTo(DataStream);


            search.BirthDay = studentDto.BirthDay;
            search.departmentId = studentDto.departmentId;
            search.Email = studentDto.Email;
            search.Phone = studentDto.Phone;
            search.Form = DataStream.ToArray();
            search.Name = studentDto.Name;
            

            _studentRepository.Update(search);
            _studentRepository.Save();

            return Ok(search);
        }

        [HttpDelete]
        public async Task<IActionResult> deleteStudent(int id)
        {
            var search= await _studentRepository.GetById(id);

            if (search is null)
                return NotFound("student is not exist");

            _studentRepository.delete(search);

            return Ok(search);
        }






    }


}

using DomainLayer.Entities;
using DomainLayer.EntitiesDTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiddelLayer.Interfaces;

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IGenericRepository<Doctor> _doctorRepository;

        public DoctorController(IGenericRepository<Doctor> doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var query = await _doctorRepository.GetAll(new[] { "department" });
            
            if (query is null)
                return NotFound("not exist Departments");

            return Ok(query);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var query = await _doctorRepository.GetById(id);

            if (query is null)
                return NotFound("Doctor is not exist");

            return Ok(query);
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetDoctorByName(string name)
        {
            var query = await _doctorRepository.FindBy(x => x.Name == name, new[] { "department" });

            if (query is null)
                return NotFound("Doctor is not exist");

            return Ok(query);
        }

        [HttpGet("GetByPhone")]
        public async Task<IActionResult> GetDoctorByPhone(string phone)
        {
            var query = await _doctorRepository.FindBy(x => x.Phone == phone, new[] { "department" });

            if (query is null)
                return NotFound("Doctor is not exist");

            return Ok(query);
        }

        [HttpGet("GetByEmail")]
        public async Task<IActionResult> GetDoctorByEmail(string email)
        {
            var query = await _doctorRepository.FindBy(x => x.Email == email, new[] { "department" });

            if (query is null)
                return NotFound("Doctor is not exist");

            return Ok(query);
        }


        [HttpGet("GetByDepartmentId")]
        public async Task<IActionResult> GetDoctorByName(int departmentId)
        {
            var query = await _doctorRepository.FindBy(x => x.departmentId == departmentId, new[] { "department" });

            if (query is null)
                return NotFound("Doctor is not exist");

            return Ok(query);
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctor([FromBody] DoctorDTO doctorDTO)
        {
            if (doctorDTO == null)
                return BadRequest("doctor is null");

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");

            Doctor doctor = new Doctor()
            {
                Name= doctorDTO.Name,
                departmentId=doctorDTO.departmentId,
                Email= doctorDTO.Email,
                DateOfBirth = doctorDTO.DateOfBirth,
                Phone = doctorDTO.Phone
            };

            _doctorRepository.Add(doctor);
            _doctorRepository.Save();

            return Created("", doctor);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor([FromBody] DoctorDTO doctorDTO, int id)
        {
            var search = await _doctorRepository.GetById(id);

            if (search is null)
                return BadRequest("this doctor is not exist");

            if (doctorDTO == null)
                return BadRequest("doctor is null");

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");


            search.Phone = doctorDTO.Phone;
            search.DateOfBirth = doctorDTO.DateOfBirth;
            search.Email = doctorDTO.Email;
            search.departmentId = doctorDTO.departmentId;
            search.Name=doctorDTO.Name;


            _doctorRepository.Update(search);
            _doctorRepository.Save();

            return Ok(search);
        }

        [HttpDelete]
        public async Task<IActionResult> deleteDoctor(int id)
        {
            var search = await _doctorRepository.GetById(id);

            if (search is null)
                return NotFound("Doctor is not exist");

            _doctorRepository.delete(search);

            return Ok(search);
        }

    }
}

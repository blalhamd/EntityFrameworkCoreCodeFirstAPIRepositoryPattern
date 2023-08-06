using DomainLayer.Entities;
using DomainLayer.EntitiesDTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiddelLayer.Interfaces;

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IGenericRepository<Department> _departmentRepository;

        public DepartmentController(IGenericRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var query = await _departmentRepository.GetAll(new[] { "Students", "doctors" });

            if (query is null)
                return NotFound("not exist Departments");

            return Ok(query);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var query = await _departmentRepository.GetById(id);

            if (query is null)
                return NotFound("Department is not exist");

            return Ok(query);
        }
        

        [HttpGet("GetByDescription")]
        public async Task<IActionResult> GetDepartmentByDescription(string description)
        {
            var query = await _departmentRepository.FindBy(x => x.Description == description, new[] { "Students", "doctors" });

            if (query is null)
                return NotFound("Department is not exist");

            return Ok(query);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetDepartmentBy(int id)
        {
            var query = await _departmentRepository.FindBy(x => x.Id == id, new[] { "Students", "doctors" });

            if (query is null)
                return NotFound("Department is not exist");

            return Ok(query);
        }


        [HttpGet("GetByDepartmentTitle")]
        public async Task<IActionResult> GetDepartmentByName(string title)
        {
            var query = await _departmentRepository.FindBy(x => x.Title == title, new[] { "Students", "doctors" });

            if (query is null)
                return NotFound("department is not exist");

            return Ok(query);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentDTO departmentDTO)
        {
            if (departmentDTO == null)
                return BadRequest("department is null");

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");

            Department department = new Department()
            {
               Title= departmentDTO.Title,
               Description= departmentDTO.Description,
            };

            _departmentRepository.Add(department);
            _departmentRepository.Save();

            return Created("",department);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentDTO departmentDTO, int id)
        {
            var search = await _departmentRepository.GetById(id);

            if (search is null)
                return BadRequest("this department is not exist");

            if (departmentDTO == null)
                return BadRequest("department is null");

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");


            search.Title = departmentDTO.Title;
            search.Description=departmentDTO.Description;


            _departmentRepository.Update(search);
            _departmentRepository.Save();

            return Ok(search);
        }

        [HttpDelete]
        public async Task<IActionResult> deleteDepartment(int id)
        {
            var search = await _departmentRepository.GetById(id);

            if (search is null)
                return NotFound("Department is not exist");

            _departmentRepository.delete(search);

            return Ok(search);
        }

    }
}

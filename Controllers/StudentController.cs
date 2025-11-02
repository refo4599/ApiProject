using ApiProject.DTOs;
using ApiProject.Models;
using ApiProject.unitofwork;

using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public StudentController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Student
        [HttpGet]
        public IActionResult GetAll()
        {
            var students = _unitOfWork.StudentRepo.GetAll();

            var result = students.Select(s => new StudentDto
            {
                StudentId = s.Id,
                StudentName = s.Name,
                Grade = s.Grade,
                Age = DateTime.Now.Year - s.BirthDate.Year,
                ParentId = s.ParentId
            }).ToList();

            return Ok(result);
        }

        // GET: api/Student/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = _unitOfWork.StudentRepo.GetById(id);
            if (student == null)
                return NotFound("Student not found");

            var dto = new StudentDto
            {
                StudentId = student.Id,
                StudentName = student.Name,
                Grade = student.Grade,
                Age = DateTime.Now.Year - student.BirthDate.Year,
                ParentId = student.ParentId
            };

            return Ok(dto);
        }

        // POST: api/Student
        [HttpPost]
        public IActionResult Add(StudentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = new Student
            {
                Name = dto.StudentName,
                Grade = dto.Grade,
                ParentId = dto.ParentId,
                BirthDate = DateTime.Now.AddYears(-dto.Age)
            };

            _unitOfWork.StudentRepo.Add(student);
            return Ok("Student added successfully");
        }

        // PUT: api/Student/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, StudentDto dto)
        {
            var student = _unitOfWork.StudentRepo.GetById(id);
            if (student == null)
                return NotFound("Student not found");

            student.Name = dto.StudentName;
            student.Grade = dto.Grade;
            student.ParentId = dto.ParentId;
            student.BirthDate = DateTime.Now.AddYears(-dto.Age);

            _unitOfWork.StudentRepo.Update(student);
            return Ok("Student updated successfully");
        }

        // DELETE: api/Student/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = _unitOfWork.StudentRepo.GetById(id);
            if (student == null)
                return NotFound("Student not found");

            _unitOfWork.StudentRepo.Delete(id);
            return Ok("Student deleted successfully");
        }
    }
}

using ApiProject.DTOs;
using ApiProject.Models;
using ApiProject.unitofwork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ApiProject.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public ParentController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Parent
        [HttpGet]
        public IActionResult GetAll()
        {
            var parents = _unitOfWork.ParentRepo.GetAll();

            var result = parents.Select(p => new ParentDto
            {
                ParentId = p.Id,
                ParentName = p.FullName,
                Email = p.Email,
                PhoneNumber = p.Phone
            }).ToList();

            return Ok(result);
        }

        // GET: api/Parent/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var parent = _unitOfWork.ParentRepo.GetById(id);
            if (parent == null)
                return NotFound("Parent not found");

            var dto = new ParentDto
            {
                ParentId = parent.Id,
                ParentName = parent.FullName,
                Email = parent.Email,
                PhoneNumber = parent.Phone
            };

            return Ok(dto);
        }

        // POST: api/Parent
        [HttpPost]
        public IActionResult Add(ParentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var parent = new Parent
            {
                FullName = dto.ParentName,
                Email = dto.Email,
                Phone = dto.PhoneNumber
            };

            _unitOfWork.ParentRepo.Add(parent);
            return Ok("Parent added successfully");
        }

        // PUT: api/Parent/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, ParentDto dto)
        {
            var parent = _unitOfWork.ParentRepo.GetById(id);
            if (parent == null)
                return NotFound("Parent not found");

            parent.FullName = dto.ParentName;
            parent.Email = dto.Email;
            parent.Phone = dto.PhoneNumber;

            _unitOfWork.ParentRepo.Update(parent);
            return Ok("Parent updated successfully");
        }

        // DELETE: api/Parent/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var parent = _unitOfWork.ParentRepo.GetById(id);
            if (parent == null)
                return NotFound("Parent not found");

            _unitOfWork.ParentRepo.Delete(id);
            return Ok("Parent deleted successfully");
        }

        [HttpGet("{parentId}/students")]
        public IActionResult GetStudentsByParent(int parentId)
        {
           
            var students = _unitOfWork.StudentRepo
                .GetAll()
                .Where(s => s.ParentId == parentId)
                .ToList();

            if (!students.Any())
                return NotFound($"لا يوجد أبناء لولي الأمر بالرقم {parentId}");

            //  Manual Mapping From Student To StudentDto
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
    
}
}

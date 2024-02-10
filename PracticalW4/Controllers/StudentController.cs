using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticalW4.Model;
using PracticalW4.Repostory;

namespace PracticalW4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public IStudents _students {  get; set; }
        public StudentController(IStudents students)
        {
            _students = students;
        }

        [HttpGet("ID")]
        public IActionResult Get(int id)
        {
            return Ok(_students.GetStudents(id));
        }
        [HttpGet("ALL")]
        public IActionResult GetStudent() 
        {
            return Ok(_students.GetAllStudents());
        }
        [HttpPost]
        public IActionResult AddStudent([FromBody] Students students)
        {
            _students.AddStudents(students);
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateStudent(Students students)
        {
            _students.UpdateStudents(students);
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteStudent(int id) 
        {
            _students.DeleteStudents(id);
            return Ok();    
        }
    }
}

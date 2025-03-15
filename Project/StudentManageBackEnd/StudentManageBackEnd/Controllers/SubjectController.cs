using BusinessLogicLayer.Services;
using BusinessLogicLayer.ServicesInterface;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentManageBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subjects>>> GetAllSubjects()
        {
            var subjects = await _subjectService.GetAllSubjects();
            return Ok(subjects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subjects>> GetSubjectById(int id)
        {
            var subject = await _subjectService.GetSubjectById(id);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }

        [HttpPost]
        public async Task<ActionResult<Subjects>> InsertSubject(Subjects newSubject)
        {
            var subject = await _subjectService.InsertSubject(newSubject);
            if (subject == null)
            {
                return BadRequest();
            }

            return Ok(subject);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Subjects>> UpdateSubject([FromBody] Subjects newSubject, int id)
        {
            var subject = await _subjectService.GetSubjectById(id);
            if (subject == null)
            {
                return NotFound();
            }
            newSubject.Id = id;
            subject = newSubject;
            await _subjectService.UpdateSubject(subject);

            return Ok(subject);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _subjectService.DeleteSubject(id);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }
    }
}

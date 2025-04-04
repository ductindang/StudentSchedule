using BusinessLogicLayer.Services;
using BusinessLogicLayer.ServicesInterface;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subjects>>> GetAllSubjects()
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Không xác thực được người dùng" });
            }
            var subjects = await _subjectService.GetAllSubjects(userId.Value);
            return Ok(subjects);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Subjects>> GetSubjectById(int id)
        {
            var userId = GetUserIdFromToken();

            var subject = await _subjectService.GetSubjectById(id);

            if(subject.UserId != userId.Value)
            {
                return Unauthorized(new { message = "Không xác thực được người dùng" });
            }

            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Subjects>> InsertSubject([FromBody] Subjects newSubject)
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Không xác thực được người dùng" });
            }

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            newSubject.UserId = userId.Value;

            var subject = await _subjectService.InsertSubject(newSubject);
            if (subject == null)
            {
                return BadRequest(new { message = "Thêm môn học thất bại" });
            }

            return CreatedAtAction(nameof(GetSubjectById), new { id = subject.Id }, subject);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Subjects>> UpdateSubject([FromBody] Subjects newSubject)
        {
            var userId = GetUserIdFromToken();
            if(userId == null)
            {
                return Unauthorized(new { message = "Không xác thực được người dùng" });
            }

            var subjects = await _subjectService.GetAllSubjects(userId.Value);
            var subject = subjects.FirstOrDefault(s => s.Id == newSubject.Id);
            if (subject == null)
            {
                return NotFound();
            }

            newSubject.UserId = userId.Value;
            subject = newSubject;
            await _subjectService.UpdateSubject(subject);

            return Ok(subject);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Không xác thực được người dùng" });
            }

            var subjects = await _subjectService.GetAllSubjects(userId.Value);
            var subject = subjects.FirstOrDefault(s => s.Id == id);
            if (subject == null)
            {
                return NotFound();
            }

            var subjectExistInSchedule = await _subjectService.CheckSubjectExistInSchedule(id);
            if (subjectExistInSchedule)
            {
                return BadRequest("Bạn không thể xóa môn học này");
            }
            await _subjectService.DeleteSubject(id);
            
            return Ok(subject);
        }

        private int? GetUserIdFromToken()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if(identity != null)
            {
                var userClaims = identity.Claims;
                var userIdClaim = userClaims.FirstOrDefault(c => c.Type == "UserId");
                if(userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;
                }
            }
            return null;
        }
    }
}

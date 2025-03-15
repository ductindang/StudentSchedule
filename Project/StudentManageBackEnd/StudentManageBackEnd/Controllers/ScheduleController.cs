using BusinessLogicLayer.ServicesInterface;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentManageBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedules>>> GetAllSchedules()
        {
            var schedules = await _scheduleService.GetAllSchedules();
            return Ok(schedules);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Schedules>> GetScheduleById(int id)
        {
            var schedule = await _scheduleService.GetScheduleById(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return Ok(schedule);
        }

        [HttpPost]
        public async Task<ActionResult<Schedules>> InsertSchedule(Schedules newSchedule)
        {
            var schedule = await _scheduleService.InsertSchedule(newSchedule);
            if(schedule == null)
            {
                return BadRequest();
            }
            return Ok(schedule);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Schedules>> UpdateSchedule(Schedules newSchedule, int id)
        {
            var schedule = await _scheduleService.GetScheduleById(id);
            if(schedule == null)
            {
                return NotFound();
            }
            newSchedule.Id = id;
            schedule = newSchedule;
            await _scheduleService.UpdateSchedule(schedule);

            return Ok(schedule);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSchedule(int id)
        {
            var schedule = await _scheduleService.DeleteSchedule(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return Ok(schedule);
        }
    }
}

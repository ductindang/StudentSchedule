using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using StudentManageFrontEnd.Models;
using StudentManageFrontEnd.Services.IServices;
using System.Runtime.CompilerServices;

namespace StudentManageFrontEnd.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly ISubjectService _subjectService;
        private string _token;
        //private string _token;

        public ScheduleController(IScheduleService scheduleService, ISubjectService subjectService)
        {
            _scheduleService = scheduleService;
            _subjectService = subjectService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            _token = HttpContext.Session.GetString("AuthToken") ?? string.Empty;
        }

        public async Task<IActionResult> Index()
        {
            var schedules = await _scheduleService.GetScheduleByUserId(_token);
            if(schedules != null)
            {
                var subjects = await _subjectService.GetAllSubject(_token);
                var subjectDict = subjects.ToDictionary(s => s.Id, s => s.Name);

                foreach (var schedule in schedules)
                {
                    if (subjectDict.TryGetValue(schedule.SubjectId, out var subjectName))
                    {
                        schedule.SubjectName = subjectName;
                    }
                }

                // Sắp xếp theo thứ tự trong tuần trước, sau đó theo thời gian bắt đầu
                schedules = schedules.OrderBy(s => s.DayOfWeek)
                                     .ThenBy(s => s.StartTime)
                                     .ToList();
            }

            return View(schedules);
        }


        public async Task<IActionResult> Insert()
        {
            await GetSubjectData();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Schedule schedule)
        {
            await GetSubjectData();
            if (schedule.EndTime <= schedule.StartTime)
            {
                ModelState.AddModelError("EndTime", "Thời gian kết thúc phải sau thời gian bắt đầu.");
            }
            if (ModelState.IsValid)
            {
                var newSchedule = await _scheduleService.InsertSchedule(schedule, _token);
                if(newSchedule == null)
                {
                    TempData["ErrorMessage"] = "Thêm lịch học thất bại, có thể đã trùng lịch học.";
                    return View(schedule);
                }
                return RedirectToAction("Index", "Schedule");
            }
            return View(schedule);
        }

        public async Task<IActionResult> Update(int id)
        {
            var schedule = await _scheduleService.GetScheduleById(id, _token);
            await GetSubjectData();
            return View(schedule);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Schedule schedule)
        {
            if (!ModelState.IsValid)
            {
                await GetSubjectData();
                return View(schedule);
            }

            
            var newSchedule = await _scheduleService.UpdateSchedule(schedule, _token);
            if (newSchedule == null)
            {
                TempData["ErrorMessage"] = "Sửa lịch học thất bại";
                await GetSubjectData();
                return View(newSchedule);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var subject = await _scheduleService.DeleteSchedule(id, _token);
            if (subject == null)
            {
                TempData["ErrorMessage"] = "Xóa lịch học thất bại";
            }

            return RedirectToAction("Index");
        }

        public async Task GetSubjectData()
        {
            var subjects = await _subjectService.GetAllSubject(_token);
            ViewBag.Subjects = new SelectList(subjects, "Id", "Name");
        }
    }
}

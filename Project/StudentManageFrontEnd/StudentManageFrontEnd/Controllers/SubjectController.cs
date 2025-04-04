using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StudentManageFrontEnd.Models;
using StudentManageFrontEnd.Services.IServices;

namespace StudentManageFrontEnd.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectService _subjectService;

        private string _token;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            _token = HttpContext.Session.GetString("AuthToken") ?? string.Empty;
        }

        public async Task<IActionResult> Index()
        {
            var subjects = await _subjectService.GetAllSubject(_token);

            return View(subjects);
        }

        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return View(subject);
            }

            var newSubject = await _subjectService.InsertSubject(subject, _token);
            if (newSubject == null)
            {
                ModelState.AddModelError("", "Thêm môn học thất bại");
                return View(subject);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var subject = await _subjectService.GetSubjectById(id, _token);
            return View(subject);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return View(subject);
            }

            var newSubject = await _subjectService.UpdateSubject(subject, _token);
            if(newSubject == null)
            {
                TempData["ErrorMessage"] = "Sửa môn học thất bại";
                return View(newSubject);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var subject = await _subjectService.DeleteSubject(id, _token);
            if (subject == null)
            {
                TempData["ErrorMessage"] = "Xóa môn học thất bại";
            }

            return RedirectToAction("Index");
        }


    }
}

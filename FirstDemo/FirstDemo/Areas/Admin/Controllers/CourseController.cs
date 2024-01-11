using Autofac;
using FirstDemo.Areas.Admin.Models;
using FirstDemo.Domain.Exceptions;
using FirstDemo.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<CourseController> _logger;

        public CourseController(ILifetimeScope scope,
            ILogger<CourseController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var model = _scope.Resolve<CourseCreateModel>();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);
                    await model.CreateCourseAsync();

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Course created successfuly",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
                catch (DuplicateTitleException de)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = de.Message,
                        Type = ResponseTypes.Danger
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in creating course",
                        Type = ResponseTypes.Danger
                    });
                }
            }

            return View(model);
        }

        public async Task<JsonResult> GetCourses()
        {
            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
            var model = _scope.Resolve<CourseListModel>();
            var data = await model.GetPagedCoursesAsync(dataTablesModel);
            return Json(data);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var model = _scope.Resolve<CourseUpdateModel>();
            await model.LoadAsync(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CourseUpdateModel model)
        {
            model.Resolve(_scope);

            if (ModelState.IsValid)
            {
                try
                {
                    await model.UpdateCourseAsync();
                    return RedirectToAction("Index");
                }
                catch (DuplicateTitleException de)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = de.Message,
                        Type = ResponseTypes.Danger
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in updating course",
                        Type = ResponseTypes.Danger
                    });
                }
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = _scope.Resolve<CourseListModel>();

            if (ModelState.IsValid)
            {
                try
                {
                    await model.DeleteCourseAsync(id); TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Course deleted successfuly",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in deleting course",
                        Type = ResponseTypes.Danger
                    });
                }
            }

            return RedirectToAction("Index");
        }
    }

}




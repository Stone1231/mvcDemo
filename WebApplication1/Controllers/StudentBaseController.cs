using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business;
using Information;
using System.IO;
namespace WebApplication1.Controllers
{
    public partial class StudentBaseController : Controller
    {
        public ActionResult Index(int? page)
        {
            StudentListModel model = new StudentListModel();

            var list = StudentBiz.GetAll();

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            int pageSize = 2;
            while (currentPageIndex > 0 &&
                list.Count <= currentPageIndex * pageSize)
            {
                currentPageIndex--;
            }

            model.List = new MvcPaging.PagedList<StudentInfo>(list, currentPageIndex, pageSize);

            bindList(model);

            return View(model);
        }

        public ActionResult Details(int? Sn)
        {
            if (Sn != null)
            {
                StudentModel model = new StudentModel();
                model.Info = StudentBiz.GetInfo((int)Sn);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            StudentModel model = new StudentModel();
            bindEdit(model);
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Create(StudentModel model)
        {
            if (!ModelState.IsValid)
            {
                bindEdit(model);
                return View("Edit", model);
            }
            if (model.PhotoFile != null)
            {
                var PhotoPath = HttpContext.Server.MapPath("~/Files/Stud");
                if (!Directory.Exists(PhotoPath))
                {
                    Directory.CreateDirectory(PhotoPath);
                }
                var PhotoFileName = Path.Combine(PhotoPath, model.PhotoFile.FileName);
                if (System.IO.File.Exists(PhotoFileName))
                {
                    System.IO.File.Delete(PhotoFileName);
                }
                model.PhotoFile.SaveAs(PhotoFileName);
                model.Info.Photo = model.PhotoFile.FileName;
            }
            StudentBiz.AddNew(model.Info);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? Sn)
        {
            if (Sn != null)
            {
                StudentModel model = new StudentModel();
                model.Info = StudentBiz.GetInfo((int)Sn);
                bindEdit(model);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(StudentModel model)
        {
            if (!ModelState.IsValid)
            {
                bindEdit(model);
                return View(model);
            }
            var info = StudentBiz.GetInfo(model.Info.Sn);
            info.Sn = model.Info.Sn;
            info.Name = model.Info.Name;
            info.ClassId = model.Info.ClassId;
            info.Hight = model.Info.Hight;
            info.Weight = model.Info.Weight;
            info.Birthday = model.Info.Birthday;
            if (model.PhotoFile != null)
            {
                var PhotoPath = HttpContext.Server.MapPath("~/Files/Stud");
                if (!Directory.Exists(PhotoPath))
                {
                    Directory.CreateDirectory(PhotoPath);
                }
                var PhotoFileName = Path.Combine(PhotoPath, model.PhotoFile.FileName);
                if (System.IO.File.Exists(PhotoFileName))
                {
                    System.IO.File.Delete(PhotoFileName);
                }
                model.PhotoFile.SaveAs(PhotoFileName);
                info.Photo = model.PhotoFile.FileName;
            }
            info.Memo = model.Info.Memo;
            StudentBiz.Update(info);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Sn)
        {
            if (StudentBiz.Del(Sn))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return JavaScript("alert('刪除失敗');");
            }
        }

        void bindEdit(StudentModel model)
        {
            model.ClassIdSelect = ClassMBiz.GetAll().Select(s => new SelectListItem
            {
                Value = s.Id,
                Text = s.Name
            });
        }

        void bindList(StudentListModel model)
        {
            model.ClassIdSelect = ClassMBiz.GetAll().Select(s => new SelectListItem
            {
                Value = s.Id,
                Text = s.Name
            });
        }
    }
}

using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using CoreDemo.Models;
using DataAccessLayer.Contrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterRepository());
        Context c = new Context();

        //Logged
        [Authorize]
        public IActionResult Index()
        {
            var userMail = User.Identity.Name;
            ViewBag.v = userMail;
            Context c = new Context();
            var writerName = c.Writers.Where(x => x.Email == userMail).Select(y => y.Name).FirstOrDefault();
            ViewBag.v2 = writerName;
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }
        
        public PartialViewResult WriterNavBarPartial()
        {
            return PartialView();
        }
        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }

        [HttpGet]
        public IActionResult WriterEditProfile()
        {
            var userMail = User.Identity.Name;
            var writerId = c.Writers.Where(x => x.Email == userMail).Select(y => y.WriterId).FirstOrDefault();
            var values = wm.GetWriterListById(writerId);
            var writerValues = wm.TGetById(writerId);
            return View(writerValues);
        }
        //Güncelleme
        [HttpPost]
        public IActionResult WriterEditProfile(Writer writer)
        {
            WriterValidator wl = new WriterValidator();
            ValidationResult results = wl.Validate(writer);
            
            if (results.IsValid)
            {
                wm.TUpdate(writer);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName,item.ErrorMessage);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult WriterAdd()
        {
            return View();
        }

        //ResimEkleme
        [HttpPost]
        public IActionResult WriterAdd(AddProfileImage writer)
        {
            Writer w = new Writer();
            if (writer.Image!=null)
            {
                var extension = Path.GetExtension(writer.Image.FileName);
                var newImage = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFile/", newImage);
                var stream = new FileStream(location, FileMode.Create);
                writer.Image.CopyTo(stream);
                w.Image = newImage;
            }
            w.Email = writer.Email;
            w.Name = writer.Name;
            w.Password = writer.Password;
            w.Status = true;
            w.About = writer.About;
            wm.TAdd(w);
            return RedirectToAction("Index","Dashboard");
        }


    }
}

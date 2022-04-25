using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using CoreDemo.Models;
using DataAccessLayer.Contrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;

        public WriterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


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
        public async Task<IActionResult> WriterEditProfile()
        {
            //var username = User.Identity.Name;
            //var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            //UserManager userManager = new UserManager(new EfUserRepository());
            //var id = c.Users.Where(x => x.Email == usermail).Select(y => y.Id).FirstOrDefault();
            //var writerValues = userManager.TGetById(id);

            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserUpdateViewModel model = new UserUpdateViewModel();
            model.mail = values.Email;
            model.namesurname = values.NameSurname;
            model.imageurl = values.ImageUrl;
            model.username = values.UserName;
            return View(model);
        }

        //[HttpGet]
        //public IActionResult WriterEditProfile()
        //{
        //    var username = User.Identity.Name;
        //    var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
        //    UserManager userManager = new UserManager(new EfUserRepository());
        //    //var writerId = c.Writers.Where(x => x.Email == usermail).Select(y => y.WriterId).FirstOrDefault();
        //    //var values = wm.GetWriterListById(writerId);
        //    var id = c.Users.Where(x => x.Email == usermail).Select(y => y.Id).FirstOrDefault();
        //    var writerValues = userManager.TGetById(id);
        //    return View(writerValues);
        //}



        //Güncelleme

        [HttpPost]
        public async Task<IActionResult> WriterEditProfile(UserUpdateViewModel model)
        {
            //WriterValidator wl = new WriterValidator();
            //ValidationResult results = wl.Validate(writer);
            //if (results.IsValid)
            //{
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            values.NameSurname = model.namesurname;
            values.ImageUrl = model.imageurl;
            values.Email = model.mail;
            values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, model.password);
            var result = await _userManager.UpdateAsync(values);
                return RedirectToAction("Index", "Dashboard");
            //}
            //else
            //{
            //    foreach (var item in results.Errors)
            //    {
            //        ModelState.AddModelError(item.PropertyName,item.ErrorMessage);
            //    }
            //}
            //return View();
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

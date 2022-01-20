using battleFaceDataAccess;
using battleFaceDataAccess.Models;
using battleFaceJWTAuth.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using battleFaceJWTAuth.Security;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace battleFaceJWTAuth.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDBContext _db;

        public AccountController(ApplicationDBContext applicationDBContext)
        {
            _db = applicationDBContext;
        }

        public IActionResult Index()
        {
            return View();
        }

       
        [HttpPost]
        public IActionResult Index(UserViewModel model)
        {
            if (ModelState.IsValid)
            {           
                User user = new User { Id = Guid.NewGuid().ToString(), UserName = model.UserName };
                string passwordHash = SecurityManager.HashPassword(user, model.Password);
                var newUser = UserRepo.SaveUser(_db, user, passwordHash);

                if(newUser == null)
                {
                    ViewBag.ErrorMessage = "The username already exists.";
                    return View("index");
                }
                return RedirectToAction("login");
            }

            return Redirect("/home/error");
        }

        public IActionResult Login()
        {
            return View();
        }

        //This is for the View
        
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = SecurityManager.GetUser(_db, model.UserName);
                try
                {
                    if (SecurityManager.CheckPassword(_db, user, model.Password))
                    {
                        string token = SecurityManager.BuildToken(user);


                        HttpContext.Session.SetString("JWToken", token);

                        return Redirect("/quotation");
                    }

                    ViewBag.ErrorMessage = "Wrong Credentials";
                    return View("login");
                }
                catch (Exception ex)
                {
                    return Redirect("/home/error");
                }
            }
            return Redirect("/home/error");
        }


        //This is for the API
        [Route("/refreshtoken")]
        [HttpPost]
        public IActionResult RefreshToken([FromBody] LoginViewModel model)
        {
            User user = SecurityManager.GetUser(_db, model.UserName);
            try
            {
                if (SecurityManager.CheckPassword(_db, user, model.Password))
                {
                    string token = SecurityManager.BuildToken(user);

                    return new ObjectResult(new { Access_Token = token });
                }

                return Unauthorized();
            }
            catch(NullReferenceException ex)
            {
                return BadRequest();
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LoginandRegistration.Models;
using Microsoft.AspNetCore.Http;

namespace LoginandRegistration.Controllers
{
    public class HomeController : Controller
    {
        private UserContext context;

        private PasswordHasher<User> RegisterHasher = new PasswordHasher<User>();
        private PasswordHasher<LoginUser> LoginHasher = new PasswordHasher<LoginUser>();

        public HomeController(UserContext user)
        {
            context = user;
        }
        
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Login")]
        public IActionResult LoginPage()
        {
            return View("Login");
        }

        [HttpPost("Register")]
        public IActionResult Register(User u)
        {
            if(ModelState.IsValid)
            {
                u.Password = RegisterHasher.HashPassword(u, u.Password);
                context.Users.Add(u);
                context.SaveChanges();
                HttpContext.Session.SetInt32("UserId", u.UserId);
                return View("Success");
            }
            return View("Index", u);
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginUser l)
        {
            if(ModelState.IsValid)
            {
            User logging_in_user = context.Users.FirstOrDefault(u => u.Email == l.LoginEmail);  
            if(logging_in_user != null)
                {
                    var result = LoginHasher.VerifyHashedPassword(l, logging_in_user.Password, l.LoginPassword);
                    if (result == 0)
                    {
                        ModelState.AddModelError("LoginPassword", "Invalid Password");

                    }
                    else
                    {
                        HttpContext.Session.SetInt32("UserId", logging_in_user.UserId);
                        return View("Success");
                    }
                }
            else
                {
                ModelState.AddModelError("LoginEmail", "Invalid Email");   
                }
                
            }
            return View("Login", l);
        }

        [HttpGet("Success")]
        public IActionResult Success()
            {
                int? UserId =HttpContext.Session.GetInt32("UserId");
                if(UserId == null)
                {
                    return Redirect("/");
                }
                return View("Success");
            }
        [HttpGet("Logout")]
        public IActionResult Logout()
            {
                HttpContext.Session.Remove("UserId");
                return Redirect("/");
            }
        }
    }


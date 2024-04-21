using DocSpot.Models;
using DocSpot.Repository.DAL.Interfaces;
using DocSpot.ViewModels;
using DocSpot.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace DocSpot.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGenericRepository<Patient> _repositoryPatient;
        private readonly IGenericRepository<Admin> _repositoryAdmin;
        private readonly IGenericRepository<Doctor> _repositoryDoctor;
        public AccountController(IGenericRepository<Patient> repositoryPatient,
                               IGenericRepository<Admin> repositoryAdmin,
                                 IGenericRepository<Doctor> repositoryDoctor)
        {
            _repositoryAdmin = repositoryAdmin;
            _repositoryPatient = repositoryPatient;
            _repositoryDoctor = repositoryDoctor;
        }



        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            switch (model.AccountType)
            {
                case "1":
                    Expression<Func<Admin, bool>> filter = x => x.Username == model.UserName && x.Password == model.Password;
                    var login = _repositoryAdmin.GetAll(filter).FirstOrDefault();
                    if (login != null)
                    {
                        HttpContext.Session.SetInt32("AdminId", login.Id);
                        return RedirectToAction("Index", "Admin");
                    }
                    ModelState.AddModelError(String.Empty, "Invalid Credentials");
                    return View(model);
                case "2":
                    Expression<Func<Doctor, bool>> docFilter = x => x.Username == model.UserName && x.Password == model.Password;
                    var doc = _repositoryDoctor.GetAll(docFilter).FirstOrDefault();
                    if (doc != null)
                    {
                        HttpContext.Session.SetInt32("DoctorId", doc.Id);
                        return RedirectToAction("Index", "Doctor");
                    }
                    ModelState.AddModelError(String.Empty, "Invalid Credentials");
                    return View(model);
                case "3":
                    Expression<Func<Patient, bool>> patFilter = x => x.Username == model.UserName && x.Password == model.Password;
                    var pat = _repositoryPatient.GetAll(patFilter).FirstOrDefault();
                    if (pat != null)
                    {
                        HttpContext.Session.SetInt32("PatientId", pat.Id);
                        return RedirectToAction("Index", "Patient");
                    }
                    ModelState.AddModelError(String.Empty, "Invalid Credentials");
                    return View(model);
                default:
                    return View(model);

            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVM model)
        {
            Expression<Func<Patient, bool>> patFilter = x => x.Username == model.Username;
            var pat = _repositoryPatient.GetAll(patFilter).FirstOrDefault();
            if (pat != null)
            {
                ModelState.AddModelError(String.Empty, "Username already exists! Pick another username.");
                return View(model);
            }
            else
            {
                var add = new Patient()
                {
                    Username = model.Username,
                    Password = model.Password,
                    Address = model.Address,
                    DOB = (DateTime)model.DOB,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Mobile = model.Mobile,
                };
                _repositoryPatient.Add(add);
                return RedirectToAction("Login", "Login");
            }
           
        }
    }
}


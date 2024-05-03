using DocSpot.Models;
using DocSpot.Repository.DAL.Interfaces;
using DocSpot.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DocSpot.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        //private readonly IGenericRepository<Patient> _repositoryPatient;
        //private readonly IGenericRepository<Admin> _repositoryAdmin;
        //private readonly IGenericRepository<Doctor> _repositoryDoctor;
        //public AccountController(IGenericRepository<Patient> repositoryPatient,
        //                       IGenericRepository<Admin> repositoryAdmin,
        //                         IGenericRepository<Doctor> repositoryDoctor)
        //{
        //    _repositoryAdmin = repositoryAdmin;
        //    _repositoryPatient = repositoryPatient;
        //    _repositoryDoctor = repositoryDoctor;
        //}

        //[HttpPost]
        //public IActionResult Login(LoginVM model)
        //{
        //    switch (model.AccountType)
        //    {
        //        case 1:
        //            Expression<Func<Admin, bool>> filter = x => x.Username == model.UserName && x.Password == model.Password;
        //            var login = _repositoryAdmin.GetAll(filter).FirstOrDefault();
        //            if (login != null)
        //            {
        //            }
        //            ModelState.AddModelError(String.Empty, "Invalid Credentials");
        //            return View(model);
        //        case 2:
        //            Expression<Func<Doctor, bool>> docFilter = x => x.Username == model.UserName && x.Password == model.Password;
        //            var doc = _repositoryDoctor.GetAll(docFilter).FirstOrDefault();
        //            if (doc != null)
        //            {
        //                HttpContext.Session.SetInt32("DoctorId", doc.Id);
        //                return RedirectToAction("Index", "Doctor");
        //            }
        //            ModelState.AddModelError(String.Empty, "Invalid Credentials");
        //            return View(model);
        //        case 3:
        //            Expression<Func<Patient, bool>> patFilter = x => x.Username == model.UserName && x.Password == model.Password;
        //            var pat = _repositoryPatient.GetAll(patFilter).FirstOrDefault();
        //            if (pat != null)
        //            {
        //                HttpContext.Session.SetInt32("PatientId", pat.Id);
        //                return RedirectToAction("Index", "Patient");
        //            }
        //            ModelState.AddModelError(String.Empty, "Invalid Credentials");
        //            return View(model);
        //        default:
        //            return View(model);

        //    }
        //}

        //public IActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Register(RegisterVM model)
        //{
        //    Expression<Func<Patient, bool>> patFilter = x => x.Username == model.Username;
        //    var pat = _repositoryPatient.GetAll(patFilter).FirstOrDefault();
        //    if (pat != null)
        //    {
        //        ModelState.AddModelError(String.Empty, "Username already exists! Pick another username.");
        //        return View(model);
        //    }
        //    else
        //    {
        //        var add = new Patient()
        //        {
        //            Username = model.Username,
        //            Password = model.Password,
        //            Address = model.Address,
        //            DOB = (DateTime)model.DOB,
        //            Email = model.Email,
        //            FirstName = model.FirstName,
        //            LastName = model.LastName,
        //            Mobile = model.Mobile,
        //        };
        //        _repositoryPatient.Add(add);
        //        return RedirectToAction("Login", "Account");
        //    }

        //}

        //public IActionResult Logout()
        //{
        //    HttpContext.Session.Clear();

        //    return RedirectToAction("Index", "Admin");
        //}


        //private JwtSecurityToken CreateToken(List<Claim> authClaims)
        //{
        //    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        //    _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

        //    var token = new JwtSecurityToken(
        //        issuer: _configuration["JWT:ValidIssuer"],
        //        audience: _configuration["JWT:ValidAudience"],
        //        expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
        //        claims: authClaims,
        //        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        //        );

        //    return token;
        //}

        //private static string GenerateRefreshToken()
        //{
        //    var randomNumber = new byte[64];
        //    using var rng = RandomNumberGenerator.Create();
        //    rng.GetBytes(randomNumber);
        //    return Convert.ToBase64String(randomNumber);
        //}

    }
}

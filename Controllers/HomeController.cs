using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GuideOfTurkey_AdminPanel.Models;
using GuideOfTurkey_AdminPanel.Data;
using GuideOfTurkey_AdminPanel.ViewModels;
using BCrypt;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace GuideOfTurkey_AdminPanel.Controllers
{
    public class HomeController : Controller
    {
        private readonly GuideContext db;
        private readonly IHostingEnvironment hostingEnviroment;
        public HomeController(GuideContext context, IHostingEnvironment environment)
        {
            db = context;
            hostingEnviroment = environment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            string cookieID = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieID);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    return RedirectToAction("Home");
                }
                else 
                {
                    return View();
                }
            }
            else 
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult Index(string phoneNumber, string password)
        {
            var result = db.userAccounts.Where(x => x.PhoneNumber == phoneNumber && x.userRank == true).FirstOrDefault();
            if(result != null)
            {
                bool validPassword = BCrypt.Net.BCrypt.Verify(password, result.Password);
                if(validPassword)
                {
                    string text = result.PhoneNumber + result.Fullname + result.ID;
                    string token = BCrypt.Net.BCrypt.HashPassword(text);
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddHours(3);
                    Response.Cookies.Append("token", token, option);
                    Response.Cookies.Append("userId", result.ID.ToString(), option);
                    return RedirectToAction("Home");
                }
                else
                {
                    ViewData["loginResponse"] = "wrongEntry";   
                    return View();
                }
            }
            else 
            {
                ViewData["loginResponse"] = "wrongEntry";   
                return View();
            }
        }
        [HttpGet]
        [Route("/home")]
        public IActionResult Home()
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    return View();
                }
                else 
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        [Route("/users")]
        public IActionResult Users()
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var userData = db.userAccounts.ToList();
                    return View(userData);
                }
                else 
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [Route("/users/disableuser")]
        public string UsersDelete(int userID)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieId != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var userAcc = db.userAccounts.Where(x => x.ID == userID).FirstOrDefault();
                    if(userAcc.deleteState)
                    {
                        userAcc.deleteState = false;
                    }
                    else
                    {
                        userAcc.deleteState = true;
                    }
                    var saveControl = db.SaveChanges();
                    if(saveControl == 1)
                    {
                        return "success";
                    }
                    else
                    {
                        return "saveControlPorblem";
                    }
                }
                else
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpGet]
        [Route("/sliders")]
        public IActionResult Sliders()
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var data = db.Photos.Where(x => x.deleteState == false).ToList();
                    return View(data);
                }
                else 
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [Route("/sliders/change")]
        public string Sliders(List<int> idList, List<int> oldId)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    foreach(int old in oldId)
                    {
                        var data = db.Photos.Where(x => x.ID == old).FirstOrDefault();
                        data.sliderState = false;
                    }
                    foreach(int y in idList)
                    {
                        var dataTwo = db.Photos.Where(x => x.ID == y).FirstOrDefault();
                        dataTwo.sliderState = true;
                    }
                    int control = db.SaveChanges();
                    return "success";
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpPost]
        [Route("/sliders/addphoto")]
        public string Sliders(IFormFile photo)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    if(photo != null)
                    {
                        string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "sliderPhotos");
                        string uniqeFileName = Guid.NewGuid().ToString() + " " + photo.FileName;
                        string filePath =  Path.Combine(uploadsFolder, uniqeFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                        Photo slidePhoto = new Photo
                        {
                            sliderState = false,
                            photoUrl = uniqeFileName,
                        };
                        db.Photos.Add(slidePhoto);
                        int control = db.SaveChanges();
                        if(control == 1)
                        {
                            RedirectToAction("Sliders");
                            return "success";
                        }
                        else
                        {
                            return "saveControlError";
                        }
                    }
                    else
                    {
                        return "noPhoto";
                    }
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpGet]
        [Route("/types")]
        public IActionResult Types()
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var types = db.Types.Where(x => x.deleteState == false).ToList();
                    return View(types);
                }
                else 
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [Route("/types/add")]
        public string TypesAddType(int typeId, string name, IFormFile photo)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    if(photo != null)
                    {
                        string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "typePhotos");
                        string uniqeFileName = Guid.NewGuid().ToString() + " " + photo.FileName;
                        string filePath =  Path.Combine(uploadsFolder, uniqeFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                        PlaceType newType = new PlaceType
                        {
                            TypeName = name,
                            homepageState = false,
                            photoUrl = uniqeFileName
                        }; 
                        db.Types.Add(newType);
                        int saveControl = db.SaveChanges();
                        if(saveControl == 1)
                        {
                            RedirectToAction("Types");
                            return "success";
                        }
                        else
                        {
                            return "saveControlError";
                        }
                    }
                    else
                    {
                        return "photoError";
                    }
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpPost]
        [Route("/types/updatephoto")]
        public string Types(IFormFile photo, int typeId)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "typePhotos");
                    string uniqeFileName = Guid.NewGuid().ToString() + " " + photo.FileName;
                    string filePath =  Path.Combine(uploadsFolder, uniqeFileName);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    var data = db.Types.Where(x => x.ID == typeId).FirstOrDefault();
                    data.photoUrl = uniqeFileName;
                    int saveControl = db.SaveChanges();
                    if(saveControl == 1)
                    {
                        RedirectToAction("Types");
                        return "success";
                    }
                    else
                    {
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpPost]
        [Route("types/deletephoto")]
        public string TypesDeletePhoto(int typeId)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var data = db.Types.Where(x => x.ID == typeId).FirstOrDefault();
                    data.photoUrl = null;
                    int saveControl = db.SaveChanges();
                    if(saveControl == 1)
                    {
                        return "success";
                    }
                    else
                    {
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpPost]
        [Route("types/deletetype")]
        public string Types(int typeId)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var data = db.Types.Where(x => x.ID == typeId).FirstOrDefault();
                    data.deleteState = true;
                    int saveControl = db.SaveChanges();
                    if(saveControl == 1)
                    {
                        return "success";
                    }
                    else
                    {
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpPost]
        [Route("types/addhomepage")]
        public string TypesAddHomePages(int typeId)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var data = db.Types.Where(x => x.ID == typeId).FirstOrDefault();
                    if(data.homepageState)
                    {
                        data.homepageState = false;
                    }
                    else
                    {
                        data.homepageState = true;
                    }
                    int saveControl = db.SaveChanges();
                    if(saveControl == 1)
                    {
                        return "success";
                    }
                    else
                    {
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpGet]
        [Route("/cities")]
        public IActionResult Cities()
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var data = db.Cities.Where(x => x.deleteState == false).ToList();
                    return View(data);
                }
                else 
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        [Route("/countries")]
        public IActionResult Countries()
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var data = db.Countries.Where(x => x.deleteState == false).ToList();
                    return View(data);
                }
                else 
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        [Route("/city/getCountry")]
        public string GetCountry()
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var data = db.Countries.Where(x => x.deleteState == false).ToList();
                    string json = JsonConvert.SerializeObject(data);
                    return json;
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpGet]
        [Route("/districts")]
        public IActionResult Districts()
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var data = db.Districts.Where(x => x.deleteState == false).ToList();
                    return View(data);
                }
                else 
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        [Route("/district/getCity")]
        public string GetCity()
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var data = db.Cities.Where(x => x.deleteState == false).ToList();
                    string json = JsonConvert.SerializeObject(data);
                    return json;
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpPost]
        [Route("/country/deletephoto")]
        public string Country(int photoID) 
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var data = db.Countries.Where(x => x.ID == photoID).FirstOrDefault();
                    data.photoUrl = null;
                    int control = db.SaveChanges();
                    if(control == 1)
                    {
                        return "success";
                    }
                    else
                    {
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpPost]
        [Route("/city/deletephoto")]
        public string City(int photoID)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var data = db.Cities.Where(x => x.ID == photoID).FirstOrDefault();
                    data.photoUrl = null;
                    int control = db.SaveChanges();
                    if(control == 1)
                    {
                        return "success";
                    }
                    else
                    {
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpPost]
        [Route("/district/deletephoto")]
        public string District(int photoID)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var data = db.Districts.Where(x => x.ID == photoID).FirstOrDefault();
                    data.photoUrl = null;
                    int control = db.SaveChanges();
                    if(control == 1)
                    {
                        return "success";
                    }
                    else
                    {
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpPost]
        [Route("country/updatephoto")]
        public string Country(IFormFile photo, int countryId)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "countryPhotos");
                    string uniqeFileName = Guid.NewGuid().ToString() + " " + photo.FileName;
                    string filePath =  Path.Combine(uploadsFolder, uniqeFileName);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    var data = db.Countries.Where(x => x.ID == countryId).FirstOrDefault();
                    data.photoUrl = uniqeFileName;
                    int control = db.SaveChanges();
                    if(control == 1)
                    {
                        RedirectToAction("cities");
                        return "success";
                    }
                    else
                    {
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpPost]
        [Route("city/updatephoto")]
        public string City(IFormFile photo, int cityID)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "cityPhotos");
                    string uniqeFileName = Guid.NewGuid().ToString() + " " + photo.FileName;
                    string filePath =  Path.Combine(uploadsFolder, uniqeFileName);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    var data = db.Cities.Where(x => x.ID == cityID).FirstOrDefault();
                    data.photoUrl = uniqeFileName;
                    int control = db.SaveChanges();
                    if(control == 1)
                    {
                        RedirectToAction("cities");
                        return "success";
                    }
                    else
                    {
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpPost]
        [Route("district/updatephoto")]
        public string District(IFormFile photo, int districtID)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "districtPhotos");
                    string uniqeFileName = Guid.NewGuid().ToString() + " " + photo.FileName;
                    string filePath =  Path.Combine(uploadsFolder, uniqeFileName);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    var data = db.Districts.Where(x => x.ID == districtID).FirstOrDefault();
                    data.photoUrl = uniqeFileName;
                    int control = db.SaveChanges();
                    if(control == 1)
                    {
                        RedirectToAction("cities");
                        return "success";
                    }
                    else
                    {
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpPost]
        [Route("country/galleryadd")]
        public string CountryPhoto(IFormFile photo, int countryId)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "countryPhotos");
                    string uniqeFileName = Guid.NewGuid().ToString() + " " + photo.FileName;
                    string filePath =  Path.Combine(uploadsFolder, uniqeFileName);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    CountryGallery newCG = new CountryGallery
                    {
                        photoUrl = uniqeFileName,
                        CountryID = countryId
                    };
                    db.CountryGalleries.Add(newCG);
                    int control = db.SaveChanges();
                    if(control == 1)
                    {
                        RedirectToAction("Cities");
                        return "success";
                    }
                    else
                    {
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpPost]
        [Route("city/galleryadd")]
        public string CityPhoto(IFormFile photo, int cityID)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "cityPhotos");
                    string uniqeFileName = Guid.NewGuid().ToString() + " " + photo.FileName;
                    string filePath =  Path.Combine(uploadsFolder, uniqeFileName);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    CityGallery newCG = new CityGallery
                    {
                        photoUrl = uniqeFileName,
                        CityID = cityID
                    };
                    db.CityGalleries.Add(newCG);
                    int control = db.SaveChanges();
                    if(control == 1)
                    {
                        RedirectToAction("Cities");
                        return "success";
                    }
                    else
                    {
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpPost]
        [Route("district/galleryadd")]
        public string DistrictPhoto(IFormFile photo, int districtID)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "districtPhotos");
                    string uniqeFileName = Guid.NewGuid().ToString() + " " + photo.FileName;
                    string filePath =  Path.Combine(uploadsFolder, uniqeFileName);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    DistrictGallery newCG = new DistrictGallery
                    {
                        photoUrl = uniqeFileName,
                        districtID = districtID
                    };
                    db.DistrictGalleries.Add(newCG);
                    int control = db.SaveChanges();
                    if(control == 1)
                    {
                        RedirectToAction("Cities");
                        return "success";
                    }
                    else
                    {
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpPost]
        [Route("country/deletecountry")]
        public string CountryDelete(int countryId)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var data = db.Countries.Where(x => x.ID == countryId).FirstOrDefault();
                    data.deleteState = true;
                    int control = db.SaveChanges();
                    if(control == 1)
                    {
                        return "success";
                    }
                    else
                    {
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "loginError";
                }
            }
            else
            {
                return "tokenError";
            }
        }
        [HttpPost]
        [Route("city/deletecity")]
        public string CityDelete(int cityID)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var data = db.Cities.Where(x => x.ID == cityID).FirstOrDefault();
                    data.deleteState = true;
                    int control = db.SaveChanges();
                    if(control == 1)
                    {
                        return "success";
                    }
                    else
                    {
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "loginError";
                }
            }
            else
            {
                return "tokenError";
            }
        }
        [HttpPost]
        [Route("district/deletedistrict")]
        public string DistrictDelete(int districtID)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var data = db.Districts.Where(x => x.ID == districtID).FirstOrDefault();
                    data.deleteState = true;
                    int control = db.SaveChanges();
                    if(control == 1)
                    {
                        return "success";
                    }
                    else
                    {
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "loginError";
                }
            }
            else
            {
                return "tokenError";
            }
        }
        [HttpPost]
        [Route("city/homepagestate")]
        public string CityHomePage(int cityID)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var data = db.Cities.Where(x => x.ID == cityID).FirstOrDefault();
                    if(data.homepageState)
                    {
                        data.homepageState = false;
                    }
                    else
                    {
                        data.homepageState = true;
                    }
                    int control = db.SaveChanges();
                    if(control == 1)
                    {
                        return "success";
                    }
                    else
                    {
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "loginError";
                }
            }
            else
            {
                return "tokenError";
            }
        }
        [HttpPost]
        [Route("country/add")]
        public string CountryAdd(IFormFile photo, string name, string explain)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "countryPhotos");
                    string uniqeFileName = Guid.NewGuid().ToString() + " " + photo.FileName;
                    string filePath =  Path.Combine(uploadsFolder, uniqeFileName);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    Country newC = new Country
                    {
                        Explain = explain,
                        Name = name,
                        photoUrl = uniqeFileName
                    };
                    db.Countries.Add(newC);
                    int control = db.SaveChanges();
                    if(control == 1)
                    {
                        RedirectToAction("cities");
                        return "success";
                    }
                    else
                    {
                        RedirectToAction("cities");
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "loginError";
                }
            }
            else
            {
                return "tokenError";
            }
        }
        [HttpPost]
        [Route("city/add")]
        public string CityAdd(IFormFile photo, string name, string explain, int cityCountry)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "cityPhotos");
                    string uniqeFileName = Guid.NewGuid().ToString() + " " + photo.FileName;
                    string filePath =  Path.Combine(uploadsFolder, uniqeFileName);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    City newC = new City
                    {
                        Explain = explain,
                        Name = name,
                        photoUrl = uniqeFileName,
                        CountryID = cityCountry,
                        homepageState = false,
                    };
                    db.Cities.Add(newC);
                    int control = db.SaveChanges();
                    if(control == 1)
                    {
                        RedirectToAction("cities");
                        return "success";
                    }
                    else
                    {
                        RedirectToAction("cities");
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "loginError";
                }
            }
            else
            {
                return "tokenError";
            }
        }
        [HttpPost]
        [Route("district/add")]
        public string DistrictAdd(IFormFile photo, string name, string explain, int districtCity)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "cityPhotos");
                    string uniqeFileName = Guid.NewGuid().ToString() + " " + photo.FileName;
                    string filePath =  Path.Combine(uploadsFolder, uniqeFileName);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    District newC = new District
                    {
                        Explain = explain,
                        Name = name,
                        photoUrl = uniqeFileName,
                        CityID = districtCity
                    };
                    db.Districts.Add(newC);
                    int control = db.SaveChanges();
                    if(control == 1)
                    {
                        RedirectToAction("cities");
                        return "success";
                    }
                    else
                    {
                        RedirectToAction("cities");
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "loginError";
                }
            }
            else
            {
                return "tokenError";
            }
        }
        [HttpGet]
        [Route("places/getdistrict")]
        public string GetDistrict()
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var data = db.Districts.Where(x => x.deleteState == false).ToList();
                    string json = JsonConvert.SerializeObject(data);
                    return json;
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpGet]
        [Route("places/gettypes")]
        public string GetTypes()
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var data = db.Types.Where(x => x.deleteState == false).ToList();
                    string json = JsonConvert.SerializeObject(data);
                    return json;
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpGet]
        [Route("/places")]
        public IActionResult Places()
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var data = db.Places.Where(x => x.deleteState == false).ToList();
                    return View(data);
                }
                else 
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [Route("places/add")]
        public string PlacesAdd(IFormFile photo, string name, string explain, int placeDistrict, int placeType)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "placePhotos");
                    string uniqeFileName = Guid.NewGuid().ToString() + " " + photo.FileName;
                    string filePath =  Path.Combine(uploadsFolder, uniqeFileName);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    Place newC = new Place
                    {
                        Explain = explain,
                        Name = name,
                        photoUrl = uniqeFileName,
                        DistrictID = placeDistrict,
                        TypeID = placeType
                    };
                    db.Places.Add(newC);
                    int control = db.SaveChanges();
                    if(control == 1)
                    {
                        RedirectToAction("places");
                        return "success";
                    }
                    else
                    {
                        RedirectToAction("places");
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "loginError";
                }
            }
            else
            {
                return "tokenError";
            }
        }
        [HttpPost]
        [Route("/places/deletephoto")]
        public string PlacesDelete(int photoID)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var data = db.Places.Where(x => x.ID == photoID).FirstOrDefault();
                    data.photoUrl = null;
                    int control = db.SaveChanges();
                    if(control == 1)
                    {
                        return "success";
                    }
                    else
                    {
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpPost]
        [Route("places/updatephoto")]
        public string PlacesUpdatePhoto(IFormFile photo, int placeID)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "placePhotos");
                    string uniqeFileName = Guid.NewGuid().ToString() + " " + photo.FileName;
                    string filePath =  Path.Combine(uploadsFolder, uniqeFileName);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    var data = db.Places.Where(x => x.ID == placeID).FirstOrDefault();
                    data.photoUrl = uniqeFileName;
                    int control = db.SaveChanges();
                    if(control == 1)
                    {
                        RedirectToAction("places");
                        return "success";
                    }
                    else
                    {
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpPost]
        [Route("places/galleryadd")]
        public string PlacesGallery(IFormFile photo, int placeID)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "districtPhotos");
                    string uniqeFileName = Guid.NewGuid().ToString() + " " + photo.FileName;
                    string filePath =  Path.Combine(uploadsFolder, uniqeFileName);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    PlaceGallery newPG = new PlaceGallery
                    {
                        photoUrl = uniqeFileName,
                        PlaceId = placeID
                    };
                    db.PlaceGalleries.Add(newPG);
                    int control = db.SaveChanges();
                    if(control == 1)
                    {
                        RedirectToAction("Cities");
                        return "success";
                    }
                    else
                    {
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "tokenError";
                }
            }
            else
            {
                return "loginError";
            }
        }
        [HttpPost]
        [Route("places/deleteplace")]
        public string PlaceDelete(int placeID)
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    var data = db.Places.Where(x => x.ID == placeID).FirstOrDefault();
                    data.deleteState = true;
                    int control = db.SaveChanges();
                    if(control == 1)
                    {
                        return "success";
                    }
                    else
                    {
                        return "saveControlError";
                    }
                }
                else 
                {
                    return "loginError";
                }
            }
            else
            {
                return "tokenError";
            }
        }
        [HttpGet]
        [Route("/comments")]
        public IActionResult Comments()
        {
            string cookieId = Request.Cookies["userId"];
            string cookieToken = Request.Cookies["token"];
            if(cookieToken != null && cookieToken != null)
            {
                int id = Convert.ToInt16(cookieId);
                var user = db.userAccounts.Where(x => x.ID == id && x.userRank == true).FirstOrDefault();
                string dbToken = user.PhoneNumber + user.Fullname + user.ID;
                bool validToken = BCrypt.Net.BCrypt.Verify(dbToken, cookieToken);
                if(validToken)
                {
                    return View();
                }
                else 
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        [Route("/logout")]
        public IActionResult Logout()
        {
            foreach (var cookie in Request.Cookies)
            {
                Response.Cookies.Delete(cookie.Key);
                Response.Cookies.Delete(cookie.Value);
            }
            return RedirectToAction("Index");
        }
    }
}
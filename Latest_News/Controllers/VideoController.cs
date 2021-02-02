using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Latest_News.Models;
using Latest_News.Models.Rep;
using Latest_News.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Latest_News.Controllers
{
    public class VideoController : Controller
    {
        public Repository<Video_News> Rep_News { get; }
        public UserManager<AppUsers> UserManager { get; }
        public SignInManager<AppUsers> SignIn { get; }
        public IHostingEnvironment Hosting { get; }

        public VideoController(Repository<Video_News> rep_News,
             UserManager<AppUsers> userManager,
            SignInManager<AppUsers> signIn,
            IHostingEnvironment hosting)
        {
            Rep_News = rep_News;
            UserManager = userManager;
            SignIn = signIn;
            Hosting = hosting;
        }
        // GET: Video
        public ActionResult Index()
        {
            if (SignIn.IsSignedIn(User))
            {

                if (get_Admin() == "Admin")
                {
                    var vid = Rep_News.List();
            return View(vid);
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // GET: Video/Details/5
        public ActionResult Details(int id)
        {
                    if (SignIn.IsSignedIn(User))
                    {

                        if (get_Admin() == "Admin")
                        {
                            var find_ = Rep_News.Find(id);
            return View(find_);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    return RedirectToAction("login", "Account");
                }

        // GET: Video/Create
        public ActionResult Create()
        {
                            if (SignIn.IsSignedIn(User))
                            {

                                if (get_Admin() == "Admin")
                                {
                                    return View();
                                }
                                else
                                {
                                    return NotFound();
                                }
                            }
                            return RedirectToAction("login", "Account");
                        }

        // POST: Video/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Video_ViewModel models)
        {
            if (SignIn.IsSignedIn(User))
            {

                if (get_Admin() == "Admin")
                {
                    try
            {
                string file_Name = "";
                if (models.file.FileName != null)
                {
                    string chemain = Path.Combine(Hosting.WebRootPath, "Video_News");
                    file_Name = models.file.FileName;
                    string path = Path.Combine(chemain, file_Name);
                    models.file.CopyTo(new FileStream(path, FileMode.Create));
                }
                var vid = new Video_News()
                {
                    date_Pub = DateTime.Now,
                    titre = models.titre,
                    Vid = file_Name
                };
                Rep_News.Add(vid);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // GET: Video/Edit/5
        public ActionResult Edit(int id)
        {
                    if (SignIn.IsSignedIn(User))
                    {

                        if (get_Admin() == "Admin")
                        {
                            var find_ = Rep_News.Find(id);
            var model = new Video_ViewModel()
            {
                titre = find_.titre,
                Vid = find_.Vid
            };
            return View(model);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    return RedirectToAction("login", "Account");
                }

        // POST: Video/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Video_ViewModel models)
        {
            if (SignIn.IsSignedIn(User))
            {

                if (get_Admin() == "Admin")
                {
                    if (ModelState.IsValid)
            {
                try
                {
                    var find_ = Rep_News.Find(id);
                    string file_Name = "";
                    string vid = find_.Vid;
                    if (models.file.FileName != null)
                    {
                        string chemain = Path.Combine(Hosting.WebRootPath, "Video_News");
                        file_Name = models.file.FileName;
                        string path = Path.Combine(chemain, file_Name);
                        string old_Paht = Path.Combine(chemain, vid);
                        if (old_Paht != path)
                        {
                            System.IO.File.Delete(old_Paht);
                            models.file.CopyTo(new FileStream(path, FileMode.Create));
                        }
                    }
                    else
                    {
                        file_Name = vid;
                    }
                    find_.titre = models.titre;
                    find_.Vid = models.Vid;
                    Rep_News.Update(find_);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View(models);
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // GET: Video/Delete/5
        public ActionResult Delete(int id)
        {
            if (SignIn.IsSignedIn(User))
            {

                if (get_Admin() == "Admin")
                {
                    var find_ = Rep_News.Find(id);
            return View(find_);
        }
                else
                {
                    return NotFound();
    }
}
            return RedirectToAction("login", "Account");
        }

        // POST: Video/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            if (SignIn.IsSignedIn(User))
            {

                if (get_Admin() == "Admin")
                {
                    try
            {
                Rep_News.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        else
        {
            return NotFound();
        }
    }
    return RedirectToAction("login", "Account");
}
        public string get_Admin()
        {
            var userId = UserManager.GetUserId(User);
            var user = UserManager.Users.SingleOrDefault(u => u.Id == userId);
            if (user.UserType == "Admin")
            {
                return "Admin";
            }
            else
            {
                return "no Admin";
            }
        }
    }
}
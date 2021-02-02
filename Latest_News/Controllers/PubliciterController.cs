using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Latest_News.Models;
using Latest_News.Models.Rep;
using Latest_News.ViewModel;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Latest_News.Controllers
{
    public class PubliciterController : Controller
    {
        public Repository<Publiciter> Rep_Pub { get; }
        public UserManager<AppUsers> UserManager { get; }
        public SignInManager<AppUsers> SignIn { get; }
        public HostingEnvironment Hosting { get; }

        public PubliciterController(Repository<Publiciter> rep_Pub,
             UserManager<AppUsers> userManager,
            SignInManager<AppUsers> signIn,
            HostingEnvironment hosting)
        {
            Rep_Pub = rep_Pub;
            UserManager = userManager;
            SignIn = signIn;
            Hosting = hosting;
        }
        // GET: Publiciter
        public ActionResult Index()
        {
            if (SignIn.IsSignedIn(User))
            {

                if (get_Admin() == "Admin")
                {
                    var list_ = Rep_Pub.List();
            return View(list_);
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // GET: Publiciter/Details/5
        public ActionResult Details(int id)
        {
                    if (SignIn.IsSignedIn(User))
                    {

                        if (get_Admin() == "Admin")
                        {
                            var find = Rep_Pub.Find(id);
            return View(find);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    return RedirectToAction("login", "Account");
                }

        // GET: Publiciter/Create
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

        // POST: Publiciter/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Publiciter_ViewModel model)
        {
            if (SignIn.IsSignedIn(User))
            {

                if (get_Admin() == "Admin")
                {
                    if (ModelState.IsValid)
            {
                try
                {
                    string fileName = "";
                    if (model.file.FileName != null)
                    {
                        string chemain = Path.Combine(Hosting.WebRootPath, "Image_Pub");
                        fileName = model.file.FileName;
                        string path = Path.Combine(chemain, fileName);
                        model.file.CopyTo(new FileStream(path, FileMode.Create));
                    }
                    var publ = new Publiciter()
                    {
                        date_Publiciter = DateTime.Now,
                        Titre = model.Titre,
                        img = fileName,
                        Name_Company=model.Name_Company
                    };
                    Rep_Pub.Add(publ);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View(model);
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // GET: Publiciter/Edit/5
        public ActionResult Edit(int id)
        {
                    if (SignIn.IsSignedIn(User))
                    {

                        if (get_Admin() == "Admin")
                        {
                            var find_ = Rep_Pub.Find(id);
            var model = new Publiciter_ViewModel()
            {
                img = find_.img,
                Titre = find_.Titre,
                id = find_.id
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

        // POST: Publiciter/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Publiciter_ViewModel model)
        {
            if (SignIn.IsSignedIn(User))
            {

                if (get_Admin() == "Admin")
                {
                    if (ModelState.IsValid)
            {
                try
                {
                    string fileName = "";
                    string img = "";
                    var find_ = Rep_Pub.Find(id);
                    img = find_.img;
                    if (model.file.FileName != null)
                    {
                        string chemain = Path.Combine(Hosting.WebRootPath, "Image_Pub");
                        fileName = model.file.FileName;
                        string path = Path.Combine(chemain, fileName);
                        string old_Paht = Path.Combine(chemain, img);
                        if (old_Paht != path)
                        {
                            System.IO.File.Delete(old_Paht);
                            model.file.CopyTo(new FileStream(path, FileMode.Create));
                        }
                    }
                    else
                    {
                        fileName = img;
                    }
                    find_.Titre = model.Titre;
                    find_.img = fileName;
                    find_.Name_Company = model.Name_Company;
                    Rep_Pub.Update(find_);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View(model);
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // GET: Publiciter/Delete/5
        public ActionResult Delete(int id)
        {
                    if (SignIn.IsSignedIn(User))
                    {

                        if (get_Admin() == "Admin")
                        {
                            var find_ = Rep_Pub.Find(id);
            return View(find_);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    return RedirectToAction("login", "Account");
                }

        // POST: Publiciter/Delete/5
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
                Rep_Pub.Delete(id);
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
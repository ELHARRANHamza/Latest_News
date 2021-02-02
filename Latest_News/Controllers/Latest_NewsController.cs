using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public class Latest_NewsController : Controller
    {
        public Repository<Models.Latest_News> Rep_News { get; }
        public Repository<Categories> Rep_Cat { get; }
        public UserManager<AppUsers> UserManager { get; }
        public SignInManager<AppUsers> SignIn { get; }
        public IHostingEnvironment Hosting { get; }

        public Latest_NewsController(Repository<Models.Latest_News> rep_News,
            Repository<Categories> rep_Cat,
            UserManager<AppUsers> userManager,
            SignInManager<AppUsers> signIn,
            IHostingEnvironment hosting)
        {
            Rep_News = rep_News;
            Rep_Cat = rep_Cat;
            UserManager = userManager;
            SignIn = signIn;
            Hosting = hosting;
        }
        // GET: Latest_News
        public ActionResult Index()
        {
            if (SignIn.IsSignedIn(User))
            {

                if (get_Admin() == "Admin")
                {
                    var list_ = Rep_News.List();
            return View(list_);
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // GET: Latest_News/Details/5
        public ActionResult Details(int id)
        {
            if (SignIn.IsSignedIn(User))
            {

                if (get_Admin() == "Admin")
                {
                    var find = Rep_News.Find(id);
            return View(find);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    return RedirectToAction("login", "Account");
                }

        // GET: Latest_News/Create
        public ActionResult Create()
        {
            if (SignIn.IsSignedIn(User))
            {

                if (get_Admin() == "Admin")
                {
                    var list_Cat = Rep_Cat.List();
            var model = new NewsViewModels()
            {
                GetCategories = list_Cat
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

        // POST: Latest_News/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewsViewModels models)
        {
            if (SignIn.IsSignedIn(User))
            {

                if (get_Admin() == "Admin")
                {
                    if (ModelState.IsValid)
            {
                try
                {
                    string file_Name = "";
                    int id_Cat = models.id_Cat;
                    var find_Cat = Rep_Cat.Find(id_Cat);
                    if (models.file.FileName != null)
                    {
                        string chemain = Path.Combine(Hosting.WebRootPath, "Image_News");
                        file_Name = models.file.FileName;
                        string path = Path.Combine(chemain, file_Name);
                        models.file.CopyTo(new FileStream(path, FileMode.Create));
                    }
                    var news = new Models.Latest_News()
                    {
                        Titre = models.Titre,
                        date_Publiciter = DateTime.Now,
                        Description = models.Description,
                        GetCategories = find_Cat,
                        lien_Ytb = models.url,
                        Image = file_Name
                    };
                    Rep_News.Add(news);
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

        // GET: Latest_News/Edit/5
        public ActionResult Edit(int id)
        {
            if (SignIn.IsSignedIn(User))
            {

                if (get_Admin() == "Admin")
                {
                    var find_News = Rep_News.Find(id);
          int id_Cat = find_News.GetCategories.id;
            var model = new NewsViewModels()
            {
                Description=find_News.Description,
                id_Cat=id_Cat,
                Image=find_News.Image,
                Titre=find_News.Titre,
                url=find_News.lien_Ytb,
                GetCategories=Rep_Cat.List()
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

        // POST: Latest_News/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NewsViewModels models)
        {
            if (SignIn.IsSignedIn(User))
            {

                if (get_Admin() == "Admin")
                {
                    if (ModelState.IsValid)
            {
                try
                {
                    string file_Name = "";
                    int id_Cat = models.id_Cat;
                    var find_Cat = Rep_Cat.Find(id_Cat);
                    var find_News = Rep_News.Find(id);
                    string img = find_News.Image;
                    if (models.file != null)
                    {
                        string chemain = Path.Combine(Hosting.WebRootPath, "Image_News");
                        file_Name = models.file.FileName;
                        string path = Path.Combine(chemain, file_Name);
                        string old_Path = Path.Combine(chemain, img);
                        if (path != old_Path)
                        {
                            System.IO.File.Delete(old_Path);
                            models.file.CopyTo(new FileStream(path, FileMode.Create));
                        }
                    }
                    else
                    {
                        file_Name = img;
                    }

                    find_News.Titre = models.Titre;
                    //find_News.date_Publiciter = DateTime.Now;
                    find_News.Description = models.Description;
                    find_News.GetCategories = find_Cat;
                    find_News.lien_Ytb = models.url;
                    find_News.Image = file_Name;
                    Rep_News.Update(find_News);
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

        // GET: Latest_News/Delete/5
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

        // POST: Latest_News/Delete/5
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
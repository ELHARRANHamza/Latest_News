using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Latest_News.Models;
using Latest_News.Models.Rep;
using Latest_News.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Latest_News.Controllers
{
    public class HomeController : Controller
    {
        public Repository<Models.Latest_News> Rep_News { get; }
        public Repository<Publiciter> Rep_Publ { get; }
        public Repository<Video_News> Rep_Vid { get; }
        public Repository<Categories> Rep_Cat { get; }
        public SignInManager<AppUsers> SignIn { get; }
        public UserManager<AppUsers> UserManager { get; }
        public Repository<Commentaire> Rep_Cmt { get; }

        public HomeController(Repository<Models.Latest_News> rep_News,
            Repository<Publiciter> rep_Publ,
            Repository<Video_News> rep_Vid,
            Repository<Categories> rep_Cat,
            SignInManager<AppUsers> signIn,
            UserManager<AppUsers> userManager,
           Repository<Commentaire> rep_Cmt

)
        {
            Rep_News = rep_News;
            Rep_Publ = rep_Publ;
            Rep_Vid = rep_Vid;
            Rep_Cat = rep_Cat;
            SignIn = signIn;
            UserManager = userManager;
            Rep_Cmt = rep_Cmt;
        }
        // GET: Home
        public ActionResult Index()
        {
            if (SignIn.IsSignedIn(User))
            {
                var Max_Date = Rep_News
             .List()
             .Max(n => n.date_Publiciter);
            ViewBag.News = Rep_News
                .List()
                .Where(n => n.date_Publiciter == Max_Date)
                .Single();
            var list_News = Rep_News
                .List()
                .OrderByDescending(n =>n.date_Publiciter)
                .Where(n =>n.date_Publiciter!= Max_Date)
                .ToList();
            var list_Publ = Rep_Publ.List();
            var list_Cat = Rep_Cat.List();
            var list_Vid = Rep_Vid.List();
         
            var model = new HomeViewModel()
            {
                GetPubliciters = list_Publ,
                GetCategories=list_Cat,
                GetVideo_News=list_Vid,
                GetLatest_News=list_News
            };
            return View(model);
            }
            return RedirectToAction("login", "Account");
        }
        public ActionResult Blog()
        {
            if (SignIn.IsSignedIn(User))
            {
                var list_Blog = Rep_News.List().OrderByDescending(b =>b.date_Publiciter).ToList();
                var list_Cat = Rep_Cat.List();
                ViewBag.Publ = Rep_Publ.List();
            var model = new BlogViewModel()
            {
                GetLatest_News = list_Blog,
                GetCategories=list_Cat
            };
            return View(model);
                }
                return RedirectToAction("login", "Account");
            }

        public ActionResult Contact()
        {
            return View();
        }
        // GET: Home/Details/5
        public ActionResult Details_Blog(int id)
        {
            if (SignIn.IsSignedIn(User))
            {
                var find_ = Rep_News.Find(id);
            var list_Post = Rep_News.List().Where(l => l.id != id).ToList();
            var list_Cat = Rep_Cat.List();
            var list_Cmt = Rep_Cmt.List().Where(c => c.news == find_).ToList();
           
            var model = new BlogViewModel()
            {
                id=id,
                latest_ = find_,
                GetLatest_News=list_Post,
                GetCategories=list_Cat,
                GetCommentaires=list_Cmt
            };
            return View(model);
                    }
                    return RedirectToAction("login", "Account");
                }

       public ActionResult Category()
        {
            if (SignIn.IsSignedIn(User))
            {
                var list_News = Rep_News
                .List()
                .OrderByDescending(n => n.date_Publiciter)
                .ToList();
                var list_Cat = Rep_Cat.List();
                var model = new HomeViewModel()
                {
                    GetLatest_News = list_News,
                    GetCategories = list_Cat
                };
                return View(model);
            }
            return RedirectToAction("login", "Account");
        }
        [HttpPost]
        public IActionResult Poster_Commentaire(int id, string msg)
        {
            
            if (SignIn.IsSignedIn(User))
            {
                var user_Id = UserManager.GetUserId(User);
                var find_User = UserManager.Users.SingleOrDefault(u => u.Id == user_Id);
                
                var find_News = Rep_News.Find(id);
                var cmt = new Commentaire()
                {
                    commentaire = msg,
                    date_Cmt = DateTime.Now,
                    pos = 1,
                    users = find_User,
                    news = find_News
                };
                Rep_Cmt.Add(cmt);
                return RedirectToAction(nameof(Details_Blog), new { id });
            }
            return RedirectToAction("login", "Account");
        }
        public IList<Models.Latest_News> Search(string term)
        {
            var news = Rep_News.List().Where(n => n.Titre.Contains(term)
            || n.Description.Contains(term)
            || n.GetCategories.cat_Nom.Contains(term)
              ).OrderByDescending(n => n.date_Publiciter).ToList();
            return news;
        }

        public IActionResult Search_(string term)
        {
            var Search_News = Search(term);
            var list_Cat = Rep_Cat.List();
            var model = new BlogViewModel()
            {
                GetLatest_News = Search_News,
                GetCategories = list_Cat
            };
            return View("Blog",model);
        }
    }
}
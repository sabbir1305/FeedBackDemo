using FeedBackDemo.Models;
using FeedBackDemo.Models.ViewModels;
using FeedBackDemo.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedBackDemo.Controllers
{
    public class FeedBackController : Controller
    {
        // GET: FeedBack
        public ActionResult Index()
        {
            FeedBackVM fb = new FeedBackVM();
            using (var context= new ApplicationDbContext())
            {
                FeedBackRepo feedBack = new FeedBackRepo(context);
           
              
                feedBack.GetAllPosts(1, fb);
            }
           
            return View(fb);
        }

        [HttpPost]
        public ActionResult Index(FeedBackVM fb)
        {
            using (var context = new ApplicationDbContext())
            {
                FeedBackRepo feedBack = new FeedBackRepo(context);
                //Reset page count
                if (fb.HiddenItemPerPage != fb.ItemPerPage)
                {
                    fb.page = 1;

                }
                else
                {
                    fb.page = fb.page;

                }
                fb.HiddenItemPerPage = fb.ItemPerPage;

                feedBack.GetAllPosts((int)fb.page, fb);
            }

            return View(fb);
        }


    }
}
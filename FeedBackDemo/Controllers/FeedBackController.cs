using FeedBackDemo.Models;
using FeedBackDemo.Models.ViewModels;
using FeedBackDemo.Repo;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedBackDemo.Controllers
{
    [Authorize]
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

        public JsonResult Vote(Vote vote)
        {
            using (var context = new ApplicationDbContext())
            {
                FeedBackRepo feedBack = new FeedBackRepo(context);
                var userId = User.Identity.GetUserId();
                if (context.Votes.Any(x => x.CommentId == vote.CommentId && x.CreatedBy== userId))
                {
                    var type = context.Votes.FirstOrDefault(x => x.CommentId == vote.CommentId && x.CreatedBy== userId);
                    if (type.Type != vote.Type)
                    {
                        type.Type = vote.Type;
                        vote.CreateDate = DateTime.UtcNow;
                     
                        context.SaveChanges();

                        var voteCount = feedBack.CountVote(vote.CommentId);

                        return Json(new { like = voteCount[0], dislike = voteCount[1] }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var voteCount = feedBack.CountVote(vote.CommentId);
                        return Json(new { like = voteCount[0], dislike = voteCount[1] }, JsonRequestBehavior.AllowGet);
                    }


                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        vote.CreatedBy = User.Identity.GetUserId();
                        vote.CreateDate = DateTime.UtcNow;
                       

                        context.Votes.Add(vote);
                        context.SaveChanges();
                        var voteCount = feedBack.CountVote(vote.CommentId);
                        return Json(new { like = voteCount[0], dislike = voteCount[1] }, JsonRequestBehavior.AllowGet);
                    }
                }

            }


            return Json(false);
        }


        
    }
}
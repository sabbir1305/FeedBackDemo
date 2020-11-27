using FeedBackDemo.Models;
using FeedBackDemo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FeedBackDemo.Repo
{
    public class FeedBackRepo
    {
        private readonly ApplicationDbContext context;

        public FeedBackRepo(ApplicationDbContext _context)
        {
            context = new ApplicationDbContext();
        }

        public void GetAllPosts(int Page , FeedBackVM feedBack)
        {

            //var posts = context.Posts.Take(1).Include(u=>u.User).Include(y => y.PostComments).ToList();

            //var voteLit = context.Votes.ToList();

            //FeedBackVM feedBack = new FeedBackVM();
            //foreach (var p in posts)
            //{


            //}

            //var Post = (from p in context.Posts
            //            join c in context.Comments on p.Id equals c.PostId into cc
            //            from cmnt in cc.DefaultIfEmpty()
            //            select new
            //            {
            //                p,
            //                cc,
            //                votes = context.Votes.Where(x => x.CommentId == cmnt.Id)
            //            }).ToList();

            var posts = context.Posts.OrderBy(x=>x.Id).Include(x => x.User);
          

            var posts2 = posts;
            feedBack.TotalPost = posts2.Count();

            var pager = new Pager(feedBack.TotalPost, Page, 5);

            var   PostList = posts.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();

            var postId = PostList.Select(x => x.Id).ToList();

            var comments = context.Comments.Include(x => x.User).Where(x=>postId.Contains(x.PostId)).ToList();
           // var comments = context.Comments.Include(x => x.User).ToList();

            var commentIds = comments.Select(x => x.Id).ToList();

            var votes = context.Votes.Where(v=>commentIds.Contains(v.CommentId)).ToList();


            foreach (var item in comments)
            {
                item.VoteList = votes.Where(x => x.CommentId == item.Id).ToList();
            }
            foreach (var item in posts)
            {
                item.PostComments = comments.Where(x => x.PostId == item.Id).ToList();
            }


            feedBack.ItemPerPage = 5;
            feedBack.HiddenItemPerPage = 5;
            // stock.StockList = items;
            feedBack.pager = pager;
            feedBack.PostList = PostList;
        }

        public List<int> CountVote(int commentId)
        {
            var voteList = new List<int>();
            var countLike = context.Votes.Where(x => x.CommentId == commentId).ToList();

            voteList.Add(countLike.Count(x => x.Type == 1));
            voteList.Add(countLike.Count(x => x.Type == 2));
            return voteList;
        }

        public void UpdateCommentCount(int commentId , int Like, int DisLike)
        {
            var comment = context.Comments.Find(commentId);
            comment.LikeCount = Like;
            comment.DisLikeCount = DisLike;

            context.SaveChanges();
        }


        public void GetAllPost(int Page, FeedBackVM feedBack)
        {



            var posts = context.Posts.Include("User").Include("PostComments").Include("PostComments.User");


            var posts2 = posts;
            feedBack.TotalPost = posts2.Count();

            var pager = new Pager(feedBack.TotalPost, Page, 5);

            feedBack.PostList = posts.OrderBy(x=>x.Id).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();


            feedBack.ItemPerPage = 5;
            feedBack.HiddenItemPerPage = 5;
            feedBack.pager = pager;
          
        }



        //    public void SingleQueryTestData()
        //    {
        //        var allData = from p in context.Posts
        //                      join c in context.Comments on p.Id equals c.PostId into cc from c1 in cc.DefaultIfEmpty()                       
        //                      group new { p , c1} by new {p} into gpp
        //                      select new
        //                      {
        //                          gpp.Key.p,
        //                          Comments = gpp.GroupBy(x=>x.c1)
        //                      }



        //                      List< Continent > List = MyRepository.GetList<GetAllCountriesAndCities>("EXEC sp_GetAllCountriesAndCities")
        //.GroupBy(x => x.ContinentName)
        //.Select(g => new Continent
        //{
        //    ContinentName = g.Key,
        //    Countries = g.GroupBy(x => x.CountryName)
        //                 .Select(cg => new Country
        //                 {
        //                     CountryName = cg.Key,
        //                     Cities = cg.GroupBy(x => x.CityName)
        //                                .Select(cityG => new City { CityName = cityG.Key })
        //                                .ToList()
        //                 })
        //                 .ToList()
        //})
        //.ToList();


        //    }
    }
}
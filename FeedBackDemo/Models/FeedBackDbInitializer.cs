using FeedBackDemo.Repo;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FeedBackDemo.Models
{
    public class FeedBackDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.



            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool    
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                   

                var user = new ApplicationUser();
                user.UserName = "Admin";
                user.Email = "admin@mail.com";
                //  user.UserGroupId = 4;

                string userPWD = "Abc123456!";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            if (!roleManager.RoleExists("User"))
            {

                // first we create Admin rool    
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "User";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website 

                for (int i = 1; i < 6; i++)
                {
                    var user = new ApplicationUser();
                    user.UserName = "user" + i;
                    user.Email = "user" + i + "@mail.com";
                    //  user.UserGroupId = 4;

                    string userPWD = "Abc123456!";

                    var chkUser = UserManager.Create(user, userPWD);

                    //Add default User to Role User    
                    if (chkUser.Succeeded)
                    {
                        var result1 = UserManager.AddToRole(user.Id, "User");

                    }
                }


            }

            using (UserRepo repo = new UserRepo(context))
            {
                var AdminUsers = repo.GetUsersInRole("Admin");
                int postCount = 1;
                var newPosts = new List<Post>();
                foreach (var user in AdminUsers)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        newPosts.Add(new Post
                        {
                            CreateDate = DateTime.UtcNow,
                            CreatedBy = user.Id,
                            Title = "Post " + postCount

                        });
                        postCount++;

                    }
                    context.Posts.AddRange(newPosts);
                    context.SaveChanges();


                }

                var normalUsers = repo.GetUsersInRole("User");
                var AllPost = context.Posts.ToList();
                postCount = AllPost.Count;
                int numberOfComment = 1;
                foreach (var user in normalUsers)
                {
                    Random r = new Random();
                    //int randomlyComment = r.Next(0, postCount);

                    var commentLst = new List<Comment>();
                    foreach (var item in AllPost.Take(5))
                    {
                        commentLst.Add(new Comment
                        {
                            CreateDate = DateTime.UtcNow,
                            CreatedBy = user.Id,
                            Description = "Comment " + numberOfComment,
                            PostId = item.Id
                        });
                        numberOfComment++;
                      
                    }
                    context.Comments.AddRange(commentLst);

                    context.SaveChanges();


                }

            }


            base.Seed(context);
        }
    }
}

using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    public class CommentsController : Controller
    {
        private BlogContext db = new BlogContext();

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AuthorName,Email,WebsiteUrl,CommentText,PostId")] Comment comment)
        {
            try
            {
                comment.DatePosted = DateTime.Now;

                if(ModelState.IsValid)
                {
                    db.Comments.Add(comment);
                    db.SaveChanges();
                    return RedirectToAction("Details", "Posts", new { id=comment.PostId });
                }

                return RedirectToAction("Details", "Posts", new { id = comment.PostId });
            }
            catch
            {
                return RedirectToAction("Details", "Posts", new { id = comment.PostId });
            }

        }
    }
}

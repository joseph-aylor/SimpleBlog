using SimpleBlog.Models;
using SimpleBlog.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
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
                // This would be a parameter, but it had hyphens.
                // So we'll pull it out of the Request Object
                var recaptchaReponse = Request["g-recaptcha-response"];

                comment.DatePosted = DateTime.Now;

                if(ModelState.IsValid && CaptchaUtility.Verify(recaptchaReponse, "6Lfukw8TAAAAAPcA4h__z3i6Y9EZU0G-kER1Ad60"))
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

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Url]
        [Display(Name = "Website Url")]
        public string WebsiteUrl { get; set; }
        [Required]
        [Display(Name = "Content")]
        public string CommentText { get; set; }
        public DateTime DatePosted { get; set; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
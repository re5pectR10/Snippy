﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippy.Models
{
    public class Language
    {
        private ICollection<Snippet> snippets;
        public Language()
        {
            this.snippets = new HashSet<Snippet>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Snippet> Snippets { get { return this.snippets; } set { this.snippets = value; } }
    }
}

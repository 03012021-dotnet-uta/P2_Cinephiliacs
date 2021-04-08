using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class Language
    {
        public Language()
        {
            MovieLanguages = new HashSet<MovieLanguage>();
        }

        public string LanguageName { get; set; }

        public virtual ICollection<MovieLanguage> MovieLanguages { get; set; }
    }
}

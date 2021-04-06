using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.DatabaseModels
{
    public partial class MovieLanguage
    {
        public string MovieId { get; set; }
        public string LanguageName { get; set; }

        public virtual Language LanguageNameNavigation { get; set; }
        public virtual Movie Movie { get; set; }
    }
}

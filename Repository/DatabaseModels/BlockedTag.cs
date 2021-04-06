using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.DatabaseModels
{
    public partial class BlockedTag
    {
        public string Username { get; set; }
        public string TagName { get; set; }

        public virtual Tag TagNameNavigation { get; set; }
        public virtual User UsernameNavigation { get; set; }
    }
}

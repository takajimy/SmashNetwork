using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmashNetwork.Models;

namespace SmashNetwork.Areas.Users.Models
{
    public class Role : AbstractModel
    {
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}

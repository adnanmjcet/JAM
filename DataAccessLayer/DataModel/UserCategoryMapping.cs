using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataModel
{
    [Table("UserCategoryMapping")]
    public partial class UserCategoryMapping
    {
        public UserCategoryMapping()
        {

        }

        public int ID { get; set; }

        public int UserID { get; set; }

        public int CategoryID { get; set; }

        public bool IsSelected { get; set; }

        public virtual Category Category { get; set; }

        public virtual User User { get; set; }
    }
}

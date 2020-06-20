using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Data.Entity
{
    public class ShoppingCarEntity
    {
        public int Id { get; set; }
        public ICollection<ShoppingItemEntity> Items { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public UserEntity User;
    }
}

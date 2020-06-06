using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Data.Entity
{
    public class PurchaseEntity
    {
        public int Id { get; set; }
        public UserEntity User { get; set; }
        public DateTime Date { get; set; }
        public ICollection<PurchaseItemDetailEntity> PurchaseItemDetails { get; set; }
        public ICollection<PurchaseServiceDetailEntity> PurchaseServiceDetails { get; set; }
        public double Total => PurchaseItemDetails.Sum(x => x.Total) + PurchaseServiceDetails.Sum(x => x.Total);
    }
}

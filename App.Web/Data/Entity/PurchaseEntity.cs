using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Data.Entity
{
    public class PurchaseEntity
    {
        public int Id { get; set; }
        public UserEntity User { get; set; }
       
        [Display(Name ="Fecha")]
        public DateTime Date { get; set; }
        public ICollection<PurchaseItemDetailEntity> PurchaseItemDetails { get; set; }
        public ICollection<PurchaseServiceDetailEntity> PurchaseServiceDetails { get; set; }
        public double Total =>(PurchaseItemDetails!=null &&PurchaseServiceDetails!=null)? PurchaseItemDetails.Sum(x => x.Total) + PurchaseServiceDetails.Sum(x => x.Total):0.0;
    }
}

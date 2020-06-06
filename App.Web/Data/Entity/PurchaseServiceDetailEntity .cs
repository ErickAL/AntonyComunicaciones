using System.ComponentModel.DataAnnotations;

namespace App.Web.Data.Entity
{
    public class PurchaseServiceDetailEntity
    {
        public int Id { get; set; }
        [Display(Name = "Cantidad")]
        public int Amount { get; set; }
        public ServiceEntity Service { get; set; }
        public double Total => Amount * Service.Price;
        public PurchaseEntity Purchase { get; set; }
    }
}
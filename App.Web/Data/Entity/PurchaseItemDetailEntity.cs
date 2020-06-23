using System.ComponentModel.DataAnnotations;

namespace App.Web.Data.Entity
{
    public class PurchaseItemDetailEntity
    {
        public int Id { get; set; }
        [Display(Name = "Cantidad")]
        public int Amount { get; set; }

        public ItemEntity Item { get; set; }
        public double Total => Amount * Item.Price;
        public PurchaseEntity Purchase { get; set; }
    }
}
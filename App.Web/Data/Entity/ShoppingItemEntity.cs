using App.Common.Enums;
using System;

namespace App.Web.Data.Entity
{
    public class ShoppingItemEntity
    {
        public int Id { get; set; }
        public ItemEntity Item { get; set; }
        public bool IsPaid { get; set; }
       
    }
}
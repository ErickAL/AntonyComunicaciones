using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Data.Entity
{
    public class ServiceEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public double Price { get; set; }
        public ICollection<PurchaseServiceDetailEntity> Purchases { get; set; }
    }
}

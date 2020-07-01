using App.Web.Data;
using App.Web.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Models
{
    public class FacturaViewModel
    {
        public PurchaseEntity Factura { get; set; }

        public ICollection<ServiceEntity> Servicios { get; set; }
        public ICollection<ItemEntity> Articulo { get; set; }
        public void AddService(ServiceEntity service)
        {
            Factura.PurchaseServiceDetails.Add(new PurchaseServiceDetailEntity { Amount = 1, Service = service });
        }
    }
}

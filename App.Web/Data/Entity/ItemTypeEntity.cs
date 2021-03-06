﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Web.Data.Entity
{
    public class ItemTypeEntity
    {
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        public ICollection<ItemEntity> Items { get; set; }
        
    }
}
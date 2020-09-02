using System;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABCEntities
{
    public class Product
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid ProductID { get; set; }
        
        
        public string ProductName { get; set; }

        public string ProductCategory { get; set; }

        public int ProductPrice { get; set; }

        public int ProductQuantity { get; set; }

        public string ProductStatus { get; set; }

        
    }
}

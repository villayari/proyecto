using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cibertec.Models
{
    
    public class Products
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public int CategoryID { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Discontinued { get; set; }
    }
}

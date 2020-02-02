using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.Models
{
    public class DetalleCotizacion
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public string IDCodigo { get; set; }
        public int Cantidad { get; set; }
        public decimal MUnit { get; set; }
        public decimal IGV { get; set; }
        public decimal MTotal { get; set; }
        public decimal MBase { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Fecha { get; set; }
    }
}

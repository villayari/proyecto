using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.Models
{
    public class CabeceraCotizacion
    {
        public int Id { get; set; }
        public string Cotizacion { get; set; }
        public string Evento { get; set; }
        public string Asesor { get; set; }
        public decimal Monto { get; set; }
        public decimal Descuento { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Fecha { get; set; }
    }
}

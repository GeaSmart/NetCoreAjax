using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAjax.Entities
{
    public class Movimiento
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string NumeroCuenta { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Referencia { get; set; }
                
        public int Cantidad { get; set; }
    }
}

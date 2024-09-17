using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF.PROYECTO.Utilitario
{
    public class Catalogo
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public bool? Obligatorio { get; set; }
        public string Ejemplo { get; set; } = string.Empty;
    }
}

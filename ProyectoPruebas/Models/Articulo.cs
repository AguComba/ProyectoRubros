using System.ComponentModel.DataAnnotations;

namespace ProyectoPruebas.Models
{
    public class Articulo
    {
        [Key]
        public int ArticuloID { get; set; }
        public string Descripcion { get; set; }
        public DateTime UltAct { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PorcentajeGanancia { get; set; }
        public decimal PrecioVenta { get; set; }
        public int SubRubroID { get; set; }
        public bool Eliminado { get; set; } 
        public virtual SubRubro SubRubro { get; set; }
        
    
    }

    public class ArticuloMostrar
    {
        public int ArticuloID { get; set; }
        public string Descripcion { get; set; }
        public DateTime UltAct { get; set; }
        public string UltActString { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PorcentajeGanancia { get; set; }
        public decimal PrecioVenta { get; set; }
        public int SubRubroID { get; set; }
        public string SubRubroDescripcion { get; set; }
        public int RubroID { get; set; }
        public string RubroDescripcion { get; set; }
        public bool Eliminado { get; set; } 
    }
}

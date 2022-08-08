using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoPruebas.Data;
using ProyectoPruebas.Models;
using System.Globalization;

namespace ProyectoPruebas.Controllers
{
    public class ArticulosController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ArticulosController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var rubros = _context.Rubro.Where(p=>p.Eliminado == false).ToList();
            rubros.Add(new Rubro { RubroID = 0, Descripcion = "[SELECCIONE UN RUBRO]" });
            ViewBag.RubroID = new SelectList(rubros.OrderBy(p => p.Descripcion),"RubroID", "Descripcion");   

            List<SubRubro>subRubros = new List<SubRubro>();
            subRubros.Add(new SubRubro { SubRubroID = 0, Descripcion = "[SELECCIONE UN RUBRO]" });
            ViewBag.SubRubroID = new SelectList(subRubros.OrderBy(p => p.Descripcion), "SubRubroID", "Descripcion");
            return View();
        }

        public JsonResult BuscarArticulos()
        {
            var articulos = _context.Articulos.Include(e => e.SubRubro).Include(e => e.SubRubro.Rubro).ToList();

            List<ArticuloMostrar> listadoArticuloMostrar = new List<ArticuloMostrar>();

            //var mapsArticulo = articulos.Select(e => new ArticuloMostrar
            //{
            //    ArticuloID = e.ArticuloID,
            //    Descripcion = e.Descripcion,
            //    SubRubroDescripcion = e.SubRubro.Descripcion,
            //    RubroDescripcion = e.SubRubro.Rubro.Descripcion,
            //    UltAct = e.UltAct,
            //    UltActString = e.UltAct.ToString("dd/MM/yyyy"),
            //    PrecioCosto = e.PrecioCosto,
            //    PorcentajeGanancia = e.PorcentajeGanancia,
            //    PrecioVenta = e.PrecioVenta,
            //});
            foreach (var articulo in articulos)
            {
                var mostrar = new ArticuloMostrar
                {
                    ArticuloID = articulo.ArticuloID,
                    Descripcion = articulo.Descripcion,
                    RubroID = articulo.SubRubro.Rubro.RubroID,
                    RubroDescripcion = articulo.SubRubro.Rubro.Descripcion,
                    SubRubroID = articulo.SubRubro.SubRubroID,
                    SubRubroDescripcion = articulo.SubRubro.Descripcion,
                    UltAct = articulo.UltAct,
                    UltActString = articulo.UltAct.ToString("dd/MM/yyyy"),
                    PrecioCosto = articulo.PrecioCosto,
                    PorcentajeGanancia = articulo.PorcentajeGanancia,
                    PrecioVenta = articulo.PrecioVenta,
                    Eliminado = articulo.Eliminado
                };
                listadoArticuloMostrar.Add(mostrar);
            }
            return Json(listadoArticuloMostrar);
        }

        public JsonResult GuardarArticulo(int ArticuloID, string Descripcion, int SubRubroID, DateTime UltAct, decimal PrecioCosto, decimal PorcentajeGanancia, decimal PrecioVenta, bool Eliminado)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-AR");
            int resultado = 0;
            if (!string.IsNullOrEmpty(Descripcion))
            {
                Descripcion = Descripcion.ToUpper();
                if (ArticuloID == 0)
                {
                    if (_context.Articulos.Any(e => e.Descripcion == Descripcion && e.SubRubroID == SubRubroID))
                    {
                        resultado = 2;
                    }
                    else
                    {
                        var articuloCrear = new Articulo
                        {
                            Descripcion = Descripcion,
                            SubRubroID = SubRubroID,
                            UltAct = DateTime.Now,
                            PrecioCosto = Convert.ToDecimal(PrecioCosto),
                            PorcentajeGanancia = Convert.ToDecimal(PorcentajeGanancia),
                            PrecioVenta = Convert.ToDecimal(PrecioVenta),
                            Eliminado= Eliminado,
    
                        };
                        _context.Add(articuloCrear);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    if (_context.Articulos.Any(e => e.Descripcion == Descripcion && e.ArticuloID != ArticuloID && e.SubRubroID == SubRubroID))
                    {
                        resultado = 2;
                    }
                    else
                    {
                        //Editar
                        var articulo = _context.Articulos.Single(p => p.ArticuloID == ArticuloID);
                        articulo.Descripcion = Descripcion;
                        articulo.SubRubroID = SubRubroID;
                        articulo.UltAct = DateTime.Now;
                        articulo.PrecioCosto = PrecioCosto;
                        articulo.PorcentajeGanancia = PorcentajeGanancia;
                        articulo.PrecioVenta = PrecioVenta;
                        _context.SaveChanges();

                    }

                }
            }

            return Json(resultado);
        }

        public JsonResult BuscarArticulo(int ArticuloID, int RubroID, int SubRubroID )
        {
            var articulo = _context.Articulos.FirstOrDefault(m => m.ArticuloID == ArticuloID);

            return Json(articulo);
        }

        public JsonResult EliminarArticulo(int ArticuloID, int Elimina)
        {
            bool resultado = true;

            var articulo = _context.Articulos.Find(ArticuloID);
            if (articulo != null)
            {
                if (Elimina == 1)
                {
                    articulo.Eliminado = true;
                }
                else
                {
                    articulo.Eliminado = false;
                }
                _context.SaveChanges();
            }

            return Json(resultado);
        }
    }


    
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoPruebas.Data;
using ProyectoPruebas.Models;

namespace ProyectoPruebas.Controllers
{
    public class SubRubrosController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SubRubrosController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var rubros = _context.Rubro.Where(p => p.Eliminado == false).ToList();
            rubros.Add(new Rubro { RubroID = 0, Descripcion="[SELECCIONE UN RUBRO]" });
            ViewBag.RubroID = new SelectList(rubros.OrderBy(p => p.Descripcion), "RubroID", "Descripcion");
            return View();
        }

        public JsonResult ComboSubRubro(int id)
        {
            var subRubros = (from o in _context.SubRubros where o.RubroID == id && o.Eliminado == false select o).ToList();
            return Json(new SelectList(subRubros,"SubRubroID", "Descripcion"));
        }

        public JsonResult BuscarSubRubros()
        {
            var subrubros = _context.SubRubros.Include(e=>e.Rubro).ToList();
            //List<SubRubroMostrar> listadoSubRubroMostrar = new List<SubRubroMostrar>();
            var mapsubrubro = subrubros.Select(e => new SubRubroMostrar
            {

                SubRubroID = e.SubRubroID,
                Descripcion = e.Descripcion,
                RubroID = e.RubroID,
                RubroDescripcion = e.Rubro.Descripcion,
                Eliminado = e.Eliminado,

            });
            //foreach (var subrubro in subrubros)
            //{
            //    var mostrar = new SubRubroMostrar
            //    {
            //        SubRubroID = subrubro.SubRubroID,
            //        Descripcion = subrubro.Descripcion,
            //        RubroID=subrubro.RubroID,
            //        RubroDescripcion=subrubro.Rubro.Descripcion,
            //    };
            //    listadoSubRubroMostrar.Add(mostrar);
            //} 
            return Json(mapsubrubro);
        }

        public JsonResult GuardarSubRubro(int SubRubroID, string Descripcion, int RubroID)
        {
            int resultado = 0;
            if (!string.IsNullOrEmpty(Descripcion))
            {
                Descripcion = Descripcion.ToUpper();
                    if (SubRubroID == 0)
                    {
                        if(_context.SubRubros.Any(e => e.Descripcion == Descripcion && e.RubroID == RubroID))
                        {
                        resultado = 2;
                        }
                        else
                        {
                            //Aca va el crear
                            var subRubroCrear = new SubRubro
                            {
                                Descripcion = Descripcion,
                                RubroID = RubroID
                            };
                            _context.Add(subRubroCrear);
                            _context.SaveChanges();
                        }
                    }
                    else
                    {
                        if(_context.SubRubros.Any(e => e.Descripcion == Descripcion && e.SubRubroID != SubRubroID && e.RubroID == RubroID ))
                        {
                            resultado = 2;
                        }
                        else
                        {
                            //Aca va el editar
                            var subRubro= _context.SubRubros.Single(p => p.SubRubroID== SubRubroID);
                            subRubro.Descripcion = Descripcion;
                            subRubro.RubroID= RubroID;
                            _context.SaveChanges();
                        }                       

                    }
               
            }

            return Json(resultado);
        }

        public JsonResult BuscarSubRubro(int SubRubroID )
        {
            var subRubro= _context.SubRubros.FirstOrDefault(m => m.SubRubroID== SubRubroID );

            return Json(subRubro);
        }

        public JsonResult EliminarSubRubro(int SubRubroID,int Elimina)
        {
            bool resultado = true;

            var subRubro = _context.SubRubros.Find(SubRubroID);
            if (subRubro != null)
            {
                if(Elimina == 1)
                {
                    subRubro.Eliminado = true;
                }
                else
                {
                    subRubro.Eliminado = false;
                }
                _context.SaveChanges();
            }

            return Json(resultado);
        }
    }


}

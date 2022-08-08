#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoPruebas.Data;
using ProyectoPruebas.Models;

namespace ProyectoPruebas.Controllers
{
    public class RubrosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RubrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rubroes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rubro.ToListAsync());
        }

        public JsonResult BuscarRubros()
        {
            var rubros = _context.Rubro.ToList();   
            return Json(rubros);
        }

        public JsonResult GuardarRubro(int RubroID, string Descripcion)
        {
            int resultado = 0;
            //Si es 0 es correcto
            //Si es 1 descripcion vacia
            //Si es 2 campo existente
            if (!string.IsNullOrEmpty(Descripcion))
            {
                Descripcion = Descripcion.ToUpper();
                    if (RubroID == 0)
                    {
                        if(_context.Rubro.Any(e => e.Descripcion == Descripcion))
                        {
                        resultado = 2;
                        }
                        else
                        {
                        //Aca va el crear
                            var rubroCrear = new Rubro
                            {
                            Descripcion = Descripcion
                            };
                            _context.Add(rubroCrear);
                            _context.SaveChanges();
                        }
                    }
                    else
                    {
                        if (_context.Rubro.Any(e => e.Descripcion == Descripcion && e.RubroID != RubroID))
                        {
                            resultado = 2;
                        }
                        else
                        {
                            //Aca va el editar
                            var rubro = _context.Rubro.Single(p => p.RubroID == RubroID);
                            rubro.Descripcion = Descripcion;
                            _context.SaveChanges();
                        }
                    }
            
            }

            return Json(resultado);
        }

        public JsonResult BuscarRubro(int RubroID)
        {
            var rubro = _context.Rubro.FirstOrDefault(m => m.RubroID == RubroID);

            return Json(rubro);
        }

        public JsonResult EliminarRubro(int RubroID ,int Elimina)
        {
            bool resultado = true;

            var rubro = _context.Rubro.Find(RubroID);
            if (rubro != null)
            {
                if(Elimina == 1)
                {
                    rubro.Eliminado = true;
                }
                else
                {
                    rubro.Eliminado = false;
                }
                _context.SaveChanges();
            }

            return Json(resultado);
        }
        

        private bool RubroExists(int id)
        {
            return _context.Rubro.Any(e => e.RubroID == id);
        }
    }
}

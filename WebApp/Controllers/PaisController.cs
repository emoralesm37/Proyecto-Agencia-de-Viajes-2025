using Microsoft.AspNetCore.Mvc;
using Proyectofinal.Data;
using Proyectofinal.Models;

namespace Proyectofinal.Controllers
{
    public class PaisController : Controller
    {
        PaisData _data = new PaisData();

        public IActionResult Listar()
        {
            //LA VISTA MOSTRARÁ UNA LISTA DE CONTACTOS
            var oLista = _data.Listar();

            return View(oLista);
        }

        public IActionResult Guardar()
        {
            //METODO SOLO DEVUELVE LA VISTA
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(Pais obj)
        {
            //METODO RECIBE EL OBJETO PARA GUARDARLO EN BD
            if (!ModelState.IsValid)
                return View();


            var respuesta = _data.Guardar(obj);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult Editar(string CodigoPais)
        {
            //METODO SOLO DEVUELVE LA VISTA
            var obj = _data.Obtener(CodigoPais);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Editar(Pais obj)
        {
            if (!ModelState.IsValid)
                return View();


            var respuesta = _data.Editar(obj);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }


        public IActionResult Eliminar(string CodigoPais)
        {
            //METODO SOLO DEVUELVE LA VISTA
            var obj = _data.Obtener(CodigoPais);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Eliminar(Pais obj)
        {

            var respuesta = _data.Eliminar(obj.CodigoPais);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
    }
}

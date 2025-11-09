using Microsoft.AspNetCore.Mvc;
using Proyectofinal.Data;
using Proyectofinal.Models;

namespace Proyectofinal.Controllers
{
    public class EmailTuristaController : Controller
    {
        EmailTuristaData _data = new EmailTuristaData();

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
        public IActionResult Guardar(EmailTurista obj)
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

        public IActionResult Editar(string cdoigo)
        {
            //METODO SOLO DEVUELVE LA VISTA
            var obj = _data.Obtener(cdoigo);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Editar(EmailTurista obj)
        {
            if (!ModelState.IsValid)
                return View();


            var respuesta = _data.Editar(obj);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }


        public IActionResult Eliminar(string codigo)
        {
            //METODO SOLO DEVUELVE LA VISTA
            var obj = _data.Obtener(codigo);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Eliminar(EmailTurista obj)
        {

            var respuesta = _data.Eliminar(obj.CodigoTurista);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
    }
}

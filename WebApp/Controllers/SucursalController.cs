using Microsoft.AspNetCore.Mvc;
using Proyectofinal.Data;
using Proyectofinal.Models;

namespace Proyectofinal.Controllers
{
    public class SucursalController : Controller
    {
        SurcursalData _data = new SurcursalData();

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
        public IActionResult Guardar(Sucursal obj)
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

        public IActionResult Editar(string CodigoSucursal)
        {
            //METODO SOLO DEVUELVE LA VISTA
            var obj = _data.Obtener(CodigoSucursal);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Editar(Sucursal obj)
        {
            if (!ModelState.IsValid)
                return View();


            var respuesta = _data.Editar(obj);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }


        public IActionResult Eliminar(string CodigoSucursal)
        {
            //METODO SOLO DEVUELVE LA VISTA
            var obj = _data.Obtener(CodigoSucursal);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Eliminar(Sucursal obj)
        {

            var respuesta = _data.Eliminar(obj.CodigoSucursal);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
    }
}

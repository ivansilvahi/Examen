using Examen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Examen.Controllers
{
    public class ProductoController : Controller
    {
        TIENDAEntities2 cnx;


        public ProductoController()
        {
            cnx = new TIENDAEntities2();
        }

        public ActionResult Formulario()
        {
            return View();
        }

        public ActionResult Guardar(string nombre, string codigobarra, string familia, string precio, string descuentotope, string descripcion)
        {

            Examen.Models.Producto producto = new Examen.Models.Producto
            {
  
                Nombre = nombre,
                CodigoBarra = codigobarra,
                Familia = familia,
                Precio = precio,
                DescuentoTope = descuentotope,
                Descripcion = descripcion

            };

            cnx.Producto.Add(producto);
            cnx.SaveChanges();

            return View("Listado", ListadoProductos());
        }
        public ActionResult Listado()
        {

            return View("Listado", ListadoProductos());

        }
        public ActionResult Ficha(int id)
        {

            return View(BuscarProducto(id));
        }

        private Producto BuscarProducto(int id)
        {
            Producto nuevo = new Producto();
            foreach (Producto producto in cnx.Producto.ToList())
            {
                if (producto.Id == id)
                {
                    nuevo = producto;
                }
            }
            return nuevo;
        }
        public ActionResult Visualizar(int id)
        {
            Producto nuevo = BuscarProducto(id);

            if (nuevo != null)
            {
                return View("Ficha", nuevo);
            }
            return View("Listado", cnx.Producto.ToList());
        }

        public ActionResult Eliminar(int id)

        {
            cnx.Producto.Remove(cnx.Producto.Where(x => x.Id.Equals(id)).First());

            cnx.SaveChanges();

            return View("Listado", cnx.Producto.ToList());

        }


        private List<Examen.Models.Producto> ListadoProductos()
        {

            cnx.Database.Connection.Open();


            List<Examen.Models.Producto> auto = cnx.Producto.ToList();

            cnx.Database.Connection.Close();

            return auto;
        }

    }
}
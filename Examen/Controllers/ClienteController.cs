using Examen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Examen.Controllers
{
    public class ClienteController : Controller
    {
        TIENDAEntities2 cnx;


        public ClienteController()
        {
            cnx = new TIENDAEntities2();
        }
        public ActionResult Formulario()
        {
            return View();
        }
        public ActionResult Guardar(string rut, string nombre, string apellido,string direccion, string tipocliente)
        {

            Examen.Models.Cliente cliente = new Examen.Models.Cliente
            {

                Rut = rut,
                Nombre = nombre,
                Apellido = apellido,
                Direccion = direccion,
                TipoCliente = tipocliente

            };

            cnx.Cliente.Add(cliente);
            cnx.SaveChanges();


            return View("Listado", ListadoClientes());
        }
        public ActionResult Listado()
        {

            return View("Listado", ListadoClientes());
        }
        public ActionResult Ficha(int id)
        {

            return View(BuscarCliente(id));
        }

        private Cliente BuscarCliente(int id)
        {
            Cliente nuevo = new Cliente();
            foreach (Cliente cliente in cnx.Cliente.ToList())
            {
                if (cliente.Id == id)
                {
                    nuevo = cliente;
                }
            }
            return nuevo;
        }
        public ActionResult Visualizar(int id)
        {
            Cliente nuevo = BuscarCliente(id);

            if (nuevo != null)
            {
                return View("Ficha", nuevo);
            }
            return View("Listado", cnx.Cliente.ToList());
        }

        public ActionResult Eliminar(int id)

        {
            cnx.Cliente.Remove(cnx.Cliente.Where(x => x.Id.Equals(id)).First());

            cnx.SaveChanges();

            return View("Listado", cnx.Cliente.ToList());

        }

        private List<Examen.Models.Cliente> ListadoClientes()
        {

            cnx.Database.Connection.Open();

            List<Examen.Models.Cliente> auto = cnx.Cliente.ToList();

            cnx.Database.Connection.Close();

            return auto;
        }

    }
}
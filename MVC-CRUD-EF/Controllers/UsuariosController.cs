using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_CRUD_EF.Models;
using MVC_CRUD_EF.Repository.Entities;
using MVC_CRUD_EF.Services.Interfaces;
using Usuario = MVC_CRUD_EF.Repository.Entities.Usuario;

namespace MVC_CRUD_EF.Controllers
{
    //Proyecto copia, al que se le agregó Paginación, ordenamiento y búsqueda.
    //Se utiliza patrón Inyección de dependencia y Repository. 
    public class UsuariosController : Controller
    {
        private readonly BdmvccrudContext _context;
        private IUsuarioServices _usuarioServices; 
        

        public UsuariosController(IUsuarioServices usuarioServices)
        {
            this._usuarioServices = usuarioServices;
        }

        public async Task<IActionResult> Index2()
        {
            return View(await _usuarioServices.ObtenerUsuarios());
        }

        public async Task<IActionResult> Index(string buscar, string ordenActual, int? numpag, string filtroActual, int cantRegistros)
        {            
            ViewData["OrdenActual"] = ordenActual; //Estos viewdata se necesitan para la vista, para poder navegar entre páginas.
            ViewData["FiltroActual"] = buscar;

            ViewData["FiltroNombre"] = String.IsNullOrEmpty(ordenActual) ? "NombreDescendente" : ""; //Para ordenamiento.
            ViewData["FiltroFecha"] = ordenActual == "FechaAscendente" ? "FechaDescendente" : "FechaAscendente"; //Para ordenamiento.

            ViewData["CantidadRegistros"] = cantRegistros;
            

            try
            {
                return View(await _usuarioServices.ObtenerUsuariosPaginados(buscar, ordenActual, numpag, filtroActual, cantRegistros));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }            
        }          


        public async Task<IActionResult> Details(int id)
        {
            var usuariosDto2 = new UsuariosDto();
            var usuario = new MVC_CRUD_EF.Models.Usuario();

            usuariosDto2 = await _usuarioServices.ObtenerUsuarios();

            foreach (var item in usuariosDto2.Usuarios)
            {                
                if (id == item.Id)
                {
                    usuario.Id = item.Id;
                    usuario.Nombre = item.Nombre;
                    usuario.Clave = item.Clave;
                    usuario.Fecha = item.Fecha;
                }
            }

            return await Task.Run(() => View(usuario));
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Clave,Fecha")] UsuarioDto usuarioDto)
        {            
            await _usuarioServices.CrearUsuario(usuarioDto).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {            

            var usuariosDto2 = new UsuariosDto();
            var usuario = new MVC_CRUD_EF.Models.Usuario();

            usuariosDto2 = await _usuarioServices.ObtenerUsuarios();

            foreach (var item in usuariosDto2.Usuarios)
            {
                if (id == item.Id)
                {
                    usuario.Id = item.Id;
                    usuario.Nombre = item.Nombre;
                    usuario.Clave = item.Clave;
                    usuario.Fecha = item.Fecha;
                }
            }

            return await Task.Run(() => View(usuario));
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Clave,Fecha")] UsuarioDto usuarioDto)
        {
            await _usuarioServices.EditarUsuario(usuarioDto).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        //GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {            
            var usuariosDto2 = new UsuariosDto();
            var usuario = new MVC_CRUD_EF.Models.Usuario();

            usuariosDto2 = await _usuarioServices.ObtenerUsuarios();

            foreach (var item in usuariosDto2.Usuarios)
            {
                if (id == item.Id)
                {
                    usuario.Id = item.Id;
                    usuario.Nombre = item.Nombre;
                    usuario.Clave = item.Clave;
                    usuario.Fecha = item.Fecha;
                }
            }

            return await Task.Run(() => View(usuario));
        }

        

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {            
            await _usuarioServices.BorrarUsuarioPorId(id).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {            
            return true;
        }
    }
}

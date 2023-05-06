using Microsoft.EntityFrameworkCore;
using MVC_CRUD_EF.Models;
using MVC_CRUD_EF.Repository.Entities;
using MVC_CRUD_EF.Repository.Interfaces;
using Usuario = MVC_CRUD_EF.Repository.Entities.Usuario;

namespace MVC_CRUD_EF.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private BdmvccrudContext _context = new BdmvccrudContext();
        private List<Models.Usuario> _usuarios = new List<Models.Usuario>();

        public async Task<List<MVC_CRUD_EF.Models.Usuario>> ObtenerUsuarios()
        {
            
            var lista = from vehiculosBD in _context.Usuarios
                        select new
                        {
                            ident = vehiculosBD.Id,
                            nombre = vehiculosBD.Nombre,
                            clave = vehiculosBD.Clave,
                            fecha = vehiculosBD.Fecha
                        };

            foreach (var ve in lista)
            {
                _usuarios.Add(new Models.Usuario()
                {
                    Id = ve.ident,
                    Nombre = ve.nombre,
                    Clave = ve.clave,
                    Fecha = ve.fecha
                });
            }

            //return await Task.Run(() => _usuarios.Skip(1).Take(3).ToList());
            return await Task.Run(() => _usuarios);
        }

        public async Task<IQueryable<MVC_CRUD_EF.Repository.Entities.Usuario>> ObtenerUsuariosPaginados()
        {
            var usuarios = from usuariosBD in _context.Usuarios select usuariosBD;

            return await Task.Run(() => usuarios);
        }

        public async Task<bool> CrearUsuario (MVC_CRUD_EF.Models.Usuario usuario)
        {
            Repository.Entities.Usuario usuarioBD = new Repository.Entities.Usuario()
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Clave = usuario.Clave,
                Fecha = usuario.Fecha
            };

            _context.Usuarios.Add(usuarioBD);
            _context.SaveChanges();

            return await Task.Run(() => true);
        }

        public async Task<bool> BorrarUsuarioPorId(int id)
        {
            var usuarioID = from usuariosBD in _context.Usuarios
                            where usuariosBD.Id == id
                            select usuariosBD;

            foreach (var item in usuarioID)
            {
                _context.Usuarios.Remove(item);                
            }

            _context.SaveChanges();

            return await Task.Run(() => true);
        }

        public async Task<bool> EditarUsuario(Models.Usuario usuario)
        {
            Repository.Entities.Usuario usuarioBD = new Usuario()
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Clave = usuario.Clave,
                Fecha = usuario.Fecha
            };

            _context.Update(usuarioBD);
            _context.SaveChanges();

            return await Task.Run(() => true);
        }
    }
}

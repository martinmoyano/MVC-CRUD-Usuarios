using MVC_CRUD_EF.Models;
using MVC_CRUD_EF.Repository.Entities;
using MVC_CRUD_EF.Repository.Interfaces;
using MVC_CRUD_EF.Services.Interfaces;
using System.Collections;

namespace MVC_CRUD_EF.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        private IUsuarioRepository _usuarioRepository;
        private UsuariosDto _usuariosDto;
        private List<Models.Usuario> _usuarios = new List<Models.Usuario>();        

        public UsuarioServices(IUsuarioRepository usuarioRepository)
        {
            this._usuarioRepository = usuarioRepository;
        }

        public async Task<UsuariosDto> ObtenerUsuarios()
        {
            _usuarios = await _usuarioRepository.ObtenerUsuarios().ConfigureAwait(false);

            _usuariosDto = new UsuariosDto()
            {
                Usuarios = _usuarios
            };

            return await Task.Run(() => _usuariosDto);
        }

        
        public async Task<Paginacion<MVC_CRUD_EF.Models.Usuario>> ObtenerUsuariosPaginados(string buscar, string ordenActual, int? numpag, string filtroActual, int cantRegistros)
        {
            var usuarios = await _usuarioRepository.ObtenerUsuariosPaginados();


            int contador = 0;
            int cantidadRegistros = 5;            

            foreach (var item in usuarios)
            {
                contador++;
            }

            if (buscar != null)
                numpag = 1;
            else
                buscar = filtroActual;

            if (!String.IsNullOrEmpty(buscar))
            {
                usuarios = usuarios.Where(s => s.Nombre!.Contains(buscar));                
            }

            switch (ordenActual)
            {
                case "NombreDescendente":
                    usuarios = usuarios.OrderByDescending(usuario => usuario.Nombre);                    
                    break;
                case "FechaDescendente":
                    usuarios = usuarios.OrderByDescending(usuarios => usuarios.Fecha);
                    break;
                case "FechaAscendente":
                    usuarios = usuarios.OrderBy(usuarios => usuarios.Fecha);
                    break;
                default:
                    usuarios = usuarios.OrderBy(usuario => usuario.Nombre);
                    break;
            }

            foreach (var item in usuarios)
            {
                _usuarios?.Add(new Models.Usuario()
                {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    Clave = item.Clave,
                    Fecha = item.Fecha
                });
            }

            var items = _usuarios.ToList().Skip(((numpag ?? 1) - 1) * cantidadRegistros).Take(cantidadRegistros);

            return await Task.Run(() => new Paginacion<Models.Usuario>(items, contador, numpag ?? 1, cantidadRegistros));
        }

        public async Task<bool> CrearUsuario(Models.UsuarioDto usuarioDto)
        {
            Models.Usuario usuario = new Models.Usuario()
            {
                Id = usuarioDto.Id,
                Nombre = usuarioDto.Nombre,
                Clave = usuarioDto.Clave,
                Fecha = usuarioDto.Fecha
            };

            return await _usuarioRepository.CrearUsuario(usuario);
        }

        public async Task<bool> BorrarUsuarioPorId(int id)
        {
            //return await _usuarioRepository.BorrarUsuarioPorId(id);
            return await Task.Run(() => _usuarioRepository.BorrarUsuarioPorId(id));
        }

        public async Task<bool> EditarUsuario(Models.UsuarioDto usuarioDto)
        {
            Models.Usuario usuario = new Models.Usuario()
            {
                Id = usuarioDto.Id,
                Nombre = usuarioDto.Nombre,
                Clave = usuarioDto.Clave,
                Fecha = usuarioDto.Fecha
            };

            return await Task.Run(() => _usuarioRepository.EditarUsuario(usuario));
        }
    }
}

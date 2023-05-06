using MVC_CRUD_EF.Models;

namespace MVC_CRUD_EF.Services.Interfaces
{
    public interface IUsuarioServices
    {
        public Task<UsuariosDto> ObtenerUsuarios();
        public Task<Paginacion<MVC_CRUD_EF.Models.Usuario>> ObtenerUsuariosPaginados(string buscar, string ordenActual, int? numpag, string filtroActual, int cantRegistros);
        public Task<bool> CrearUsuario(Models.UsuarioDto usuarioDto);
        public Task<bool> BorrarUsuarioPorId(int id);
        public Task<bool> EditarUsuario(Models.UsuarioDto usuarioDto);
    }
}

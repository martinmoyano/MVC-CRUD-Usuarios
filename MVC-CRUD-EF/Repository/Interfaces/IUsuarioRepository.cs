using MVC_CRUD_EF.Models;

namespace MVC_CRUD_EF.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        public Task<List<Usuario>> ObtenerUsuarios();
        public Task<IQueryable<MVC_CRUD_EF.Repository.Entities.Usuario>> ObtenerUsuariosPaginados();
        public Task<bool> CrearUsuario(MVC_CRUD_EF.Models.Usuario usuario);
        public Task<bool> BorrarUsuarioPorId(int id);
        public Task<bool> EditarUsuario(Models.Usuario usuario);
    }
}

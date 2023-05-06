using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MVC_CRUD_EF.Models
{
    public class Paginacion<T> : List<T>
    {
        public int PaginaInicio { get; private set; } //Página principal donde se muestra el index, y los datos paginados
        public int PaginasTotales { get; private set; }        

        

        public Paginacion(IEnumerable<T> items, int contador, int paginaInicio, int cantidadregistros)
        {

            PaginaInicio = paginaInicio;
            PaginasTotales = (int)Math.Ceiling(contador / (double)cantidadregistros);

            try
            {
                if (items is null)
                {
                    throw new ArgumentNullException("Id Argument cannot be null");
                }
                else
                {
                    this.AddRange(items);
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }

            
        }


        public bool PaginasAnteriores => PaginaInicio > 1; //Pagina inicio hace referencia a la página actual.
        public bool PaginasPosteriores => PaginaInicio < PaginasTotales; //Si la página de inicio, es menor a las páginas totales, significa que hay páginas posteriores.
                
        

    }
}

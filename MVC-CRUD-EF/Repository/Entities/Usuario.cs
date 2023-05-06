using System;
using System.Collections.Generic;

namespace MVC_CRUD_EF.Repository.Entities;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public int? Clave { get; set; }

    public DateTime? Fecha { get; set; }
}

﻿namespace MVC_CRUD_EF.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int? Clave { get; set; }
        public DateTime? Fecha { get; set; } 
    }
}

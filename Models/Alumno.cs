using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Alumno
    {


        public int AlumnoId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }

    }
}

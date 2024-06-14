using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models;

public partial class Matricula
{
    public int idmatricula { get; set; }

    public int? idalumno { get; set; }

    public int? idgrado { get; set; }

    public DateOnly fechamatricula { get; set; }

    public string? seccion { get; set; }
    [JsonIgnore]
    public virtual Alumno? IdAlumnoNavigation { get; set; }
    [JsonIgnore]
    public virtual Grado? IdGradoNavigation { get; set; }
}

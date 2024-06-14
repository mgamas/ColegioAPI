using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models;

public partial class Profesor
{
    public int IdProfesor { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Genero { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Grado> Grados { get; set; } = new List<Grado>();
}

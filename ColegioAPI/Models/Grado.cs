using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models;

public partial class Grado
{
    public int idgrado { get; set; }

    public string nombre { get; set; } = null!;

    public int? idprofesor { get; set; }
    [JsonIgnore]
    public virtual Profesor? IdProfesorNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}

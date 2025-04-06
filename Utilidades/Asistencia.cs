namespace Utilidades
{
    public class Asistencia
    {
        public string Fecha { get; set; }
        public string CodigoGrupo { get; set; }
        public Dictionary<string, bool> EstadoPorDni { get; set; } = new Dictionary<string, bool>();
    }
}

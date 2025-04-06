namespace Utilidades
{
    public class Grupo
    {
        public string CodigoGrupo { get; set; }
        public List<string> DnisEstudiantes { get; set; } = new List<string>();
        public bool YaParticipóEnSorteo { get; set; } = false;
    }
}

namespace Utilidades
{
    public class Estudiante
    {
        public string DNI { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Grupo { get; set; } = "Sin asignar";
        public bool Participo { get; set; } = false; // Para el sorteo

        public Estudiante(string dni, string apellido, string nombre, string correo)
        {
            DNI = dni;
            Apellido = apellido;
            Nombre = nombre;
            Correo = correo;
        }
    }
}

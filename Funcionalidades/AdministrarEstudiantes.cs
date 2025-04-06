using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Utilidades;

namespace Funcionalidades
{
    public static class AdministrarEstudiantes
    {
        private static string archivo = "Datos/estudiantes.json";

        public static List<Estudiante> Cargar()
        {
            if (!File.Exists(archivo))
                return new List<Estudiante>();

            string json = File.ReadAllText(archivo);
            return JsonSerializer.Deserialize<List<Estudiante>>(json) ?? new List<Estudiante>();
        }
    }
}

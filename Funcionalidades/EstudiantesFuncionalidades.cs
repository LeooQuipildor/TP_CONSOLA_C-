using System;
using System.Collections.Generic;
using System.Linq;
using Utilidades;

namespace Funcionalidades
{
    public static class EstudiantesFuncionalidades
    {
        private static string rutaArchivo = "Datos/estudiantes.json";

        public static void AltaEstudiante()
        {
            Console.WriteLine("\n=== Alta de Estudiante ===");
            
            Console.Write("Ingrese DNI: ");
            string dni = Console.ReadLine();

            Console.Write("Ingrese Apellido: ");
            string apellido = Console.ReadLine();

            Console.Write("Ingrese Nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Ingrese Correo Electrónico: ");
            string correo = Console.ReadLine();

            var nuevoEstudiante = new Estudiante(dni, apellido, nombre, correo);

            // Carga de estudiantes para no perderlos cada vez que agregamos uno nuevo
            List<Estudiante> estudiantes = JsonHelper.LeerDesdeJson<Estudiante>(rutaArchivo) ?? new List<Estudiante>();

            // Agregar nuevo estudiante
            estudiantes.Add(nuevoEstudiante);

            // Guardar estudiante en JSON
            JsonHelper.GuardarEnJson(estudiantes, rutaArchivo);
            Console.WriteLine("Estudiante guardado exitosamente.");
        }
    }
}

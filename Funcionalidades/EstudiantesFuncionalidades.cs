using System;
using System.Collections.Generic;
using System.Linq;
using Utilidades;
using System.Text.Json;


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

        public static void BuscarEstudiante()
        {
            Console.WriteLine("\n=== Buscar Estudiante ===");
            Console.WriteLine("1. Buscar por DNI");
            Console.WriteLine("2. Buscar por Apellido");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();
            Console.Clear();

            List<Estudiante> estudiantes = JsonHelper.LeerDesdeJson<Estudiante>(rutaArchivo);

            if (estudiantes.Count == 0)
            {
                Console.WriteLine("No hay estudiantes registrados.");
                return;
            }

            Estudiante encontrado = null;

            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese el DNI: ");
                    string dni = Console.ReadLine();
                    encontrado = estudiantes.FirstOrDefault(e => e.DNI == dni);
                    break;

                case "2":
                    Console.Write("Ingrese el Apellido: ");
                    string apellido = Console.ReadLine();
                    encontrado = estudiantes.FirstOrDefault(e => e.Apellido.Equals(apellido, StringComparison.OrdinalIgnoreCase));
                    break;

                default:
                    Console.WriteLine("Opción inválida.");
                    return;
            }

            if (encontrado != null)
            {
                Console.WriteLine("\nEstudiante encontrado:");
                Console.WriteLine($"DNI: {encontrado.DNI}");
                Console.WriteLine($"Apellido: {encontrado.Apellido}");
                Console.WriteLine($"Nombre: {encontrado.Nombre}");
                Console.WriteLine($"Correo: {encontrado.Correo}");
                Console.WriteLine($"Grupo: {encontrado.Grupo}");
                Console.WriteLine($"¿Participó?: {(encontrado.Participo ? "Sí" : "No")}");
            }
            else
            {
                Console.WriteLine("Estudiante no encontrado.");
            }
        }

        public static void ModificarEstudiante()
        {
            Console.Clear();
            Console.WriteLine("=== Modificar Estudiante ===");
            Console.WriteLine("1. Buscar por DNI");
            Console.WriteLine("2. Buscar por Apellido");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            List<Estudiante> estudiantes = JsonHelper.LeerDesdeJson<Estudiante>(rutaArchivo);
            if (estudiantes.Count == 0)
            {
                Console.WriteLine("No hay estudiantes registrados.");
                return;
            }

            Estudiante encontrado = null;

            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese el DNI: ");
                    string dni = Console.ReadLine();
                    encontrado = estudiantes.FirstOrDefault(e => e.DNI == dni);
                    break;
                case "2":
                    Console.Write("Ingrese el Apellido: ");
                    string apellido = Console.ReadLine();
                    encontrado = estudiantes.FirstOrDefault(e => e.Apellido.Equals(apellido, StringComparison.OrdinalIgnoreCase));
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    return;
            }

            if (encontrado == null)
            {
                Console.WriteLine("Estudiante no encontrado.");
                return;
            }

            Console.WriteLine("\nDatos actuales:");
            Console.WriteLine($"1. DNI: {encontrado.DNI}");
            Console.WriteLine($"2. Apellido: {encontrado.Apellido}");
            Console.WriteLine($"3. Nombre: {encontrado.Nombre}");
            Console.WriteLine($"4. Correo: {encontrado.Correo}");

            Console.WriteLine("\nSeleccione qué desea modificar:");
            Console.WriteLine("1. Cambiar DNI");
            Console.WriteLine("2. Cambiar Apellido");
            Console.WriteLine("3. Cambiar Nombre");
            Console.WriteLine("4. Cambiar Correo");
            Console.WriteLine("5. Cambiar TODOS");
            Console.Write("Opción: ");
            string opcionModificar = Console.ReadLine();

            switch (opcionModificar)
            {
                case "1":
                    Console.Write("Nuevo DNI: ");
                    encontrado.DNI = Console.ReadLine();
                    break;
                case "2":
                    Console.Write("Nuevo Apellido: ");
                    encontrado.Apellido = Console.ReadLine();
                    break;
                case "3":
                    Console.Write("Nuevo Nombre: ");
                    encontrado.Nombre = Console.ReadLine();
                    break;
                case "4":
                    Console.Write("Nuevo Correo: ");
                    encontrado.Correo = Console.ReadLine();
                    break;
                case "5":
                    Console.Write("Nuevo DNI: ");
                    encontrado.DNI = Console.ReadLine();
                    Console.Write("Nuevo Apellido: ");
                    encontrado.Apellido = Console.ReadLine();
                    Console.Write("Nuevo Nombre: ");
                    encontrado.Nombre = Console.ReadLine();
                    Console.Write("Nuevo Correo: ");
                    encontrado.Correo = Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    return;
            }

            Console.Write("\n¿Desea guardar los cambios? (SI/NO): ");
            string confirmar = Console.ReadLine().ToUpper();

            if (confirmar == "SI")
            {
                JsonHelper.GuardarEnJson(estudiantes, rutaArchivo);
                Console.WriteLine("Estudiante modificado con éxito.");
            }
            else
            {
                Console.WriteLine("Modificación cancelada.");
            }
        }


        public static void EliminarEstudiante()
        {
            Console.Clear();
            Console.WriteLine("=== Eliminar Estudiante ===");
            Console.WriteLine("1. Buscar por DNI");
            Console.WriteLine("2. Buscar por Apellido");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            List<Estudiante> estudiantes = JsonHelper.LeerDesdeJson<Estudiante>(rutaArchivo);
            if (estudiantes.Count == 0)
            {
                Console.WriteLine("No hay estudiantes registrados.");
                return;
            }

            Estudiante encontrado = null;

            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese el DNI: ");
                    string dni = Console.ReadLine();
                    encontrado = estudiantes.FirstOrDefault(e => e.DNI == dni);
                    break;
                case "2":
                    Console.Write("Ingrese el Apellido: ");
                    string apellido = Console.ReadLine();
                    encontrado = estudiantes.FirstOrDefault(e => e.Apellido.Equals(apellido, StringComparison.OrdinalIgnoreCase));
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    return;
            }

            if (encontrado == null)
            {
                Console.WriteLine("Estudiante no encontrado.");
                return;
            }

            Console.WriteLine("\nEstudiante encontrado:");
            Console.WriteLine($"DNI: {encontrado.DNI}");
            Console.WriteLine($"Apellido: {encontrado.Apellido}");
            Console.WriteLine($"Nombre: {encontrado.Nombre}");
            Console.WriteLine($"Correo: {encontrado.Correo}");

            Console.Write("\n¿Desea eliminar este estudiante? (SI/NO): ");
            string confirmar = Console.ReadLine().ToUpper();

            if (confirmar == "SI")
            {
                estudiantes.Remove(encontrado);
                JsonHelper.GuardarEnJson(estudiantes, rutaArchivo);
                Console.WriteLine("Estudiante eliminado con éxito.");
            }
            else
            {
                Console.WriteLine("Operación cancelada.");
            }
        }


        public static void SorteoEstudiante()
{
    Console.Clear();
    Console.WriteLine("=== Sorteo de Estudiante ===");

    List<Estudiante> estudiantes = JsonHelper.LeerDesdeJson<Estudiante>(rutaArchivo);
    
    // Filtrar los estudiantes que aún no participaron
    var noParticiparon = estudiantes.Where(e => !e.Participo).ToList();

    if (noParticiparon.Count == 0)
    {
        // Si todos participaron, reiniciar la lista
        Console.WriteLine("Todos los estudiantes ya participaron. Se reiniciará el ciclo.");
        foreach (var estudiante in estudiantes)
        {
            estudiante.Participo = false;
        }
        JsonHelper.GuardarEnJson(estudiantes, rutaArchivo);
        noParticiparon = estudiantes;
    }

    // Seleccionar un estudiante aleatorio
    Random random = new Random();
    Estudiante seleccionado = noParticiparon[random.Next(noParticiparon.Count)];

    Console.WriteLine($"\n🎉 Estudiante seleccionado: {seleccionado.Nombre} {seleccionado.Apellido}");
    Console.WriteLine($"DNI: {seleccionado.DNI}");
    Console.WriteLine($"Correo: {seleccionado.Correo}");

    // Confirmar si participó
    Console.Write("\n¿Este estudiante pasó al pizarrón? (SI/NO): ");
    string confirmacion = Console.ReadLine().ToUpper();

    if (confirmacion == "SI")
    {
        seleccionado.Participo = true;
        JsonHelper.GuardarEnJson(estudiantes, rutaArchivo);
        Console.WriteLine("Estudiante marcado como 'Participó'.");
    }
    else
    {
        Console.WriteLine("Este estudiante quedará como prioridad para el próximo sorteo.");
    }
}




    }
}

using System;
using Funcionalidades;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n=== MENÚ PRINCIPAL ===");
            Console.WriteLine("1. Alta de estudiante");
            Console.WriteLine("2. Buscar estudiante");
            Console.WriteLine("3. Modificar estudiante");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();
            Console.Clear();

            switch (opcion)
            {
                case "1":
                    EstudiantesFuncionalidades.AltaEstudiante();
                    break;
                case "2":
                    EstudiantesFuncionalidades.BuscarEstudiante();
                    break;
                case "3":
                    EstudiantesFuncionalidades.ModificarEstudiante();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }


        }
    }
}


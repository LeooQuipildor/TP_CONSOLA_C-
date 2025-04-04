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
            Console.WriteLine("2. Salir");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    EstudiantesFuncionalidades.AltaEstudiante();
                    break;
                case "2":
                    return;
                default:
                    Console.WriteLine("Opcion invalida.");
                    break;
            }
        }
    }
}

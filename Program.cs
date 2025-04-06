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
            Console.WriteLine("4. Eliminar estudiante");
            Console.WriteLine("5. Sorteo de estudiante");
            Console.WriteLine("6. Gestión de grupos");
            Console.WriteLine("7. Gestión de asistencias");
            Console.WriteLine("8. Salir");
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
                    EstudiantesFuncionalidades.EliminarEstudiante();
                    break;
                case "5":
                    EstudiantesFuncionalidades.SorteoEstudiante();
                    break;
                case "6":
                    MenuGrupos();
                    break;
                case "7":
                    MenuAsistencias();
                    break;
                case "8":
                    return;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }

    static void MenuGrupos()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("====== MENÚ DE GRUPOS ======");
            Console.WriteLine("1. Crear Grupo");
            Console.WriteLine("2. Modificar Grupo");
            Console.WriteLine("3. Mover Estudiante de Grupo");
            Console.WriteLine("4. Eliminar Grupo");
            Console.WriteLine("5. Mostrar Todos los Grupos");
            Console.WriteLine("6. Estudiantes sin Grupo");
            Console.WriteLine("7. Grupos Incompletos (< 6 estudiantes)");
            Console.WriteLine("8. Realizar Sorteo por Grupo");
            Console.WriteLine("0. Volver al Menú Principal");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    GruposFuncionalidades.CrearGrupo();
                    break;
                case "2":
                    GruposFuncionalidades.ModificarGrupo();
                    break;
                case "3":
                    GruposFuncionalidades.MoverEstudiante();
                    break;
                case "4":
                    GruposFuncionalidades.EliminarGrupo();
                    break;
                case "5":
                    GruposFuncionalidades.MostrarTodosLosGrupos();
                    break;
                case "6":
                    GruposFuncionalidades.EstudiantesSinGrupo();
                    break;
                case "7":
                    GruposFuncionalidades.GruposIncompletos();
                    break;
                case "8":
                    GruposFuncionalidades.SorteoGrupo();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opción inválida. Presione una tecla para continuar.");
                    Console.ReadKey();
                    break;
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }
    }

    static void MenuAsistencias()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("====== MENÚ DE ASISTENCIAS ======");
            Console.WriteLine("1. Registrar Asistencia");
            Console.WriteLine("2. Ver Asistencia por Estudiante");
            Console.WriteLine("3. Ver Reporte de Asistencia por Grupo");
            Console.WriteLine("0. Volver al Menú Principal");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    AdministrarAsistencias.RegistrarAsistencia();
                    break;
                case "2":
                    AdministrarAsistencias.VerAsistenciaPorEstudiante();
                    break;
                case "3":
                    AdministrarAsistencias.VerReportePorGrupo();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opción inválida. Presione una tecla para continuar.");
                    Console.ReadKey();
                    break;
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}

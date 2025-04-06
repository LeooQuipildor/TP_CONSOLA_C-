using System.Globalization;
using Utilidades;

namespace Funcionalidades
{
    public static class AdministrarAsistencias
    {
        static string rutaGrupos = "Datos/grupos.json";
        static string rutaEstudiantes = "Datos/estudiantes.json";
        static string rutaAsistencias = "Datos/asistencias.json";

        public static void RegistrarAsistencia()
        {
            var grupos = Persistencia.Leer<List<Grupo>>(rutaGrupos);
            var estudiantes = Persistencia.Leer<List<Estudiante>>(rutaEstudiantes);
            var asistencias = Persistencia.Leer<List<Asistencia>>(rutaAsistencias);

            Console.Write("Ingrese código del grupo: ");
            string codigoGrupo = Console.ReadLine();

            var grupo = grupos.FirstOrDefault(g => g.CodigoGrupo == codigoGrupo);
            if (grupo == null)
            {
                Console.WriteLine("Grupo no encontrado.");
                return;
            }

            Console.Write("Ingrese fecha (dd/MM/yyyy): ");
            string fecha = Console.ReadLine();

            if (!DateTime.TryParseExact(fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                Console.WriteLine("Fecha inválida.");
                return;
            }

            if (asistencias.Any(a => a.CodigoGrupo == codigoGrupo && a.Fecha == fecha))
            {
                Console.WriteLine("Ya se registró asistencia para ese grupo en esa fecha.");
                return;
            }

            var nuevaAsistencia = new Asistencia
            {
                CodigoGrupo = codigoGrupo,
                Fecha = fecha
            };

            foreach (string dni in grupo.DnisEstudiantes)
            {
                var est = estudiantes.FirstOrDefault(e => e.DNI == dni);
                if (est != null)
                {
                    Console.Write($"¿Asistió {est.Apellido}, {est.Nombre}? (SI/NO): ");
                    string respuesta = Console.ReadLine().ToUpper();
                    bool asistio = respuesta == "SI";
                    nuevaAsistencia.EstadoPorDni[dni] = asistio;
                }
            }

            asistencias.Add(nuevaAsistencia);
            Persistencia.Guardar(rutaAsistencias, asistencias);
            Console.WriteLine("Asistencia registrada correctamente.");
        }

        public static void VerAsistenciaPorEstudiante()
        {
            var asistencias = Persistencia.Leer<List<Asistencia>>(rutaAsistencias);
            Console.Write("Ingrese DNI del estudiante: ");
            string dni = Console.ReadLine();

            var registros = asistencias
                .Where(a => a.EstadoPorDni.ContainsKey(dni))
                .Select(a => new { a.Fecha, a.CodigoGrupo, Asistio = a.EstadoPorDni[dni] })
                .ToList();

            if (registros.Count == 0)
            {
                Console.WriteLine("No hay asistencias registradas para ese estudiante.");
                return;
            }

            foreach (var reg in registros)
            {
                Console.WriteLine($"Fecha: {reg.Fecha} - Grupo: {reg.CodigoGrupo} - {(reg.Asistio ? "Presente" : "Ausente")}");
            }
        }

        public static void VerReportePorGrupo()
        {
            var asistencias = Persistencia.Leer<List<Asistencia>>(rutaAsistencias);
            var estudiantes = Persistencia.Leer<List<Estudiante>>(rutaEstudiantes);

            Console.Write("Ingrese código del grupo: ");
            string codigoGrupo = Console.ReadLine();

            var registros = asistencias
                .Where(a => a.CodigoGrupo == codigoGrupo)
                .ToList();

            if (registros.Count == 0)
            {
                Console.WriteLine("No hay asistencias para ese grupo.");
                return;
            }

            foreach (var asistencia in registros)
            {
                Console.WriteLine($"\nFecha: {asistencia.Fecha}");
                foreach (var kvp in asistencia.EstadoPorDni)
                {
                    var est = estudiantes.FirstOrDefault(e => e.DNI == kvp.Key);
                    string nombre = est != null ? $"{est.Apellido}, {est.Nombre}" : kvp.Key;
                    Console.WriteLine($"- {nombre}: {(kvp.Value ? "Presente" : "Ausente")}");
                }
            }
        }
    }
}

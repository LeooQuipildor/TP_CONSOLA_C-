using Utilidades;
using System.Text.Json;

namespace Funcionalidades
{
    public class AdministrarGrupo
    {
        private static string archivo = "grupos.json";

        public static List<Grupo> CargarGrupos()
        {
            if (!File.Exists(archivo))
                return new List<Grupo>();

            string json = File.ReadAllText(archivo);
            return JsonSerializer.Deserialize<List<Grupo>>(json) ?? new List<Grupo>();
        }

        public static void GuardarGrupos(List<Grupo> grupos)
        {
            string json = JsonSerializer.Serialize(grupos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(archivo, json);
        }

        public static void CrearGrupo()
        {
            Console.Write("Ingrese el código del nuevo grupo: ");
            string codigoGrupo = Console.ReadLine();

            var grupos = CargarGrupos();
            if (grupos.Any(g => g.CodigoGrupo == codigoGrupo))
            {
                Console.WriteLine("Ya existe un grupo con ese código.");
                return;
            }

            var nuevoGrupo = new Grupo { CodigoGrupo = codigoGrupo };
            var estudiantes = AdministrarEstudiantes.Cargar();




            while (nuevoGrupo.DnisEstudiantes.Count < 6)
            {
                Console.Write("Ingrese el DNI del estudiante (o 'FINALIZAR'): ");
                string entrada = Console.ReadLine();
                if (entrada.ToUpper() == "FINALIZAR") break;

                var estudiante = estudiantes.FirstOrDefault(e => e.DNI == entrada);
                if (estudiante == null)
                {
                    Console.WriteLine("Estudiante no encontrado.");
                    continue;
                }

                var grupoExistente = grupos.FirstOrDefault(g => g.DnisEstudiantes.Contains(entrada));
                if (grupoExistente != null)
                {
                    Console.WriteLine($"El estudiante ya pertenece al grupo {grupoExistente.CodigoGrupo}.");
                    Console.Write("¿Desea moverlo al nuevo grupo? (SI/NO): ");
                    string respuesta = Console.ReadLine().ToUpper();
                    if (respuesta != "SI")
                    {
                        continue;
                    }
                    grupoExistente.DnisEstudiantes.Remove(entrada);
                }

                if (!nuevoGrupo.DnisEstudiantes.Contains(entrada))
                {
                    nuevoGrupo.DnisEstudiantes.Add(entrada);
                    Console.WriteLine("Estudiante agregado.");
                }
                else
                {
                    Console.WriteLine("El estudiante ya fue agregado a este grupo.");
                }
            }

            grupos.Add(nuevoGrupo);
            GuardarGrupos(grupos);
            Console.WriteLine("Grupo creado correctamente.");
        }
    }
}

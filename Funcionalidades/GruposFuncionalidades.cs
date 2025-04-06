using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Utilidades;

namespace Funcionalidades
{
    public static class GruposFuncionalidades
    {
        private static string rutaGrupos = "Datos/grupos.json";
        private static string rutaEstudiantes = "Datos/estudiantes.json";

        public static void CrearGrupo()
        {
            Console.Clear();
            Console.WriteLine("=== CREACIÓN DE GRUPO ===");
            Console.Write("Ingrese código del nuevo grupo: ");
            string codigoGrupo = Console.ReadLine();

            var grupos = JsonHelper.LeerDesdeJson<Grupo>(rutaGrupos) ?? new List<Grupo>();
            var estudiantes = JsonHelper.LeerDesdeJson<Estudiante>(rutaEstudiantes) ?? new List<Estudiante>();

            // Validar si ya existe un grupo con ese código
            if (grupos.Any(g => g.CodigoGrupo == codigoGrupo))
            {
                Console.WriteLine("Ya existe un grupo con ese código. Intente con otro.");
                return;
            }

            Grupo nuevoGrupo = new Grupo { CodigoGrupo = codigoGrupo };

            while (nuevoGrupo.DnisEstudiantes.Count < 6)
            {
                Console.Write($"Ingrese DNI del estudiante #{nuevoGrupo.DnisEstudiantes.Count + 1} (o escriba FINALIZAR): ");
                string input = Console.ReadLine();

                if (input.Trim().ToUpper() == "FINALIZAR")
                    break;

                var estudiante = estudiantes.FirstOrDefault(e => e.DNI == input);

                if (estudiante == null)
                {
                    Console.WriteLine("No se encontró un estudiante con ese DNI.");
                    continue;
                }

                if (!string.IsNullOrEmpty(estudiante.Grupo))
                {
                    Console.WriteLine($"El estudiante ya pertenece al grupo: {estudiante.Grupo}");
                    Console.Write("¿Desea cambiarlo a este nuevo grupo? (SI/NO): ");
                    string confirmacion = Console.ReadLine().ToUpper();

                    if (confirmacion != "SI")
                        continue;

                    // Removerlo del grupo anterior
                    var grupoAnterior = grupos.FirstOrDefault(g => g.CodigoGrupo == estudiante.Grupo);
                    if (grupoAnterior != null)
                        grupoAnterior.DnisEstudiantes.Remove(estudiante.DNI);
                }

                // Asignar nuevo grupo
                estudiante.Grupo = codigoGrupo;
                nuevoGrupo.DnisEstudiantes.Add(estudiante.DNI);
                Console.WriteLine($"Estudiante {estudiante.Nombre} {estudiante.Apellido} agregado.");
            }

            grupos.Add(nuevoGrupo);

            JsonHelper.GuardarEnJson(grupos, rutaGrupos);
            JsonHelper.GuardarEnJson(estudiantes, rutaEstudiantes);

            Console.WriteLine("\nGrupo creado exitosamente.");
        }
    }
}
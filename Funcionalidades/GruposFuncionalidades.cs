using System;
using System.Collections.Generic;
using System.Linq;
using Utilidades;

namespace Funcionalidades
{
    public static class GruposFuncionalidades
    {
        private static string archivoGrupos = "Datos/grupos.json";
        private static string archivoEstudiantes = "Datos/estudiantes.json";

        // B.1 Crear grupo
        public static void CrearGrupo()
        {
            Console.Clear();
            Console.WriteLine("=== Crear Nuevo Grupo ===");

            Console.Write("Ingrese el código del grupo: ");
            string codigo = Console.ReadLine();

            List<Grupo> grupos = JsonHelper.LeerDesdeJson<Grupo>(archivoGrupos) ?? new List<Grupo>();
            List<Estudiante> estudiantes = JsonHelper.LeerDesdeJson<Estudiante>(archivoEstudiantes);

            if (grupos.Any(g => g.CodigoGrupo == codigo))
            {
                Console.WriteLine("Ya existe un grupo con ese código.");
                return;
            }

            Grupo nuevoGrupo = new Grupo { CodigoGrupo = codigo };

            while (nuevoGrupo.DnisEstudiantes.Count < 6)
            {
                Console.Write("Ingrese DNI del estudiante (o escriba FINALIZAR): ");
                string entrada = Console.ReadLine();

                if (entrada.ToUpper() == "FINALIZAR")
                    break;

                Estudiante estudiante = estudiantes.FirstOrDefault(e => e.DNI == entrada);
                if (estudiante == null)
                {
                    Console.WriteLine("DNI no encontrado.");
                    continue;
                }

                var grupoActual = grupos.FirstOrDefault(g => g.DnisEstudiantes.Contains(estudiante.DNI));
                if (grupoActual != null)
                {
                    Console.WriteLine($"Este estudiante ya pertenece al grupo {grupoActual.CodigoGrupo}.");
                    Console.Write("¿Desea moverlo al nuevo grupo? (SI/NO): ");
                    if (Console.ReadLine().ToUpper() != "SI") continue;

                    grupoActual.DnisEstudiantes.Remove(estudiante.DNI);
                }

                estudiante.Grupo = nuevoGrupo.CodigoGrupo;
                nuevoGrupo.DnisEstudiantes.Add(estudiante.DNI);
            }

            grupos.Add(nuevoGrupo);
            JsonHelper.GuardarEnJson(grupos, archivoGrupos);
            JsonHelper.GuardarEnJson(estudiantes, archivoEstudiantes);
            Console.WriteLine("Grupo creado correctamente.");
        }

        // B.2 Modificar grupo
        public static void ModificarGrupo()
        {
            Console.Clear();
            Console.WriteLine("=== Modificar Grupo ===");

            List<Grupo> grupos = JsonHelper.LeerDesdeJson<Grupo>(archivoGrupos);
            List<Estudiante> estudiantes = JsonHelper.LeerDesdeJson<Estudiante>(archivoEstudiantes);

            Console.Write("Ingrese código del grupo a modificar: ");
            string codigo = Console.ReadLine();

            var grupo = grupos.FirstOrDefault(g => g.CodigoGrupo == codigo);
            if (grupo == null)
            {
                Console.WriteLine("Grupo no encontrado.");
                return;
            }

            MostrarEstudiantes(grupo, estudiantes);

            Console.WriteLine("\n1. Agregar estudiante");
            Console.WriteLine("2. Eliminar estudiante");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            if (opcion == "1")
            {
                if (grupo.DnisEstudiantes.Count >= 6)
                {
                    Console.WriteLine("El grupo ya tiene 6 integrantes.");
                    return;
                }

                Console.Write("Ingrese DNI del estudiante: ");
                string dni = Console.ReadLine();

                var estudiante = estudiantes.FirstOrDefault(e => e.DNI == dni);
                if (estudiante == null)
                {
                    Console.WriteLine("Estudiante no encontrado.");
                    return;
                }

                if (!string.IsNullOrEmpty(estudiante.Grupo))
                {
                    Console.WriteLine($"Este estudiante ya está en el grupo {estudiante.Grupo}.");
                    Console.Write("¿Desea moverlo? (SI/NO): ");
                    if (Console.ReadLine().ToUpper() != "SI") return;

                    var grupoAnterior = grupos.FirstOrDefault(g => g.DnisEstudiantes.Contains(dni));
                    grupoAnterior?.DnisEstudiantes.Remove(dni);
                }

                grupo.DnisEstudiantes.Add(dni);
                estudiante.Grupo = grupo.CodigoGrupo;

                Console.WriteLine("Estudiante agregado correctamente.");
            }
            else if (opcion == "2")
            {
                Console.Write("Ingrese DNI del estudiante a eliminar: ");
                string dni = Console.ReadLine();

                if (!grupo.DnisEstudiantes.Contains(dni))
                {
                    Console.WriteLine("Estudiante no pertenece al grupo.");
                    return;
                }

                Console.Write("¿Está seguro? (SI/NO): ");
                if (Console.ReadLine().ToUpper() == "SI")
                {
                    grupo.DnisEstudiantes.Remove(dni);
                    var estudiante = estudiantes.FirstOrDefault(e => e.DNI == dni);
                    if (estudiante != null)
                        estudiante.Grupo = null;

                    Console.WriteLine("Estudiante eliminado del grupo.");
                }
            }

            JsonHelper.GuardarEnJson(grupos, archivoGrupos);
            JsonHelper.GuardarEnJson(estudiantes, archivoEstudiantes);
        }

        // B.3 Mover estudiante a otro grupo
        public static void MoverEstudiante()
        {
            Console.Clear();
            Console.WriteLine("=== Mover Estudiante entre Grupos ===");

            List<Grupo> grupos = JsonHelper.LeerDesdeJson<Grupo>(archivoGrupos);
            List<Estudiante> estudiantes = JsonHelper.LeerDesdeJson<Estudiante>(archivoEstudiantes);

            Console.Write("Ingrese DNI del estudiante: ");
            string dni = Console.ReadLine();

            var estudiante = estudiantes.FirstOrDefault(e => e.DNI == dni);
            if (estudiante == null)
            {
                Console.WriteLine("Estudiante no encontrado.");
                return;
            }

            Console.WriteLine($"Nombre: {estudiante.Nombre} {estudiante.Apellido}");
            Console.WriteLine($"Grupo actual: {estudiante.Grupo ?? "Ninguno"}");

            Console.Write("Ingrese nuevo código de grupo: ");
            string nuevoCodigo = Console.ReadLine();

            var grupoNuevo = grupos.FirstOrDefault(g => g.CodigoGrupo == nuevoCodigo);
            if (grupoNuevo == null)
            {
                Console.WriteLine("Grupo no encontrado.");
                return;
            }

            if (grupoNuevo.DnisEstudiantes.Count >= 6)
            {
                Console.WriteLine("El grupo ya tiene 6 integrantes.");
                return;
            }

            Console.Write("¿Desea mover al estudiante? (SI/NO): ");
            if (Console.ReadLine().ToUpper() != "SI") return;

            var grupoActual = grupos.FirstOrDefault(g => g.DnisEstudiantes.Contains(dni));
            grupoActual?.DnisEstudiantes.Remove(dni);

            grupoNuevo.DnisEstudiantes.Add(dni);
            estudiante.Grupo = grupoNuevo.CodigoGrupo;

            JsonHelper.GuardarEnJson(grupos, archivoGrupos);
            JsonHelper.GuardarEnJson(estudiantes, archivoEstudiantes);

            Console.WriteLine("Estudiante movido correctamente.");
        }

        // B.4 Eliminar grupo
        public static void EliminarGrupo()
        {
            Console.Clear();
            Console.WriteLine("=== Eliminar Grupo ===");

            List<Grupo> grupos = JsonHelper.LeerDesdeJson<Grupo>(archivoGrupos);
            List<Estudiante> estudiantes = JsonHelper.LeerDesdeJson<Estudiante>(archivoEstudiantes);

            Console.Write("Ingrese código del grupo a eliminar: ");
            string codigo = Console.ReadLine();

            var grupo = grupos.FirstOrDefault(g => g.CodigoGrupo == codigo);
            if (grupo == null)
            {
                Console.WriteLine("Grupo no encontrado.");
                return;
            }

            foreach (string dni in grupo.DnisEstudiantes)
            {
                var est = estudiantes.FirstOrDefault(e => e.DNI == dni);
                if (est != null) est.Grupo = null;
            }

            grupos.Remove(grupo);
            JsonHelper.GuardarEnJson(grupos, archivoGrupos);
            JsonHelper.GuardarEnJson(estudiantes, archivoEstudiantes);

            Console.WriteLine("Grupo eliminado correctamente.");
        }

        // B.5 Listar todos los grupos
        public static void MostrarTodosLosGrupos()
        {
            Console.Clear();
            Console.WriteLine("=== Listado de Grupos ===");

            List<Grupo> grupos = JsonHelper.LeerDesdeJson<Grupo>(archivoGrupos);
            List<Estudiante> estudiantes = JsonHelper.LeerDesdeJson<Estudiante>(archivoEstudiantes);

            foreach (var grupo in grupos)
            {
                Console.WriteLine($"\nGrupo {grupo.CodigoGrupo}:");
                var integrantes = estudiantes
                    .Where(e => grupo.DnisEstudiantes.Contains(e.DNI))
                    .OrderBy(e => e.Apellido)
                    .ToList();

                foreach (var est in integrantes)
                {
                    Console.WriteLine($"- {est.Apellido}, {est.Nombre} (DNI: {est.DNI})");
                }
            }
        }

        // B.6 Estudiantes sin grupo
        public static void EstudiantesSinGrupo()
        {
            Console.Clear();
            Console.WriteLine("=== Estudiantes sin Grupo ===");

            List<Estudiante> estudiantes = JsonHelper.LeerDesdeJson<Estudiante>(archivoEstudiantes);
            var sinGrupo = estudiantes.Where(e => string.IsNullOrEmpty(e.Grupo)).ToList();

            foreach (var e in sinGrupo)
                Console.WriteLine($"{e.Apellido}, {e.Nombre} - DNI: {e.DNI}");
        }

        // B.7 Grupos con menos de 6 integrantes
        public static void GruposIncompletos()
        {
            Console.Clear();
            Console.WriteLine("=== Grupos con menos de 6 integrantes ===");

            List<Grupo> grupos = JsonHelper.LeerDesdeJson<Grupo>(archivoGrupos);

            foreach (var g in grupos.Where(g => g.DnisEstudiantes.Count < 6))
                Console.WriteLine($"{g.CodigoGrupo}: {g.DnisEstudiantes.Count} integrantes");
        }

        // B.8 Sorteo por grupos
        public static void SorteoGrupo()
        {
            Console.Clear();
            Console.WriteLine("=== Sorteo de Grupo ===");

            List<Grupo> grupos = JsonHelper.LeerDesdeJson<Grupo>(archivoGrupos);
            var disponibles = grupos.Where(g => !g.YaParticipóEnSorteo).ToList();

            if (disponibles.Count == 0)
            {
                Console.WriteLine("Todos los grupos ya participaron. Reiniciando sorteo...");
                foreach (var g in grupos) g.YaParticipóEnSorteo = false;
                JsonHelper.GuardarEnJson(grupos, archivoGrupos);
                disponibles = grupos;
            }

            Random rand = new Random();
            var seleccionado = disponibles[rand.Next(disponibles.Count)];
            Console.WriteLine($"🎉 Grupo seleccionado: {seleccionado.CodigoGrupo}");

            seleccionado.YaParticipóEnSorteo = true;
            JsonHelper.GuardarEnJson(grupos, archivoGrupos);
        }

        private static void MostrarEstudiantes(Grupo grupo, List<Estudiante> estudiantes)
        {
            Console.WriteLine($"Grupo {grupo.CodigoGrupo}:");
            int i = 1;
            foreach (string dni in grupo.DnisEstudiantes)
            {
                var est = estudiantes.FirstOrDefault(e => e.DNI == dni);
                if (est != null)
                    Console.WriteLine($"{i++}. {est.Apellido}, {est.Nombre} - DNI: {est.DNI}");
            }
        }
    }
}

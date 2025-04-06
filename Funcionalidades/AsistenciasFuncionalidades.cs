using System;
using System.Collections.Generic;
using Funcionalidades;

namespace Funcionalidades
{
    public static class AsistenciasFuncionalidades
    {
        public static void RegistrarAsistencias()
        {
            AdministrarAsistencias.RegistrarAsistencia();
        }

        public static void VerAsistenciasPorEstudiante()
        {
            AdministrarAsistencias.VerAsistenciaPorEstudiante();
        }

        public static void ReportePorGrupo()
        {
            AdministrarAsistencias.VerReportePorGrupo();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json; 
// Instalar Newtonsoft.Json para convertir objetos como Estudiantes en texto JSON Leer ese texto y convertirlo otra vez en objetos.
// Comando: dotnet add package Newtonsoft.Json


namespace Utilidades
{
    public static class JsonHelper
    {
        public static void GuardarEnJson<T>(List<T> data, string rutaArchivo)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented); // Convierte la lista de objetos en texto JSON(guarda)
            File.WriteAllText(rutaArchivo, json);
        }

        public static List<T> LeerDesdeJson<T>(string rutaArchivo)
        {
            if (!File.Exists(rutaArchivo))
                return new List<T>();

            string json = File.ReadAllText(rutaArchivo);
            return JsonConvert.DeserializeObject<List<T>>(json); // Convierte el texto JSON en lista de objetos(lee)
        }
    }
}

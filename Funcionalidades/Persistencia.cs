using System.Text.Json;

namespace Funcionalidades
{
    public static class Persistencia
    {
        public static void Guardar<T>(string ruta, T datos)
        {
            string json = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ruta, json);
        }

        public static T Leer<T>(string ruta)
        {
            if (!File.Exists(ruta))
                return Activator.CreateInstance<T>();

            string json = File.ReadAllText(ruta);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}

using System.Text.Json;

public static class JsonFileLoader
{
    private static readonly JsonSerializerOptions Options = new JsonSerializerOptions()
    {
        PropertyNameCaseInsensitive = true
    };

    public static List<T> LoadList<T>(string rutaArchivo)
    {
        string json = File.ReadAllText(rutaArchivo);
        return JsonSerializer.Deserialize<List<T>>(json, Options) ?? new List<T>();
    }
}

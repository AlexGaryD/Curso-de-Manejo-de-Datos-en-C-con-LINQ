public static class ConsolePrinter
{
    private const string Formato = "{0,-60} {1,15} {2,15}";

    public static void PrintValues(IEnumerable<Book> listadelibros)
    {
        Console.WriteLine(Formato + "\n", "Title", "N. Paginas", "Fecha publicación");
        foreach (var item in listadelibros)
        {
            Console.WriteLine(Formato, item.Title, item.PageCount, item.PublishedDate.ToShortDateString());
        }
    }

    public static void PrintValue(string etiqueta, object? valor)
    {
        Console.WriteLine($"{etiqueta}: {valor}");
    }
}

public static class BookQueryExtensions
{
    public static IEnumerable<Book> PublicadosDespuesDe(this IEnumerable<Book> libros, int anio)
    {
        return libros.Where(p => p.PublishedDate.Year > anio);
    }

    public static IEnumerable<Book> DeCategoria(this IEnumerable<Book> libros, string categoria)
    {
        return libros.Where(p => p.Categories.Contains(categoria));
    }

    public static IEnumerable<Book> ConMasDePaginas(this IEnumerable<Book> libros, int paginas)
    {
        return libros.Where(p => p.PageCount > paginas);
    }

    public static IEnumerable<Book> ConPaginasEntre(this IEnumerable<Book> libros, int minimo, int maximo)
    {
        return libros.Where(p => p.PageCount >= minimo && p.PageCount <= maximo);
    }

    public static IEnumerable<Book> ConTituloQueContiene(this IEnumerable<Book> libros, string texto)
    {
        return libros.Where(p => p.Title.Contains(texto));
    }
}

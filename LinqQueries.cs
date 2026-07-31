using System.Text.Json;

public class LinqQueries
{
    private const string DefaultDataFilePath = "books.json";

    private readonly List<Book> librosCollection;

    public LinqQueries() : this(DefaultDataFilePath)
    {
    }

    public LinqQueries(string dataFilePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(dataFilePath);

        string json;
        try
        {
            json = File.ReadAllText(dataFilePath);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            throw new InvalidOperationException(
                $"No se pudo leer el archivo de datos '{Path.GetFullPath(dataFilePath)}'.", ex);
        }

        List<Book?>? libros;
        try
        {
            libros = JsonSerializer.Deserialize<List<Book?>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException(
                $"El archivo de datos '{Path.GetFullPath(dataFilePath)}' no contiene un JSON válido.", ex);
        }

        if (libros is null)
        {
            throw new InvalidOperationException(
                $"El archivo de datos '{Path.GetFullPath(dataFilePath)}' no contiene una colección de libros.");
        }

        int indiceNulo = libros.FindIndex(l => l is null);
        if (indiceNulo >= 0)
        {
            throw new InvalidOperationException(
                $"El archivo de datos '{Path.GetFullPath(dataFilePath)}' contiene un libro nulo en la posición {indiceNulo}.");
        }

        librosCollection = libros.Select(l => l!).ToList();
    }

    public IEnumerable<Book> TodaLaColeccion()
    {
        return this.librosCollection;
    }
    public IEnumerable<Book> LibrosDespuesdel2000()
    {
       //extesion method
       // return librosCollection.Where(p=> p.PublishedDate.Year > 2000);
       return from l in librosCollection
              where l.PublishedDate.Year > 2000
              select l;
    }
    public IEnumerable<Book> LibrosConMasde250PagConPalabrasInAction()
    {
        //extension method
        //return librosCollection.Where(p=> p.PageCount > 250 && p.Title.Contains("in Action"));
        //query expression
        return from l in librosCollection
               where l.PageCount > 250 && l.Title is not null && l.Title.Contains("in Action")
               select l;
    }

    public bool TodosLosLibrosTienenStatus()
    {
        return librosCollection.All(p=> !string.IsNullOrWhiteSpace(p.Status));
    }

    public bool SiAlgunLibroFuePublicado2005()
    {
        return librosCollection.Any(p=> p.PublishedDate.Year == 2005);
    }

    public IEnumerable<Book> LibrosdePython()
    {
        return librosCollection.Where(p=> TieneCategoria(p, "Python"));
    }

    public IEnumerable<Book> LibrosdeJavaPorNombreAscendente()
    {
        return librosCollection.Where(p=> TieneCategoria(p, "Java")).OrderBy(p=> p.Title);
    }

    public IEnumerable<Book> Librosdemas450pagDescendente()
    {
        return librosCollection.Where(p=> p.PageCount > 450).OrderByDescending(p=> p.PageCount);
    }

    public IEnumerable<Book> TresLibrosOrdenadosPorFecha()
    {
        return librosCollection.Where(p=> TieneCategoria(p, "Java")).OrderByDescending(p=> p.PublishedDate).Take(3);
    }

    public IEnumerable<Book> CuatroLibrosdemas400pag()
    {
        return librosCollection.Where(p=> p.PageCount > 400).Take(4).Skip(2);
    }

    public IEnumerable<Book> TresPrimerosLibros()
    {
        return librosCollection.Take(3).Select(p=>new Book (){ Title = p.Title, PageCount = p.PageCount});
    }

    public int CantidadLibros()
    {
        return librosCollection.Where(p=> p.PageCount>=200 && p.PageCount<=500).Count();
    }

     public long CantidadLibros64bits()
    {
        return librosCollection.LongCount(p=> p.PageCount>=200 && p.PageCount<=500);
    }

    public DateTime FechaMenorReciente()
    {
        return LibrosNoVacios(nameof(FechaMenorReciente)).Min(p=> p.PublishedDate);
    }

    public DateTime FechaMasReciente()
    {
        return LibrosNoVacios(nameof(FechaMasReciente)).Max(p=> p.PublishedDate);
    }

    public int NumerodePagMayor()
    {
        return LibrosNoVacios(nameof(NumerodePagMayor)).Max(p=> p.PageCount);
    }

    public Book LibroconMenorNumeroDePaginas()
    {
        return LibrosNoVacios(nameof(LibroconMenorNumeroDePaginas))
            .Where(p=> p.PageCount>0)
            .MinBy(p=> p.PageCount)
            ?? throw new InvalidOperationException(
                "Ningún libro de la colección tiene un número de páginas mayor que cero.");
    }

    public Book LibroconFechaMasReciente()
    {
        return LibrosNoVacios(nameof(LibroconFechaMasReciente)).MaxBy(p=> p.PublishedDate)!;
    }

    public int SumaTotaldePaginas()
    {
        return librosCollection.Where(p=> p.PageCount >=0 && p.PageCount <= 500).Sum(p=> p.PageCount);
    }

    public string TitulosLibrosDespuesdel2015()
    {
        return librosCollection.Where(p=> p.PublishedDate.Year > 2015)
        
        .Aggregate("", (TitulosLibros, next) =>
        {
            if (TitulosLibros != string.Empty)
            {
                TitulosLibros += ", - " + next.Title;
            }
            else
            {
                TitulosLibros += next.Title;
            }
            return TitulosLibros;
        });
    }
    public ILookup<char, Book> DictionaryBookByChar()
    {
        Book? libroSinTitulo = librosCollection.FirstOrDefault(x => string.IsNullOrEmpty(x.Title));
        if (libroSinTitulo is not null)
        {
            throw new InvalidOperationException(
                "No se puede agrupar por letra inicial: la colección contiene libros sin título.");
        }

	    // En el ToLookUp se pone los valores del diccionario que vas a retornar (char, book)
	    return librosCollection.ToLookup(x => x.Title![0], x => x);
    }   

    private static bool TieneCategoria(Book libro, string categoria)
    {
        return libro.Categories is not null && libro.Categories.Contains(categoria);
    }

    private List<Book> LibrosNoVacios(string consulta)
    {
        if (librosCollection.Count == 0)
        {
            throw new InvalidOperationException(
                $"La colección de libros está vacía; '{consulta}' requiere al menos un libro.");
        }

        return librosCollection;
    }
}

using System.Reflection;

public class LinqQueries
{
    private List<Book> librosCollection = new List<Book>();
    public LinqQueries()
    {
        using(StreamReader reader = new StreamReader("books.json"))
        {
            string json = reader.ReadToEnd();
            this.librosCollection = System.Text.Json.JsonSerializer.Deserialize<List<Book>>
             (json, new System.Text.Json.JsonSerializerOptions()
                 {
                    PropertyNameCaseInsensitive = true
                 }
             );
        }
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
               where l.PageCount > 250 && l.Title.Contains("in Action")
               select l;
    }

    public bool TodosLosLibrosTienenStatus()
    {
        return librosCollection.All(p=> p.Status != string.Empty);
    }

    public bool SiAlgunLibroFuePublicado2005()
    {
        return librosCollection.Any(p=> p.PublishedDate.Year == 2005);
    }

    public IEnumerable<Book> LibrosdePython()
    {
        return librosCollection.Where(p=> p.Categories.Contains("Python"));
    }

    public IEnumerable<Book> LibrosdeJavaPorNombreAscendente()
    {
        return librosCollection.Where(p=> p.Categories.Contains("Java")).OrderBy(p=> p.Title);
    }

    public IEnumerable<Book> Librosdemas450pagDescendente()
    {
        return librosCollection.Where(p=> p.PageCount > 450).OrderByDescending(p=> p.PageCount);
    }

    public IEnumerable<Book> TresLibrosOrdenadosPorFecha()
    {
        return librosCollection.Where(p=> p.Categories.Contains("Java")).OrderByDescending(p=> p.PublishedDate).Take(3);
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
        return librosCollection.Min(p=> p.PublishedDate);
    }

    public DateTime FechaMasReciente()
    {
        return librosCollection.Max(p=> p.PublishedDate);
    }

    public int NumerodePagMayor()
    {
        return librosCollection.Max(p=> p.PageCount);
    }

    public Book LibroconMenorNumeroDePaginas()
    {
        return librosCollection.Where(p=> p.PageCount>0).MinBy(p=> p.PageCount);
    }

    public Book LibroconFechaMasReciente()
    {
        return librosCollection.MaxBy(p=> p.PublishedDate);
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
	    // En el ToLookUp se pone los valores del diccionario que vas a retornar (char, book)
	    return librosCollection.ToLookup(x => x.Title[0], x => x);
    }   


    
}
public class LinqQueries
{
    private List<Book> librosCollection = new List<Book>();
    public LinqQueries()
    {
        this.librosCollection = JsonFileLoader.LoadList<Book>("books.json");
    }
    public IEnumerable<Book> TodaLaColeccion()
    {
        return this.librosCollection;
    }
    public IEnumerable<Book> LibrosDespuesdel2000()
    {
        return librosCollection.PublicadosDespuesDe(2000);
    }
    public IEnumerable<Book> LibrosConMasde250PagConPalabrasInAction()
    {
        return librosCollection.ConMasDePaginas(250).ConTituloQueContiene("in Action");
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
        return librosCollection.DeCategoria("Python");
    }

    public IEnumerable<Book> LibrosdeJavaPorNombreAscendente()
    {
        return librosCollection.DeCategoria("Java").OrderBy(p=> p.Title);
    }

    public IEnumerable<Book> Librosdemas450pagDescendente()
    {
        return librosCollection.ConMasDePaginas(450).OrderByDescending(p=> p.PageCount);
    }

    public IEnumerable<Book> TresLibrosOrdenadosPorFecha()
    {
        return librosCollection.DeCategoria("Java").OrderByDescending(p=> p.PublishedDate).Take(3);
    }

    public IEnumerable<Book> CuatroLibrosdemas400pag()
    {
        return librosCollection.ConMasDePaginas(400).Take(4).Skip(2);
    }

    public IEnumerable<Book> TresPrimerosLibros()
    {
        return librosCollection.Take(3).Select(p=>new Book (){ Title = p.Title, PageCount = p.PageCount});
    }

    public int CantidadLibros()
    {
        return librosCollection.ConPaginasEntre(200, 500).Count();
    }

     public long CantidadLibros64bits()
    {
        return librosCollection.ConPaginasEntre(200, 500).LongCount();
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
        return librosCollection.ConMasDePaginas(0).MinBy(p=> p.PageCount);
    }

    public Book LibroconFechaMasReciente()
    {
        return librosCollection.MaxBy(p=> p.PublishedDate);
    }

    public int SumaTotaldePaginas()
    {
        return librosCollection.ConPaginasEntre(0, 500).Sum(p=> p.PageCount);
    }

    public string TitulosLibrosDespuesdel2015()
    {
        return string.Join(", - ", librosCollection.PublicadosDespuesDe(2015).Select(p => p.Title));
    }

    public ILookup<char, Book> DictionaryBookByChar()
    {
	    // En el ToLookUp se pone los valores del diccionario que vas a retornar (char, book)
	    return librosCollection.ToLookup(x => x.Title[0], x => x);
    }
}

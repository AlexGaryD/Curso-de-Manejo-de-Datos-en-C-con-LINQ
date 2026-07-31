public class LinqQueriesTests
{
    private readonly LinqQueries queries = new LinqQueries();
    private readonly List<Book> coleccion;

    public LinqQueriesTests()
    {
        coleccion = queries.TodaLaColeccion().ToList();
    }

    [Fact]
    public void TodaLaColeccion_CargaLosLibrosDelJson()
    {
        Assert.NotEmpty(coleccion);
        Assert.All(coleccion, l => Assert.False(string.IsNullOrWhiteSpace(l.Title)));
    }

    [Fact]
    public void LibrosDespuesdel2000_SoloDevuelveLibrosPosterioresAl2000()
    {
        var resultado = queries.LibrosDespuesdel2000().ToList();

        Assert.All(resultado, l => Assert.True(l.PublishedDate.Year > 2000));
        Assert.Equal(coleccion.Count(l => l.PublishedDate.Year > 2000), resultado.Count);
    }

    [Fact]
    public void LibrosConMasde250PagConPalabrasInAction_FiltraPorPaginasYTitulo()
    {
        var resultado = queries.LibrosConMasde250PagConPalabrasInAction().ToList();

        Assert.All(resultado, l =>
        {
            Assert.True(l.PageCount > 250);
            Assert.Contains("in Action", l.Title);
        });
        Assert.Equal(
            coleccion.Count(l => l.PageCount > 250 && l.Title.Contains("in Action")),
            resultado.Count);
    }

    [Fact]
    public void TodosLosLibrosTienenStatus_EsVerdaderoCuandoNingunStatusEstaVacio()
    {
        Assert.Equal(coleccion.All(l => l.Status != string.Empty), queries.TodosLosLibrosTienenStatus());
    }

    [Fact]
    public void SiAlgunLibroFuePublicado2005_CoincideConLaColeccion()
    {
        Assert.Equal(coleccion.Any(l => l.PublishedDate.Year == 2005), queries.SiAlgunLibroFuePublicado2005());
    }

    [Fact]
    public void LibrosdePython_SoloDevuelveLibrosDeLaCategoriaPython()
    {
        var resultado = queries.LibrosdePython().ToList();

        Assert.All(resultado, l => Assert.Contains("Python", l.Categories));
        Assert.Equal(coleccion.Count(l => l.Categories.Contains("Python")), resultado.Count);
    }

    [Fact]
    public void LibrosdeJavaPorNombreAscendente_FiltraJavaYOrdenaPorTitulo()
    {
        var resultado = queries.LibrosdeJavaPorNombreAscendente().ToList();

        Assert.All(resultado, l => Assert.Contains("Java", l.Categories));
        Assert.Equal(resultado.OrderBy(l => l.Title, StringComparer.CurrentCulture).Select(l => l.Title), resultado.Select(l => l.Title));
    }

    [Fact]
    public void Librosdemas450pagDescendente_FiltraPorPaginasYOrdenaDescendente()
    {
        var resultado = queries.Librosdemas450pagDescendente().ToList();

        Assert.All(resultado, l => Assert.True(l.PageCount > 450));
        Assert.Equal(resultado.OrderByDescending(l => l.PageCount).Select(l => l.PageCount), resultado.Select(l => l.PageCount));
    }

    [Fact]
    public void TresLibrosOrdenadosPorFecha_DevuelveComoMaximoTresLibrosDeJavaMasRecientes()
    {
        var resultado = queries.TresLibrosOrdenadosPorFecha().ToList();
        var esperados = coleccion
            .Where(l => l.Categories.Contains("Java"))
            .OrderByDescending(l => l.PublishedDate)
            .Take(3)
            .Select(l => l.Title);

        Assert.True(resultado.Count <= 3);
        Assert.Equal(esperados, resultado.Select(l => l.Title));
    }

    [Fact]
    public void CuatroLibrosdemas400pag_TomaCuatroLibrosYOmiteLosDosPrimeros()
    {
        var resultado = queries.CuatroLibrosdemas400pag().ToList();
        var esperados = coleccion.Where(l => l.PageCount > 400).Take(4).Skip(2).Select(l => l.Title);

        Assert.Equal(esperados, resultado.Select(l => l.Title));
        Assert.True(resultado.Count <= 2);
    }

    [Fact]
    public void TresPrimerosLibros_ProyectaSoloTituloYPaginas()
    {
        var resultado = queries.TresPrimerosLibros().ToList();

        Assert.Equal(Math.Min(3, coleccion.Count), resultado.Count);
        Assert.Equal(coleccion.Take(3).Select(l => l.Title), resultado.Select(l => l.Title));
        Assert.All(resultado, l =>
        {
            Assert.Null(l.Status);
            Assert.Null(l.Authors);
            Assert.Null(l.Categories);
            Assert.Equal(default, l.PublishedDate);
        });
    }

    [Fact]
    public void CantidadLibros_CuentaLibrosEntre200Y500Paginas()
    {
        Assert.Equal(coleccion.Count(l => l.PageCount >= 200 && l.PageCount <= 500), queries.CantidadLibros());
    }

    [Fact]
    public void CantidadLibros64bits_CoincideConLaVersionDe32bits()
    {
        Assert.Equal(queries.CantidadLibros(), queries.CantidadLibros64bits());
    }

    [Fact]
    public void FechaMenorReciente_DevuelveLaFechaMinima()
    {
        Assert.Equal(coleccion.Min(l => l.PublishedDate), queries.FechaMenorReciente());
    }

    [Fact]
    public void FechaMasReciente_DevuelveLaFechaMaxima()
    {
        Assert.Equal(coleccion.Max(l => l.PublishedDate), queries.FechaMasReciente());
    }

    [Fact]
    public void NumerodePagMayor_DevuelveElMaximoDePaginas()
    {
        Assert.Equal(coleccion.Max(l => l.PageCount), queries.NumerodePagMayor());
    }

    [Fact]
    public void LibroconMenorNumeroDePaginas_IgnoraLibrosSinPaginas()
    {
        var libro = queries.LibroconMenorNumeroDePaginas();

        Assert.NotNull(libro);
        Assert.True(libro.PageCount > 0);
        Assert.Equal(coleccion.Where(l => l.PageCount > 0).Min(l => l.PageCount), libro.PageCount);
    }

    [Fact]
    public void LibroconFechaMasReciente_DevuelveElLibroConLaFechaMaxima()
    {
        var libro = queries.LibroconFechaMasReciente();

        Assert.NotNull(libro);
        Assert.Equal(coleccion.Max(l => l.PublishedDate), libro.PublishedDate);
    }

    [Fact]
    public void SumaTotaldePaginas_SumaSoloLibrosDeHasta500Paginas()
    {
        Assert.Equal(
            coleccion.Where(l => l.PageCount >= 0 && l.PageCount <= 500).Sum(l => l.PageCount),
            queries.SumaTotaldePaginas());
    }

    [Fact]
    public void TitulosLibrosDespuesdel2015_ConcatenaLosTitulosSeparadosPorComa()
    {
        var resultado = queries.TitulosLibrosDespuesdel2015();
        var titulos = coleccion.Where(l => l.PublishedDate.Year > 2015).Select(l => l.Title).ToList();

        Assert.Equal(string.Join(", - ", titulos), resultado);
    }

    [Fact]
    public void DictionaryBookByChar_AgrupaLosLibrosPorLaPrimeraLetraDelTitulo()
    {
        var lookup = queries.DictionaryBookByChar();

        Assert.Equal(coleccion.Select(l => l.Title[0]).Distinct().Count(), lookup.Count);
        foreach (var grupo in lookup)
        {
            Assert.All(grupo, l => Assert.Equal(grupo.Key, l.Title[0]));
        }
        Assert.Equal(coleccion.Count, lookup.Sum(g => g.Count()));
    }
}

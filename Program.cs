try
{
    LinqQueries queries = new LinqQueries();

    //Toda la colección
    PrintValues(queries.TodaLaColeccion());

    //Libros despues del 2000
    //PrintValues(queries.LibrosDespuesdel2000());
    //Libros con más de 250 páginas y que contienen la palabra "In Action"
    //PrintValues(queries.LibrosConMasde250PagConPalabrasInAction());
    //Todos los libros tienen un status?
    //Console.WriteLine($" ¿Todos los libros tienen un status? - {queries.TodosLosLibrosTienenStatus()}");
    //Si algún libro fue publicado en 2005
    //Console.WriteLine($" ¿Algún libro fue publicado en 2005? - {queries.SiAlgunLibroFuePublicado2005()}");
    //Libros de Python
    //PrintValues(queries.LibrosdePython());
    //Libros de Java por nombre ascendente
    //PrintValues(queries.LibrosdeJavaPorNombreAscendente());
    //Libros con más de 450 páginas ordenados por nombre descendente
    //PrintValues(queries.Librosdemas450pagDescendente());
    //Tres libros ordenados por fecha
    //PrintValues(queries.TresLibrosOrdenadosPorFecha());
    //Cuatro libros con más de 400 páginas
    //PrintValues(queries.CuatroLibrosdemas400pag());
    //Tres primeros libros
    //PrintValues(queries.TresPrimerosLibros());
    //Cantidad de libros con páginas entre 200 y 500
    //Console.WriteLine($"Cantidad de libros con páginas entre 200 y 500: {queries.CantidadLibros()}");
    //Cantidad de libros con páginas entre 200 y 500 (64 bits)
    //Console.WriteLine($"Cantidad de libros con páginas entre 200 y 500 (64 bits): {queries.CantidadLibros64bits()}");
    //Fecha más reciente
    //Console.WriteLine($"Fecha más reciente: {queries.FechaMasReciente().ToShortDateString()}");
    //Fecha más antigua
    //Console.WriteLine($"Fecha más antigua: {queries.FechaMenorReciente().ToShortDateString()}");
    //Número de páginas mayor
    //Console.WriteLine($"Número de páginas mayor: {queries.NumerodePagMayor()}");
    //Libro con menor número de páginas
    //Console.lsWriteLine($"Libro con menor número de páginas: {queries.LibroconMenorNumeroDePaginas().Title}");
    //Libro con fecha más reciente
    //Console.WriteLine($"Libro con fecha más reciente: {queries.LibroconFechaMasReciente().Title}");
    //Suma total de páginas
    //Console.WriteLine($"Suma total de páginas: {queries.SumaTotaldePaginas()}");
    //Títulos de libros publicados después del 2015
    //Console.WriteLine($"Títulos de libros publicados después del 2015: {queries.TitulosLibrosDespuesdel2015()}");
    //Dictionary of books by first character of their title
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Error: {ex.Message}");
    for (Exception? causa = ex.InnerException; causa is not null; causa = causa.InnerException)
    {
        Console.Error.WriteLine($"Causa: {causa.Message}");
    }

    Environment.ExitCode = 1;
}

void PrintValues(IEnumerable<Book> listadelibros)
{
    ArgumentNullException.ThrowIfNull(listadelibros);

    Console.WriteLine("{0,-60} {1,15} {2, 15}\n", "Title", "N. Paginas", "Fecha publicación");
    foreach (var item in listadelibros)
    {
        Console.WriteLine("{0,-60} {1,15} {2, 15}", item.Title, item.PageCount, item.PublishedDate.ToShortDateString());
    }
}
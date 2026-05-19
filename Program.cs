LinqQueries queries = new LinqQueries();

//Toda la colección


//PrintValues(queries.TodaLaColeccion());

//Libros despues del 2000
//PrintValues(queries.LibrosDespuesdel2000());
//Libros con más de 250 páginas y que contienen la palabra "In Action"
//PrintValues(queries.LibrosConMasde250PagConPalabrasInAction());
//Todos los libros tienen un status?
//Console.WriteLine($" ¿Todos los libros tienen un status? - {queries.TodosLosLibrosTienenStatus()}");
//Si algún libro fue publicado en 2005
Console.WriteLine($" ¿Algún libro fue publicado en 2005? - {queries.SiAlgunLibroFuePublicado2005()}");


void PrintValues(IEnumerable<Book> listadelibros)
{
    Console.WriteLine("{0,-60} {1,15} {2, 15}\n", "Title", "N. Paginas", "Fecha publicación");
    foreach (var item in listadelibros)
    {
        Console.WriteLine("{0,-60} {1,15} {2, 15}", item.Title, item.PageCount, item.PublishedDate.ToShortDateString());
    }
}
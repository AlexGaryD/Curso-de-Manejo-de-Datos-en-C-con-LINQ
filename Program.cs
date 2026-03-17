LinqQueries queries = new LinqQueries();

//Toda la colección


//PrintValues(queries.TodaLaColeccion());

//Libros despues del 2000
PrintValues(queries.LibrosDespuesdel2000());

void PrintValues(IEnumerable<Book> listadelibros)
{
    Console.WriteLine("{0,-60} {1,15} {2, 15}\n", "Title", "N. Paginas", "Fecha publicación");
    foreach (var item in listadelibros)
    {
        Console.WriteLine("{0,-60} {1,15} {2, 15}", item.Title, item.PageCount, item.PublishedDate.ToShortDateString());
    }
}
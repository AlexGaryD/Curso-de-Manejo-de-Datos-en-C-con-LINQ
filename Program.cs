LinqQueries queries = new LinqQueries();

PrintValues(queries.TodaLaColeccion());

void PrintValues(IEnumerable<Book> listadelibros)
{
    Console.WriteLine("{0,-60} {1,15} {2, 15}\n", "Title", "N. Paginas", "Fecha publicación");
    foreach (var item in listadelibros)
    {
        Console.WriteLine("{0,-60} {1,15} {2, 15}", item.Title, item.PageCount, item.PublishedDate.ToShortDateString());
    }
}
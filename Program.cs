LinqQueries queries = new LinqQueries();

//Toda la colección


//ConsolePrinter.PrintValues(queries.TodaLaColeccion());

//Libros despues del 2000
//ConsolePrinter.PrintValues(queries.LibrosDespuesdel2000());
//Libros con más de 250 páginas y que contienen la palabra "In Action"
//ConsolePrinter.PrintValues(queries.LibrosConMasde250PagConPalabrasInAction());
//Todos los libros tienen un status?
//ConsolePrinter.PrintValue("¿Todos los libros tienen un status?", queries.TodosLosLibrosTienenStatus());
//Si algún libro fue publicado en 2005
//ConsolePrinter.PrintValue("¿Algún libro fue publicado en 2005?", queries.SiAlgunLibroFuePublicado2005());
//Libros de Python
//ConsolePrinter.PrintValues(queries.LibrosdePython());
//Libros de Java por nombre ascendente
//ConsolePrinter.PrintValues(queries.LibrosdeJavaPorNombreAscendente());
//Libros con más de 450 páginas ordenados por nombre descendente
//ConsolePrinter.PrintValues(queries.Librosdemas450pagDescendente());
//Tres libros ordenados por fecha
//ConsolePrinter.PrintValues(queries.TresLibrosOrdenadosPorFecha());
//Cuatro libros con más de 400 páginas
//ConsolePrinter.PrintValues(queries.CuatroLibrosdemas400pag());
//Tres primeros libros
//ConsolePrinter.PrintValues(queries.TresPrimerosLibros());
//Cantidad de libros con páginas entre 200 y 500
//ConsolePrinter.PrintValue("Cantidad de libros con páginas entre 200 y 500", queries.CantidadLibros());
//Cantidad de libros con páginas entre 200 y 500 (64 bits)
//ConsolePrinter.PrintValue("Cantidad de libros con páginas entre 200 y 500 (64 bits)", queries.CantidadLibros64bits());
//Fecha más reciente
//ConsolePrinter.PrintValue("Fecha más reciente", queries.FechaMasReciente().ToShortDateString());
//Fecha más antigua
//ConsolePrinter.PrintValue("Fecha más antigua", queries.FechaMenorReciente().ToShortDateString());
//Número de páginas mayor
//ConsolePrinter.PrintValue("Número de páginas mayor", queries.NumerodePagMayor());
//Libro con menor número de páginas
//ConsolePrinter.PrintValue("Libro con menor número de páginas", queries.LibroconMenorNumeroDePaginas().Title);
//Libro con fecha más reciente
//ConsolePrinter.PrintValue("Libro con fecha más reciente", queries.LibroconFechaMasReciente().Title);
//Suma total de páginas
//ConsolePrinter.PrintValue("Suma total de páginas", queries.SumaTotaldePaginas());
//Títulos de libros publicados después del 2015
//ConsolePrinter.PrintValue("Títulos de libros publicados después del 2015", queries.TitulosLibrosDespuesdel2015());
//Dictionary of books by first character of their title

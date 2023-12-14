using DemoConsole.entities;
using DemoConsole.utils;
using System.Text;
using System.Text.Json;

namespace DemoConsole.services
{
    //Clase estática para manejar los libros, no es necesario instanciarla, accedemos a los métodos de manera directa
    public static class BookService
    {
        private static List<Book> books = new List<Book>();

        //Recupera la lista de libros del archivo books.json
        public static List<Book> LoadFromFile()
        {
            // Verificar si existe el archivo
            if (!File.Exists(Constants.BOOKS_FILE))
            {
                // Si no existe, retornar una lista vacía
                return new List<Book>();
            }

            // Leer el contenido del archivo
            string json = File.ReadAllText(Constants.BOOKS_FILE);
            if (string.IsNullOrWhiteSpace(json))
            {
                // Si el archivo está vacío, retornar una lista vacía
                return new List<Book>();
            }

            // Deserializar el contenido del archivo a una lista de libros
            return JsonSerializer.Deserialize<List<Book>>(json);
        }

        //Guarda la lista de libros en el archivo books.json
        public static void SaveToFile(List<Book> books)
        {

            // Serializar la lista de libros a JSON
            string json = JsonSerializer.Serialize(books);

            // Guardar la lista de libros en el archivo
            File.WriteAllText("books.json", json);
        }

        //Método para actualizar un libro
        public static void UpdateBook(int id, string newName, string newAuthor, string newCategory)
        {
            // Recuperar la lista de libros
            List<Book> books = LoadFromFile();

            // Buscar el libro por id
            Book book = books.FirstOrDefault(b => b.Id == id);

            // Si el libro existe, actualizarlo
            if (book != null)
            {
                book.Title = newName;
                book.Author = newAuthor;
                book.Category = newCategory;
                SaveToFile(books);
            }
        }

        //Método para eliminar un libro
        public static void DeleteBook(int id)
        {
            // Recuperar la lista de libros
            List<Book> books = LoadFromFile();

            // Buscar el libro por id, se podría encaptsular en un método aparte
            Book book = books.FirstOrDefault(b => b.Id == id);

            // Si el libro existe, eliminarlo
            if (book != null)
            {
                books.Remove(book);
                SaveToFile(books);
            }
        }

        //Método para buscar un libro por su nombre
        public static Book GetBookByName(string name)
        {
            // Recuperar la lista de libros
            List<Book> books = LoadFromFile();

            // Buscar el libro por nombre
            return books.FirstOrDefault(b => b.Title == name);
        }

        //Método para buscar todos los libros por el nombre ingresado
        public static List<Book> GetBooksByName(string name)
        {
            // Recuperar la lista de libros
            List<Book> books = LoadFromFile();
            // Buscar todas las coincidencias por nombre
            return books.Where(b => b.Title == name).ToList();
        }

        //Método para crear un libro, este método es llamado desde el Program.cs
        public static string Create()
        {
            //Ingreso de datos por consola
            Console.WriteLine("AGREGAR UN LIBRO");
            Console.WriteLine("Ingrese el Titulo del Libro:");
            string name = Console.ReadLine();
            Console.WriteLine("Ingrese el Autor del Libro:");
            string author = Console.ReadLine();
            Console.WriteLine("Ingrese la Categoría del Libro:");
            string category = Console.ReadLine();

            // Recuperar la lista de libros
            List<Book> books = LoadFromFile();

            // Crear un nuevo libro
            var book = new Book
            {
                Id = books.Count + 1, // Generar un id único,recuperamos la cantidad de libros y le sumamos 1
                                      // Se puede optimizar con un método que retorne el último id y agregando 1
                Title = name,
                Author = author,
                IsAvailable = true,
                Category = category,
            };

            // Agregar el nuevo libro a la lista
            books.Add(book);
            //Llamamos al método SaveToFile para guardar la lista de libros en el archivo books.json
            SaveToFile(books);
            return $"El libro '{name}' se ha creado correctamente";
        }

        //Método para actualizar un libro llamado desde el Program.cs
        public static string Update()
        {
            //ingreso de datos por consola
            Console.WriteLine("ACTUALIZAR UN LIBRO");
            Console.WriteLine("Ingrese el código del Libro a actualizar:");
            string message = string.Empty;

            //Validación de la opción ingresada, si es un numero se continua con la actualización
            if (int.TryParse(Console.ReadLine(), out int idBook))
            {
                Console.WriteLine("Ingrese el nuevo Titulo del Libro:");
                string newName = Console.ReadLine();
                Console.WriteLine("Ingrese el nuevo Author del Libro:");
                string newAuthor = Console.ReadLine();
                Console.WriteLine("Ingrese la nueva Categoría del Libro:");
                string newCategory = Console.ReadLine();

                //Llamamos al método UpdateBook para actualizar el libro del archivo books.json
                UpdateBook(idBook, newName, newAuthor, newCategory);
                message = $"El libro con el id {idBook} se ha actualizado correctamente";
            }
            else
            {
                message = $"No se ha encontrado el libro con el id: {idBook}.";
            }
            return message;
        }

        public static string Delete()
        { 
            //ingreso de datos por consola
            Console.WriteLine("ELIMINAR UN LIBRO");
            Console.WriteLine("Ingrese el código del Libro a eliminar:");

            //Validación de la opción ingresada, si es un numero se continua con la eliminación
            if (int.TryParse(Console.ReadLine(), out int idBook))
            {
                //Llamamos al método DeleteBook para eliminar el libro del archivo books.json
                DeleteBook(idBook);
                return $"Libro con el id {idBook} eliminado satisfactoriamente";
            }
            else
            {
                return $"No se ha encontrado el libro con el id: {idBook}.";
            }
        }

        //Método para listar todos los libros llamado desde el Program.cs
        public static string GetAll()
        {
            List<Book> books = LoadFromFile();

            //Si la lista de libros está vacía retornamos un mensaje
            if (!books.Any())
                return "No hay libros registrados";

            //Armar la lista de libros en formato string
            var builder = new StringBuilder();
            builder.AppendLine("|ID".PadRight(10) + "|Título".PadRight(20) + "|Autor".PadRight(20) + "|Categoría".PadRight(20) + "|Disponible".PadRight(10));
            foreach (var book in books)
            {
                builder.AppendLine($"|{book.Id.ToString().PadRight(9)}|{book.Title.PadRight(19)}|{book.Author.PadRight(19)}|{book.Category.PadRight(19)}|{(book.IsAvailable ? "Sí" : "No").PadRight(9)}");

            }
            return builder.ToString();
        }

        //Método para buscar un libro por su nombre, podemos colocar estas opciones en el program y simplemente llamar a este método
        public static string SearchByName()
        {
            Console.WriteLine("BUSCAR UN TITULO POR SU NOMBRE");
            Console.WriteLine("Ingrese el Titulo del Libro:");
            string name = Console.ReadLine();
            Book book = GetBookByName(name);
            return $"El libro '{name}' con tiene la siguiente información. Id : {book.Id}, Author: {book.Author}";
        }

        //Método para buscar todos los libros por el nombre ingresado, podemos colocar estas opciones en el program y simplemente llamar a este método
        public static string SearchAllByName()
        {
            Console.WriteLine("BUSCAR UN TITULO POR SU NOMBRE");
            Console.WriteLine("Ingrese el Titulo del Libro:");
            string name = Console.ReadLine();
            var matchingBooks = books.Where(b => b.Title.Equals(name, StringComparison.OrdinalIgnoreCase));

            //Si la lista de libros está vacía retornamos un mensaje
            if (!matchingBooks.Any())
            {
                return $"No se encontraron libros con el título '{name}'";
            }
            else
            {
                var bookStrings = GetBooksByName(name);
                return $"Libros con el título '{name}':\n{string.Join("\n", bookStrings)}";
            }
        }

        
    }
}

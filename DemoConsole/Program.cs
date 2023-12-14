// See https://aka.ms/new-console-template for more information
using DemoConsole.services;
using DemoConsole.utils;

//While para que el programa se ejecute hasta que el usuario decida salir
while (true)
{
    //Limpia la consola despues de cada llamado y muestra el encabezado y opciones
    Console.Clear();

    //Recupera la opcion seleccionada por el usuario
    int optionInt = Options();

    //Convierte el numero de opcion al enum OptionsEnum que lo definimos en el archivo utils/OptionsEnum.cs
    OptionsEnum optionEnum = (OptionsEnum)optionInt;

    if (optionEnum == OptionsEnum.Exit)
        return;

    //switch con cada opcion llamando a su respectivo metodo
    string message = optionEnum switch
    {
        OptionsEnum.Add => BookService.Create(),
        OptionsEnum.Update => BookService.Update(),
        OptionsEnum.Delete => BookService.Delete(),
        OptionsEnum.List => BookService.GetAll(),
        _ => "Opción no válida"
    };

    //imprime el mensaje de respuesta obtenido de cada metodo
    Console.WriteLine(message);


    Console.WriteLine("");
    Console.WriteLine("------- Ingrese una letra para volver al menú principal-------");
    Console.ReadKey();
}
    //Método para mostrar el encabezado del menú
    static void Header()
    {
        Console.WriteLine(",-----.                  ,--.                                         ,--.  ,--.,------.,--------. \r\n|  |) /_  ,---.  ,---. ,-'  '-. ,---. ,--,--.,--,--,--. ,---.         |  ,'.|  ||  .---''--.  .--' \r\n|  .-.  \\| .-. || .-. |'-.  .-'| .--'' ,-.  ||        || .-. |        |  |' '  ||  `--,    |  |    \r\n|  '--' /' '-' '' '-' '  |  |  \\ `--.\\ '-'  ||  |  |  || '-' '    .--.|  | `   ||  `---.   |  |    \r\n`------'  `---'  `---'   `--'   `---' `--`--'`--`--`--'|  |-'     '--'`--'  `--'`------'   `--'");
        Console.WriteLine("Sistema de Reserva y consulta de libros");
    }

    //Método para mostrar las opciones del menú
    static int Options()
    {
        while (true)
        {
            Console.Clear();
            Header();
            Console.WriteLine("1. Agrega un libro");
            Console.WriteLine("______________________");
            Console.WriteLine("2. Actualiza un libro");
            Console.WriteLine("______________________");
            Console.WriteLine("3. Elimina libros");
            Console.WriteLine("______________________");
            Console.WriteLine("4. Consulta");
            Console.WriteLine("______________________");
            Console.WriteLine("5. Salir");

            Console.WriteLine("----------Selecciona una opción----------");
            
            //Validación de la opción ingresada
            string input = Console.ReadLine();
            if (int.TryParse(input, out int option))
            {
                return option;
            }

            Console.WriteLine("Opción no válida. Por favor, ingrese un número.");
        }
    }
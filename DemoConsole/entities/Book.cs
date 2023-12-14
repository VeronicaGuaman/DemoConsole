namespace DemoConsole.entities;
public class Book
{ 
    //propiedades base de un libro
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public bool IsAvailable{ get; set; }
    public required string Category { get; set; }
        
}

public class Program
{
    public static void Main(string[] args)
    {
        using var game = new DinkleBurg.Engine();
        game.Run();
    }
}

public static class FileManager
{
    public static void Build_System()
    {
        // check if game files already exist

        //-- yes -- > load files.

        //-- no -- > create folders and files.
    }
}


using System;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        FileManager.Build_System();
        using var game = new DinkleBurg.Engine();
        game.Run();
    }
}

public static class FileManager
{
    public static void Build_System()
    {
        if (!Directory.Exists(Environment.CurrentDirectory + "/Data"))
        {
            Directory.CreateDirectory(Environment.CurrentDirectory + "/Data");
        }
        if (!Directory.Exists(Environment.CurrentDirectory + "/Maps"))
        {
            Directory.CreateDirectory(Environment.CurrentDirectory + "/Maps");
        }
    }
}

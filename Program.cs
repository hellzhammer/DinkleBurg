using DinkleBurg.Map_Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		// load in the tile definitions for the editor and game.
		if (File.Exists(Environment.CurrentDirectory + "/TerrainTileDefinitions.json"))
		{
			Debug.WriteLine("Found the file!");
			string json = File.ReadAllText(Environment.CurrentDirectory + "/TerrainTileDefinitions.json");
			Dictionary<string, TileDef> defs = JsonConvert.DeserializeObject<Dictionary<string, TileDef>>(json);
			Globals.terrain_definitions = defs;
		}

        if (!Directory.Exists(Environment.CurrentDirectory + "/Data"))
        {
            Directory.CreateDirectory(Environment.CurrentDirectory + "/Data");
            if (!File.Exists(Environment.CurrentDirectory + "/Data/slot1.json") && !File.Exists(Environment.CurrentDirectory + "/Data/slot1.json") && !File.Exists(Environment.CurrentDirectory + "/Data/slot1.json"))
            {
				string json = "";
				for (int i = 1; i < 4; i++)
				{
					using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "/Data/slot" + i + ".json"))
					{
						sw.Write(json);
						sw.Close();
						sw.Dispose();
					}
				}
			}
        }
        else
        {
			// file check
			if (!File.Exists(Environment.CurrentDirectory + "/Data/slot1.json") && !File.Exists(Environment.CurrentDirectory + "/Data/slot1.json") && !File.Exists(Environment.CurrentDirectory + "/Data/slot1.json"))
			{
				string json = "";
				for (int i = 1; i < 4; i++)
				{
					using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "/Data/slot" + i + ".json"))
					{
						sw.Write(json);
						sw.Close();
						sw.Dispose();
					}
				}
			}

			// now load the data needed to run the game.
		}


        if (!Directory.Exists(Environment.CurrentDirectory + "/Maps"))
        {
            Directory.CreateDirectory(Environment.CurrentDirectory + "/Maps");
        }
        // else load the data from above
    }
}

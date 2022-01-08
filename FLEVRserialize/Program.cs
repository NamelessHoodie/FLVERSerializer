using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SoulsFormats;

namespace FLEVRserialize
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Serialize and Deserialize FLEVR Files by dragging them onto the executable.");
            string filePath = args.First();
            if (!File.Exists(filePath))
            {
                Console.WriteLine("The given FLVER file path does not exist.");
                return;
            }
            switch (Path.GetExtension(filePath).ToLower())
            {
                case ".json":
                    DeserializeFLVER(filePath);
                    break;
                case ".flver":
                    SerializeFLVER(filePath);
                    break;
                default:
                    Console.WriteLine("The given file is not a FLVER file");
                    break;
            }
            Console.ReadLine();
        }

        private static void DeserializeFLVER(string jsonFilePath)
        {
            string jsonFile = File.ReadAllText(jsonFilePath);
            FLVER2 flevr = JsonConvert.DeserializeObject<FLVER2>(jsonFile);
            string newFlevrFilePath = Path.ChangeExtension(jsonFilePath, "flver");

            if (File.Exists(newFlevrFilePath))
                File.Delete(newFlevrFilePath);

            flevr.Write(Path.GetFullPath(newFlevrFilePath));

            Console.WriteLine("Finished deserializing FLVER file!");
        }

        private static void SerializeFLVER(string flverFilePath)
        {
            JsonSerializer serializer = new JsonSerializer();
            FLVER2 flever = FLVER2.Read(flverFilePath);

            var serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };

            string jsonFileFlevr = JsonConvert.SerializeObject(flever, serializerSettings);
            string newJsonFilePath = Path.ChangeExtension(flverFilePath, "json");

            if (File.Exists(newJsonFilePath))
                File.Delete(newJsonFilePath);

            File.WriteAllText(newJsonFilePath, jsonFileFlevr);

            Console.WriteLine("Finished serializing FLVER file!");
        }
    }
}

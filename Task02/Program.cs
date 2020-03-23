using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
using Task02.Models;
using Tut2.Models;

namespace Tut2
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputPath = args.Length > 0 ? args[0] : @"Files\\data.csv";
            var outputPath = args.Length > 1 ? args[1] : @"Files\\result";
            var outputType = args.Length > 2 ? args[2] : "xml";


            Console.WriteLine($"{inputPath}\n{outputPath}\n{outputType}");

            try
            {
                if (!File.Exists(inputPath))
                    throw new FileNotFoundException("File doe not exist\n", inputPath.Split("\\")[^1]);



                var university = new University
                {
                    Author = "Suman Bhurtel"
                };
                foreach (var line in File.ReadAllLines(inputPath))
                {

                    var splitted = line.Split(",");

                    if (splitted.Length < 9)
                    {
                        File.AppendAllText("Files\\Log.txt", $"ERR NOT enough infomration in line { line}\n");
                        continue;
                    }
                    foreach (var field in splitted)
                    {
                        if (field.Length <= 0)
                        {
                            File.AppendAllText(@"Files\Log.txt", $"{DateTime.UtcNow} Empty fields in student  {splitted[0]} {splitted[1]} \n");
                            continue;
                        }
                    }

                    var stud = new Student
                    {
                        FirstName = splitted[0],
                        LastName = splitted[1],
                        IndexNumber = splitted[4],
                        EmailId = splitted[6],
                        DateOfBirth = splitted[5],
                        MotherName = splitted[7],
                        FatherName = splitted[8],
                        Study = new Studies
                        {
                            NameOfStudy = splitted[2],
                            ModeOfStudy = splitted[3]
                        }
                    };

                    university.Students.Add(stud);
                }



                //xml
                using var writer = new FileStream($"{outputPath}.{outputType}", FileMode.Create);
                var serializer = new XmlSerializer(typeof(University));
                serializer.Serialize(writer, university);

                //json
                var jsonString = JsonSerializer.Serialize(university);
                File.WriteAllText($"{outputPath}.json", jsonString);
            }



            catch (FileNotFoundException e)
            {
                File.AppendAllText("Files\\Log.txt", $"{DateTime.UtcNow} {e.Message} {e.FileName}\n");
            }
        }
    }
}

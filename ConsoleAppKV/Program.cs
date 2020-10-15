using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using FileHandling;

namespace ConsoleAppKV
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write("Kenen tietoja haetaan?( tyhjä lopettaa sovelluksen ): ");

                // the assumption is that the target files are all lower case
                string fileName = Console.ReadLine().ToLower();

                if (fileName.Equals(""))
                {
                    break;
                }

                // NOTE: change path! - needs to be a path to this project's solution
                string filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

                // custom class
                PersonFileReader personFileReader = new PersonFileReader(fileName, filePath);
                string[] linesOfTheFile = personFileReader.readFile();

                // if the reader didn't find any matches...
                if (linesOfTheFile.Length == 0)
                {
                    Console.WriteLine("Eipä löytyny nimellä '" + fileName + "'. Nih!");
                    Console.WriteLine();
                    continue;
                }
                // saves the options and the answers to their own variables inside the class
                personFileReader.setOptionsAndAnswers(linesOfTheFile);

                Console.Write("Mitä haluat tietää? (" + personFileReader.optionsToString() + "): ");
                string whatInfoToSearch = Console.ReadLine().ToLower();

                // variables for cleaner code
                List<string> answers = personFileReader.getAnswers();
                List<string> options = personFileReader.getOptions();

                try
                {
                    string answer = answers[options.IndexOf(whatInfoToSearch)];
                    Console.WriteLine(answer);
                }
                catch
                {
                    Console.WriteLine("Eipä ollu!");
                }
                Console.WriteLine();
            }
        }
    }
}

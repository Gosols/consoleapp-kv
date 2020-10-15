using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FileHandling
{
    class PersonFileReader
    {
        private string fileName;
        private string path;
        private List<string> options;
        private List<string> answers;
        public PersonFileReader(string fileName, string path)
        {
            this.fileName = fileName;
            this.path = path;
            options = new List<string>();
            answers = new List<string>();
        }

        public List<string> getOptions()
        {
            return this.options;
        }

        private void setOptions(List<string> options)
        {
            this.options = options;
        }

        public List<string> getAnswers()
        {
            return this.answers;
        }

        private void setAnswers(List<string> answers)
        {
            this.answers = answers;
        }

        public void setOptionsAndAnswers(string[] fileLines)
        // readFile() needs to be called before this function to get fileLines
        {
            /*
                *NOTE: the two lists below could be switched into a single
                 KeyValuePair list for better code
               */

            var infoOptions = new List<string>(); // all info options (eg. ikä, pituus, paino)
            var answers = new List<string>(); // all answers (eg. 25, 190cm, 78kg)

            // adding options and answers to lists...
            foreach (string line in fileLines)
            {
                // if it's an empty line..
                if (line.Length == 0)
                {
                    continue;
                }

                string[] lineParts = line.Split(':');

                infoOptions.Add(lineParts[0]);
                answers.Add(lineParts[1].Trim());
            }
            setAnswers(answers);
            setOptions(infoOptions);
        }

        public string[] readFile() // returns an array of the text file's lines
        {
            FileInfo[] filesInDir = searchForFiles();
            string[] linesOfTheFile = { };

            //if no files found...
            if (filesInDir.Length == 0)
            {
                return linesOfTheFile;
            }
            else
            {
                string pathToFile = filesInDir[0].FullName;
                // an array of all the lines in the file
                linesOfTheFile = File.ReadAllLines(pathToFile);

                return linesOfTheFile;
            }
        }

        private FileInfo[] searchForFiles()
        {
            DirectoryInfo DirectoryToSearch = new DirectoryInfo(this.path);

            //list of files matching the search criteria
            FileInfo[] filesInDir = DirectoryToSearch.GetFiles("*" + fileName + "*.txt*");

            return filesInDir;
        }

        public string optionsToString()
        // this function returns all possible info options as a string, separated by commas

        {
            string optionsString = "";

            for (int i = 0; i < this.options.Count; i++)
            {
                optionsString += this.options[i];

                if (this.options[i] == this.options.Last())
                {
                    break;
                }
                else
                {
                    optionsString += ", ";
                }
            }
            return optionsString;
        }
    }
}

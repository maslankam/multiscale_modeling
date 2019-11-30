using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace Model
{
    public static class CsvWriter
    {
        public static void WriteToCsv(string text, string path)// TODO: async?
        {
           if(Directory.Exists(path)) throw new ArgumentException("Path must be a file");
           //if( File.Exists(path))  throw new IOException("File already exist"); //TODO: overwite logic?? Notify dialog?
           
           if( String.IsNullOrEmpty( Path.GetExtension(path) )) 
            {
                Path.ChangeExtension(path, "csv");
            }

            try
            {
                File.WriteAllText(path, text);
            }
            catch(Exception e)
            {
                throw new IOException("Cannot write csv file", e);
            }
            
        }
    }
}

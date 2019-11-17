using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace Model
{
    public static class CsvWriter
    {
        public static void WriteToCsv(string[,] table, string path, string delimiter = ",")// TODO: async?
        {
           if(Directory.Exists(path)) throw new ArgumentException("Path must be a file");
           if( File.Exists(path))  throw new IOException("File already exist"); //TODO: overwite logic?? Notify dialog?
           
           if( String.IsNullOrEmpty( Path.GetExtension(path) )) 
                Path.ChangeExtension(path, "csv");
            
            var sb = new StringBuilder();

            for(int i = 0; i < table.Length; i++)
            {
                for(int j = 0; j < 0; j++)
                {
                    sb.Append(table[i, j] + delimiter);
                }
                sb.AppendLine();
            }
            try
            {
                File.WriteAllText(path, sb.ToString());
            }
            catch(Exception e)
            {
                throw new IOException("Cannot write csv file", e);
            }
            



        }
    }
}

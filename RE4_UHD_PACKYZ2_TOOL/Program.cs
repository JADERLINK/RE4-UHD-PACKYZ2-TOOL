using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PACKYZ2_TOOL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("### JADERLINK PACKYZ2 TOOL ###");
            Console.WriteLine("### VERSION 1.0.0.0        ###");

            if (args.Length >= 1)
            {
                string file = args[0];
                FileInfo info = null;

                try
                {
                    info = new FileInfo(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in the directory: " + Environment.NewLine + ex);
                }
                if (info != null)
                {
                    Console.WriteLine("File: " + info.Name);
                    if (info.Exists)
                    {
                        if (info.Extension.ToUpperInvariant() == ".PACK"
                         || info.Extension.ToUpperInvariant() == ".YZ2")
                        {     
                            try
                            {
                                Extract.ExtractFile(file);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex);
                            }
                           
                        }
                        else if (info.Extension.ToUpperInvariant() == ".IDXPACK")
                        {
                            try
                            {
                                Repack.RepackFile(file);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex);
                            }
                        }
                        else
                        {
                            Console.WriteLine("The extension is not valid: " + info.Extension);
                        }

                    }
                    else
                    {
                        Console.WriteLine("File specified does not exist.");
                    }

                }
   
            }
            else
            {
                Console.WriteLine("Unspecified file directory.");
            }

            Console.WriteLine("Finished!!!");
            Console.WriteLine("");
        }
    }
}

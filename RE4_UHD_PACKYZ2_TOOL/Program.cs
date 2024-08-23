﻿using System;
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
            Console.WriteLine("# JADERLINK PACKYZ2 TOOL");
            Console.WriteLine("# By: JADERLINK");
            Console.WriteLine("# youtube.com/@JADERLINK");
            Console.WriteLine("# VERSION 1.0.4 (2024-08-23)");

            if (args.Length == 0)
            {
                Console.WriteLine("For more information read:");
                Console.WriteLine("https://github.com/JADERLINK/RE4-UHD-PACKYZ2-TOOL");
                Console.WriteLine("Press any key to close the console.");
                Console.ReadKey();
            }
            else if (args.Length >= 1 && File.Exists(args[0]))
            {
                FileInfo info = null;

                try
                {
                    info = new FileInfo(args[0]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in the path: " + Environment.NewLine + ex);
                }
                if (info != null)
                {
                    Console.WriteLine("File: " + info.Name);

                    if (info.Extension.ToUpperInvariant() == ".PACK"
                     || info.Extension.ToUpperInvariant() == ".YZ2")
                    {
                        try
                        {
                            Extract.ExtractFile(info.FullName);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + Environment.NewLine + ex);
                        }

                    }
                    else if (info.Extension.ToUpperInvariant() == ".IDXPACK")
                    {
                        try
                        {
                            Repack.RepackFile(info.FullName);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + Environment.NewLine + ex);
                        }
                    }
                    else
                    {
                        Console.WriteLine("The extension is not valid: " + info.Extension);
                    }

                }
   
            }
            else
            {
                Console.WriteLine("File specified does not exist.");
            }

            Console.WriteLine("Finished!!!");
        }
    }
}

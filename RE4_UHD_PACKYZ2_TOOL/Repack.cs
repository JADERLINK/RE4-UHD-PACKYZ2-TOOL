using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PACKYZ2_TOOL
{
    public static class Repack
    {
        public static void RepackFile(string file)
        {
            StreamReader idx = null;
            FileInfo fileInfo = new FileInfo(file);
            string DirectoryName = fileInfo.DirectoryName;

            try
            {
                idx = fileInfo.OpenText();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + Environment.NewLine + ex);
            }

            if (idx != null)
            {

                Dictionary<string, string> pair = new Dictionary<string, string>();

                List<string> lines = new List<string>();

                string endLine = "";
                while (endLine != null)
                {
                    endLine = idx.ReadLine();
                    lines.Add(endLine);
                }

                idx.Close();

                foreach (var item in lines)
                {
                    if (item != null)
                    {
                        var split = item.Split(new char[] { ':' });
                        if (split.Length >= 2)
                        {
                            string key = split[0].ToUpper().Trim();
                            if (!pair.ContainsKey(key))
                            {
                                pair.Add(key, split[1].Trim());
                            }
                        }
                    }
                }

                //=====

                uint magic = 0;

                if (pair.ContainsKey("MAGIC"))
                {
                    try
                    {
                        magic = uint.Parse(ReturnValidHexValue(pair["MAGIC"].ToUpper()), System.Globalization.NumberStyles.HexNumber);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("MAGIC tag error: " + Environment.NewLine + ex);
                    }
                }
                else
                {
                    Console.WriteLine("MAGIC tag does not exist.");
                }

                Console.WriteLine("Magic: " + magic.ToString("X8"));

                string ImageFolder = DirectoryName + "\\" + magic.ToString("x8");

                if (Directory.Exists(ImageFolder))
                {

                    BinaryWriter packFile = null;

                    try
                    {
                        FileInfo packFileInfo = new FileInfo(fileInfo.FullName.Substring(0, fileInfo.FullName.Length - fileInfo.Extension.Length));
                        packFile = new BinaryWriter(packFileInfo.Create());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + Environment.NewLine + ex);
                    }

                    if (packFile != null)
                    {
                        uint iCount = 0; // quantidade de imagens
                        bool asFile = true;

                        while (asFile)
                        {
                            string ddspath = ImageFolder + "\\" + iCount.ToString("D4") + ".dds";
                            string tgapath = ImageFolder + "\\" + iCount.ToString("D4") + ".tga";
                            string empty = ImageFolder + "\\" + iCount.ToString("D4") + ".empty";
                            string reference = ImageFolder + "\\" + iCount.ToString("D4") + ".reference";

                            if (File.Exists(ddspath) || File.Exists(tgapath) || File.Exists(empty) || File.Exists(reference))
                            {
                                iCount++;
                            }
                            else 
                            {
                                asFile = false;
                            }
                        }

                        Console.WriteLine("Count: " + iCount);

                        packFile.Write(magic);
                        packFile.Write(iCount);

                        //header calculo
                        uint line = ((iCount + 2) * 4) / 16;
                        float rest = ((iCount + 2) * 4) % 16;
                        if (rest != 0)
                        {
                            line++;
                        }
                        if (line < 2)
                        {
                            line = 2;
                        }

                        uint nextOffset = line * 16;

                        //id, offset
                        Dictionary<int, uint> offsetVisiteds = new Dictionary<int, uint>();

                        for (int i = 0; i < iCount; i++)
                        {
                            string ddspatch = ImageFolder + "\\" + i.ToString("D4") + ".dds";
                            string tgapatch = ImageFolder + "\\" + i.ToString("D4") + ".tga";

                            FileInfo imageFile = null;
                            if (File.Exists(ddspatch))
                            {
                                imageFile = new FileInfo(ddspatch);
                            }
                            else if (File.Exists(tgapatch))
                            {
                                imageFile = new FileInfo(tgapatch);
                            }

                            if (imageFile != null)
                            {
                                offsetVisiteds.Add(i, nextOffset);

                                packFile.BaseStream.Position = 8 + (i * 4);
                                packFile.Write(nextOffset);

                                packFile.BaseStream.Position = nextOffset;

                                packFile.Write((uint)imageFile.Length);
                                packFile.Write(0xFFFFFFFF);
                                packFile.Write(magic);

                                if (imageFile.Extension.ToUpperInvariant().Contains("DDS"))
                                {
                                    packFile.Write((uint)1);
                                }
                                else
                                {
                                    packFile.Write((uint)0);
                                }

                                var fileStream = imageFile.OpenRead();
                                fileStream.CopyTo(packFile.BaseStream);
                                fileStream.Close();

                                //alinhamento
                                uint aLine = (uint)packFile.BaseStream.Position / 16;
                                uint aRest = (uint)packFile.BaseStream.Position % 16;
                                aLine += aRest != 0 ? 1u : 0u;
                                int aDif = (int)((aLine * 16) - packFile.BaseStream.Position);
                                packFile.Write(new byte[aDif]);

                                nextOffset = (uint)packFile.BaseStream.Position;

                                Console.WriteLine("Add file: " + imageFile.Name);
                            }
                            else 
                            {
                                int Id = 0;
                                uint Offset = 0;

                                string reference = ImageFolder + "\\" + i.ToString("D4") + ".reference";
                                if (File.Exists(reference))
                                {
                                    string cont = ReturnValidDecValue(File.ReadAllText(reference));
                                    if (int.TryParse(cont, out Id))
                                    {
                                        if (offsetVisiteds.ContainsKey(Id))
                                        {
                                            Offset = offsetVisiteds[Id];
                                        }
                                    } 
                                }

                                packFile.BaseStream.Position = 8 + (i * 4);
                                packFile.Write(Offset);

                                if (Offset != 0)
                                {
                                    Console.WriteLine("ID: " + i.ToString("D4") + " references the ID " + Id.ToString("D4"));
                                }
                                else 
                                {
                                    Console.WriteLine("ID: " + i.ToString("D4") + " is empty");
                                }
                               
                            }

                        }

                        packFile.Close();
                    }

                }
                else 
                {
                    Console.WriteLine($"The folder {magic.ToString("x8")} does not exist.");
                }

            }

        }


        private static string ReturnValidHexValue(string cont)
        {
            string res = "";
            foreach (var c in cont)
            {
                if (char.IsDigit(c)
                    || c == 'A'
                    || c == 'B'
                    || c == 'C'
                    || c == 'D'
                    || c == 'E'
                    || c == 'F'
                    )
                {
                    res += c;
                }
            }
            return res;
        }

        private static string ReturnValidDecValue(string cont)
        {
            string res = "0";
            foreach (var c in cont)
            {
                if (char.IsDigit(c))
                {
                    res += c;
                }
            }
            return res;
        }
    }
}

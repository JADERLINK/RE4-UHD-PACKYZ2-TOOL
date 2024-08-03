using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PACKYZ2_TOOL
{
    public static class Extract
    {
        public static void ExtractFile(string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            string baseName = fileInfo.Name;
            string baseDiretory = fileInfo.DirectoryName;

            var pack = new BinaryReader(fileInfo.OpenRead());
 
            uint PackID = pack.ReadUInt32();
            uint Amount = pack.ReadUInt32();

            Console.WriteLine("Magic: " + PackID.ToString("X8"));
            Console.WriteLine("Amount: " + Amount);

            var idx = new FileInfo(baseDiretory + "\\" + baseName + ".idxpack").CreateText();
            Directory.CreateDirectory(baseDiretory + "\\" + PackID.ToString("x8"));

            idx.WriteLine(":##############################");
            idx.WriteLine(":### JADERLINK PACKYZ2 TOOL ###");
            idx.WriteLine(":##############################");
            idx.WriteLine("MAGIC:" + PackID.ToString("X8"));

            List<uint> offsets = new List<uint>();

            for (int i = 0; i < Amount; i++)
            {
                uint offset = pack.ReadUInt32();
                offsets.Add(offset);
            }

            // offset, id
            Dictionary<uint, int> offsetVisiteds = new Dictionary<uint, int>();

            for (int i = 0; i < offsets.Count; i++)
            {
                if (offsets[i] != 0)
                {
                    if (offsetVisiteds.ContainsKey(offsets[i]))
                    {
                        int refId = offsetVisiteds[offsets[i]];

                        File.WriteAllText(baseDiretory + "\\" + PackID.ToString("x8") + "\\" + i.ToString("D4") + ".reference", refId.ToString("D4"));
                        Console.WriteLine("ID: " + i.ToString("D4") + " refers to the ID " + refId.ToString("D4"));
                    }
                    else
                    {
                        offsetVisiteds.Add(offsets[i], i);

                        pack.BaseStream.Position = offsets[i];
                        uint fileLength = pack.ReadUInt32();
                        uint ff_ff_ff_ff = pack.ReadUInt32();
                        uint PackID_ = pack.ReadUInt32();
                        uint Type = pack.ReadUInt32();

                        string Extension = "null";
                        // in uhd
                        // Type 1 == "dds"  //44445320
                        // Type 0 == "tga"
                        //if (Type == 1){Extension = "dds";}else if (Type == 0) {Extension = "tga";}

                        byte[] imagebytes = new byte[fileLength];
                        pack.BaseStream.Read(imagebytes, 0, (int)fileLength);

                        uint imagemagic = BitConverter.ToUInt32(imagebytes, 0);
                        if (imagemagic == 0x20534444)
                        {
                            Extension = "dds";
                        }
                        else
                        {
                            Extension = "tga";
                        }

                        File.WriteAllBytes(baseDiretory + "\\" + PackID.ToString("x8") + "\\" + i.ToString("D4") + "." + Extension, imagebytes);
                        Console.WriteLine("Extracted file: " + PackID.ToString("x8") + "\\" + i.ToString("D4") + "." + Extension);
                    }
                }
                else
                {
                    File.WriteAllText(baseDiretory + "\\" + PackID.ToString("x8") + "\\" + i.ToString("D4") + ".empty", "");
                    Console.WriteLine("ID: " + i.ToString("D4") + " is empty");
                }

            }

            pack.Close();
            idx.Close();
        } 
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kcots.Utilities
{
    public class Data
    {
        public static Dictionary<string, string> APIIntervalOptions = new Dictionary<string, string>();


        //TODO - UNIT TEST
        public static void LoadGraphicIPMappings()
        {
            try
            {
                if (Directory.Exists("Configuration"))
                {
                    if (File.Exists("Configuration\\APIIntervalChoices.txt"))
                    {
                        APIIntervalOptions.Clear();
                        foreach (string s in File.ReadAllLines("Configuration\\APIIntervalChoices.txt"))
                        {
                            if (s.Trim() != "")
                            {

                                string[] split = s.Split('=');

                                APIIntervalOptions.Add(split[0], split[1]);

                            }
                        }
                    }
                }
                else { }
            }
            catch (Exception ex) { }
        }
    }
}

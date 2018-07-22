using System;
using System.Collections.Generic;

namespace PoE_2_Manifest_Maker.Model
{
    public class GameVersion
    {
        public string Min;
        public string Max;
    }

    public class Manifest_1_0
    {
        public Dictionary<string, string> Title;
        public Dictionary<string, string> Description;
        public String Author;
        public String ModVersion;
        public GameVersion SupportedGameVersion;
    }
}

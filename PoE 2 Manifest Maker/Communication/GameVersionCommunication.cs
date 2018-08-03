using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace PoE_2_Manifest_Maker.Communication
{
    public class GameVersionCommunication : BaseCommunication, IDataErrorInfo
    {
        private static string minVersion;
        private static string maxVersion;

        public String _version;
        public String Version
        {
            get
            {
                return _version;
            }

            set
            {
                _version = value;
                if (Name == "MinVersion_1_0")
                {
                    minVersion = value;
                }
                else if (Name == "MaxVersion_1_0")
                {
                    maxVersion = value;
                }

            }
        }
        public bool IsValid;

        private static Regex _regex = new Regex(@"^[\d]+\.[\d]+\.[\d]+$");

        public GameVersionCommunication(String name, String version, CommunicationDirection direction) : base(name, direction)
        {
            this.Version = version;
        }

        public string this[string propertyName]
        {
            get
            {
                if (propertyName == "Version")
                {
                    IsValid = _regex.IsMatch(Version) || string.Empty.Equals(Version);
                    if (!IsValid)
                        return "Version format is incorrect - it should be like: 'n.n.n'";

                    // If the versions is the empty string, then everything is ok
                    if (string.Empty.Equals(minVersion) || string.Empty.Equals(maxVersion))
                        return null;

                    Version versionMin = new Version(minVersion);
                    Version versionMax = new Version(maxVersion);

                    IsValid = versionMin <= versionMax;
                    if (!IsValid)
                    {
                        return "The max version must be bigger or equal to the min version";
                    }
                }
                return null;
            }
        }

        public string Error
        {
            get { return null; }
        }
    }
}

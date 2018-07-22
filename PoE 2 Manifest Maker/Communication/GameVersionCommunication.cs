using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace PoE_2_Manifest_Maker.Communication
{
    public class GameVersionCommunication : BaseCommunication, IDataErrorInfo
    {
        public String Version;
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
                    IsValid = _regex.IsMatch(Version);
                    if (!IsValid)
                    {
                        return "Version format is incorrect - it should be like: 'n.n.n'";
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

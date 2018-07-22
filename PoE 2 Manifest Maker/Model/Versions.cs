using Prism.Mvvm;
using System.Globalization;
using System.Linq;

namespace PoE_2_Manifest_Maker.Model
{
    public class Versions : BindableBase
    {
        public string[] VersionList { get; set; }

        public Versions(string[] versions)
        {
            this.VersionList = versions;
        }

        public string GetHighestVersion()
        {
            NumberFormatInfo numberInfo = NumberFormatInfo.InvariantInfo;
            return VersionList.Select(x =>
            {
                decimal outValue;
                decimal.TryParse(x, NumberStyles.Any, CultureInfo.InvariantCulture, out outValue);
                return outValue;
            }).Max().ToString("N", numberInfo);
    }
}
}

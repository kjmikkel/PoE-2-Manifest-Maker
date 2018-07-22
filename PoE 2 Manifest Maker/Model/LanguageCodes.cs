using Prism.Mvvm;

namespace PoE_2_Manifest_Maker.Model
{
    public class LanguageCodes : BindableBase
    {
        public string[] Codes { get; set; }
        
        public LanguageCodes(string[] codes)
        {
            this.Codes = codes;
        }
    }
}

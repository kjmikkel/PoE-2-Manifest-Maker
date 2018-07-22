namespace PoE_2_Manifest_Maker.Model
{
    public class LocalizedData_1_0
    {
        public string LanguageCode { get; set; }
        public string LanguageContent { get; set; }

        public LocalizedData_1_0 (string languageCode, string languageContent)
        {
            LanguageCode = languageCode;
            LanguageContent = languageContent;
        }
    }
}

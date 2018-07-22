using Newtonsoft.Json;
using PoE_2_Manifest_Maker.Model;
using PoE_2_Manifest_Maker.MVVM_Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace PoE_2_Manifest_Maker.MVVM
{
    public class InsertDataViewModel_1_0 : SimpleObservableObject
    {
        // The language combobox
        public ObservableCollection<string> LanguageCodes { get; private set; }

        public String SelectedLanguageCode { get; set; }

        // The text field
        public String TextBoxContent { get; set; }

        public bool? DialogResult { get; private set; }

        // The commands
        public RelayCommand AcceptCommand { get; private set; }

        public InsertDataViewModel_1_0(IEnumerable<string> languageCodesToRemove)
        {
            using (StreamReader r = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"/Resources/language_codes.json"))
            {
                var langCodes = JsonConvert.DeserializeObject<LanguageCodes>(r.ReadToEnd());
                LanguageCodes = new ObservableCollection<string>(langCodes.Codes);

                foreach(string code in languageCodesToRemove)
                {
                    LanguageCodes.Remove(code);
                }

                if (LanguageCodes.Contains("en"))
                {
                    SelectedLanguageCode = "en";
                } else
                {
                    SelectedLanguageCode = LanguageCodes[0];
                }
            }

            TextBoxContent = "";
            AcceptCommand = new RelayCommand(AcceptButtonCommand, CanExecute);
        }

        public InsertDataViewModel_1_0(LocalizedData_1_0 languageData, IEnumerable<string> languageCodesToRemove) : this(languageCodesToRemove)
        {
            SelectedLanguageCode = languageData.LanguageCode;
            TextBoxContent = languageData.LanguageContent;
        }
   
        public void AcceptButtonCommand()
        {
            DialogResult = true;
            RaisePropertyChangedEvent("DialogResult");
        }
        
        public bool CanExecute()
        {
            return !String.IsNullOrEmpty(TextBoxContent);
        }
    }
}

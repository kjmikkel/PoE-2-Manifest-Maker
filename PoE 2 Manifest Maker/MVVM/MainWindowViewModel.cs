using Newtonsoft.Json;
using PoE_2_Manifest_Maker.Model;
using PoE_2_Manifest_Maker.MVVM_Helper;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace PoE_2_Manifest_Maker.MVVM
{
    class MainWindowViewModel : SimpleObservableObject
    {
        public ObservableCollection<String> Versions { get; set; }

        private String _selectedVersion;
        public String SelectedVersion
        {
            get
            {
                return _selectedVersion;
            }
            set
            {
                _selectedVersion = value;
            }
        }

        public bool VersionEnabled { get; set; }

        public MainWindowViewModel()
        {
            using (StreamReader r = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"/Resources/version.json"))
            {
                var versions = JsonConvert.DeserializeObject<Versions>(r.ReadToEnd());
                Versions = new ObservableCollection<String>(versions.VersionList);
                VersionEnabled = Versions.Count > 1;
                SelectedVersion = versions.GetHighestVersion();
            }
        }
    }
}

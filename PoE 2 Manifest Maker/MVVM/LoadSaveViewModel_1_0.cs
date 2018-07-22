using Microsoft.Practices.Prism.Events;
using Newtonsoft.Json;
using PoE_2_Manifest_Maker.Communication;
using PoE_2_Manifest_Maker.Model;
using PoE_2_Manifest_Maker.MVVM_Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PoE_2_Manifest_Maker.MVVM
{
    public class LoadSaveViewModel_1_0 : SimpleObservableObject
    {
        // public String FileLoadLocation { get; set; }
        public String FileSaveLocation { get; set; }

        public RelayCommand NewFileCommand { get; private set; }
        public RelayCommand LoadCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand SaveAsCommand { get; private set; }

        public Manifest_1_0 Manifest { get; private set; }

        private GameVersionCommunication _minVersion;
        private GameVersionCommunication _maxVersion;

        public LoadSaveViewModel_1_0()
        {
            NewFileCommand = new RelayCommand(NewFile);
            LoadCommand = new RelayCommand(BrowseFileButton);
            SaveCommand = new RelayCommand(SaveButton, IsSaveValid);
            SaveAsCommand = new RelayCommand(SaveAsButton, CanSave);

            IEventAggregator _eventAggregator = ApplicationService.Instance.EventAggregator;

            _eventAggregator.GetEvent<LanguageControlChanged>().Subscribe(GetLocalizedData, ThreadOption.PublisherThread, true, CheckIncomming);
            _eventAggregator.GetEvent<GameVersionChanged>().Subscribe(GetGameVersionData, ThreadOption.PublisherThread, true, CheckIncomming);
            _eventAggregator.GetEvent<FreeformChanged>().Subscribe(GetFreeformData, ThreadOption.PublisherThread, true, CheckIncomming);

            Reset();
        }

        public void Reset()
        {
            Manifest = new Manifest_1_0
            {
                Description = new Dictionary<string, string>(),
                Title = new Dictionary<string, string>(),
                Author = "",
                ModVersion = "",
                SupportedGameVersion = new GameVersion()
                {
                    Min = "1.0.0",
                    Max = "1.0.0"
                }
            };

            FileSaveLocation = "";

            SendUpdateToComponents();
        }

        public bool CheckIncomming(BaseCommunication communication)
        {
            return communication.CheckDirection(CommunicationDirection.FromComponent);
        }

        private void GetGameVersionData(GameVersionCommunication newVersion)
        {
            if (newVersion.Name == "MinVersion_1_0")
            {
                Manifest.SupportedGameVersion.Min = newVersion.Version;
                _minVersion = newVersion;
            }
            else if (newVersion.Name == "MaxVersion_1_0")
            {
                Manifest.SupportedGameVersion.Max = newVersion.Version;
                _maxVersion = newVersion;
            }
        }

        private void GetFreeformData(FreeformCommunication freeformChanged)
        {
            if (freeformChanged.Name == "AuthorTextBox_1_0")
            {
                Manifest.Author = freeformChanged.FreeformData;
            }
            else if (freeformChanged.Name == "ModVersion_1_0")
            {
                Manifest.ModVersion = freeformChanged.FreeformData;
            }
        }

        private void GetLocalizedData(LanguageControlCommunication localizedData)
        {
            if (localizedData.Name == "TitleLanguage_1_0")
            {
                Manifest.Title = MakeSerializableData(localizedData.Items);
            }
            else if (localizedData.Name == "DescriptionLanguage_1_0")
            {
                Manifest.Description = MakeSerializableData(localizedData.Items);
            }
        }

        private void NewFile()
        {
            Reset();
        }

        #region Load
        private void BrowseFileButton()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "JSON file | *.json";
            var result = openFileDialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                string location = openFileDialog.FileName;

                if (!String.IsNullOrEmpty(location) && location.EndsWith("manifest.json"))
                {
                    FileSaveLocation = location;
                    LoadButton(location);
                }
            }
        }

        private void LoadButton(String location)
        {
            String loadedData = null;

            if (File.Exists(location))
            {
                loadedData = File.ReadAllText(location);

                Manifest = JsonConvert.DeserializeObject<Manifest_1_0>(loadedData);

                SendUpdateToComponents();
            }
            else
            {
                System.Windows.MessageBox.Show($"Cannot find file at {location}");
            }
        }

        private void SendUpdateToComponents()
        {
            IEventAggregator _eventAggregator = ApplicationService.Instance.EventAggregator;

            // Localization
            _eventAggregator.GetEvent<LanguageControlChanged>().Publish(new LanguageControlCommunication("TitleLanguage_1_0", MakeLocalizedData(Manifest.Title), CommunicationDirection.TooComponent));
            _eventAggregator.GetEvent<LanguageControlChanged>().Publish(new LanguageControlCommunication("DescriptionLanguage_1_0", MakeLocalizedData(Manifest.Description), CommunicationDirection.TooComponent));

            // Freeform text
            _eventAggregator.GetEvent<FreeformChanged>().Publish(new FreeformCommunication("AuthorTextBox_1_0", Manifest.Author, CommunicationDirection.TooComponent));
            _eventAggregator.GetEvent<FreeformChanged>().Publish(new FreeformCommunication("ModVersion_1_0", Manifest.ModVersion, CommunicationDirection.TooComponent));

            _eventAggregator.GetEvent<GameVersionChanged>().Publish(new GameVersionCommunication("MinVersion_1_0", Manifest.SupportedGameVersion.Min, CommunicationDirection.TooComponent));
            _eventAggregator.GetEvent<GameVersionChanged>().Publish(new GameVersionCommunication("MaxVersion_1_0", Manifest.SupportedGameVersion.Max, CommunicationDirection.TooComponent));
        }

        private IEnumerable<LocalizedData_1_0> MakeLocalizedData(Dictionary<string, string> rawData)
        {
            List<LocalizedData_1_0> data = new List<LocalizedData_1_0>();
            foreach (string key in rawData.Keys)
            {
                data.Add(new LocalizedData_1_0(key, rawData[key]));
            }
            return data;
        }
        #endregion

        private Dictionary<string, string> MakeSerializableData(IEnumerable<LocalizedData_1_0> rawData)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (LocalizedData_1_0 localized in rawData)
            {
                result.Add(localized.LanguageCode, localized.LanguageContent);
            }
            return result;
        }

        private void SaveAsButton()
        {
            var saveFolderDialog = new FolderBrowserDialog();
            saveFolderDialog.Description = "Select the folder you want to save the manifest.json to";
            var result = saveFolderDialog.ShowDialog();

            if (result == DialogResult.OK && Directory.Exists(saveFolderDialog.SelectedPath))
            {
                saveFolderDialog.RootFolder = Environment.SpecialFolder.CommonProgramFilesX86;
                FileSaveLocation = Path.Combine(saveFolderDialog.SelectedPath, "manifest.json");
                SaveButton();
            }
        }

        private void SaveButton()
        {
            string fileLocation = FileSaveLocation;
            if (Directory.Exists(FileSaveLocation))
            {
                fileLocation += Path.Combine(FileSaveLocation, "manifest.json");
            }

            if (fileLocation.EndsWith("manifest.json"))
            {
                using (StreamWriter sw = new StreamWriter(fileLocation))
                {
                    sw.Write(JsonConvert.SerializeObject(Manifest));
                }
            }
        }

        public bool CanSave()
        {
            return (_minVersion?.IsValid ?? false) && (_maxVersion?.IsValid ?? false);
        }

        private bool IsSaveValid()
        {
            return File.Exists(FileSaveLocation) && CanSave();
        }
    }
}

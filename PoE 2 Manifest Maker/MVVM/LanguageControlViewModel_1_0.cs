using Newtonsoft.Json;
using PoE_2_Manifest_Maker.Communication;
using PoE_2_Manifest_Maker.Model;
using PoE_2_Manifest_Maker.MVVM_Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace PoE_2_Manifest_Maker.MVVM
{
    public class LanguageControlViewModel_1_0 : ObservableObject<LanguageControlChanged, LanguageControlCommunication>
    {
        public ObservableCollection<LocalizedData_1_0> Items { get; private set; }
        public LocalizedData_1_0 SelectedContent { get; set; }
        public int SelectedIndex { get; set; }

        public RelayCommand AddButtonCommand { get; private set; }
        public RelayCommand EditButtonCommand { get; private set; }
        public RelayCommand RemoveButtonCommand { get; private set; }

        private int totalAmountLanguageCodes;

        private InsertDataDialogService_1_0 _dialogService;


        private List<string> CodesToRemove
        {
            get
            {
                List<string> codesToRemove = new List<string>();
                foreach (LocalizedData_1_0 localizedEntries in Items)
                {
                    codesToRemove.Add(localizedEntries.LanguageCode);
                }

                return codesToRemove;
            }
        }

        public LanguageControlViewModel_1_0(String name) : base(name)
        {
            Items = new ObservableCollection<LocalizedData_1_0>();
            AddButtonCommand = new RelayCommand(AddButton, CanAdd);
            EditButtonCommand = new RelayCommand(EditCommand, CanExecute);
            RemoveButtonCommand = new RelayCommand(RemoveCommand, CanExecute);
            _dialogService = new InsertDataDialogService_1_0();

            using (StreamReader r = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"/Resources/language_codes.json"))
            {
                var langCodes = JsonConvert.DeserializeObject<LanguageCodes>(r.ReadToEnd());
                totalAmountLanguageCodes = langCodes.Codes.Length;
            }
        }

        private void AddButton()
        {
            var dialog = new InsertDataViewModel_1_0(CodesToRemove);
            var result = _dialogService.ShowDialog(dialog);

            if (result.HasValue && result.Value)
            {
                LocalizedData_1_0 data = new LocalizedData_1_0(dialog.SelectedLanguageCode, dialog.TextBoxContent);
                Items.Add(data);
                Publish();
            }
        }

        private void EditCommand()
        {
            List<string> localRemoveList = CodesToRemove;
            localRemoveList.Remove(SelectedContent.LanguageCode);
            var dialog = new InsertDataViewModel_1_0(SelectedContent, localRemoveList);
            var result = _dialogService.ShowDialog(dialog);

            int oldIndex = SelectedIndex;

            if (result.HasValue && result.Value)
            {
                LocalizedData_1_0 data = new LocalizedData_1_0(dialog.SelectedLanguageCode, dialog.TextBoxContent);
                Items.Remove(SelectedContent);
                Items.Add(data);
                Publish();
            }
        }

        private void RemoveCommand()
        {
            // get the new content
            int index = Items.IndexOf(SelectedContent);
            Items.Remove(SelectedContent);

            LocalizedData_1_0 newSelectedContent = null;
            if (Items.Count > 0)
            {
                if (index == Items.Count)
                {
                    // We were at the bottom, so we go up once
                    newSelectedContent = Items[index - 1];
                } else
                {
                    newSelectedContent = Items[index];
                }
            }

            SelectedContent = newSelectedContent;
            RaisePropertyChangedEvent("SelectedContent");
            Publish();
        }

        private void Publish()
        {
            LanguageControlCommunication communication = new LanguageControlCommunication(Name, Items, CommunicationDirection.FromComponent);
            Publish(communication);
        }

        private bool CanExecute()
        {
            return SelectedContent != null;
        }

        protected override void SetData(LanguageControlCommunication setData)
        {
            Items.Clear();
            Items.AddRange(setData.Items);
        }

        private bool CanAdd()
        {
            return Items.Count < totalAmountLanguageCodes;
        }
    }
}

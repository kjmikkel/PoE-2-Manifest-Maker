using PoE_2_Manifest_Maker.Communication;
using PoE_2_Manifest_Maker.MVVM_Helper;
using System;

namespace PoE_2_Manifest_Maker.MVVM
{
    public class TextboxFreeViewModel : ObservableObject<FreeformChanged, FreeformCommunication>
    {
        private String _text;
        public String Text
        {
            get
            {
                return _text;
            }

            set
            {
                _text = value;
                FreeformCommunication textboxCommunication = new FreeformCommunication(name, _text, CommunicationDirection.FromComponent);
                Publish(textboxCommunication);
            }
        }

        public FreeformCommunication freeformCommunication;

        public TextboxFreeViewModel(String name) : base(name)
        {
            // let blank on purpose
        }
        
        protected override void SetData(FreeformCommunication setData)
        {
            _text = setData.FreeformData;
            RaisePropertyChangedEvent("Text");
        }
    }
}

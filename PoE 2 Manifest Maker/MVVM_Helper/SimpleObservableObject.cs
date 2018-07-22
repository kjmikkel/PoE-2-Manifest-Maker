using System.ComponentModel;

namespace PoE_2_Manifest_Maker.MVVM_Helper
{
    public abstract class SimpleObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

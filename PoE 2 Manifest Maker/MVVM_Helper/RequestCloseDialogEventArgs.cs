using System;

namespace PoE_2_Manifest_Maker.MVVM_Helper
{
    public class RequestCloseDialogEventArgs : EventArgs
    {
        public bool DialogResult { get; set; }
        public RequestCloseDialogEventArgs(bool dialogresult)
        {
            this.DialogResult = dialogresult;
        }
    }
}

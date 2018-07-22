using PoE_2_Manifest_Maker.View;

namespace PoE_2_Manifest_Maker.MVVM_Helper
{
    public class InsertDataDialogService_1_0 : IUIWindowDialogService
    {
        public bool? ShowDialog(object datacontext)
        {
            var win = new InsertData_1_0
            {
                DataContext = datacontext
            };

            return win.ShowDialog();
        }
    }
}

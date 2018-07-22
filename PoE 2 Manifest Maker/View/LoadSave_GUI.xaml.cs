using PoE_2_Manifest_Maker.MVVM;
using System.Windows.Controls;

namespace PoE_2_Manifest_Maker.View
{
    /// <summary>
    /// Interaction logic for LoadSave_GUI.xaml
    /// </summary>
    public partial class LoadSave_GUI : UserControl
    {
        public LoadSave_GUI()
        {
            InitializeComponent();
            this.DataContext = new LoadSaveViewModel_1_0();
        }
    }
}

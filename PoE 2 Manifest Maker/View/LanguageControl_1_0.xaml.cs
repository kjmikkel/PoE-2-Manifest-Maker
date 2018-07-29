using System.Windows.Controls;
using PoE_2_Manifest_Maker.MVVM;
using PoE_2_Manifest_Maker.MVVM_Helper;

namespace PoE_2_Manifest_Maker.View
{
    /// <summary>
    /// Interaction logic for LanguesControl.xaml
    /// </summary>
    public partial class LanguageControl_1_0 : UserControl
    {
        public LanguageControl_1_0()
        {
            InitializeComponent();           
            this.Loaded += LanguageControl_1_0_Loaded;
        }

        private void LanguageControl_1_0_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DataContext = new LanguageControlViewModel_1_0(Name);
        }
    }
}

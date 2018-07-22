using PoE_2_Manifest_Maker.MVVM;
using System.Windows;
using System.Windows.Controls;

namespace PoE_2_Manifest_Maker.View
{
    /// <summary>
    /// Interaction logic for FreeformTextbox.xaml
    /// </summary>
    public partial class FreeformTextbox : UserControl
    {
        public FreeformTextbox()
        {
            InitializeComponent();
            Loaded += FreeformText_Loaded;
        }

        private void FreeformText_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new TextboxFreeViewModel(Name);
        }
    }
}

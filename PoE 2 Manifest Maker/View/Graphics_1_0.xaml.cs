using PoE_2_Manifest_Maker.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PoE_2_Manifest_Maker.View
{
    /// <summary>
    /// Interaction logic for Graphics_1_0.xaml
    /// </summary>
    public partial class Graphics_1_0 : UserControl
    {
        public Graphics_1_0()
        {
            InitializeComponent();
            this.Loaded += Graphics_1_0_Loaded;
        }

        private void Graphics_1_0_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new GraphicsViewModel_1_0(Name);
        }
    }
}

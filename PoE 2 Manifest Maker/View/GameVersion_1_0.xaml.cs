using PoE_2_Manifest_Maker.MVVM;
using System.Windows;
using System.Windows.Controls;

namespace PoE_2_Manifest_Maker.View
{
    /// <summary>
    /// Interaction logic for GameVersion_1_0.xaml
    /// </summary>
    public partial class GameVersion_1_0 : UserControl
    {
        public GameVersion_1_0()
        {
            InitializeComponent();
            Loaded += GameVersion_1_0_Loaded;
        }

        private void GameVersion_1_0_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new GameVersionViewModel_1_0(Name);
        }
    }
}

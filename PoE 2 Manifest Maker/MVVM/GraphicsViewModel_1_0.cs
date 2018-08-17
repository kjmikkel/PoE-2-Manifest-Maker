using PoE_2_Manifest_Maker.Communication;
using PoE_2_Manifest_Maker.MVVM_Helper;
using System;
using System.Windows.Media.Imaging;

namespace PoE_2_Manifest_Maker.MVVM
{
    class GraphicsViewModel_1_0 : ObservableObject<ImageChanged, ImageCommunication>
    {
        public BitmapImage Image { get; private set; }

        public RelayCommand PickImageCommand { get; private set; }

        public GraphicsViewModel_1_0(String Name) : base(Name)
        {
            PickImageCommand = new RelayCommand(PickImage);
        }

        protected override void SetData(ImageCommunication setData)
        {
            SetImage(setData.Image);
        }

        private void PickImage()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image files |*.png; *.bmp; *.jpg";
            var result = openFileDialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                SetImage(openFileDialog.FileName);
                this.Publish(new ImageCommunication(this.Name, openFileDialog.FileName, CommunicationDirection.FromComponent));
                RaisePropertyChangedEvent("Image");
            }
        }

        private void SetImage(String imagePath)
        {
            if (!String.IsNullOrEmpty(imagePath))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(imagePath);
                image.EndInit();
                Image = image;
            }
            else
            {
                Image = null;
            }

            RaisePropertyChangedEvent("Image");

        }
    }
}

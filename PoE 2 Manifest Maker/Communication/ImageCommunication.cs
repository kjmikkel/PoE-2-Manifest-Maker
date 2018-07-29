using System;
using System.Drawing;

namespace PoE_2_Manifest_Maker.Communication
{
    class ImageCommunication : BaseCommunication
    {
        public String Image;

        public ImageCommunication(String Name, String Image, CommunicationDirection Direction) : base(Name, Direction)
        {
            this.Image = Image;
        }
    }
}

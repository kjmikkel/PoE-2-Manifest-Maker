using System;

namespace PoE_2_Manifest_Maker.Communication
{
    public class FreeformCommunication : BaseCommunication
    {
        public String FreeformData;

        public FreeformCommunication(String Name, String FreeformData, CommunicationDirection Direction) : base(Name, Direction)
        {
            this.FreeformData = FreeformData;
        }
    }
}

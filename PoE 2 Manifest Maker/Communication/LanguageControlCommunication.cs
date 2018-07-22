using PoE_2_Manifest_Maker.Model;
using System;
using System.Collections.Generic;

namespace PoE_2_Manifest_Maker.Communication
{
    public class LanguageControlCommunication : BaseCommunication
    {
        public IEnumerable<LocalizedData_1_0> Items;

        public LanguageControlCommunication(String Name, IEnumerable<LocalizedData_1_0> Items, CommunicationDirection Direction) : base(Name, Direction)
        {
            this.Items = Items;
        }
    }
}

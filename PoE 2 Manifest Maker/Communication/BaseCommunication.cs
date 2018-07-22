
namespace PoE_2_Manifest_Maker.Communication
{
    public abstract class BaseCommunication
    {
        private CommunicationDirection Direction;
        public string Name { get; private set; }

        public BaseCommunication()
        {

        }

        public BaseCommunication(string name, CommunicationDirection direction)
        {
            this.Name = name;
            this.Direction = direction;
        }

        public bool CheckDirection(CommunicationDirection direction)
        {
            return Direction == direction;
        }
    }
}

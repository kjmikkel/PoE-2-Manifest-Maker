using Microsoft.Practices.Prism.Events;

namespace PoE_2_Manifest_Maker.Communication
{
    abstract public class BaseChanged<T> : CompositePresentationEvent<T> where T : BaseCommunication
    {
        // Left blank on purpose
    }
}

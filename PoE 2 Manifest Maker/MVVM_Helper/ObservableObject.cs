using Microsoft.Practices.Prism.Events;
using PoE_2_Manifest_Maker.Communication;

namespace PoE_2_Manifest_Maker.MVVM_Helper
{
    public abstract class ObservableObject<T, K> : SimpleObservableObject where T : BaseChanged<K>, new()
        where K : BaseCommunication

    {
        private readonly IEventAggregator _eventAggregator;
        protected string Name;

        public ObservableObject(string name)
        {
            this.Name = name;
            _eventAggregator = ApplicationService.Instance.EventAggregator;
            _eventAggregator.GetEvent<T>().Subscribe(SetData, ThreadOption.PublisherThread, true, x => x.CheckDirection(CommunicationDirection.ToComponent) && x.Name == name);
        }

        protected void Publish(K dataToPublish)
        {
            _eventAggregator.GetEvent<T>().Publish(dataToPublish);
        }

        protected abstract void SetData(K setData);
    }
}

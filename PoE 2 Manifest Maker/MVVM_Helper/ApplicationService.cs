using Microsoft.Practices.Prism.Events;

namespace PoE_2_Manifest_Maker.MVVM_Helper
{
    internal sealed class ApplicationService
    {
        private ApplicationService() { }

        internal static ApplicationService Instance { get; } = new ApplicationService();

        private IEventAggregator _eventAggregator;
        internal IEventAggregator EventAggregator
        {
            get
            {
                if (_eventAggregator == null)
                    _eventAggregator = new EventAggregator();

                return _eventAggregator;
            }
        }
    }
}

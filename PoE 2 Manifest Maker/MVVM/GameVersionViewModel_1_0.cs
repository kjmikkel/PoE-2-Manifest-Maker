using PoE_2_Manifest_Maker.Communication;
using PoE_2_Manifest_Maker.MVVM_Helper;
using System;
using System.ComponentModel;

namespace PoE_2_Manifest_Maker.MVVM
{
    class GameVersionViewModel_1_0 : ObservableObject<GameVersionChanged, GameVersionCommunication>, IDataErrorInfo
    {
        private GameVersionCommunication _gameVersion;
        private String _version;

        public String Version
        {
            get
            {
                return _version;
            }

            set
            {
                if (value == _version)
                {
                    return;
                }

                _version = value;

                if (String.Empty.Equals(_version) && !NoVersion)
                    NoVersion = true;

                RaisePropertyChangedEvent("Version");
            }
        }

        public bool Enabled { get { return !_noVersion; } }

        private bool _noVersion;
        public bool NoVersion
        {
            get { return _noVersion; }
            set
            {
                _noVersion = value;

                RaisePropertyChangedEvent("NoVersion");
                RaisePropertyChangedEvent("Enabled");

                Version = _noVersion ?  "" : "1.0.0";
            }
        }

        public string VersionError { get; set; }

        public GameVersionViewModel_1_0(String name) : base(name)
        {
            _gameVersion = new GameVersionCommunication(name, "1.0.0", CommunicationDirection.FromComponent);
            Version = _gameVersion.Version;
        }

        public string this[string propertyName]
        {
            get
            {
                if (propertyName == "Version")
                {
                    _gameVersion.Version = Version;
                }

                VersionError = _gameVersion[propertyName];
                Publish(_gameVersion);
                RaisePropertyChangedEvent("VersionError");

                return VersionError;
            }
        }

        protected override void SetData(GameVersionCommunication setData)
        {
            Version = setData.Version;
            RaisePropertyChangedEvent("Version");
        }

        public string Error
        {
            get { return _gameVersion.Error; }
        }
    }
}

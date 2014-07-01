using System;

    public class MetaData
    {
        #region DEFINITIONS

        private string _key;
        private string _description;

        #endregion

        #region PROPERTIES

        public string Key { get { return _key; } set { _key = value; } }
        public string Description { get { return _description; } set { _description = value; } }

        #endregion

        #region CONSTRUCTORS

        public MetaData(string Key, string Description)
        {
            NewMetaData(Key, Description);
        }

        #endregion

        #region PRIVATE METHODS

        private void NewMetaData(string Key, string Description)
        {
            this.Key = Key;
            this.Description = Description;
        }

        #endregion

        #region PUBLIC METHODS

        #endregion
    }


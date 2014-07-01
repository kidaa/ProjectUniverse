using System;

    public class MetaDataToEntity
    {
         #region DEFINITIONS

        private int _id;
        private Enums.Entity_Type _entity_type;
        private string _metadata_key;
        private string _value;

        #endregion

        #region PROPERTIES

        public int ID { get {return _id;} set {_id = value;} }
        public Enums.Entity_Type Entity_Type { get { return _entity_type; } set { _entity_type = value; } }
        public string MetaData_Key { get { return _metadata_key; } set { _metadata_key = value; } }
        public string Value { get { return _value; } set { _value = value; } }

        #endregion

        #region CONSTRUCTORS

        public MetaDataToEntity()
        {
            NewMetaDataToEntity(0, Enums.Entity_Type.None, string.Empty, string.Empty);
        }

        public MetaDataToEntity(int ID,
            Enums.Entity_Type Entity_Type,
            string MetaData_Key,
            string Value)
        {
            NewMetaDataToEntity(ID, Entity_Type, MetaData_Key, Value);
        }

        #endregion

        #region PRIVATE METHODS

        private void NewMetaDataToEntity(int ID,
            Enums.Entity_Type Entity_Type,
            string MetaData_Key,
            string Value)
        {
            this.ID = ID;
            this.Entity_Type = Entity_Type;
            this.MetaData_Key = MetaData_Key;
            this.Value = Value;
        }

        #endregion

        #region PUBLIC METHODS

        #endregion
    }


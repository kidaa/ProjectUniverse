using System;

    public class Inventory
    {
        #region DEFINITIONS

        private int _id;
        private Enums.Entity_Type _entity_type;
        private int _container_id;
        private int _capacity;

        private bool _is_dirty;

        #endregion

        #region PROPERTIES

        public int ID { get { return _id; } set { _id = value; _is_dirty = true; } }
        public Enums.Entity_Type Entity_Type { get { return _entity_type; } set { _entity_type = value; _is_dirty = true; } }
        public int Container_ID { get { return _container_id; } set { _container_id = value; _is_dirty = true; } }
        public int Capacity { get { return _capacity; } set { _capacity = value; _is_dirty = true; } }
        
        public bool Is_Dirty { get { return _is_dirty; } set { _is_dirty = value;  } }

        #endregion

        #region CONSTRUCTORS

        public Inventory()
        {
            NewInventory(0, Enums.Entity_Type.None, 0, 0);
        }

        public Inventory(int ID,
            Enums.Entity_Type Entity_Type,
            int Container_ID,
            int Capacity)
        {
            NewInventory(ID, Entity_Type, Container_ID, Capacity);
        }

        #endregion

        #region PRIVATE METHODS

        private void NewInventory(int ID,
            Enums.Entity_Type Entity_Type,
            int Container_ID,
            int Capacity)
        {
            this.ID = ID;
            this.Entity_Type = Entity_Type;
            this.Container_ID = Container_ID;
            this.Capacity = Capacity;
        }

        #endregion

        #region PUBLIC METHODS

        #endregion
    }


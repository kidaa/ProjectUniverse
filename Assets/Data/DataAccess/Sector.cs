using System;

    public class Sector
    {

        #region DEFINITIONS

        private int _id;
        private string _name;
        private string _description;
        private int _faction_id;

        private bool _is_dirty;

        #endregion

        #region PROPERTIES

        public int ID { get { return _id; } set { _id = value; _is_dirty = true; } }
        public string Name { get { return _name; } set { _name = value; _is_dirty = true; } }
        public string Description { get { return _description; } set { _description = value; _is_dirty = true; } }
        public int Faction_ID { get { return _faction_id; } set { _faction_id = value; _is_dirty = true; } }

        public bool Is_Dirty { get { return _is_dirty; } set { _is_dirty = value;} }
        
        #endregion

        #region CONSTRUCTORS

        public Sector()
        {
            NewSector(0, string.Empty, string.Empty, 0);
        }

        public Sector(int ID,
            string Name,
            string Description,
            int Faction_ID)
        {
            NewSector(ID, Name, Description, Faction_ID);
        }

        #endregion

        #region PRIVATE METHODS

        private void NewSector(int ID,
            string Name,
            string Description,
            int Faction_ID)
        {
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
            this.Faction_ID = Faction_ID;
        }

        #endregion

        #region PUBLIC METHODS
        
        #endregion
    }


using System;


    public class Jumpgate
    {
        #region DEFINITIONS

        private int _id;
        private int _sector_id;
        private string _name;
        private string _description;
        private int _base_toll;
        private int _faction_id;
        private int _destination_sector_id;
        private int _destination_gate_id;

        private bool _is_dirty;

        #endregion

        #region PROPERTIES

        public int ID { get { return _id; } set { _id = value; _is_dirty = true; } }
        public int Sector_ID { get { return _sector_id; } set { _sector_id = value; _is_dirty = true; } }
        public string Name { get { return _name; } set { _name = value; _is_dirty = true; } }
        public string Description { get { return _description; } set { _description = value; _is_dirty = true; } }
        public int Base_Toll { get { return _base_toll; } set { _base_toll = value; _is_dirty = true; } }
        public int Faction_ID { get { return _faction_id; } set { _faction_id = value; _is_dirty = true; } }
        public int Destination_Sector_ID { get { return _destination_sector_id; } set { _destination_sector_id = value; _is_dirty = true; } }
        public int Destination_Gate_ID { get { return _destination_gate_id; } set { _destination_gate_id = value; _is_dirty = true; } }

        public bool Is_Dirty { get { return _is_dirty; } set { _is_dirty = value; } }

        #endregion

        #region CONSTRUCTORS

        public Jumpgate()
        {
            NewJumpgate(0, 0, string.Empty, string.Empty, 0, 0, 0, 0);
        }

        public Jumpgate(int ID,
            int Sector_ID,
            string Name,
            string Description,
            int Base_Toll,
            int Faction_ID,
            int Destination_Sector_ID,
            int Destination_Gate_ID)
        {
            NewJumpgate(ID, Sector_ID, Name, Description, Base_Toll, Faction_ID, Destination_Sector_ID, Destination_Gate_ID);
        }

        #endregion

        #region PRIVATE METHODS

        private void NewJumpgate(int ID,
            int Sector_ID,
            string Name,
            string Description,
            int Base_Toll,
            int Faction_ID,
            int Destination_Sector_ID,
            int Destination_Gate_ID)
        {
            this.ID = ID;
            this.Sector_ID = Sector_ID;
            this.Name = Name;
            this.Description = Description;
            this.Base_Toll = Base_Toll;
            this.Faction_ID = Faction_ID;
            this.Destination_Sector_ID = Destination_Sector_ID;
            this.Destination_Gate_ID = Destination_Gate_ID;
        }

        #endregion

        #region PUBLIC METHODS

        #endregion
    }


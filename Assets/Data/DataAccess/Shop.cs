using System;

    public class Shop
    {
         #region DEFINITIONS

        private int _id;
        private int _station_id;
        private string _name;
        private string _description;
        private string _atlas_avatar;

        private bool _is_dirty;

        #endregion

        #region PROPERTIES

        public int ID { get { return _id; } set { _id = value; _is_dirty = true; } }
        public int Station_ID { get { return _station_id; } set { _station_id = value; _is_dirty = true; } }
        public string Name { get { return _name; } set { _name = value; _is_dirty = true; } }
        public string Description { get { return _description; } set { _description = value; _is_dirty = true; } }
        public string Atlas_Avatar { get { return _atlas_avatar; } set { _atlas_avatar = value; _is_dirty = true; } }
        
        public bool Is_Dirty { get { return _is_dirty; } set { _is_dirty = value; } }

        #endregion

        #region CONSTRUCTORS

        public Shop()
        {
            NewShop(0, 0, string.Empty, string.Empty, string.Empty);
        }

        public Shop(int ID,
            int Station_ID,
            string Name,
            string Description,
            string Atlas_Avatar)
        {
            NewShop(ID, Station_ID, Name, Description, Atlas_Avatar);
        }

        #endregion

        #region PRIVATE METHODS

        private void NewShop(int ID,
            int Station_ID,
            string Name,
            string Description,
            string Atlas_Avatar)
        {
            this.ID = ID;
            this.Station_ID = Station_ID;
            this.Name = Name;
            this.Description = Description;
            this.Atlas_Avatar = Atlas_Avatar;
        }

        #endregion

        #region PUBLIC METHODS
        
        #endregion
    }


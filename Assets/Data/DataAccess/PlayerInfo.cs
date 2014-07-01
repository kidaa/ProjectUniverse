using System;

    public class PlayerInfo
    {
        #region DEFINITIONS

        private int _id;
        private string _name;
        private string _atlas_avatar;

        private bool _is_dirty;

        #endregion

        #region PROPERTIES

        public int ID { get { return _id; } set { _id = value; _is_dirty = true; } }
        public string Name { get { return _name; } set { _name = value; _is_dirty = true; } }
        public string Atlas_Avatar { get { return _atlas_avatar; } set { _atlas_avatar = value; _is_dirty = true; } }

        public bool Is_Dirty { get { return _is_dirty; } set { _is_dirty = value; } }

        #endregion

        #region CONSTRUCTORS

        public PlayerInfo()
        {
            NewPlayerInfo(0, string.Empty, string.Empty);
        }

        public PlayerInfo(int ID,
            string Name,
            string Atlas_Avatar)
        {
            NewPlayerInfo(ID, Name, Atlas_Avatar);
        }

        #endregion

        #region PRIVATE METHODS

        private void NewPlayerInfo(int ID,
            string Name,
            string Atlas_Avatar)
        {
            this.ID = ID;
            this.Name = Name;
            this.Atlas_Avatar = Atlas_Avatar;
        }

        #endregion

        #region PUBLIC METHODS

        #endregion
    }


using System;
using UnityEngine;          //For vector3

    public class PlayerInfo
    {
        #region DEFINITIONS

        private int _id;
        private string _name;
        private string _atlas_avatar;
        private Vector3 _current_position;
        private Vector3 _current_rotation;

        private string _current_sector;

        private bool _is_dirty;

        #endregion

        #region PROPERTIES

        public int ID { get { return _id; } set { _id = value; _is_dirty = true; } }
        public string Name { get { return _name; } set { _name = value; _is_dirty = true; } }
        public string Atlas_Avatar { get { return _atlas_avatar; } set { _atlas_avatar = value; _is_dirty = true; } }
        public Vector3 Current_Position { get { return _current_position; } set { _current_position = value; _is_dirty = true; } }
        public Vector3 Current_Rotation { get { return _current_rotation; } set { _current_rotation = value; _is_dirty = true; } }

        public string Current_Sector { get { return _current_sector; } set { _current_sector = value; _is_dirty = true; } }

        public bool Is_Dirty { get { return _is_dirty; } set { _is_dirty = value; } }

        #endregion

        #region CONSTRUCTORS

        public PlayerInfo()
        {
            NewPlayerInfo(0, string.Empty, string.Empty, string.Empty, Vector3.zero, Vector3.zero);
        }

        public PlayerInfo(int ID,
            string Name,
            string Atlas_Avatar)
        {
            NewPlayerInfo(ID, Name, Atlas_Avatar, string.Empty, Vector3.zero, Vector3.zero);
        }

        public PlayerInfo(int ID,
            string Name,
            string Atlas_Avatar,
            string Current_Sector)
        {
            NewPlayerInfo(ID, Name, Atlas_Avatar, Current_Sector, Vector3.zero, Vector3.zero);
        }

        public PlayerInfo(int ID,
            string Name,
            string Atlas_Avatar,
            string Current_Sector,
            Vector3 Current_Position,
            Vector3 Current_Rotation)
        {
            NewPlayerInfo(ID, Name, Atlas_Avatar, string.Empty, Current_Position, Current_Rotation);
        }

        #endregion

        #region PRIVATE METHODS

        private void NewPlayerInfo(int ID,
            string Name,
            string Atlas_Avatar,
            string Current_Sector,
            Vector3 Current_Position,
            Vector3 Current_Rotation)
        {
            this.ID = ID;
            this.Name = Name;
            this.Atlas_Avatar = Atlas_Avatar;
            this.Current_Sector = Current_Sector;
            this.Current_Position = Current_Position;
            this.Current_Rotation = Current_Rotation;            
        }

        #endregion

        #region PUBLIC METHODS

        #endregion
    }


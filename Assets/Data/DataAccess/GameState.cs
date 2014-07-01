using System;

    public class GameState
    {
        #region DEFINITIONS

        private int _id;
        private string _gu_id;
        private string _name;
        private DateTime _last_load_date;
        private DateTime _last_save_date;

        private bool _is_dirty;

        #endregion

        #region PROPERTIES

        public int ID { get { return _id; } set { _id = value; _is_dirty = true; } }
        public string GU_ID { get { return _gu_id; } set { _gu_id = value; _is_dirty = true; } }
        public string Name { get { return _name; } set { _name = value; _is_dirty = true; } }
        public DateTime Last_Load_Date { get { return _last_load_date; } set { _last_load_date = value; _is_dirty = true; } }
        public DateTime Last_Save_Date { get { return _last_save_date; } set { _last_save_date = value; _is_dirty = true; } }     

        public bool Is_Dirty { get { return _is_dirty; } set { _is_dirty = value; } }

        #endregion

        #region CONSTRUCTORS

        public GameState()
        {
            NewGameState(0, Guid.NewGuid().ToString(), string.Empty, DateTime.MinValue, DateTime.MinValue);
        }

        public GameState(int ID,
            string GU_ID,
            string Name,
            DateTime Last_Load_Date,
            DateTime Last_Save_Date)
        {
            NewGameState(ID, GU_ID, Name, Last_Load_Date, Last_Save_Date);
        }
            


        #endregion

        #region PRIVATE METHODS

        private void NewGameState(int ID,
            string GU_ID,
            string Name,
            DateTime Last_Load_Date,
            DateTime Last_Save_Date)
        {
            this.ID = ID;
            this.GU_ID = GU_ID;
            this.Name = Name;
            this.Last_Load_Date = Last_Load_Date;
            this.Last_Save_Date = Last_Save_Date;
        }

        #endregion

        #region PUBLIC METHODS


        #endregion
    }


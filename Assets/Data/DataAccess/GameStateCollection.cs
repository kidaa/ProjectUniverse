using System;
using System.Collections.Generic;

    public class GameStateCollection
    {
        #region DEFINITIONS

        public List<GameState> Game_States = new List<GameState>();
        public bool Is_Dirty = false;

        #endregion


        #region PUBLIC METHODS

        public void Add(GameState GameState)
        {
            //If we have this one in there, just update
            if (Game_States.Contains(GameState))
            {
                int idx = Game_States.IndexOf(GameState);
                Game_States[idx] = GameState;
            }
            else
            {
                Game_States.Add(GameState);
            }
        }
        

        #endregion  

    }


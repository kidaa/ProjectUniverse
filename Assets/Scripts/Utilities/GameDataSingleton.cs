using UnityEngine;
using System.Collections.Generic;

public class GameDataSingleton : Singleton<GameDataSingleton> {
	protected GameDataSingleton() {}

    public SDS SDS = new SDS();
    public GameStateCollection GSC = new GameStateCollection();

    public string Current_Game_ID;                      
    public int GlobalCommodityCapacity = 1000000;       //Just a default. Will be set once the II assignment has been made.
    public int DestinationJumpgateID;                   //Reference for when we pop out on the other side; get pos and rot and position for player
}

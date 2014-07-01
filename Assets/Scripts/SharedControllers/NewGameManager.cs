using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Generates a new game
/// </summary>
/// <remarks>
/// <para>
/// Handles all aspects of creating a new game, including initialization of 
/// non MDS data objects in SDS, and the generation of new game data based on
/// MDS data and data rules
/// </para>
/// <para>
/// #######################################################################
/// EVENTS:
/// Handler: SaveGameEventHandler
///     Handles: GSM.SaveGameEvent, for when the new game is saved
/// </para>
/// <para>
/// #######################################################################
/// BROADCAST/LISTENERS:
/// L: "SetupNewGame" - Transfers PilotInfo to GDS and sets up empty GSC
///     B: NewGamePanelManager.cs
/// </para>
/// </remarks>
public class NewGameManager : MonoBehaviour
{
    #region DECLARATIONS

    private GameDataSingleton gds;                          //Game data store
    private GameStateManager gsm;                           //Game state manager for loading/saving
    private UIManager uim;                                  //UI manager for showing/hiding UI elements

    #endregion

    #region EVENTS
    #endregion

    #region UNITY METHODS

    /// <summary>
    /// Unity AWAKE
    /// </summary>
    /// <remarks>
    /// <para>
    /// Set up the GDS. 
    /// </para>
    /// </remarks>
    void Awake()
    {
        gds = GameDataSingleton.Instance;
    }

    /// <summary>
    /// Unity START
    /// </summary>
    /// <remarks>
    /// <para>
    /// Sets up GDS initialization, gets reference to UIController and it's UIManager component,
    /// SceneController and it's GameStateManager component. Also, adds handler for GSM.SaveGameEvent
    /// and a broadcast listener for "SetupNewGame".
    /// </para>
    /// </remarks>
    void Start()
    {
        GameObject sceneController = gameObject;
        GameObject uic = GameObject.Find("UIController");
        gsm = sceneController.GetComponent<GameStateManager>();
        gsm.SaveGameEvent += SaveGameEventHandler;

        uim = uic.GetComponent<UIManager>();
    }

    #endregion

    #region PROPERTIES
    #endregion

    #region CONSTRUCTORS
    #endregion

    #region PUBLIC METHODS

    /// <summary>
    /// Generates and saves a new game
    /// </summary>
    public void NewGame()
    {
        //If something goes wrong, we need to re-show the UI and show a message.
        GenerateCommodityInventory();

        gsm.SaveGame();
    }

    public void NewGame(PlayerInfo NewPlayerInfo)
    {
        gds.SDS.PlayerInfo.Name = NewPlayerInfo.Name;
        gds.SDS.PlayerInfo.Atlas_Avatar = NewPlayerInfo.Atlas_Avatar;

        gds.SDS.CurrentGameState.ID = 1;                                //Unused
        gds.SDS.CurrentGameState.GU_ID = Guid.NewGuid().ToString();
        gds.SDS.CurrentGameState.Name = "New Game";
        gds.SDS.CurrentGameState.Last_Load_Date = DateTime.Now;
        gds.SDS.CurrentGameState.Last_Save_Date = DateTime.Now;

        gds.GSC.Game_States.Add(gds.SDS.CurrentGameState);

        gds.Current_Game_ID = gds.SDS.CurrentGameState.GU_ID;

        NewGame();
    }

    #endregion

    #region PRIVATE METHODS

    /// <summary>
    /// Generates commodity inventory distribution throughout the entire game
    /// </summary>
    /// <remarks>
    /// <para>
    /// Inventory is distributed by dividing the capacity of a container by the number
    /// of different items the container will hold. Factor in the RARITY property of the 
    /// item to create the base quantity in that container for that item. 
    /// </para>
    /// <para>
    /// There is a 50% chance the item will be BUY or SELL. No item will ever be both at
    /// the same station. 
    /// </para>
    /// <para>
    /// Once the BUY/SELL and initial quantity are generated, a new instance of InventoryItem
    /// is generated with reference to the current container (Inventory object from MDS) and
    /// is added to a List of InventoryItem.
    /// </para>
    /// <para>
    /// Once all items have been assigned to the current container, we loop through all items in 
    /// that container. In this loop, random items in the container will have random amounts of 
    /// inventory removed, and added to the current loop item. This ensures that we respect the 
    /// capacity of the container, but achive a random dstribution of quantity among items in that
    /// container. At the end of this process, the InventoryItems for the current container are
    /// added to the GDS.SDS.InventoryItems collection.
    /// </para>
    /// <para>
    /// The final step is to set the GDS.GlobalCommodityCapacity to the total of all container capacity
    /// in the game. This GDS property is used in the calculation of prices at all stations. 
    /// </para>
    /// </remarks>
    private bool GenerateCommodityInventory()
    {

        bool success = true;

        //We need this outside counter
        // to generate the ID of the InventoryItem
        //  when created.
        int iiID = 1;

        int globalContainerCapacity = 0;

        //Starting at the INVENTORY level, get all the inventories that are marked as COMMODITY
        List<Inventory> commodityContainers = gds.SDS.FindByInventoryType(Enums.Entity_Type.Commodity);

        //We need one InventoryItem object for each item we're going to add. 
        //We ONLY want ItemGroup COMMODITY item from the MDSItems group.
        List<Item> commodityItems = gds.SDS.FindByItemGroup("Commodity");

        foreach (Inventory i in commodityContainers)
        {
            //Get the container capacity of the current Inventory object
            int containerCapacity = int.Parse(gds.SDS.GetItemMetaValue(i.Container_ID, "MaxCapacity"));
            globalContainerCapacity += containerCapacity;

            //Loop through Items and set up an InventoryItem for each one
            //List<> holds the II while we're working on them
            List<InventoryItem> _inventoryItems = new List<InventoryItem>();

            foreach (Item itm in commodityItems)
            {
                int eqDivision = (int)(containerCapacity / commodityItems.Count());
                int itemShare = (int)(eqDivision - (float)(itm.Rarity / 10.0f));

                //Buy/Sell Change
                int buySellChance = GetRandomInt(0, 50);

                InventoryItem ii = new InventoryItem(iiID, i.ID, itm.ID, itemShare, buySellChance > 25 ? "BUY" : "SELL");

                _inventoryItems.Add(ii);

                iiID++;
            }

            //All items have InventoryItem objects for this Inventory. 
            foreach (InventoryItem ii in _inventoryItems)
            {
                int rndAmount = GetRandomInt(1, ii.Quantity);
                int rndItem = GetRandomInt(0, _inventoryItems.Count() - 1);

                _inventoryItems.ElementAt(rndItem).Quantity += rndAmount;
                ii.Quantity -= rndAmount;
            }

            gds.SDS.InventoryItems.AddRange(_inventoryItems);

        }

        gds.GlobalCommodityCapacity = globalContainerCapacity;

        return success;

    }

    /// <summary>
    /// Generates a random number between MIN and MAX
    /// </summary>
    /// <param name="min">Int</param>
    /// <param name="max"><Int/param>
    /// <returns>Int</returns>
    protected int GetRandomInt(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    #endregion

    #region EVENT DELEGATES
    #endregion

    #region EVENT HANDLERS & LISTENERS

    private void SaveGameEventHandler(ReturnObject RO)
    {
        //At this point, the new game has been generated and saved to disk. 
        //Hide the Loading UI and send the user to wherever they need to be.
        uim.HideUI(UIManager.UIELEMENTS.LoadingUI);

        //Have scene transfer live on SceneController. 
        //SceneTransfer st = sceneController.GetComponent<SceneTransfer>();
        //st.ChangeScene("[INITIAL SCENE AFTER NEW GAME]");
        Debug.Log("A-OK!");
    }
    

    #endregion
}

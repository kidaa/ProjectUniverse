using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Handles the activity involving the load game panel
/// </summary>
/// <remarks>
/// <para>
/// The Load Game panel displays a list of saved games, allowing the player to click on 
/// one to select it, and then the LOAD button to load the game, DELETE button to 
/// delete the saved game, or CANCEL to leave the UI without doing anything. 
/// </para>
/// <para>
/// #######################################################################
/// EVENTS:
/// Handler: LoadGameEventHandler - Notifies when the loading has completed
///     Handles: GameStateManager.LoadGameEvent
/// </para>
/// <para>
/// #######################################################################
/// BROADCAST/LISTENERS:
/// NONE
/// </para>
/// </remarks>
public class LoadGamePanelManager : MonoBehaviour
{
    #region DECLARATIONS

    public GameObject saveGameListing;                          //Scrollbox which will contain the list items
    public GameObject saveGameListingItem;                      //Save Game list item prefab
    
    private GameDataSingleton gds;                              //Global data store
    private GameStateManager gsm;                               //Handles saving and loading
    private UIManager uim;                                      //Handles UI hiding and showing
    private string selectedSaveGameGUID;                        //Receives the GS.GUID of the list item that was clicked
    private bool hasLoadedList;                                 //Used in Update to get the save game list from GDS
    private List<GameObject> listChildren;                      //For clearing out when the panel is closed. 

    #endregion

    #region EVENTS

    #endregion

    #region UNITY METHODS

    /// <summary>
    /// Unity AWAKE
    /// </summary>
    /// <remarks>
    /// <para>
    /// Instances the GDS, and adds a listener for the PopulateSaveGameList announcement.
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
    /// Gets referneces to the MainMenuController, and the UIController, and the
    /// components GameStateManager and UIManager respectively. Also, adds an
    /// event handler to the GSM.LoadGameEvent.
    /// </para>
    /// </remarks>
    void Start()
    {
        GameObject mmc = GameObject.Find("MainMenuController");
        GameObject uic = GameObject.Find("UIController");
        gsm = mmc.GetComponent<GameStateManager>();
        uim = uic.GetComponent<UIManager>();

        gsm.LoadGameEvent += LoadGameEventHandler;

    }

    void OnEnable()
    {
        listChildren = new List<GameObject>();

        PopulateSaveGameListListener();
    }

    void OnDisable()
    {
        ClearSaveGameList();
    }

    #endregion

    #region PROPERTIES
    #endregion

    #region CONSTRUCTORS
    #endregion

    #region PUBLIC METHODS

    #endregion

    #region PRIVATE METHODS

    /// <summary>
    /// Populates the SaveGameList
    /// </summary>
    /// <remarks>
    /// <para>
    /// The GSC is loaded from disk (if present) when the game loads. Once that has completed successfully, 
    /// a broadcast message is sent and is picked up by this component, which calls this method. 
    /// </para>
    /// <para>
    /// Looping through the Game_State objects in GDS.GSC, we create instances of the saveGameListingItem, assigning
    /// values from the Game_State object to the controls in the instance panel. We also add an event handler to deal
    /// with the CLICK event of the panel. 
    /// </para>
    /// <para>
    /// Finally, we make the instance visible, and set it's parent to that of the scrollbox. 
    /// </para>
    /// </remarks>
    private void PopulateSaveGameList()
    {
        foreach (GameState gs in gds.GSC.Game_States)
        {
            GameObject saveGameItem = (GameObject)Instantiate(saveGameListingItem, Vector3.zero, Quaternion.identity);
            

            //Set the properties on the main script of the save game object.
			dfPanel saveGameItemPanel = saveGameItem.GetComponent<dfPanel>();
            LoadGameItemPanelManager lipm = saveGameItem.GetComponent<LoadGameItemPanelManager>();
            lipm.SaveGameSelectEvent += SaveGameSelectEventHandler;     //Handles the collection of the GUID

            lipm.saveGameGUID = gs.GU_ID;
            lipm.saveGameName.Text = gs.Name;
            lipm.saveGameDate.Text = gs.Last_Save_Date.ToString();

			saveGameItemPanel.IsVisible = true;
            saveGameItem.transform.parent = saveGameListing.transform;

            listChildren.Add(saveGameItem);
        }
    }

    private void ClearSaveGameList()
    {
        foreach (GameObject go in listChildren)
            Destroy(go);
    }

    #endregion

    #region EVENT DELEGATES
    #endregion

    #region EVENT HANDLERS & LISTENERS

    /// <summary>
    /// The delegate for the Messenger Listener, which triggers the population of the scrollbox.
    /// </summary>
    private void PopulateSaveGameListListener()
    {
        PopulateSaveGameList();
    }

    /// <summary>
    /// Starts the process of loading a save game
    /// </summary>
    /// <param name="s">Button</param>
    /// <param name="e">Mouse Events</param>
    /// <remarks>
    /// <para>
    /// Assigns the previously determined Game State GUID to the GDS, hides this UI and
    /// shows the LOADING UI, and then calls LoadGame on the GameStateManager.
    /// </para>
    /// </remarks>
    public void UI_Button_LoadGame_Click(dfControl s, dfMouseEventArgs e)
    {
        gds.Current_Game_ID = selectedSaveGameGUID;

        uim.HideUI(UIManager.UIELEMENTS.LoadGameUI);
        uim.ShowUI(UIManager.UIELEMENTS.LoadingUI);

        gsm.LoadGame();

    }

    public void UI_Button_DeleteGame_Click(dfControl s, dfMouseEventArgs e)
    {
    }

    public void UI_Button_CancelLoad_Click(dfControl s, dfMouseEventArgs e)
    {
        uim.HideUI(UIManager.UIELEMENTS.LoadGameUI);
        uim.ShowUI(UIManager.UIELEMENTS.MainMenu);
    }

    /// <summary>
    /// Accepts the click event from the instance of the SaveGameListingItem, 
    /// in the form of a ReturnObject instance
    /// </summary>
    /// <param name="RO">ReturnObject</param>
    /// <remarks>
    /// <para>
    /// When a list item is clicked, it passes us a ReturnObject which contains
    /// its GUID (which is also the name of the save game file). We take that RO
    /// and store the Return_Object value (GUID) in a local variable.
    /// </para>
    /// <para>
    /// We don't "cannonize" the GUID until the player clicks the LOAD GAME button,
    /// so this handler can be called multiple times, every time the user clicks
    /// a save game entry.
    /// </para>
    /// </remarks>
    private void SaveGameSelectEventHandler(ReturnObject RO)
    {
        selectedSaveGameGUID = RO.Return_Object.ToString();
    }

    /// <summary>
    /// The final step in loading a game; hides the UI and transfers the user to the proper scene.
    /// </summary>
    /// <param name="RO">ReturnObject</param>
    private void LoadGameEventHandler(ReturnObject RO)
    {
        uim.HideUI(UIManager.UIELEMENTS.LoadingUI);

        //Have scene transfer live on SceneController. 
        //SceneTransfer st = sceneController.GetComponent<SceneTransfer>();
        //st.ChangeScene("[INITIAL SCENE AFTER NEW GAME]");
        Debug.Log("A-OK!");
    }

    #endregion

}

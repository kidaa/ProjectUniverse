using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Manages activities involving user input when creating a new game
/// </summary>
/// <remarks>
/// <para>
/// A new game setup requires that the player enter a PILOT NAME and select
/// an PILOT AVATAR used in conversation display.
/// </para>
/// <para>
/// Mostly this exists to handle buttons, but also to collect the data from
/// the form, to create a new PilotInfo object, and to start the New Game
/// process.
/// </para>
/// <para>
/// #######################################################################
/// EVENTS:
/// NONE
/// </para>
/// <para>
/// #######################################################################
/// BROADCAST/LISTENERS:
/// NONE
/// </para>
/// </remarks>
public class NewGamePanelManager : MonoBehaviour
{
    #region DECLARATIONS

    public dfTextbox txtName;                           //Player's chosen name
    public dfTextbox txtAvatar;                         //Player's chosen avatar from the avatar atlas

    private GameDataSingleton gds;                      //Game data store
    private NewGameManager ngm;                         //New game manager for creating the new game
    private UIManager uim;                              //UIManager for showing/hiding UI elements

    #endregion

    #region EVENTS

    #endregion

    #region UNITY METHODS

    /// <summary>
    /// Unity AWAKE
    /// </summary>
    /// <remarks>
    /// <para>
    /// Initialize GDS
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
    /// Set up references to MainMenuController (MMC) and UIController (UIC), and get
    /// the NewGameManager (MMC) and UIManager (UIC) components.
    /// </para>
    /// </remarks>
    void Start()
    {
        GameObject mmc = GameObject.Find("MainMenuController");
        GameObject uic = GameObject.Find("UIController");
        ngm = mmc.GetComponent<NewGameManager>();
        uim = uic.GetComponent<UIManager>();
    }
    
    #endregion

    #region PROPERTIES
    #endregion

    #region CONSTRUCTORS
    #endregion

    #region PUBLIC METHODS
    #endregion

    #region PRIVATE METHODS
    #endregion

    #region EVENT DELEGATES
    #endregion

    #region EVENT HANDLERS

    /// <summary>
    /// Initiates the actual start of a new game
    /// </summary>
    /// <param name="s">Button</param>
    /// <param name="e">Mouse Events</param>
    /// <remarks>
    /// <para>
    /// Create a "transport" PlayerInfo object and store the user's 
    /// Pilot Name and Pilot Avatar. 
    /// </para>
    /// <para>
    /// The method hides the NewGameUI and calls the NewGame method on
    /// the NewGameManager which generates the non MDS data elements for SDS.
    /// </para>
    /// </remarks>
    public void UI_Button_StartNewGame_Click(dfControl s, dfMouseEventArgs e)
    {
        PlayerInfo newPlayerInfo = new PlayerInfo(1, txtName.Text, "Pilot01");

        uim.HideUI(UIManager.UIELEMENTS.NewGameUI);
        uim.ShowUI(UIManager.UIELEMENTS.LoadingUI);
        
        ngm.NewGame(newPlayerInfo);
    }

    /// <summary>
    /// Cancels use of the New Game UI
    /// </summary>
    /// <param name="s">Button</param>
    /// <param name="e">Mouse Events</param>
    /// <remarks>
    /// <para>
    /// Closes the new game panel without doing anything
    /// </para>
    /// </remarks>
    public void UI_Button_CancelNewGame_Click(dfControl s, dfMouseEventArgs e)
    {
        txtName.Text = "";

        uim.HideUI(UIManager.UIELEMENTS.NewGameUI);
        uim.ShowUI(UIManager.UIELEMENTS.MainMenu);
    }

    #endregion

}

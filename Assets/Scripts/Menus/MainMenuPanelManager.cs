using UnityEngine;
using System.Collections;

/// <summary>
/// Handles activity for the Main Menu UI element
/// </summary>
/// <remarks>
/// <para>The main menu doesn't DO much except display buttons, so this script
/// only handles the button presses.</para>
/// <para>
/// #######################################################################
/// EVENTS:
/// None
/// </para>
/// <para>
/// #######################################################################
/// BROADCAST/LISTENERS:
/// None
/// </para>
/// </remarks>
public class MainMenuPanelManager : MonoBehaviour
{
    #region DECLARATIONS

    private UIManager uim;                          //UI Manager, for showing and hiding UI

    #endregion

    #region EVENTS

    #endregion

    #region UNITY METHODS

    /// <summary>
    /// Second Method
    /// </summary>
    /// <remarks>
    /// <para>
    /// Get a reference to the UIManager component on the UIController GameObject. 
    /// UIManager allows us to show and hide other UI elements that are referenced
    /// on the manager game object.
    /// </para>
    /// </remarks>
    void Start()
    {
        GameObject uiManager = GameObject.Find("UIController");
        uim = uiManager.GetComponent<UIManager>();
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
    /// Starts a new game
    /// </summary>
    /// <param name="s">Button</param>
    /// <param name="e">Mouse events</param>
    /// <remarks>
    /// <para>
    /// Only hides the main menu and displays the New Game UI
    /// </para>
    /// </remarks>
    public void UI_Button_NewGame_Click(dfControl s, dfMouseEventArgs e)
    {
        //Show the New Game UI
        uim.HideUI(UIManager.UIELEMENTS.MainMenu);
        uim.ShowUI(UIManager.UIELEMENTS.NewGameUI);


    }

    /// <summary>
    /// Loads a previous game
    /// </summary>
    /// <param name="s">Button</param>
    /// <param name="e">Mouse events</param>
    /// <remarks>
    /// <para>
    /// Only hides the main menu and displays Load Game UI
    /// </para>
    /// </remarks>
    public void UI_Button_LoadGame_Click(dfControl s, dfMouseEventArgs e)
    {
        //Show the New Game UI
        uim.HideUI(UIManager.UIELEMENTS.MainMenu);
        uim.ShowUI(UIManager.UIELEMENTS.LoadGameUI);
    }

    #endregion

}

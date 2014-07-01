using UnityEngine;
using System.Collections;

/// <summary>
/// Hides and displays the "Press 'F' to Dock" prompt when needed
/// </summary>
/// <remarks>
/// <para>
/// This script handles the display of the docking prompt. It's kicked off by 
/// a Playmaker event, and Playmaker will also handle listening for the 'F' 
/// key press. 
/// </para>
/// <para>
/// Normally the definition of the UIM reference would be class-level and assigned
/// in the Start() method, but because this script is on an object that isn't activated
/// when the script is called, Start() isn't called. Definition of UIM has been moved
/// into the Show and Hide methods and is instantiated when Playmaker makes the call.
/// </para>
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
public class DockingPromptPanelManager : MonoBehaviour {
    #region DECLARATIONS

    #endregion

    #region EVENTS

    #endregion

    #region UNITY METHODS

    void Start()
    {
        
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
    /// Display the docking prompt panel
    /// </summary>
    /// <remarks>
    /// Called by Playmaker.
    /// </remarks>
    public void ShowDockingPanel()
    {
        GameObject uiManager = GameObject.Find("UIController");
        UIManager uim = uiManager.GetComponent<UIManager>();
        uim.ShowUI(UIManager.UIELEMENTS.DockingPrompt);
    }

    /// <summary>
    /// Hide the docking prompt panel
    /// </summary>
    /// <remarks>
    /// Called by Playmaker.
    /// </remarks>
    public void HideDockingPanel()
    {
        GameObject uiManager = GameObject.Find("UIController");
        UIManager uim = uiManager.GetComponent<UIManager>();
        uim.HideUI(UIManager.UIELEMENTS.DockingPrompt);
    }

    #endregion
}

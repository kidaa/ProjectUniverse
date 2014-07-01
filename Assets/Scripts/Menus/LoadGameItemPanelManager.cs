using UnityEngine;
using System.Collections;

/// <summary>
/// Activity manager for Save Game Listing Game Objects
/// </summary>
/// <remarks>
/// <para>
/// There are only two functions here: accept values to display from the script
/// that instantiated this prefab, and handle the click of the instanced item
/// </para>
/// <para>
/// #######################################################################
/// EVENTS:
/// SaveGameSelectEvent - Raised when the game object is clicked.
/// </para>
/// <para>
/// #######################################################################
/// BROADCAST/LISTENERS:
/// NONE
/// </para>
/// </remarks>
public class LoadGameItemPanelManager : MonoBehaviour {


    public dfLabel saveGameName;                            //NAME property of the Game State
    public dfLabel saveGameDate;                            //LAST DAVE DATE property of the Game State
    public string saveGameGUID;                             //GUID of the Game State


    public event EventReturnObject SaveGameSelectEvent;     //Raised when the instanced Game Object is clicked.

    /// <summary>
    /// Unity START
    /// </summary>
    /// <remarks>
    /// <para>
    /// Gets a reference to this gameObject's panel, 
    /// and adds an event handler to it's Click event.
    /// </para>
    /// </remarks>
    void Start()
    {
        dfPanel gameItemPanel = gameObject.GetComponent<dfPanel>();
        gameItemPanel.Click += LoadGameItemPanel_Click;
    }


    /// <summary>
    /// Handles the Click event of the entire Game Object
    /// </summary>
    /// <param name="s">Panel</param>
    /// <param name="e">Mouse Events</param>
    /// <remarks>
    /// <para>
    /// Clicking the panel constructs a ReturnObject which is raised to the 
    /// parent (LoadGamePanelManager, in this case) and carries the GameState.GUID
    /// in order to identify which item in the list was clicked.
    /// </para>
    /// </remarks>
    public void LoadGameItemPanel_Click(dfControl s, dfMouseEventArgs e)
    {
        ReturnObject ro = new ReturnObject(Enums.Return_Status.OK, "Selected "+ saveGameGUID, "Selected " + saveGameGUID, saveGameGUID);
        OnSaveGameSelectEvent(ro);
    }

    /// <summary>
    /// Raises the SaveGameSelectEvent event
    /// </summary>
    /// <param name="RO">ReturnObject</param>
    private void OnSaveGameSelectEvent(ReturnObject RO)
    {
        if (SaveGameSelectEvent != null)
        {
            SaveGameSelectEvent(RO);
        }
    }

    /// <summary>
    /// Delegate for the event used to pass a ReturnObject
    /// </summary>
    /// <param name="RO">ReturnObject</param>
    public delegate void EventReturnObject(ReturnObject RO);

}

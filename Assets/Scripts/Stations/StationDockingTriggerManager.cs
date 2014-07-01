using UnityEngine;
using System.Collections;

/// <summary>
/// Listens for objects entering or leaving the trigger sphere
/// Takes action based on entering or leaving
/// </summary>
public class StationDockingTriggerManager : MonoBehaviour
{

    #region DECLARATIONS

    UIManager uim;
    
    #endregion

    #region UNITY METHODS
    
    void Awake()
    { }

    void Start()
    {
        GameObject uic = GameObject.Find("UIController");
        uim = uic.GetComponent<UIManager>();
    }

    /// <summary>
    /// Object is entering the docking sphere
    /// </summary>
    /// <param name="other">Collider: Collider belonging to the GO entering the sphere.</param>
    /// <remarks>
    /// <para>
    /// Right now, the focus is on having the trigger display the docking prompt to the player. 
    /// Later, however, this should also allow for NPCs to be affected by entering the docking sphere
    /// to "vanish" as if they were entering the station.
    /// </para>
    /// </remarks>
    void OnTriggerEnter(Collider other)
    {
        GameObject movingObject = other.gameObject;
        
        if (movingObject.tag == "Player")
        {
            uim.ShowUI(UIManager.UIELEMENTS.DockingPrompt);
        }
    }

    /// <summary>
    /// Object is exiting the docking sphere
    /// </summary>
    /// <param name="other">Collider: Collider belonging to the GO exiting the sphere.</param>
    /// <remarks>
    /// <para>
    /// Right now, the focus is on hiding the docking prompt from the player when exiting. 
    /// Later, however, it should also be used to show NPCs who are leaving the station. 
    /// I suppose that would mean that the NPC models would be invisible, but still perform 
    /// actions, and when leaving, should be made visible again. I don't know right now.
    /// </para>
    /// </remarks>
    void OnTriggerExit(Collider other)
    {
        GameObject movingObject = other.gameObject;

        if (movingObject.tag == "Player")
        {
            uim.HideUI(UIManager.UIELEMENTS.DockingPrompt);
        }
    }

    /// <summary>
    /// Object is inside the trigger. 
    /// </summary>
    /// <param name="other">Collider: Collider belonging to the GO that's inside the sphere </param>
    /// <remarks>
    /// <para>
    /// For players, this listens for the "F" key. If the key is pressed, the user is "docked" and the 
    /// docking prompt is hidden. We set a bit to indicate that they're docked so we can free up the "F" 
    /// key for something else. This will get set to FALSE when they close the station UI (but are still
    /// inside the trigger sphere).
    /// </para>
    /// <para>
    /// For NPCs this may be a way to keep them invisible, or to have them do something while they're
    /// sitting around.
    /// </para>
    /// </remarks>
    void OnTriggerStay(Collider other)
    {
        GameObject movingObject = other.gameObject;

        if (movingObject.tag == "Player")
        {
            //Listen for the "F" key. If heard, then dock, if not already. 
            bool isDocked = movingObject.GetComponent<PlayerVariables>().isDocked;
            if (Input.GetKeyDown(KeyCode.F) && isDocked != true)
            {
                print("You are docking!");
                uim.HideUI(UIManager.UIELEMENTS.DockingPrompt);
                uim.ShowUI(UIManager.UIELEMENTS.StationMenu);
                movingObject.GetComponent<PlayerVariables>().isDocked = true;
            }
        }
    }

    #endregion

    #region PUBLIC METHODS
    
    #endregion

    #region PRIVATE METHODS

    #endregion

}

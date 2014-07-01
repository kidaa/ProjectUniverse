using UnityEngine;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Handles the display of UI panels associated with the hosting GO.
/// </summary>
/// <remarks>
/// #########################################################################
/// <para>
/// This script is the "public face" of UI access from other code elements. 
/// When a UI needs to be displayed or hidden, this script can be called
/// from any game object that creates a reference to it's host UICONTROLLER
/// object. 
/// </para>
/// <para>
/// Elements are displayed on-demand, but elements can be hidden on-demand,
/// in reverse order of opening, or all at once. 
/// </para>
/// <para>
/// This script should be hosted on a UICONTROLLER game object. All UI elements
/// SHOULD be hosted as children of the controller, and the controller made to
/// be a prefab, but they can be hosted anywhere. 
/// The GO should be assigned to the public GO properties of this script. 
/// All of the GO's to be assigned should be of dfPanel type. 
/// Users of this script should reference:
/// 
/// UIManager uimgr = FindGameObject("UIController").GetComponent<UIManager>();
/// 
/// </para>
/// #########################################################################
/// </remarks>
public class UIManager : MonoBehaviour
{
    #region DECLARATIONS

    //PUBLIC DEFINITIONS
    public int sectorID;
    public string sectorName;
    public string sectorDescription;

    //Add any UI elements to this list.
    public GameObject mainMenu;
    public GameObject mainUI;
    public GameObject newGameUI;
    public GameObject loadGameUI;
    public GameObject dockingPrompt;
    public GameObject stationMenu;
    public GameObject shopMenu;
    public GameObject shipyardMenu;
    public GameObject barMenu;
    public GameObject jumpgateTollMenu;
    public GameObject conversationUI;
    public GameObject loadingUI;

    //Add any UI elements to this enum
    public enum UIELEMENTS
    {
        MainMenu,
        MainUI,
        NewGameUI,
        LoadGameUI,
        DockingPrompt,
        StationMenu,
        ShopMenu,
        ShipyardMenu,
        BarMenu,
        JumpgateTollMenu,
        ConversationUI,
        LoadingUI
    }

    public enum HIDELEVEL
    {
        All,
        AllPopup,
        Control,
        QueueOrder,
        ReverseQueueOrder
    }

    //PRIVATE DEFINITIONS
    List<GameObject> _windowqueue;

    #endregion

    #region UNITY METHODS

    /// <summary>
    /// Initilize variables
    /// </summary>
    void Start()
    {
        _windowqueue = new List<GameObject>();

    }

    #endregion

    #region PROPERTIES
    #endregion

    #region CONSTRUCTORS
    #endregion

    #region PUBLIC METHODS

    /// <summary>
    /// Display the element identified by the ENUM
    /// </summary>
    /// <param name="UIElement">UIELEMENTS: An enum value</param>
    public void ShowUI(UIELEMENTS UIElement)
    {
        ShowUIIndividual(UIElement);
    }

    /// <summary>
    /// Hides UI elements one by one, or all at once
    /// </summary>
    /// <param name="StepSingle">Bool: TRUE to hide one 
    /// element at a time. FALSE for all at once</param>
    /// <remarks>
    /// The order in which the elements are hidden is 
    /// determined by their order in _windowqueue, with the 
    /// last in being the first out (LIFO)
    /// </remarks>
    public void HideUI(bool StepSingle)
    {
        HideUIElement(StepSingle);
    }
    
    /// <summary>
    /// Hide a specific UI element
    /// </summary>
    /// <param name="UIElement">UIELEMENTS: Enum representing the desired element</param>
    public void HideUI(UIELEMENTS UIElement)
    {
        HideUIElement(UIElement);
    }

    #endregion

    #region PRIVATE METHODS

    /// <summary>
    /// Display a specific UI element
    /// </summary>
    /// <param name="UIElement">UIELEMENTS: Enum representing the desired element</param>
    /// <remarks>
    /// Check the queue for the element, and if it's not there, add it. Then set the panel's 
    /// visibility to TRUE.
    /// </remarks>
    private void ShowUIIndividual(UIELEMENTS UIElement)
    {
        switch (UIElement)
        {
            case UIELEMENTS.MainMenu:
                if (!CheckQueueForElement(mainMenu)) { AddElementToQueue(mainMenu); }
                mainMenu.SetActive(true);
                break;
            case UIELEMENTS.MainUI:
                //if (!CheckQueueForElement(mainUI)) { AddElementToQueue(mainUI); }
                mainUI.SetActive(true);
                break;
            case UIELEMENTS.NewGameUI:
                if (!CheckQueueForElement(newGameUI)) { AddElementToQueue(newGameUI); }
                newGameUI.SetActive(true);
                break;
            case UIELEMENTS.LoadGameUI:
                if (!CheckQueueForElement(loadGameUI)) { AddElementToQueue(loadGameUI); }
                loadGameUI.SetActive(true);
                break;
            case UIELEMENTS.DockingPrompt:
                if (!CheckQueueForElement(dockingPrompt)) { AddElementToQueue(dockingPrompt); }
                dockingPrompt.SetActive(true);
                break;
            case UIELEMENTS.StationMenu:
                if (!CheckQueueForElement(stationMenu)) { AddElementToQueue(stationMenu); }
                stationMenu.SetActive(true);
                break;
            case UIELEMENTS.ShopMenu:
                if (!CheckQueueForElement(shopMenu)) { AddElementToQueue(shopMenu); }
                shopMenu.SetActive(true);
                break;
            case UIELEMENTS.ShipyardMenu:
                if (!CheckQueueForElement(shipyardMenu)) { AddElementToQueue(shipyardMenu); }
                shipyardMenu.SetActive(true);
                break;
            case UIELEMENTS.BarMenu:
                if (!CheckQueueForElement(barMenu)) { AddElementToQueue(barMenu); }
                barMenu.SetActive(true);
                break;
            case UIELEMENTS.JumpgateTollMenu:
                //if (!CheckQueueForElement(jumpgateTollMenu)) { AddElementToQueue(jumpgateTollMenu); }
                jumpgateTollMenu.SetActive(true);
                break;
            case UIELEMENTS.ConversationUI:
                //if (!CheckQueueForElement(conversationUI)) { AddElementToQueue(conversationUI); }
                conversationUI.SetActive(true);
                break;
            case UIELEMENTS.LoadingUI:
                //if (!CheckQueueForElement(conversationUI)) { AddElementToQueue(conversationUI); }
                loadingUI.SetActive(true);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Hide UI elements that are in the _windowqueue
    /// </summary>
    /// <param name="StepSingle">
    /// Bool: TRUE if we only close the last window opened. 
    /// FALSE if we close all windows in the queue.
    /// </param>
    private void HideUIElement(bool StepSingle)
    {
        if (StepSingle == true)
        {
            //Remove the highest index item
            var uie = (from _uie in _windowqueue
                       select _uie).Last();
            RemoveElementFromQueue(uie);
            //uie.GetComponent<dfPanel>().IsVisible = false;
            uie.SetActive(false);
        }
        else
        {
            //Close all at once
            foreach (GameObject go in _windowqueue)
            {
				Debug.Log(go.name);
                //go.GetComponent<dfPanel>().IsVisible = false;
                go.SetActive(false);
            }
            _windowqueue = new List<GameObject>();
        }
    }

    /// <summary>
    /// Hide a specific UI Element
    /// </summary>
    /// <param name="UIElement">UIELEMENTS: Enum representing the desired element</param>
    /// <remarks>
    /// Remove the element from the queue, then hide it. 
    /// </remarks>
    private void HideUIElement(UIELEMENTS UIElement)
    {
        switch (UIElement)
        {
            case UIELEMENTS.MainMenu:
                RemoveElementFromQueue(mainMenu);
                mainMenu.SetActive(false);
                break;
            case UIELEMENTS.MainUI:
                break;
            case UIELEMENTS.NewGameUI:
                RemoveElementFromQueue(newGameUI);
                newGameUI.SetActive(false);
                break;
            case UIELEMENTS.LoadGameUI:
                RemoveElementFromQueue(loadGameUI);
                loadGameUI.SetActive(false);
                break;
            case UIELEMENTS.DockingPrompt:
                RemoveElementFromQueue(dockingPrompt);
                dockingPrompt.SetActive(false);
                break;
            case UIELEMENTS.StationMenu:
                RemoveElementFromQueue(stationMenu);
                stationMenu.SetActive(false);
                break;
            case UIELEMENTS.ShopMenu:
                RemoveElementFromQueue(shopMenu);
                shopMenu.SetActive(false);
                break;
            case UIELEMENTS.ShipyardMenu:
                RemoveElementFromQueue(shipyardMenu);
                shipyardMenu.SetActive(false);
                break;
            case UIELEMENTS.BarMenu:
                RemoveElementFromQueue(barMenu);
                barMenu.SetActive(false);
                break;
            case UIELEMENTS.JumpgateTollMenu:
                RemoveElementFromQueue(jumpgateTollMenu);
                jumpgateTollMenu.SetActive(false);
                break;
            case UIELEMENTS.ConversationUI:
                RemoveElementFromQueue(conversationUI);
                conversationUI.SetActive(false);
                break;
            case UIELEMENTS.LoadingUI:
                RemoveElementFromQueue(loadingUI);
                loadingUI.SetActive(false);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Check the queue for the presence of the given GameObject
    /// </summary>
    /// <param name="UIElement">GameObject: Object to find</param>
    /// <returns>Bool: TRUE if the GO is in the collection.</returns>
    bool CheckQueueForElement(GameObject UIElement)
    {
        bool inQueue = false;
        if (_windowqueue.Contains(UIElement))
        {
            inQueue = true;
        }

        return inQueue;        
    }

    /// <summary>
    /// Adds a GameObject to the queue
    /// </summary>
    /// <param name="UIElement">GameObject: Element to add</param>
    void AddElementToQueue(GameObject UIElement)
    {
        _windowqueue.Add(UIElement);
    }

    /// <summary>
    /// Removes an element from the queue
    /// </summary>
    /// <param name="UIElement">GameObject: The specific game object to remove</param>
    /// <remarks>
    /// Using LINQ, the method essentially copies the List<> of everything 
    /// in the queue EXCEPT the element specified.
    /// </remarks>
    void RemoveElementFromQueue(GameObject UIElement)
    {
        if (_windowqueue.Count > 0)
        {
            var keep = from _wq in _windowqueue
                       where _wq != UIElement
                       select _wq;
            _windowqueue = keep.ToList();
        }
    }

    #endregion

    #region EVENT HANDLERS
    #endregion

}

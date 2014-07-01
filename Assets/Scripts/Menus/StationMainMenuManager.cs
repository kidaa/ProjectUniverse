using UnityEngine;
using System.Collections;

/// <summary>
/// Deals with interaction at the initial stages of the station menu
/// </summary>
public class StationMainMenuManager : MonoBehaviour
{

    #region DECLARATIONS
    
    //References to the button UI game objects
    public GameObject stationMenuButton1;
    public GameObject stationMenuButton2;
    public GameObject stationMenuButton3;
    public GameObject stationMenuButton4;
    public GameObject stationMenuButton5;

    //References to the dfButton Components on each button
    private dfButton smb1;
    private dfButton smb2;
    private dfButton smb3;
    private dfButton smb4;
    private dfButton smb5;

    private UIManager uim;

    #endregion

    #region DELEGATES
    
    #endregion

    #region UNITY METHODS

    void Awake()
    {
        smb1 = stationMenuButton1.GetComponent<dfButton>();
        smb2 = stationMenuButton2.GetComponent<dfButton>();
        smb3 = stationMenuButton3.GetComponent<dfButton>();
        smb4 = stationMenuButton4.GetComponent<dfButton>();
        smb5 = stationMenuButton5.GetComponent<dfButton>();

        smb1.Click += StationMenuButton1_Click;
        smb2.Click += StationMenuButton2_Click;
        smb3.Click += StationMenuButton3_Click;
        smb4.Click += StationMenuButton4_Click;
        smb5.Click += StationMenuButton5_Click;

        GameObject uic = GameObject.Find("UIController");
        uim = uic.GetComponent<UIManager>();
        
    }

    void Start()
    {
        
    }

    #endregion

    #region PUBLIC METHODS

    #endregion

    #region PRIVATE METHODS

    void StationMenuButton1_Click(dfControl s, dfMouseEventArgs e)
    {
        print("Market");
    }

    void StationMenuButton2_Click(dfControl s, dfMouseEventArgs e)
    {

    }

    void StationMenuButton3_Click(dfControl s, dfMouseEventArgs e)
    {

    }

    void StationMenuButton4_Click(dfControl s, dfMouseEventArgs e)
    {

    }

    void StationMenuButton5_Click(dfControl s, dfMouseEventArgs e)
    {
        //Picked up by PlayerVariables on the Player object
        Messenger.Broadcast("IsUndocking");

        uim.HideUI(UIManager.UIELEMENTS.StationMenu);
        uim.ShowUI(UIManager.UIELEMENTS.DockingPrompt);
    }

    #endregion
}

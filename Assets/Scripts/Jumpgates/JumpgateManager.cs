using UnityEngine;
using System.Collections;

public class JumpgateManager : MonoBehaviour
{

    #region DECLARATIONS

    GameDataSingleton gds;

    public int jumpgateID;

    public int sectorID;
    public string name;
    public string description;
    public int base_toll;
    public int faction_id;
    public int destination_sector_id;
    public string destination_sector;           //get from GDS lookup by ID
    public int destination_gate_id;


    #endregion

    #region UNTIY METHODS

    void Awake()
    {
        gds = GameDataSingleton.Instance;
    }

    void Start()
    {
        GetJumpgateDataFromGDS();
    }
    
    void OnTriggerEnter(Collider other)
    {
    }

    void OnTriggerExit(Collider other)
    {
    }

    void OnTriggerStay(Collider other)
    {
    }

    #endregion

    #region PRIVATE METHODS

    void GetJumpgateDataFromGDS()
    {
        Jumpgate jg = gds.SDS.FindJumpgateByID(jumpgateID);
        if (jg != null)
        {
            sectorID = jg.Sector_ID;
            name = jg.Name;
            description = jg.Description;
            base_toll = jg.Base_Toll;
            faction_id = jg.Faction_ID;
            destination_sector_id = jg.Destination_Sector_ID;
            destination_sector = gds.SDS.FindJumpgateByID(jg.Destination_Gate_ID).Name;
            destination_gate_id = jg.Destination_Gate_ID;
        }
    }

    void SaveVariablesToGDS()
    {
        //Mostly stuff on the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerVariables pv = player.GetComponent<PlayerVariables>();
            //gds.SDS.PlayerInfo.Current_Position = player.transform.position;
            //gds.SDS.PlayerInfo.Current_Rotation = player.transform.eulerAngles;
        }
    }

    #endregion

    #region PUBLIC METHODS

    public void SwitchScene(string Destination)
    {
        //Storage
        SaveVariablesToGDS();

        //Cleanup

        //Transfer
        Application.LoadLevel(Destination);
    }

    #endregion
}

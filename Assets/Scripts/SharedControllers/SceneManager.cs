using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{

    #region DECLARATIONS

    GameDataSingleton gds;

    #endregion

    #region UNITY METHODS

    void Awake()
    {
        gds = GameDataSingleton.Instance;
    }

    void Start()
    {

    }

    #endregion

    #region PRIVATE METHODS

    /// <summary>
    /// When a player enters a sector, he needs to be placed in relation to the jumpgate
    /// that the previous jumpgate is connected to, a certain distance, and facing a
    /// certain direction.
    /// </summary>
    void PlacePlayerOnEnter()
    {
        
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

    /// <summary>
    /// Switches between scenes
    /// </summary>
    /// <param name="Destination">String: Name of the registered destination scene</param>
    /// <remarks>
    /// <para>
    /// There's three parts to switching between scenes:
    /// Storage: Storing anything in this scene that isn't manually brought over.
    /// Cleanup: Cleaning up anything that's left lying around
    /// Transfer: Moving to the next scene
    /// </para>
    /// </remarks>
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

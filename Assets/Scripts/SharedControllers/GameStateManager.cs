using UnityEngine;
using System;
using System.IO;
using System.Collections;
using Newtonsoft.Json;

public class GameStateManager : MonoBehaviour
{
    #region DECLARATIONS

	public string gameStateFile = "SaveGames.save";

    private GameDataSingleton gds;
    private string gameSavePath;

    #endregion

    #region EVENTS

    public event EventReturnObject SaveGameEvent;
    public event EventReturnObject LoadGameEvent;

    #endregion

    #region UNITY METHODS

    void Awake()
    {
        gds = GameDataSingleton.Instance;
    }

    void Start()
    {
		gameSavePath = Application.persistentDataPath + "/SaveGames/";
    }

    #endregion

    #region PROPERTIES
    #endregion

    #region CONSTRUCTORS
    #endregion

    #region PUBLIC METHODS

    /// <summary>
    /// Loads a saved game
    /// </summary>
    public void LoadGame()
    {
        ReturnObject ro = ReadSDS();
        LoadGameEvent(ro);
    }
    
    /// <summary>
    /// Saves the current game
    /// </summary>
    public void SaveGame()
    {
        ReturnObject ro = WriteSDS();
        OnSaveGameEvent(ro);
    }

    /// <summary>
    /// Loads a listing of saved games
    /// </summary>
    public void LoadSaveGameListing()
    {
    }

    #endregion

    #region PRIVATE METHODS

    /// <summary>
    /// Saves SDS and GameStateCollection to disk
    /// </summary>
    /// <remarks>
    /// Before we get here, we'll need to have updated the status on PlayerInfo
    /// and GameState with dates, sectors, and positions and such.
    /// </remarks>
    private ReturnObject WriteSDS()
    {
        ReturnObject ro = new ReturnObject();

        try
        {
            // SDS
            using (StreamWriter file = File.CreateText(gameSavePath + gds.Current_Game_ID + ".save"))
            {
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    writer.Formatting = Formatting.Indented;
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(writer, gds.SDS);

                    ro.Return_Status = Enums.Return_Status.OK;
                    ro.Friendly_Message = "SDS has been written to disk";
                }
            }

            //GameState Collection Header File
            using (StreamWriter file = File.CreateText(gameSavePath + gameStateFile))
            {
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    writer.Formatting = Formatting.Indented;
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(writer, gds.GSC);

                    ro.Return_Status = Enums.Return_Status.OK;
                    ro.Friendly_Message += "  GSC has been written to disk";
                }
            }
        }
        catch (Exception ex)
        {
            //Something went wrong with the writing of the save game file and the save game header.
            ro.Return_Status = Enums.Return_Status.Error;
            ro.Friendly_Message="Unable to save the current game.";
            ro.Technical_Message = "Unable to save the current game: " + ex.Message;
            ro.Return_Object = null;
        }

        return ro;
    }

    private ReturnObject ReadSDS()
    {
        ReturnObject ro = new ReturnObject();

        try
        {
            if (File.Exists(gameSavePath + gds.Current_Game_ID + ".save"))
            {
                string stringSDS = File.ReadAllText(gameSavePath + gds.Current_Game_ID + ".save");
                SDS sds = JsonConvert.DeserializeObject<SDS>(stringSDS);
                
                gds.SDS.InventoryItems = sds.InventoryItems;
                gds.SDS.PlayerInfo = sds.PlayerInfo;
                gds.SDS.CurrentGameState = sds.CurrentGameState;

                sds = null;

                ro.Return_Status = Enums.Return_Status.OK;
                ro.Friendly_Message = "SDS has been loaded.";
                ro.Technical_Message = ro.Friendly_Message;
            }
            else
            {
                //Uh...file doesn't exist
            }
        }
        catch (Exception ex)
        {
            //Something went wrong with the reading of the save game file
            ro.Return_Status = Enums.Return_Status.Error;
            ro.Friendly_Message = "Unable to load the selected game.";
            ro.Technical_Message = "Unable to load the selected game: " + ex.Message;
            ro.Return_Object = null;
        }

        return ro;
    }

    #endregion

    #region EVENT DELEGATES

    public delegate void EventReturnObject(ReturnObject RO);

    private void OnSaveGameEvent(ReturnObject RO)
    {
        if (SaveGameEvent != null)
        {
            SaveGameEvent(RO);
        }
    }

    private void OnLoadGameEvent(ReturnObject RO)
    {
        if (LoadGameEvent != null)
        {
            LoadGameEvent(RO);
        }
    }

    #endregion

    #region EVENT HANDLERS & LISTENERS

   

    #endregion

}

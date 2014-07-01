using UnityEngine;
using System;
using System.IO;
using System.Collections;
using Newtonsoft.Json;

/// <summary>
/// Loads the MDS and the Save Game Listing from disk
/// </summary>
/// <remarks>
/// <para>
/// Data Initialize is the first active script that executes when the game starts up.
/// It's job is to load MDS data and transfer it to the GDS.SDS object, and to 
/// load a list of previously saved games and add it to GDS.GSC.
/// </para>
/// <para>
/// #######################################################################
/// EVENTS:
/// LoadMDSDataEvent(ReturnObject): 
///     MDS has been loaded/attempted
/// LoadSaveGameListingEvent(ReturnObject): 
///     Save game listing has been loaded/attempted
/// </para>
/// <para>
/// #######################################################################
/// BROADCAST/LISTENERS:
/// None
/// </para>
/// </remarks>
public class DataInitialize : MonoBehaviour
{
    #region DECLARATIONS

    public string MDSFileName = "initvalues.data";              //MDS database file
    public string GSDFileName = "SaveGames.save";               //Save game header file

    private GameDataSingleton gds;                              //Master game data store
	private string MDSFilePath;                                 //Path to the MDS file
    private string GSCFilePath;                                 //Path to the save game files
    #endregion

    #region EVENTS

    public event EventReturnObject LoadMDSDataEvent;            //MDS data was loaded/attempted
    public event EventReturnObject LoadSaveGameListingEvent;    //GSC data was loaded/attempted

    #endregion

    #region UNITY METHODS

    /// <summary>
    /// Unity AWAKE
    /// </summary>
    /// <remarks>
    /// <para>
    /// Set up the GDS. 
    /// Set up the file paths to the save games and the MDS data store
    /// </para>
    /// </remarks>
    void Awake()
    {
        gds = GameDataSingleton.Instance;

        //Hardcoded. These should not change
        GSCFilePath = Application.persistentDataPath + "/SaveGames/";
		MDSFilePath = Application.dataPath + "/Data/";
    }

    /// <summary>
    /// Unity START
    /// </summary>
    /// <remarks>
    /// <para>
    /// Call InitializeBaseData which does the loading of the files from disk
    /// </para>
    /// </remarks>
    void Start()
    {
        InitializeBaseData();
    }

    #endregion

    #region PROPERTIES
    #endregion

    #region CONSTRUCTORS
    #endregion

    #region PUBLIC METHODS

    /// <summary>
    /// Runs the LoadMDS and LoadSaveGameListing methods
    /// </summary>
    /// <returns>ReturnObject: Status-bearing object</returns>
    /// <remarks>
    /// <para>
    /// This handles the whole pre-loading in one method call. It's tierd, so the
    /// loading of MDS needs to have been successful in order for the loading of the
    /// save game list to start. Doesn't make sense to load more data if the core data
    /// isn't even present. 
    /// </para>
    /// <para>
    /// However, if we're in DEBUG, we'll load the game list anyway, for testing purposes. s
    /// </para>
    /// </remarks>
    public ReturnObject InitializeBaseData()
    {
        ReturnObject ro = LoadMDSData();
        if (ro.Return_Status == Enums.Return_Status.OK || ro.Return_Status == Enums.Return_Status.Debug)
        {
            ro = LoadSaveGameListingData();
        }

        return ro;
    }
    
    #endregion

    #region PRIVATE METHODS

    /// <summary>
    /// Loads MDS data from disk
    /// </summary>
    /// <returns>ReturnObject: Status bearing object</returns>
    /// <remarks>
    /// <para>Loads the MDS database from disk. If there is no file, we're screwed.</para>
    /// <para>Once MDS has been loaded, it's immediately transferred to the SDS, and
    /// the loading object is nulled.</para>
    /// <para>Raises the LoadMDSDataEevent(RO) on completion.</para>
    /// </remarks>
    ReturnObject LoadMDSData()
    {

        ReturnObject ro;

        if (File.Exists(MDSFilePath + MDSFileName))
        {
            try
            {
                string stringMDS = File.ReadAllText(MDSFilePath + MDSFileName);
                MDS mds = JsonConvert.DeserializeObject<MDS>(stringMDS);

                gds.SDS.TransferFromMDS(mds);
                mds = null;

                ro = new ReturnObject(Enums.Return_Status.OK, "MDS loaded and transferred to SDS.", "MDS loaded and transferred to SDS.", null);
            }
            catch (Exception ex)
            {
                ro = new ReturnObject(Enums.Return_Status.Error, "Error loading MDS.", "Error loading MDS: " + ex.Message, null);
            }
        }
        else
        {
            //Uh...we don't have the file on disk...
            ro = new ReturnObject(Enums.Return_Status.Error, "Master data file was not found. Please reinstall.", "MDS file wasn't found at path " + MDSFilePath + MDSFileName, null);
        }

        OnLoadMDSDataEvent(ro);

        return ro;
    }

    /// <summary>
    /// Loads the Save Game Listing from disk
    /// </summary>
    /// <returns>ReturnObject: Status bearing object</returns>
    /// <remarks>
    /// <para>Unlike MDS, if we have no file to load, it's OK.</para>
    /// <para>When we DO load a file, it replaces the default empty GDS.GSC
    /// object that was created when GDS was initialized.</para>
    /// <para>Raises LoadSaveGameListingEvent(RO) on completion.</para>
    /// </remarks>
    ReturnObject LoadSaveGameListingData()
    {
        ReturnObject ro = new ReturnObject();

        if (File.Exists(GSCFilePath + GSDFileName))
        {
            try
            {
                string stringGSC = File.ReadAllText(GSCFilePath + GSDFileName);
                GameStateCollection gsc = JsonConvert.DeserializeObject<GameStateCollection>(stringGSC);

                gds.GSC = gsc;

                ro = new ReturnObject(Enums.Return_Status.OK, "GSC loaded and transferred to GDS.", "GSC loaded and transferred to GDS.", null);

                //Messenger.Broadcast("PopulateSaveGameList");
            }
            catch (Exception ex)
            {
                ro = new ReturnObject(Enums.Return_Status.Error, "Error loading GDS.", "Error loading GDS: " + ex.Message, null);
            }
        }else{
			ro = new ReturnObject(Enums.Return_Status.Notice, "No GDS found on disk. NOT AN ERROR.", "No GDS found on disk. NOT AN ERROR.", null);
		}

        OnLoadSaveGameListingEvent(ro);

        return ro;
    }

    #endregion

    #region EVENT HANDLERS

    #endregion

    #region EVENT DELEGATES

    /// <summary>
    /// Delegate type for events that allows us to pass a ReturnObject
    /// </summary>
    /// <param name="RO">ReturnObject</param>
    public delegate void EventReturnObject(ReturnObject RO);
    
    /// <summary>
    /// Raises LoadMDSDataEvent
    /// </summary>
    /// <param name="RO">ReturnObject</param>
    void OnLoadMDSDataEvent(ReturnObject RO)
    {
        if (LoadMDSDataEvent != null)
        {
            LoadMDSDataEvent(RO);
        }
    }

    /// <summary>
    /// Raises LoadSaveGameListingEvent 
    /// </summary>
    /// <param name="RO">ReturnObject</param>
    void OnLoadSaveGameListingEvent(ReturnObject RO)
    {
        if (LoadSaveGameListingEvent != null)
        {
            LoadSaveGameListingEvent(RO);
        }
    }

    #endregion

}

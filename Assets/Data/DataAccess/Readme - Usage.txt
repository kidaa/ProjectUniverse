INITIAL SETUP
-------------
1. Create an instance of GameStateManager. Best option is to include this on a Scene Controller
	GameStateManager gsm = new GameStateManager();
2. Call InitializeData()
	gsm.InitializeData();

	This will do the following:
		1. Create an instance of SDS
		2. Create an instance of PlayerInfo and assign it to SDS
		3. Create an instance of GameState and assign it to SDS
			a. Set gds.Current_Game_ID to the GUID generated for GameState. This is the "Default" 
				SaveGameID, assuming this is a new game.
		4. Loads MDS into the MDS object
			a. Transfers MDS to SDS shadow objects, then nulls MDS.
		5. Loads a list of save games into GameStateCollection, if any
			a. Adds the GSC to the GDS

NEW GAME
--------
1. Create an instance of GameStateManager (or pull it from a Scene Controller)
2. Display a UI that collects NAME and ATLAS_AVATAR values
3. After validation, call NewGame:
	gsm.NewGame(string Name, string Atlas_Avatar);

	This is a public void, so it'll need some work to raise an event on complete. 

	This method does the following:
		1. Sets the gds.SDS.PlayerInfo.Name and .Atlas_Avatar to the values from the UI
		2. Generates new data
			a. GenerateCommodityInventory();
		3. Saves the game
			a. Writes the SDS object to gds.Current_Game_ID + ".save" file
			b. Writes the SDS.GSC (GameSaveCollection) to the file name provided ("SaveGames.xxx")

		This also needs some kind of error trapping and notification so the scene can progress

LOAD GAME
---------
1. Create an instance of GameStateManager (or pull it from a Scene Controller)
2. The gds.GSC holds a list of save games that was loaded from disk during the MDS load phase
3. Loop through gds.GSC.Game_States
	foreach (UV_DataObjects.GameState gs in gds.GSC.Game_States){ ... }

	Build the list display using the gs values.

4. Handle a list item click
	a. Transfer the gs.GU_ID to a holding bin (local variable scoped to the form)
5. Handle the LOAD button click
	a. Using the local scoped variable holding gs.GU_ID, call LoadGame(string Game_ID)
		gsm.LoadGame(gds.Current_Game_ID);

		This method does the following:
			1. Loads the Curent_Game_ID + ".save" file
			2. Deserializes it to an SDS object -- but not THE SDS object.
				Because the SDS was initilizaed with MDS data, loading this data here only
					loads the generated data. We need to move this data into the gds.SDS object
			3. Copies the loaded objects to gds.SDS:
				a. InventoryItems
				b. PlayerInfo
				c. CurrentGameState

		Currently this returns a BOOL denoting a success or failure.
            

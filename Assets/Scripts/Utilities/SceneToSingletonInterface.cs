using UnityEngine;
using System.Collections;

/// <summary>
/// The SceneToSingletonInterface allows the game objects or
/// Playmaker to talk to the Singleton object and vice versa
/// </summary>
public class SceneToSingletonInterface : MonoBehaviour {

	#region UNITY METHODS
	void Start()
	{
		gds = GameDataSingleton.Instance;

	}
	#endregion
	
	#region DECLARATIONS

	private GameDataSingleton gds;

	public int sectorID = 0;		//Set on the SceneController at dev

	#endregion
	
	#region PROPERTIES
	#endregion
	
	#region CONSTRUCTORS
	#endregion
	
	#region PUBLIC METHODS
	#endregion
	
	#region PRIVATE METHODS
	#endregion
	
	#region EVENT HANDLERS
	#endregion









}

using UnityEngine;
using System.Collections;

public class DataCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameDataSingleton gds = GameDataSingleton.Instance;

        if (gds.SDS.Stations.Count == 0)
        {
            //We haven't loaded anything!
            GameObject dfNoDataSprite = GameObject.Find("UI_Sprite_NoData");
            if (dfNoDataSprite != null)
            {
                dfSprite dfs = dfNoDataSprite.GetComponent<dfSprite>();
                dfs.IsVisible = true;
            }

        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

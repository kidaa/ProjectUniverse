using UnityEngine;
using System.Collections;

/// <summary>
/// I have no idea if this'll stay here. This is strictly for Playmaker right now, 
/// and ideally the movement script that PM is being used for will make its way 
/// back into pure script. 
/// </summary>
public class PlayerVariables : MonoBehaviour
{
    #region DECLARATIONS

    public bool isDocked = false;
    public Vector3 currentPosition = Vector3.zero;
    public Vector3 currentRotation = Vector3.zero;

    #endregion

    #region UNITY METHODS

    void Start()
    {
        Messenger.AddListener("IsUndocking", IsUndocking);
    }

    #endregion

    #region PRIVATE METHODS

    void IsUndocking()
    {
        isDocked = false;
    }

    #endregion
}

using UnityEngine;
using System;

public class ReturnObject : MonoBehaviour{

    private Enums.Return_Status _return_status;
    private string _friendly_message;
    private string _technical_message;
    private object _return_object;

    public Enums.Return_Status Return_Status { get { return _return_status; } set { _return_status = value; } }
    public string Friendly_Message { get { return _friendly_message; } set { _friendly_message = value; } }
    public string Technical_Message { get { return _technical_message; } set { _technical_message = value; } }
    public object Return_Object { get { return _return_object; } set { _return_object = value; } }
    
    public ReturnObject()
    {
        NewReturnObject(Enums.Return_Status.OK, string.Empty, string.Empty, null, string.Empty);
    }

    public ReturnObject(Enums.Return_Status Return_Status,
        string Friendly_Message,
        string Technical_Message,
        object Return_Object)
    {
        NewReturnObject(Return_Status, Friendly_Message, Technical_Message, Return_Object, string.Empty);
    }

    public ReturnObject(Enums.Return_Status Return_Status,
        string Friendly_Message,
        string Technical_Message,
        object Return_Object,
        string ExtraDebugInfo)
    {
        NewReturnObject(Return_Status, Friendly_Message, Technical_Message, Return_Object, ExtraDebugInfo);
    }


    private void NewReturnObject(Enums.Return_Status Return_Status,
        string Friendly_Message,
        string Technical_Message,
        object Return_Object,
        string ExtraDebugInfo)
    {
        this.Return_Status = Return_Status;
        this.Friendly_Message = Friendly_Message;
        this.Technical_Message = Technical_Message;
        this.Return_Object = Return_Object;

        DoDebug(ExtraDebugInfo);
    }

    /// <summary>
    /// Check the status of all strings, and if full, output to console. 
    /// Eventually, this will help write to log
    /// </summary>
    private void DoDebug(string ExtraDebugInfo)
    {
        if (this.Friendly_Message.Length > 0 && this.Technical_Message.Length > 0)
        {

            string debugMessage = "ReturnObject: Status [" + this.Return_Status + "]  Message [" + this.Friendly_Message + "] Technical [" + this.Technical_Message + "]";

            switch (this.Return_Status)
            {
                case Enums.Return_Status.OK:
                    Debug.Log(debugMessage);
                    break;
                case Enums.Return_Status.Error:
                    Debug.LogError(debugMessage);
                    break;
                case Enums.Return_Status.Notice:
                    Debug.LogWarning(debugMessage);
                    break;
                case Enums.Return_Status.Debug:
                    Debug.Log(debugMessage + " - " + ExtraDebugInfo);
                    break;
                default:
                    break;
            }

		    
        }
    }

}

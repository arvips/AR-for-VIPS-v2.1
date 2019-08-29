using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class ControlScript2 : MonoBehaviour
{
    #region **INITIALIZATION**


    [Tooltip("This text will change to copy the Debug output.")]
    public GameObject debugText;
    private string output = ""; //helps print text in UI



    void Start()
    {
        ClearDebug();
    }

    private void Update()
    {
    }

    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog; //helps print text in UI
    }

    void OnDisable()
    {

        Application.logMessageReceived -= HandleLog;
    }

    #endregion

    #region HOLOLENS DEBUG UI

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        output = logString;
        if (debugText.GetComponent<TextMesh>().text.Length <= 2000)
        {
            debugText.GetComponent<TextMesh>().text += "\n" + output;
        }
        
    }

    public void ClearDebug()
    {
        debugText.GetComponent<TextMesh>().text = "Debug log cleared.";
    }

    public void ToggleDebug()
    {
        //Toggles active status of Debug Window.
        if (debugText.gameObject.transform.parent.gameObject.activeSelf)
        {
            debugText.gameObject.transform.parent.gameObject.SetActive(false);
            Debug.Log("Debug window deactivated.");
        }

        else
        {
            debugText.gameObject.transform.parent.gameObject.SetActive(true);
            Debug.Log("Debug window reactivated.");
        }
    }

    #endregion
}

using System;
using System.Collections;
using System.Collections.Generic;
using Articy.Unity;
using Articy.Stygian_Crossing.GlobalVariables;
using UnityEditor;
using UnityEngine;

public class GlobalVariableListener : MonoBehaviour
{
    public event Action<string, object> GlobalVariableChanged; 
    private void OnEnable()
    {
        // we get the default variable storage from our database and add a listener to the notification manager
        // to call our supplied method every time any Variable inside the namespace "GameState" is changed.
        ArticyDatabase.DefaultGlobalVariables.Notifications.AddListener("GlobalVariables.*", OnGlobalVariablesChanged);
    }
    
    // here we handle every variable change inside the "GameState" namespace
    private void OnGlobalVariablesChanged(string aVariableName, object aValue)
    {
        GlobalVariableChanged?.Invoke(aVariableName, aValue);
        // if (aVariableName == "VariableSet.VariableName" && (type)aValue == desiredValue)
        // {
        //     run desired code;
        // }
    
    }
    
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Alpha1))
    //     {
    //         Debug.Log("press");
    //         
    //         ArticyDatabase.DefaultGlobalVariables.SetVariableByString("GlobalVariables.Bottle", true); //This modifies global variables through code
    //     }
    // }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public bool settingsVisible;
    
    public void OpenClose()
    {
        gameObject.SetActive(!settingsVisible);
        settingsVisible = !settingsVisible;
    }
    void Update()
    {
        
    }
}

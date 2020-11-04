using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
 
public class InputManager : MonoBehaviour
{
    Dictionary<string, KeyCode> keys;
    string[] keyMaps = new string[3]
    {
        "Menu",
        "Rotate",
        "Slices"
    };
    KeyCode[] defaults = new KeyCode[3]
    {
        KeyCode.M,
        KeyCode.Mouse1,
        KeyCode.N
    };
 
    InputManager()
    {
        InitializeDictionary();
    }
 
    private void InitializeDictionary()
    {
        keys = new Dictionary<string, KeyCode>();
        for(int i=0;i<keyMaps.Length;++i)
        {
            keys.Add(keyMaps[i], defaults[i]);
        }
    }
 
    public void SetKeyMap(string keyMap,KeyCode key)
    {
        //Debug.Log("Я зашёл");
        if (!keys.ContainsKey(keyMap))
            throw new ArgumentException("Invalid KeyMap in SetKeyMap: " + keyMap); //Debug.Log("Но такая уже есть");
        
        keys[keyMap] = key;
    }
 
    public bool GetKeyDown(string keyMap)
    {
        return Input.GetKeyDown(keys[keyMap]);
    }
}

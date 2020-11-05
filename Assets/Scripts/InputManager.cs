using UnityEngine;
using System.Collections.Generic;
using System;
 
public class InputManager : MonoBehaviour
{
    private Dictionary<string, KeyCode> _keys;

    private readonly string[] _keyMaps = new string[3]
    {
        "Menu",
        "Rotate",
        "Slice"
    };
    private KeyCode[] _defaults = new KeyCode[3]
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
        _keys = new Dictionary<string, KeyCode>();
        for(int i=0;i<_keyMaps.Length;++i)
        {
            _keys.Add(_keyMaps[i], _defaults[i]);
        }
    }
 
    public bool SetKeyMap(string keyMap,KeyCode key)
    {
        if(_keys.ContainsValue(key))
            return false;
        Debug.Log(keyMap);
        _keys[keyMap] = key;
        return true;
    }
 
    public bool GetKeyDown(string keyMap)
    {
        return Input.GetKeyDown(_keys[keyMap]);
    }

    public bool GetKey(string keyMap)
    {
        return Input.GetKey(_keys[keyMap]);
    }
}

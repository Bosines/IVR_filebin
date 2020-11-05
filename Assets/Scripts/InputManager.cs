using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Dictionary<string, KeyCode> _keys;

    private readonly string[] _keyMaps = {
        "Menu",
        "Rotate",
        "Slice"
    };
    private readonly KeyCode[] _defaults = {
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

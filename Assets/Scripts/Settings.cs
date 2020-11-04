using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;


public class Settings : MonoBehaviour
{
    private InputManager _inputManager;
    
    [NonSerialized] public bool settingsVisible;
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    private KeyCode[] mouseKeys;

    private GameObject currentKey;

    public Text slice, menu, rotate;

    public void Start()
    {
        _inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        
        keys.Add("SliceSet", KeyCode.N);
        keys.Add("MenuSet", KeyCode.M);
        keys.Add("RotateSet", KeyCode.Mouse1);

        slice.text = keys["SliceSet"].ToString();
        menu.text = keys["MenuSet"].ToString();
        rotate.text = keys["RotateSet"].ToString();
        
        mouseKeys = new KeyCode[] { KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.Mouse2, KeyCode.Mouse3, KeyCode.Mouse4, KeyCode.Mouse5, KeyCode.Mouse6 };
    }
    
    public void OpenClose()
    {
        gameObject.SetActive(!settingsVisible);
        settingsVisible = !settingsVisible;
    }

    public void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;

            if (e != null)
            {
                if (e.isKey)
                {
                    //keys[currentKey.name] = e.keyCode;
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                    //Debug.Log("Keyboard pressed");
                    switch (currentKey.name)
                    {
                        case "MenuSet":
                            _inputManager.SetKeyMap("Menu", e.keyCode);
                            break;
                        case "SliceSet":
                            _inputManager.SetKeyMap("Slice", e.keyCode);
                            break;
                        case "RotateSet":
                            _inputManager.SetKeyMap("Rotate", e.keyCode);
                            break;
                    }
                    /*if (currentKey.name == "MenuSet")
                    {
                        //Debug.Log("Вышел");
                        _inputManager.SetKeyMap("Menu", e.keyCode);
                    }*/
                    currentKey = null;
                }

                if (e.isMouse)
                {
                    for (int i = 0; i < mouseKeys.Length; i++)
                    { 
                        if (Input.GetKeyDown(mouseKeys[i])) 
                        {
                            //keys[currentKey.name] = mouseKeys[i];
                            currentKey.transform.GetChild(0).GetComponent<Text>().text = mouseKeys[i].ToString();
                            switch (currentKey.name)
                            {
                                case "MenuSet":
                                    _inputManager.SetKeyMap("Menu", mouseKeys[i]);
                                    break;
                                case "SliceSet":
                                    _inputManager.SetKeyMap("Slice", mouseKeys[i]);
                                    break;
                                case "RotateSet":
                                    _inputManager.SetKeyMap("Rotate", mouseKeys[i]);
                                    break;
                            }
                            currentKey = null;
                        }
                    }
                }

            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;
    }
}

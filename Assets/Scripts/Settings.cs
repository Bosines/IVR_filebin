using System;
using UnityEngine;
using UnityEngine.UI;


public class Settings : MonoBehaviour
{
    private InputManager _inputManager;
    
    [NonSerialized] public bool SettingsVisible;
    // private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    private KeyCode[] _mouseKeys;

    private GameObject _currentKey;

    //public Text slice, menu, rotate;

    public void Start()
    {
        _inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        
        /*keys.Add("SliceSet", KeyCode.N);
        keys.Add("MenuSet", KeyCode.M);
        keys.Add("RotateSet", KeyCode.Mouse1);*/

        /*slice.text = keys["Slice"].ToString();
        menu.text = keys["MenuSet"].ToString();
        rotate.text = keys["RotateSet"].ToString();*/
        
        _mouseKeys = new KeyCode[] { KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.Mouse2, KeyCode.Mouse3, KeyCode.Mouse4, KeyCode.Mouse5, KeyCode.Mouse6 };
    }
    
    public void OpenClose()
    {
        gameObject.SetActive(!SettingsVisible);
        SettingsVisible = !SettingsVisible;
    }

    public void OnGUI()
    {
        if (_currentKey != null)
        {
            Event e = Event.current;

            if (e != null)
            {
                if (e.isKey)
                {
                    //keys[currentKey.name] = e.keyCode;
                    _currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                    //Debug.Log("Keyboard pressed");
                    switch (_currentKey.name)
                    {
                        case "MenuSet":
                            _inputManager.SetKeyMap("Menu", e.keyCode);
                            break;
                        case "SliceSet":
                            Debug.Log("SliceEnter");
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
                    _currentKey = null;
                }

                if (e.isMouse)
                {
                    for (int i = 0; i < _mouseKeys.Length; i++)
                    { 
                        if (Input.GetKeyDown(_mouseKeys[i])) 
                        {
                            //keys[currentKey.name] = mouseKeys[i];
                            _currentKey.transform.GetChild(0).GetComponent<Text>().text = _mouseKeys[i].ToString();
                            switch (_currentKey.name)
                            {
                                case "MenuSet":
                                    _inputManager.SetKeyMap("Menu", _mouseKeys[i]);
                                    break;
                                case "SliceSet":
                                    _inputManager.SetKeyMap("Slice", _mouseKeys[i]);
                                    break;
                                case "RotateSet":
                                    _inputManager.SetKeyMap("Rotate", _mouseKeys[i]);
                                    break;
                            }
                            _currentKey = null;
                        }
                    }
                }

            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        _currentKey = clicked;
    }
}

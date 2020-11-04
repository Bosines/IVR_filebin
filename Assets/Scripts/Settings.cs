using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;


public class Settings : MonoBehaviour
{
    [NonSerialized] public bool settingsVisible;
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    private KeyCode[] mouseKeys;

    private GameObject currentKey;

    public Text slice, menu, rotate;

    public void Start()
    {
        keys.Add("Slice", KeyCode.N);
        keys.Add("Menu", KeyCode.M);
        keys.Add("Rotate", KeyCode.Mouse1);

        slice.text = keys["Slice"].ToString();
        menu.text = keys["Menu"].ToString();
        rotate.text = keys["Rotate"].ToString();
        
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
                    keys[currentKey.name] = e.keyCode;
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                    currentKey = null;
                    Debug.Log("Keyboard pressed");
                }

                if (e.isMouse)
                {
                    for (int i = 0; i < mouseKeys.Length; i++)
                    { 
                        if (Input.GetKeyDown(mouseKeys[i])) 
                        {
                            keys[currentKey.name] = mouseKeys[i];
                            currentKey.transform.GetChild(0).GetComponent<Text>().text = mouseKeys[i].ToString();    
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

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
    }
    
    public void OpenClose()
    {
        gameObject.SetActive(!settingsVisible);
        settingsVisible = !settingsVisible;
    }

    public void Update()
    {
        if (currentKey != null)
        {
            if (Event.current != null)
            {
                Debug.Log("Зашёл");
                Event e = Event.current;

                if (e.isKey)
                {
                    keys[currentKey.name] = e.keyCode;
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                    currentKey = null;
                    Debug.Log("Keyboard pressed");
                }

                if (e.isMouse)
                {
                    keys[currentKey.name] = e.keyCode;
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                    currentKey = null;
                    Debug.Log("Mouse pressed");
                }
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;
    }
}

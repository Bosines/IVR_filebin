using System;
using UnityEngine;
using UnityEngine.UI;


public class Settings : MonoBehaviour
{
    private InputManager _inputManager;
    [NonSerialized] public bool SettingsVisible, AnimationsTurnedOn = true;
    private KeyCode[] _mouseKeys;
    private GameObject _currentKey;
    private SpriteRenderer[] _spriteRenderers;
    [SerializeField] private SpriteRenderer anim1, anim2, anim3, anim4;
    private Image _animOff;
    
    public void Start()
    {
        _animOff = GameObject.Find("animOff").GetComponent<Image>();
        _spriteRenderers = new[] {anim1, anim2, anim3, anim4};
        _inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        _mouseKeys = new[] { KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.Mouse2, KeyCode.Mouse3, KeyCode.Mouse4, KeyCode.Mouse5, KeyCode.Mouse6 };
    }
    
    public void OpenClose()
    {
        gameObject.SetActive(!SettingsVisible);
        SettingsVisible = !SettingsVisible;
    }

    public void ToggleAnimations()
    {
        AnimationsTurnedOn = !AnimationsTurnedOn;
        _animOff.enabled = !AnimationsTurnedOn;
        foreach (var SR in _spriteRenderers)
        {
            SR.enabled = AnimationsTurnedOn;
        }
    }
    
    public void OnGUI()
    {
        if (_currentKey == null) return;
        Event e = Event.current;

        if (e != null)
        {
            if (e.isKey)
            {
                switch (_currentKey.name)
                {
                    case "MenuSet":
                        if (_inputManager.SetKeyMap("Menu", e.keyCode)) _currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                        break;
                    case "SliceSet":
                        if (_inputManager.SetKeyMap("Slice", e.keyCode)) _currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                        break;
                    case "RotateSet":
                        if (_inputManager.SetKeyMap("Rotate", e.keyCode)) _currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                        break;
                }
                _currentKey = null;
            }

            if (e.isMouse)
            {
                for (int i = 0; i < _mouseKeys.Length; i++)
                { 
                    if (Input.GetKeyDown(_mouseKeys[i])) 
                    {
                        switch (_currentKey.name)
                        {
                            case "MenuSet":
                                if (_inputManager.SetKeyMap("Menu", _mouseKeys[i])) _currentKey.transform.GetChild(0).GetComponent<Text>().text = _mouseKeys[i].ToString();
                                break;
                            case "SliceSet":
                                if (_inputManager.SetKeyMap("Slice", _mouseKeys[i])) _currentKey.transform.GetChild(0).GetComponent<Text>().text = _mouseKeys[i].ToString();
                                break;
                            case "RotateSet":
                                if (_inputManager.SetKeyMap("Rotate", _mouseKeys[i])) _currentKey.transform.GetChild(0).GetComponent<Text>().text = _mouseKeys[i].ToString();
                                break;
                        }
                        _currentKey = null;
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

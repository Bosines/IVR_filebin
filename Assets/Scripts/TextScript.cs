using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public GameObject textThere;
    
    private GameObject _brain;
    private Camera _cameraMain;
    private Image _image;
    private Text _text;
    private MenuScript _menuScript;

    private void Awake()
    {
        _menuScript = GameObject.Find("Menu").GetComponent<MenuScript>();
        _image = textThere.GetComponentInChildren<Image>();
        _text = textThere.GetComponentInChildren<Text>();
        _brain = GameObject.Find("Brain");
        _cameraMain = Camera.main;
    }
    
    private void Update()
    {
        if (_menuScript.SliceActive)
        {
            if (_brain.transform.eulerAngles.y > 320 || _brain.transform.eulerAngles.y < 200 || (_brain.transform.eulerAngles.z > 50 && _brain.transform.eulerAngles.z < 180) || (_brain.transform.eulerAngles.z < 300 && _brain.transform.eulerAngles.z >= 180))
            {
                _image.enabled = false;
                _text.enabled = false;
            }
            else
            {
                _image.enabled = true;
                _text.enabled = true;
            }
        }
        else
        {
            if (_brain.transform.eulerAngles.y > 150 || _brain.transform.eulerAngles.y < 40 || (_brain.transform.eulerAngles.z > 50 && _brain.transform.eulerAngles.z < 180) || (_brain.transform.eulerAngles.z < 300 && _brain.transform.eulerAngles.z >= 180))
            {
                _image.enabled = false;
                _text.enabled = false;
            }
            else
            {
                _image.enabled = true;
                _text.enabled = true;
            }
        }

        Vector3 textTranspos = transform.position;
        Vector3 namePose = _cameraMain.WorldToScreenPoint(textTranspos);
        textThere.transform.position = namePose;
    }
}
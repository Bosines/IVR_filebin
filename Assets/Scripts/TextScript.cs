using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public GameObject textThere, brain;
    private Camera _cameraMain;
    private Image _image;
    private Text _text;
    private MenuScript _menuScript;

    private void Awake()
    {
        _menuScript = GameObject.Find("Menu").GetComponent<MenuScript>();
        _image = textThere.GetComponentInChildren<Image>();
        _text = textThere.GetComponentInChildren<Text>();
        brain = GameObject.Find("Brain");
        _cameraMain = Camera.main;
    }
    //0.4<x<0.9 -0.4<x<-0.9
    private void Update()
    {
        if (_menuScript.SliceActive)
        {
            if (brain.transform.eulerAngles.y > 320 || brain.transform.eulerAngles.y < 200 || (brain.transform.eulerAngles.z > 50 && brain.transform.eulerAngles.z < 180) || (brain.transform.eulerAngles.z < 300 && brain.transform.eulerAngles.z >= 180))
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
            if (brain.transform.eulerAngles.y > 150 || brain.transform.eulerAngles.y < 40 || (brain.transform.eulerAngles.z > 50 && brain.transform.eulerAngles.z < 180) || (brain.transform.eulerAngles.z < 300 && brain.transform.eulerAngles.z >= 180))
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
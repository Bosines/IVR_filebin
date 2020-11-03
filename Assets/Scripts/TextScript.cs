using UnityEngine;

public class TextScript : MonoBehaviour
{
    public GameObject textThere;
    private Camera _cameraMain;

    private void Awake()
    {
        _cameraMain = Camera.main;
    }
    
    private void Update()
    {
        Vector3 textTranspos = this.transform.position;
        Vector3 namePose = _cameraMain.WorldToScreenPoint(textTranspos);
        textThere.transform.position = namePose;
    }
}
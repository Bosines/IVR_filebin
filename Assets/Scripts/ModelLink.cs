using UnityEngine;

public class ModelLink : MonoBehaviour
{
    [SerializeField] private GameObject brain;
    private MoveSphere _ms;
    public WheelZoom wz;
    private MenuScript _menu;
    private readonly Quaternion _zerorotate = new Quaternion(0,0,0,0);
    public bool modelLinkWork;
    
    void Start()
    {
        _ms = brain.GetComponent<MoveSphere>();
        wz = brain.GetComponent<WheelZoom>();
        _menu = GameObject.Find("Menu").GetComponent<MenuScript>();
    }
    
    public void ModelLinkvoid(GameObject sphere)
    {
        if (sphere == _ms._rhalf)
        {
            brain.transform.rotation = _zerorotate;
            while (_menu.markerTimer) _menu.ChooseSlice();
            wz.Y = 0.25f;
            wz.X = 0.25f;
        }
        else if (sphere == _ms._rCerebellum)
        {
            brain.transform.rotation = _zerorotate;
            brain.transform.eulerAngles = new Vector3(0, -180, 0);
            while (_menu.markerTimer) _menu.ChooseSlice();
            wz.Y = 0.52f;
            wz.X = 0.36f;
                //Debug.Log("Cerebellum slide");
        }
        else if (sphere == _ms._bridge)
        {
            brain.transform.rotation = _zerorotate;
            brain.transform.eulerAngles = new Vector3(0f, -90, 0);
            while (!_menu.markerTimer) _menu.ChooseSlice();
            wz.Y = 0.43f;
            wz.X = 0f;
            //Debug.Log("Bridge slide");
        }             
        else if (sphere == _ms._long)
        {
            brain.transform.rotation = _zerorotate;
            brain.transform.eulerAngles = new Vector3(0f, -90, 0);
            while (!_menu.markerTimer) _menu.ChooseSlice();
            wz.Y = 0.62f;
            wz.X = 0.1f;
            //Debug.Log("Long slide");
        }
        else if (sphere == _ms._middle)
        {
            brain.transform.rotation = _zerorotate;
            brain.transform.eulerAngles = new Vector3(0f, -90, 0);
            while (!_menu.markerTimer) _menu.ChooseSlice();
            wz.Y = 0.45f;
            wz.X = -0.14f;
            //Debug.Log("middle slide");
        }
        else
        {
            brain.transform.rotation = _zerorotate;
            brain.transform.eulerAngles = new Vector3(0, -90, 0);
            while (!_menu.markerTimer) _menu.ChooseSlice();
            wz.Y = 0.45f;
        }
    }
}

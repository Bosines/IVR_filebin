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
    
    public void ModelLinkvoid(string sphere)
    {
        if (sphere == "Half")
        {
            brain.transform.rotation = _zerorotate;
            while (_menu.SliceActive) _menu.ChooseSlice();
            wz.Y = 0.25f;
            wz.X = 0.25f;
        }
        else if (sphere == "Cerebellum")
        {
            brain.transform.rotation = _zerorotate;
            brain.transform.eulerAngles = new Vector3(0, -180, 0);
            while (_menu.SliceActive) _menu.ChooseSlice();
            wz.Y = 0.52f;
            wz.X = 0.36f;
                //Debug.Log("Cerebellum slide");
        }
        else if (sphere == "Bridge")
        {
            brain.transform.rotation = _zerorotate;
            brain.transform.eulerAngles = new Vector3(0f, -90, 0);
            while (!_menu.SliceActive) _menu.ChooseSlice();
            wz.Y = 0.6f;
            wz.X = 0.5f;
            //Debug.Log("Bridge slide");
        }             
        else if (sphere == "Long")
        {
            //Debug.Log("Вышел потом");
            brain.transform.rotation = _zerorotate;
            brain.transform.eulerAngles = new Vector3(0f, -90, 0);
            while (!_menu.SliceActive) _menu.ChooseSlice();
            wz.Y = 0.8f;
            wz.X = 0.75f;
            //Debug.Log("Long slide");
        }
        else if (sphere == "Middle")
        {
            brain.transform.rotation = _zerorotate;
            brain.transform.eulerAngles = new Vector3(0f, -90, 0);
            while (!_menu.SliceActive) _menu.ChooseSlice();
            wz.Y = 0.45f;
            wz.X = 0.5f;
            //Debug.Log("middle slide");
        }
        else if (sphere == "Skip")
        {
            brain.transform.rotation = _zerorotate;
            brain.transform.eulerAngles = new Vector3(0f, -90, 0);
            while (!_menu.SliceActive) _menu.ChooseSlice();
            wz.Y = 0.35f;
            wz.X = 0.35f;
            //Debug.Log("middle slide");
        }
        else
        {
            brain.transform.rotation = _zerorotate;
            brain.transform.eulerAngles = new Vector3(0, -90, 0);
            while (!_menu.SliceActive) _menu.ChooseSlice();
            wz.Y = 0.45f;
        }
    }
}

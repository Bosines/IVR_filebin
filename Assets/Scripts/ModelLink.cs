using UnityEngine;

public class ModelLink : MonoBehaviour
{
    [SerializeField] private GameObject brain;
    public WheelZoom wz;
    private MenuScript _menu;
    private readonly Quaternion _zerorotate = new Quaternion(0,0,0,0);
    public bool modelLinkWork;
    
    void Start()
    {
        wz = brain.GetComponent<WheelZoom>();
        _menu = GameObject.Find("Menu").GetComponent<MenuScript>();
    }
    
    public void ModelLinkvoid(string sphere)
    {
        switch (sphere)
        {
            case "Half":
            {
                brain.transform.rotation = _zerorotate;
                while (_menu.SliceActive) _menu.ChooseSlice();
                wz.Y = 0.25f;
                wz.X = 0.25f;
                break;
            }
            case "Cerebellum":
            {
                brain.transform.rotation = _zerorotate;
                brain.transform.eulerAngles = new Vector3(0, -180, 0);
                while (_menu.SliceActive) _menu.ChooseSlice();
                wz.Y = 0.52f;
                wz.X = 0.36f;
                break;
            }
            case "Bridge":
            {
                brain.transform.rotation = _zerorotate;
                brain.transform.eulerAngles = new Vector3(0f, -90, 0);
                while (!_menu.SliceActive) _menu.ChooseSlice();
                wz.Y = 0.6f;
                wz.X = 0.5f;
                break;
            }
            case "Long":
            {
                brain.transform.rotation = _zerorotate;
                brain.transform.eulerAngles = new Vector3(0f, -90, 0);
                while (!_menu.SliceActive) _menu.ChooseSlice();
                wz.Y = 0.8f;
                wz.X = 0.75f;
                break;
            }
            case "Middle":
            {
                brain.transform.rotation = _zerorotate;
                brain.transform.eulerAngles = new Vector3(0f, -90, 0);
                while (!_menu.SliceActive) _menu.ChooseSlice();
                wz.Y = 0.45f;
                wz.X = 0.5f;
                break;
            }
            case "Skip":
            {
                brain.transform.rotation = _zerorotate;
                brain.transform.eulerAngles = new Vector3(0f, -90, 0);
                while (!_menu.SliceActive) _menu.ChooseSlice();
                wz.Y = 0.35f;
                wz.X = 0.35f;
                break;
            }
            default:
            {
                brain.transform.rotation = _zerorotate;
                brain.transform.eulerAngles = new Vector3(0, -90, 0);
                while (!_menu.SliceActive) _menu.ChooseSlice();
                wz.Y = 0.45f;
                break;
            }
        }
    }
}

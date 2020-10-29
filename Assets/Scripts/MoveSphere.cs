using System;
using UnityEngine;
using UnityEngine.UI;

public class MoveSphere : MonoBehaviour
{
    [NonSerialized] public bool sphereChosen;
//    private readonly Vector3 _temp = new Vector2(0.11f, 0f);
//    private readonly Vector3 _zero = Vector3.zero;
//    private Button     _button;
//    private Button     _menubutton;
    private readonly Quaternion _zeroRotate = new Quaternion(0, 0, 0, 0);

    private readonly Vector3 lhalf             = new Vector3(-0.045f, -0.134f, -0.22f);
    private readonly Vector3 rhalf             = new Vector3(0.045f, -0.134f, -0.22f);
    private readonly Vector3 halfr             = new Vector3(-90, 0, 90);
    private readonly Vector3 lCerebellum       = new Vector3(0.003f, -0.663f, 0.538f);
    private readonly Vector3 lCerebellumr      = new Vector3(-68, 176, -90);
    private readonly Vector3 rCerebellum       = new Vector3(0.003f, -0.663f, 0.533f);
    private readonly Vector3 rCerebellumr      = new Vector3(-68, 176, -90);
    private readonly Vector3 longb             = new Vector3(0, -0.815f, 0.299f);
    private readonly Vector3 longbr            = new Vector3(-35.4f, 2.057f, 59.801f);
    private readonly Vector3 bridge            = new Vector3(0, -0.652f, 0.22f);
    private readonly Vector3 bridger           = new Vector3(-73.528f, 180, 90);
    private readonly Vector3 middleb           = new Vector3(0, -0.508f, 0.273f);
    private readonly Vector3 middlebr          = new Vector3(-67.772f, -180, 90);
    private readonly Vector3 skip              = new Vector3(0, -0.363f, 0.298f);
    
    private readonly Vector3 _up = new Vector3(0, 0.25f, 0);

    [NonSerialized] public GameObject _lhalf;
    [NonSerialized] public GameObject _rhalf;
    [NonSerialized] public GameObject _lCerebellum;
    [NonSerialized] public GameObject _rCerebellum;
    [NonSerialized] public GameObject _long;
    [NonSerialized] public GameObject _bridge;
    [NonSerialized] public GameObject _middle;
    [NonSerialized] public GameObject _skipHL;
    [NonSerialized] public GameObject _skipHR;
    [NonSerialized]private GameObject _brain;
    
    private GameObject _globalText;
    private GameObject _menu;
    private MenuScript _menuScript;
    private GameObject _localText;
    private WheelZoom _wz;
    private RotateDrag _rotateDrag;
    private GameObject[] _brainParts;


    private void Awake()
    {
        _lhalf       = GameObject.Find("LHalf");
        _rhalf       = GameObject.Find("RHalf");
        _lCerebellum = GameObject.Find("LCerebellum");
        _rCerebellum = GameObject.Find("RCerebellum");
        _long        = GameObject.Find("Long");
        _bridge      = GameObject.Find("Bridge");
        _middle      = GameObject.Find("Middle");
        _skipHR        = GameObject.Find("SkipHR");
        _skipHL        = GameObject.Find("SkipHL");
        _brain       = GameObject.Find("Brain");
        _menu        = GameObject.Find("Menu");
 //       _button      = GameObject.Find("UIButton").GetComponent<Button>();
  //      _menubutton  = GameObject.Find("MenuButton").GetComponent<Button>();
        _globalText  = GameObject.Find("GlobalText");
        _localText   = GameObject.Find("LocalText");
        _brainParts = new[] {_lhalf, _rhalf, _lCerebellum, _rCerebellum, _skipHR, _long, _bridge, _middle};
        _menuScript  = _menu.GetComponent<MenuScript>();
        _wz = _brain.GetComponent<WheelZoom>();
    }

    public void SphereText(GameObject textBlock)
    {
        _localText.SetActive(true);
        textBlock.SetActive(sphereChosen);
    }

    private void NoRotate(GameObject zero)
    {
        zero.transform.rotation = _zeroRotate;
        //zero.GetComponent<RotateDrag>().enabled = false;
    }
    
    public void SMoveBack()
    {
        _wz.cubeS = _brain;
        _rotateDrag = _brain.GetComponent<RotateDrag>();
        _brain.transform.rotation = _zeroRotate;
        _brain.transform.position = new Vector3(0, 0.25f, 0);
        _rotateDrag.enabled = true;

        _lhalf.transform.position       = lhalf + _up;
        _rhalf.transform.position       = rhalf + _up;
        _lCerebellum.transform.position = lCerebellum + _up;
        _rCerebellum.transform.position = rCerebellum + _up;
        _skipHR.transform.position      = skip + _up;
        _skipHL.transform.position      = skip + _up;
        _long.transform.position        = longb + _up;
        _bridge.transform.position      = bridge + _up;
        _middle.transform.position      = middleb + _up;

        foreach (var i in _brainParts) NoRotate(i);

        _lhalf.transform.eulerAngles       = halfr;
        _rhalf.transform.eulerAngles       = halfr;
        _lCerebellum.transform.eulerAngles = lCerebellumr;
        _rCerebellum.transform.eulerAngles = rCerebellumr;
        _skipHR.transform.eulerAngles      = Vector3.zero;
        _skipHL.transform.eulerAngles      = Vector3.zero;
        _long.transform.eulerAngles        = longbr;
        _bridge.transform.eulerAngles      = bridger;
        _middle.transform.eulerAngles      = middlebr;

        sphereChosen       = false;
        _menuScript.isWork = false;
        
        if (_menuScript.markerTimer) _menuScript.ChooseSlice();
        //_globalText.SetActive(true);
        //_localText.SetActive(false);
        //_button.transform.position     -= _temp;
        //_menubutton.transform.position += _temp;
    }
}
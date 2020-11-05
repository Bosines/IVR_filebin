using UnityEngine;

public class RotateDrag : MonoBehaviour {
    private InputManager _inputManager;
    
    private Vector3 _mPrevPos = Vector3.zero, _mPosDelta = Vector3.zero;
    private static Camera _cameraMain;
    private Transform _transform1;
    private float _sensitivity = 1f;

    private void Awake()
    {
        _inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        _cameraMain = Camera.main;
        if (_cameraMain != null) _transform1 = _cameraMain.transform;
    }
    
    public void ChangeSens(float sens)
    {
        _sensitivity = sens;
    }

    private void Update() {
        if (_inputManager.GetKey("Rotate"))
        {
            _mPosDelta = ( Input.mousePosition - _mPrevPos );
            _mPosDelta.x *= _sensitivity;
            _mPosDelta.y *= _sensitivity;
            _mPosDelta.z *= _sensitivity;
            
            if (Vector3.Dot(transform.up, Vector3.up) >= 0) 
                transform.Rotate(transform.up, -Vector3.Dot(_mPosDelta, _transform1.right), Space.World);
            
            else 
                transform.Rotate(transform.up, Vector3.Dot(_mPosDelta, _transform1.right), Space.World);
            
            transform.Rotate(_transform1.right, Vector3.Dot(_mPosDelta, _transform1.up), Space.World);
        }
        _mPrevPos = Input.mousePosition;
    }
}

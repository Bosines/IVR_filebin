using UnityEngine;

public class RotateDrag : MonoBehaviour {
    private Vector3 _mPrevPos = Vector3.zero, _mPosDelta = Vector3.zero, newMousePosition = Vector3.zero;
    private static Camera _cameraMain;
    private Transform _transform1;
    private float sensitivity = 1f;

    private void Awake()
    {
        _cameraMain = Camera.main;
        _transform1 = _cameraMain.transform;
    }
    
    public void ChangeSens(float sens)
    {
        sensitivity = sens;
    }

    private void Update() {
        if (Input.GetButton("Rotate Brain"))
        {
            //newMousePosition = new Vector3(Input.mousePosition.x * sensitivity, Input.mousePosition.y * sensitivity);
            _mPosDelta = ( Input.mousePosition - _mPrevPos );
            _mPosDelta.x *= sensitivity;
            _mPosDelta.y *= sensitivity;
            _mPosDelta.z *= sensitivity;
            
            if (Vector3.Dot(transform.up, Vector3.up) >= 0) 
                transform.Rotate(transform.up, -Vector3.Dot(_mPosDelta, _transform1.right), Space.World);
            
            else 
                transform.Rotate(transform.up, Vector3.Dot(_mPosDelta, _transform1.right), Space.World);
            
            transform.Rotate(_transform1.right, Vector3.Dot(_mPosDelta, _transform1.up), Space.World);
        }
        _mPrevPos = Input.mousePosition;
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class WheelZoom : MonoBehaviour 
 {
     private readonly int minFov = -2;
     private readonly int maxFov = 11;
     private readonly int sensitivity = 20;
     [SerializeField] private Slider zoomBar;
     public GameObject cubeS;
     [NonSerialized] private float _zoom;
     [NonSerialized] public float y = 0.25f;
     [NonSerialized] public float x;
     private void Update() 
     {
         _zoom = cubeS.transform.position.z;
         _zoom += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
         _zoom = Mathf.Clamp(_zoom, minFov, maxFov);
         zoomBar.value = _zoom;
         cubeS.transform.position = new Vector3(x, y,_zoom);
     }
     
     private void ChangeFov(float zoomAmount)
     {
         cubeS.transform.position = new Vector3(0f, 0.25f, zoomAmount);
     }

     private void ButtChange(int change)
     {
         cubeS.transform.position += new Vector3(0f, 0f, change);
     }
 }
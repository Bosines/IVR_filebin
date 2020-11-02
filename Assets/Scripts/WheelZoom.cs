using System;
using UnityEngine;
using UnityEngine.UI;

public class WheelZoom : MonoBehaviour 
 {
     private const int MinFov = -2;
     private const int MaxFov = 11;
     private float sensitivity = 20;
     [SerializeField] private Slider zoomBar;
     public GameObject cubeS;
     private float _zoom;
     [NonSerialized] public float Y = 0.25f, X;
     private void Update() 
     {
         _zoom = cubeS.transform.position.z;
         _zoom += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
         _zoom = Mathf.Clamp(_zoom, MinFov, MaxFov);
         zoomBar.value = _zoom;
         cubeS.transform.position = new Vector3(X, Y,_zoom);
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
﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
    

public class MenuScript : MonoBehaviour
{
    public bool menuVisible;
    public bool isWork;
    public bool categoryOpen;
    public bool wayVisible;
    public bool captionsVisible = true;
    private int _acetTimer = -1;
    private const float TimeDelta = 0.10f;

    private Vector3 _defaultRoll = new Vector3(13.5f, 43.7f);
    //0.2 instead of 225 when screen space
    private readonly Vector3 _right = new Vector3(225, 0f, 0);
    private readonly Vector3 _left = new Vector3(-225, 0f, 0);
    private SphereCollider _brain;
    private Image _categoryMain;
    private CanvasRenderer _categoryRoll;
    [SerializeField] private Image marker;
    public bool isPresent;
    public bool sliceTimer;
    public int slideTimer;
    public bool sliceActive;
    public bool sliceButtVisible;
    
    [SerializeField] private GameObject sceneCube;
    [SerializeField] private GameObject acetHelp;
    public GameObject cube;
    public GameObject presentation;
    public GameObject slices;
    public GameObject settings;
    public GameObject sliceButt;
    public GameObject sliceCategory;

    private GameObject[] _category;
    private GameObject[] _righttag;
    private GameObject[] _longtag;
    private GameObject[] _everythingtag;
    private GameObject[] _captions;
    private GameObject[] _slicecaptions;
    private GameObject[] _fullcaptions;
    private WheelZoom _wz;
    
    [SerializeField] private GameObject currentWay;
    [SerializeField] private GameObject currentLesson;
    [SerializeField] private Image eyeLock;

    private void Awake()
    {
        _categoryMain = GameObject.Find("CategoryMain").GetComponent<Image>();
        _categoryRoll = GameObject.Find("CategoryRoll").GetComponent<CanvasRenderer>();
        _righttag = GameObject.FindGameObjectsWithTag("Right");
        _longtag = GameObject.FindGameObjectsWithTag("Long");
        _everythingtag = GameObject.FindGameObjectsWithTag("Everything");
        _category = GameObject.FindGameObjectsWithTag("Category");
        _captions = GameObject.FindGameObjectsWithTag("Caption");
        _fullcaptions = GameObject.FindGameObjectsWithTag("Full Caption");
        _slicecaptions = GameObject.FindGameObjectsWithTag("Slice Caption");
        _wz = cube.GetComponent<WheelZoom>();
        foreach (GameObject category in _category) category.SetActive(false);
        foreach (GameObject caption in _slicecaptions) caption.SetActive(false);
    }
    
    public void Update()
    {
        if (Input.GetButtonDown("Menu") & !isWork)
            Clicked();
        
        
        if (Input.GetKeyDown(KeyCode.Escape) & sliceTimer)
            SlicesButton();
        

        if (Input.GetKeyDown(KeyCode.N))
            SlicesButton();
        
        
        if (Input.GetKeyDown(KeyCode.Escape) & wayVisible)
            Ways(currentWay);

        if (Input.GetKeyDown(KeyCode.Escape) & sliceActive)
            ChooseSlice();
    }

    private void UnActive()
    {
        foreach (GameObject i in _category) 
            i.SetActive(false);
    }
    
    public void Entity(GameObject category)
    {
        if (isWork) 
            return;
        
        UnActive();
        categoryOpen = false;
        category.SetActive(true);
        categoryOpen = true;
    }

    public void Category(float y)
    {
        if (!isWork)
        {
            _categoryMain.enabled = !_categoryMain.enabled;
            _categoryRoll.GetComponent<Image>().enabled = !_categoryRoll.GetComponent<Image>().enabled;
            
            if (_categoryMain.enabled)
            {
                _defaultRoll = _categoryRoll.transform.position;
                _categoryRoll.transform.position += new Vector3(0f, y);
            } 
            else
            {
                _categoryRoll.transform.position = _defaultRoll;
                Category(y);
            }
        }
    }

    public void Clicked()
    {
        if (captionsVisible) 
            ShowCaption();
        
        if (sliceButtVisible)
            ShowSliceButt();
        
        var thisPos = transform.position;
        Vector3 rightmovement = thisPos + _right;
        Vector3 leftmovement = thisPos + _left;
        if (!isWork)
        {
            if (menuVisible)
            {
                _categoryRoll.transform.position = _defaultRoll;
                UnActive();
                categoryOpen = false;
                StartCoroutine(SmoothMove(leftmovement, TimeDelta, this.gameObject));
                _categoryMain.enabled = false;
                _categoryRoll.GetComponent<Image>().enabled = false;
            }
            else
            {
                StartCoroutine(SmoothMove(rightmovement, TimeDelta, this.gameObject));
                //cube.GetComponent<MoveSphere>().enabled = false;
                Debug.Log("Меню выезжает");
                if (captionsVisible) ShowCaption();
            }
            menuVisible = !menuVisible;
        }
    }

    IEnumerator SmoothMove(Vector3 target, float delta, GameObject movingObject)
    {
        float closeEnough = 2;
        float distance = (movingObject.transform.position - target).magnitude;

        WaitForSeconds wait = new WaitForSeconds(0.005f);
        //WaitForFixedUpdate wait = new WaitForFixedUpdate();
        //WaitForEndOfFrame wait = new WaitForEndOfFrame();
        while (distance >= closeEnough)
        {
            isWork = true;
            
            movingObject.transform.position = Vector3.Lerp(movingObject.transform.position, target, delta);
            yield return wait;            
            distance = (movingObject.transform.position - target).magnitude;
        }
        
        movingObject.transform.position = target;

        isWork = false;
    }

    public void AcetButton(Image arrow)
    {
        var acettp = acetHelp.transform.position;
        arrow.transform.localScale = new Vector3(-_acetTimer, 1, 1);
        Vector3 target;
        
        if (_acetTimer == -1) 
            target = acettp + new Vector3(170, 0 ,0 );
        else 
            target = acettp - new Vector3(170, 0 ,0 );
        
        StartCoroutine(SmoothMove(target, 0.1f, acetHelp));
        
        if (_acetTimer == -1) 
            _acetTimer = 1;
        else 
            _acetTimer = -1;
    }

    public void SlicesButton()
    {
        sliceTimer = !sliceTimer;
        slices.SetActive(sliceTimer);
        if (menuVisible) Clicked();
        if (isPresent & sliceTimer) presentation.SetActive(!sliceTimer);
        if (isPresent) cube.SetActive(!sliceTimer);
        sceneCube.SetActive(!sliceTimer);
        cube.SetActive(!sliceTimer);
    }

    public void ChooseSlice()
    {
        if (!sliceActive)
        {
            foreach (GameObject go in _righttag) go.SetActive(true);
            foreach (GameObject go in _everythingtag) go.SetActive(false);
            foreach (GameObject go in _longtag) go.SetActive(true);
            
            if (captionsVisible) 
                foreach (var caption in _slicecaptions) 
                    caption.SetActive(true);
            
            foreach (var caption in _fullcaptions) 
                caption.SetActive(false);

            _wz.X = -0.15f;
            _wz.Y = 0.25f;
        }
        else
        {
            foreach (GameObject go in _everythingtag) 
                go.SetActive(true);

            foreach (GameObject go in _righttag) 
                go.SetActive(true);
            
            if (captionsVisible) foreach (var caption in _fullcaptions) 
                caption.SetActive(true);
            
            foreach (var caption in _slicecaptions) 
                caption.SetActive(false);

            _wz.X = 0f;
            _wz.Y = 0.25f;
        }
        

        marker.enabled = !sliceActive;
        sliceActive = !sliceActive;
    }

    public void ShowSliceButt()
    {
        if (menuVisible) 
            Clicked();
        
        sliceCategory.SetActive(!sliceButtVisible);
        sliceButtVisible = !sliceButtVisible;
    }
    
    public void ToSlide(int slideNum)
    {
        slideTimer = slideNum;
    }

    public void MediatorLink(GameObject way)
    {
        PresentationMode(currentLesson);
        Ways(way);
    }

    public void PresentationMode(GameObject lesson)
    {
        PresentationMode present = lesson.GetComponent<PresentationMode>();
        present.slide = slideTimer;
        present.WindOfChange();
        sceneCube.SetActive(isPresent);
        //cube.SetActive(isPresent);
        presentation.SetActive(!isPresent);
        lesson.SetActive(!isPresent);
        if (menuVisible) Clicked();
        sliceCategory.SetActive(isPresent);
        sliceButt.SetActive(isPresent);

        isPresent = !isPresent;
        while (wayVisible) Ways(currentWay);
        currentLesson = lesson;
        if (captionsVisible) ShowCaption();
    }

    public void Ways(GameObject way)
    {
        wayVisible = !wayVisible;
        if (isPresent) 
            PresentationMode(currentLesson);
        
        ShowSliceButt();
        currentWay.SetActive(false);
        
        if (way != currentWay) 
            way.SetActive(true);
        
        currentWay = way;
        
        if (captionsVisible)
            ShowCaption();
    }

    public void ShowCaption()
    {
        if (wayVisible)
        {
            return;
        }
        eyeLock.enabled = captionsVisible;
        if (sliceActive) foreach (var caption in _slicecaptions) caption.SetActive(!captionsVisible);
        if (!sliceActive) foreach (var caption in _fullcaptions) caption.SetActive(!captionsVisible);
        foreach (var caption in _captions) caption.SetActive(!captionsVisible);
        captionsVisible = !captionsVisible;
    }    
}
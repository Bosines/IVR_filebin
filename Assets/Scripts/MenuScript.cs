using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
    

public class MenuScript : MonoBehaviour
{
    private InputManager _inputManager;
    
    
    public bool isWork;
    public bool categoryOpen;
    public bool captionsVisible = true;
    private int _acetTimer = -1;
    private const float TimeDelta = 0.10f;

    private Vector3 _defaultRoll = new Vector3(13.5f, 43.7f);
    private SphereCollider _brain;
    private Image _categoryMain;
    private CanvasRenderer _categoryRoll;
    [SerializeField] private Image marker;
    
    [NonSerialized] public bool PresentationVisible, SliceButtVisible, SliceActive, SliceTimer, MenuVisible, WayVisible;
    public int slideTimer;

    private Image _seroCh, _noraCh, _dofaCh, _acetCh;
    private Image _categoryRollImage;

    [SerializeField] private Transform menuLeftPos, menuRightPos, acetRightPos, acetLeftPos;
    
    
    [SerializeField] private GameObject sceneCube;
    [SerializeField] private GameObject acetHelp;
    
    public GameObject cube;
    public GameObject presentation;
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
    [SerializeField] public Settings settings;
    
    [SerializeField] private GameObject currentWay;
    [NonSerialized] private GameObject _currentLesson;
    [SerializeField] private Image eyeLock;

    private void Awake()
    {
        menuLeftPos = GameObject.Find("MenuPointerLeft").transform;
        menuRightPos = GameObject.Find("MenuPointerRight").transform;

        _inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        
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
        
        _acetCh = GameObject.Find("acetChosen").GetComponent<Image>();
        _dofaCh = GameObject.Find("dofaChosen").GetComponent<Image>();
        _noraCh = GameObject.Find("noraChosen").GetComponent<Image>();
        _seroCh = GameObject.Find("seroChosen").GetComponent<Image>();
        
        foreach (GameObject category in _category) category.SetActive(false);
        foreach (GameObject caption in _slicecaptions) caption.SetActive(false);

        _categoryRollImage = _categoryRoll.GetComponent<Image>();
        
        sliceCategory.SetActive(false);
    }
    
    public void Update()
    {
        if (settings.SettingsVisible) return;
        
        if ((_inputManager.GetKeyDown("Menu") || Input.GetKeyDown(KeyCode.Tab)) && !isWork)
            Clicked();
        

        if (_inputManager.GetKeyDown("Slice"))
            ShowSliceButt();
        
        
        if (Input.GetKeyDown(KeyCode.Escape) & WayVisible)
            Ways(currentWay);

        
        if (Input.GetKeyDown(KeyCode.Escape) & SliceActive)
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

    public void Category(Transform categoryTransform)
    {
        if (!isWork)
        {
            _categoryMain.enabled = !_categoryMain.enabled;
            _categoryRoll.GetComponent<Image>().enabled = !_categoryRoll.GetComponent<Image>().enabled;
            
            if (_categoryMain.enabled)
            {
                _defaultRoll = _categoryRoll.transform.position;
                _categoryRoll.transform.position = new Vector3(_categoryRoll.transform.position.x, categoryTransform.position.y);
            } 
            else
            {
                _categoryRoll.transform.position = _defaultRoll;
                Category(categoryTransform);
            }
        }
    }

    public void OpenSettings()
    {
        settings.OpenClose();
    }

    public void Clicked()
    {
        if (captionsVisible) 
            ShowCaption();
        
        if (SliceButtVisible)
            ShowSliceButt();
        
        if (isWork) return;
        if (MenuVisible)
        {
            _categoryRoll.transform.position = _defaultRoll;
            UnActive();
            categoryOpen = false;
            StartCoroutine(SmoothMove(menuLeftPos.position, TimeDelta, this.gameObject));
            _categoryMain.enabled = false;
            _categoryRollImage.enabled = false;
        }
        else
        {
            StartCoroutine(SmoothMove(menuRightPos.position, TimeDelta, this.gameObject));
            if (captionsVisible) ShowCaption();
        }
        MenuVisible = !MenuVisible;
    }

    IEnumerator SmoothMove(Vector3 target, float delta, GameObject movingObject)
    {
        const float closeEnough = 2;
        float distance = (movingObject.transform.position - target).magnitude;

        WaitForSeconds wait = new WaitForSeconds(0.02f);
        
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
        arrow.transform.localScale = new Vector3(-_acetTimer, 1, 1);
        Vector3 target;
        
        if (_acetTimer == -1) 
            target = acetRightPos.position;
        else 
            target = acetLeftPos.position;
        
        StartCoroutine(SmoothMove(target, 0.1f, acetHelp));
        
        if (_acetTimer == -1) 
            _acetTimer = 1;
        else 
            _acetTimer = -1;
    }

    public void ChooseSlice()
    {
        if (!SliceActive)
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
        

        marker.enabled = !SliceActive;
        SliceActive = !SliceActive;
    }

    public void ShowSliceButt()
    {
        if (isWork) return;
        if (MenuVisible) 
            Clicked();
        
        sliceCategory.SetActive(!SliceButtVisible);
        SliceButtVisible = !SliceButtVisible;
    }
    
    public void ToSlide(int slideNum)
    {
        slideTimer = slideNum;
    }

    public void MediatorLink(GameObject way)
    {
        PresentationMode(_currentLesson);
        Ways(way);
    }

    public void PresentationMode(GameObject lesson)
    {
        PresentationMode present = lesson.GetComponent<PresentationMode>();
        present.slide = slideTimer;
        present.WindOfChange();
        sceneCube.SetActive(PresentationVisible);
        //cube.SetActive(isPresent);
        presentation.SetActive(!PresentationVisible);
        lesson.SetActive(!PresentationVisible);
        if (MenuVisible) Clicked();
        //sliceCategory.SetActive(PresentationVisible);
        sliceButt.SetActive(PresentationVisible);

        PresentationVisible = !PresentationVisible;
        if (WayVisible) Ways(currentWay);
        
        _currentLesson = lesson;
        if (captionsVisible) ShowCaption();
        if (lesson == present.rc) present.TurnModel();
    }

    public void Ways(GameObject way)
    {
        if (WayVisible)
        {
            if (way == currentWay)
            {
                //выключаем
                way.SetActive(false);
                WayVisible = false;
                ShowChosen(true);
            }
            else
            {
                //переключаем
                currentWay.SetActive(false);
                way.SetActive(true);
                WayVisible = true;
                currentWay = way;
                ShowChosen(false);
            }
        }
        else
        {
            //включаем
            currentWay = way;
            way.SetActive(true);
            WayVisible = true;
            ShowChosen(false);
            SliceButtVisible = true;
        }
        
        
        if (PresentationVisible) 
            PresentationMode(_currentLesson);
        
        
        if (captionsVisible)
            ShowCaption();

        void ShowChosen(bool turningOff)
        {
            _seroCh.enabled = _noraCh.enabled = _acetCh.enabled = _dofaCh.enabled = false;
            if (turningOff) return;
            switch (way.name)
            {
                case "Acetilholin":
                    _acetCh.enabled = true;
                    break;
                case "dofamin":
                    _dofaCh.enabled = true;
                    break;
                case "noradrenalin":
                    _noraCh.enabled = true;
                    break;
                case "serotonin":
                    _seroCh.enabled = true;
                    break;
            }
        }
        
    }

    public void ShowCaption()
    {
        if (WayVisible)
        {
            return;
        }
        eyeLock.enabled = captionsVisible;
        if (SliceActive) foreach (var caption in _slicecaptions) caption.SetActive(!captionsVisible);
        if (!SliceActive) foreach (var caption in _fullcaptions) caption.SetActive(!captionsVisible);
        foreach (var caption in _captions) caption.SetActive(!captionsVisible);
        captionsVisible = !captionsVisible;
    }    
}
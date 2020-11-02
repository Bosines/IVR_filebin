using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
    

public class MenuScript : MonoBehaviour
{
    public bool isWork;
    public bool categoryOpen;
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
    
    public bool presentationVisible, sliceButtVisible, sliceActive, sliceTimer, menuVisible, wayVisible;
    public int slideTimer;

    private Image seroCh, noraCh, dofaCh, acetCh;

    [SerializeField] private Transform MenuLeftPos, MenuRightPos;
    
    
    [SerializeField] private GameObject sceneCube;
    [SerializeField] private GameObject acetHelp;
    
    public GameObject cube;
    public GameObject presentation;
    public GameObject slices;
    public GameObject SettingsGameObject;
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
    [SerializeField] public Settings Settings;
    
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
        
        acetCh = GameObject.Find("acetChosen").GetComponent<Image>();
        dofaCh = GameObject.Find("dofaChosen").GetComponent<Image>();
        noraCh = GameObject.Find("noraChosen").GetComponent<Image>();
        seroCh = GameObject.Find("seroChosen").GetComponent<Image>();
        
        foreach (GameObject category in _category) category.SetActive(false);
        foreach (GameObject caption in _slicecaptions) caption.SetActive(false);
        
        sliceCategory.SetActive(false);
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

    public void OpenSettings()
    {
        Settings.OpenClose();
    }

    public void Clicked()
    {
        if (captionsVisible) 
            ShowCaption();
        
        if (sliceButtVisible)
            ShowSliceButt();
        
        var thisPos = transform.position;
        Vector3 rightmovement = MenuRightPos.position;
        Vector3 leftmovement =  MenuLeftPos.position;
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
        if (presentationVisible & sliceTimer) presentation.SetActive(!sliceTimer);
        if (presentationVisible) cube.SetActive(!sliceTimer);
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
        sceneCube.SetActive(presentationVisible);
        //cube.SetActive(isPresent);
        presentation.SetActive(!presentationVisible);
        lesson.SetActive(!presentationVisible);
        if (menuVisible) Clicked();
        sliceCategory.SetActive(presentationVisible);
        sliceButt.SetActive(presentationVisible);

        presentationVisible = !presentationVisible;
        if (wayVisible) Ways(currentWay);
        
        currentLesson = lesson;
        if (captionsVisible) ShowCaption();
        if (lesson == present.rc) present.TurnModel();
    }

    public void Ways(GameObject way)
    {
        /*if (sliceButtVisible)
            ShowSliceButt();*/
        
        if (wayVisible)
        {
            if (way == currentWay)
            {
                //выключаем
                way.SetActive(false);
                wayVisible = false;
                showchosen(true);
            }
            else
            {
                //переключаем
                currentWay.SetActive(false);
                way.SetActive(true);
                wayVisible = true;
                currentWay = way;
                showchosen(false);
            }
        }
        else
        {
            //включаем
            currentWay.SetActive(false);
            currentWay = way;
            way.SetActive(true);
            wayVisible = true;
            showchosen(false);
        }
        
        
        if (presentationVisible) 
            PresentationMode(currentLesson);
        
        
        if (captionsVisible)
            ShowCaption();

        void showchosen(bool TurningOff)
        {
            seroCh.enabled = noraCh.enabled = acetCh.enabled = dofaCh.enabled = false;
            if (TurningOff) return;
            if (way == GameObject.Find("Acetilholin"))
            {
                acetCh.enabled = true;
            } 
            else if (way == GameObject.Find("dofamin"))
            {
               dofaCh.enabled = true;
            } 
            else if (way == GameObject.Find("noradrenalin"))
            {
                noraCh.enabled = true;
            } 
            else if (way == GameObject.Find("serotonin"))
            {
                seroCh.enabled = true;
            }
        }
        
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
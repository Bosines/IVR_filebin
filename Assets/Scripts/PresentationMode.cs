using UnityEngine;
using UnityEngine.UI;

public class PresentationMode : MonoBehaviour
{
    public int slide = 1;
    [SerializeField] private int totalSlides;
    [SerializeField] private Button leftButt;
    [SerializeField] private Button rightButt;
    [SerializeField] private GameObject slide1;
    [SerializeField] private GameObject slide2;
    [SerializeField] private GameObject slide3;
    [SerializeField] private GameObject slide4;
    [SerializeField] private GameObject slide5;
    [SerializeField] private GameObject slide6;
    [SerializeField] private GameObject slide7;
    [SerializeField] private GameObject slide8;
    [SerializeField] private GameObject slide9;
    [SerializeField] private GameObject slide10;
    [SerializeField] private GameObject slide11;
    [SerializeField] private GameObject slide12;
    [SerializeField] private GameObject slide13;
    [SerializeField] private GameObject slide14;
    [SerializeField] private GameObject slide15;
    [SerializeField] private GameObject slide16;
    [SerializeField] private GameObject slide17;
    [SerializeField] private GameObject slide18;
    [SerializeField] private GameObject slide19;
    [SerializeField] private GameObject slide20;
    
    //[SerializeField] private GameObject brain;
    [SerializeField] private GameObject nr;
    [SerializeField] private GameObject rp;
    [SerializeField] private GameObject ap;
    [SerializeField] private GameObject sn;
    [SerializeField] private GameObject ms;
    [SerializeField] public GameObject rc;
    [SerializeField] private GameObject hm;
    [SerializeField] private GameObject this1;
    
    //[SerializeField] private GameObject SkipHR;
    
    private MenuScript _menu;
    private ModelLink _modelLink;
    //[NonSerialized] public bool ModelLinkWork;
    

    public void Awake()
    {
        //SkipHR = GameObject.Find("SkipHR");
        _modelLink = GameObject.Find("Menu").GetComponent<ModelLink>();
    }

    public void Start()
    {
        this1 = gameObject;
        _menu = GameObject.Find("Menu").GetComponent<MenuScript>();
    }

    private void DeactSlides(GameObject slideNum)
    {
        if (slide1 != null) slide1.SetActive(false);
        if (slide2 != null) slide2.SetActive(false);
        if (slide3 != null) slide3.SetActive(false);
        if (slide4 != null) slide4.SetActive(false);
        if (slide5 != null) slide5.SetActive(false);
        if (slide6 != null) slide6.SetActive(false);
        if (slide7 != null) slide7.SetActive(false);
        if (slide8 != null) slide8.SetActive(false);
        if (slide9 != null) slide9.SetActive(false);
        if (slide10 != null) slide10.SetActive(false);
        if (slide11 != null) slide11.SetActive(false);
        if (slide12 != null) slide12.SetActive(false);
        if (slide13 != null) slide13.SetActive(false);
        if (slide14 != null) slide14.SetActive(false);
        if (slide15 != null) slide15.SetActive(false);
        if (slide16 != null) slide16.SetActive(false);
        if (slide17 != null) slide17.SetActive(false);
        if (slide18 != null) slide18.SetActive(false);
        if (slide19 != null) slide19.SetActive(false);
        if (slide20 != null) slide20.SetActive(false);
        slideNum.SetActive(true);
        ShowButt();
    }

    public void ArrowButton(bool isRight)
    {
        if (isRight)
        {
            if (slide < totalSlides) slide++;
        }
        else
        {
            if (slide > 1) slide--;
        }

        if (rc.activeInHierarchy)
        {
            TurnModel();
        }

        WindOfChange();
    }

    public void TurnModel()
    {
        switch (slide)
        {
            case 6:
                _modelLink.wz.Y = 0.6f;
                break;
            case 7:
                _modelLink.ModelLinkvoid("Long");
                break;
            case 8:
                _modelLink.ModelLinkvoid("Bridge");
                break;
            case 9:
                _modelLink.ModelLinkvoid("Cerebellum");
                break;
            case 10:
                _modelLink.ModelLinkvoid("Middle");
                break;
            case 11:
                _modelLink.ModelLinkvoid("Skip"); 
                break;
            case 12:
                _modelLink.ModelLinkvoid("Half");
                break;
        }
    }
    
    public void WindOfChange()
    {
        switch (slide)
        {
            case 1:
                DeactSlides(slide1);
                break;
            case 2:
                DeactSlides(slide2);
                break;
            case 3:
                DeactSlides(slide3);
                break;
            case 4:
                DeactSlides(slide4);
                break;
            case 5:
                DeactSlides(slide5);
                break;
            case 6:
                DeactSlides(slide6);
                break;
            case 7:
                DeactSlides(slide7);
                break;
            case 8:
                DeactSlides(slide8);
                break;
            case 9:
                DeactSlides(slide9);
                break;
            case 10:
                DeactSlides(slide10);
                break;
            case 11:
                DeactSlides(slide11);
                break;
            case 12:
                DeactSlides(slide12);
                break;
            case 13:
                DeactSlides(slide13);
                break;
            case 14:
                DeactSlides(slide14);
                break;
            case 15:
                DeactSlides(slide15);
                break;
            case 16:
                DeactSlides(slide16);
                break;
            case 17:
                DeactSlides(slide17);
                break;
            case 18:
                DeactSlides(slide18);
                break;
            case 19:
                DeactSlides(slide19);
                break;
            case 20:
                DeactSlides(slide20);
                break;
        }
    }

    public void ShowButt()
    {
        if (slide == 1)
        {
            ShowButthelp(true, false);
        }
        else if (slide == totalSlides)
        {
            ShowButthelp(false, true);
        }
        else
        {
            ShowButthelp(true, true);
        }
    }

    private void ShowButthelp(bool right, bool left)
    {
        rightButt.enabled = right;
        leftButt.enabled = left;
    }
    

    private void Reload(GameObject anime)
    {
        if (!_menu.settings.AnimationsTurnedOn) return;
        anime.SetActive(false);
        anime.SetActive(true);
    }

    public void SlideLink(int link)
    {
        slide = link;
        ShowButt();
        WindOfChange();
        TurnModel();
    }

    public void AnotherSlideLink(int link)
    {
        int link2 = link;
        int first = link2;
        while (link2 > 10) first = (link2 /= 10) % 10;

        switch (first)
        {
            case 1:
                AnotherSlideHelper(nr);
                break;
            case 2:
                AnotherSlideHelper(rp);
                break;
            case 3:
                AnotherSlideHelper(ap);
                break;
            case 4:
                AnotherSlideHelper(sn);
                break;
            case 5:
                AnotherSlideHelper(ms);
                break;
            case 6:
                AnotherSlideHelper(rc);
                break;
            case 7:
                AnotherSlideHelper(hm);
                break;
        }

        void AnotherSlideHelper(GameObject category)
        {
            category.SetActive(true);
            PresentationMode pm = category.GetComponent<PresentationMode>();
            pm.slide = link - first * 100;
            pm.ShowButt();
            pm.WindOfChange();
            if (category != this1) this1.SetActive(false);
            pm.TurnModel();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            ArrowButton(true);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            ArrowButton(false);

        if (Input.GetKeyDown(KeyCode.Escape))
            _menu.PresentationMode(this1);
    }

}
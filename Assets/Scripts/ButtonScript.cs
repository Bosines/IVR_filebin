using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject pic1;
    [SerializeField] private GameObject pic2;
    [SerializeField] private GameObject pic12;
    [SerializeField] private GameObject pic22;
    private bool _timer1;
    private bool _timer2;
    
    private void SnButton(bool is1)
    {
        if (is1)
        {
            pic1.SetActive(_timer1);
            pic12.SetActive(!_timer1);
            _timer1 = !_timer1;
        } else {
            pic2.SetActive(_timer2);
            pic22.SetActive(!_timer2);
            _timer2 = !_timer2;
        }
    }

    public void RcButton()
    {
        pic1.SetActive(!_timer1);
        _timer1 = !_timer1;
    }
}

using UnityEngine;
using UnityEngine.UI;
public class TimeTaken : MonoBehaviour
{
    public Text TxtTimeTaken;
    private float _timeTaken = 0;
    
    private void OnEnable()
    {
        EventManager.OnInit += Init;
    }
    private void OnDisable()
    {
        EventManager.OnInit -= Init;
    }
    void Init()
    {
        _timeTaken = 0;
        TxtTimeTaken.text = "Time : " + _timeTaken + "s";
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        if (!GameManager.Instance.IsAppPause)
        {
            _timeTaken += Time.deltaTime;
            TxtTimeTaken.text = "Time : " + (int)_timeTaken + "s";
            Constant.TimeTaken = (int)_timeTaken;
        }
    }
}

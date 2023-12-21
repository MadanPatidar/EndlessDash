using UnityEngine;
public class Heart : MonoBehaviour
{
    public GameObject CrossLine;
    void Start()
    {
        CrossLineHide();
    }
    public void CrossLineHide()
    {
        CrossLine.SetActive(false);
    }
    public void CrossLineShow()
    {
        CrossLine.SetActive(true);
    }
}
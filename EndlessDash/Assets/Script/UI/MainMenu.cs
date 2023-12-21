using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public Button PlayButton;
    void Start()
    {
        PlayButton.onClick.AddListener(PlayButtonTap);
    }
    void PlayButtonTap()
    {
        GameManager.Instance.ActiveInGameHud();
        EventManager.RaiseInit();
    }
}
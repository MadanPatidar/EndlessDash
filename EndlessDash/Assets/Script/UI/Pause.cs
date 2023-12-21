using UnityEngine;
using UnityEngine.UI;
public class Pause : MonoBehaviour
{
    public Button ResumeButton;
    public Button MainMenuButton;
    void Start()
    {
        ResumeButton.onClick.AddListener(ResumeButtonTap);
        MainMenuButton.onClick.AddListener(MainMenuButtonTap);
    }
    void ResumeButtonTap()
    {
        GameManager.Instance.ActiveInGameHud();   
    }
    void MainMenuButtonTap()
    {
        GameManager.Instance.ActiveMainMenu();
    }
}
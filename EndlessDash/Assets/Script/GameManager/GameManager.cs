using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField]
    private InGameHud _inGameHud;
    [SerializeField]
    private MainMenu _mainMenu;
    [SerializeField]
    private Pause _pause;
    [SerializeField]
    private GameOver _gameOver;

    //----------------//
    public static GameManager Instance;
    private bool _isPause;
    public bool IsAppPause
    {
        get
        {
            return _isPause;
        }
    }

    private void OnEnable()
    {
        EventManager.OnAppPause += AppPause;
    }
    private void OnDisable()
    {
        EventManager.OnAppPause -= AppPause;
    }
    void AppPause(bool isPause)
    {
        _isPause = isPause;
    }

    private void Awake()
    {
        Instance = this;
        _isPause = true;
        ActiveMainMenu();
    }
    public void ActiveMainMenu()
    {
        _mainMenu.gameObject.SetActive(true);
        _inGameHud.gameObject.SetActive(false);
        _pause.gameObject.SetActive(false);
        _gameOver.gameObject.SetActive(false);

        EventManager.RaiseAppPause(true);
    }
    public void ActivePauseScreen()
    {
        _mainMenu.gameObject.SetActive(false);
        _inGameHud.gameObject.SetActive(false);
        _pause.gameObject.SetActive(true);
        _gameOver.gameObject.SetActive(false);

        EventManager.RaiseAppPause(true);
    }
    public void ActiveInGameHud()
    {
        _mainMenu.gameObject.SetActive(false);
        _inGameHud.gameObject.SetActive(true);
        _pause.gameObject.SetActive(false);
        _gameOver.gameObject.SetActive(false);

        EventManager.RaiseAppPause(false);
    }
    public void ActiveGameOver()
    {
        _mainMenu.gameObject.SetActive(false);
        _inGameHud.gameObject.SetActive(false);
        _pause.gameObject.SetActive(false);
        _gameOver.gameObject.SetActive(true);

        EventManager.RaiseAppPause(true);
    }
}
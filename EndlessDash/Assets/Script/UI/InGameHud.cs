using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameHud : MonoBehaviour
{
    public Text TxtCoins;

    [Header("Pause-Button")]
    public Button PauseButton;

    [Header("Buttons")]
    public GameObject Buttons;
    public Button LeftButton;
    public Button RightButton;
    public Button JumpButton;
    public Button SlideButton;

    private int _coins = 0;

    void Start()
    {

#if UNITY_EDITOR
        Buttons.SetActive(true);
        LeftButton.onClick.AddListener(LeftButtonTap);
        RightButton.onClick.AddListener(RightButtonTap);
        JumpButton.onClick.AddListener(JumpButtonTap);
        SlideButton.onClick.AddListener(SlideButtonTap);
#else
        Buttons.SetActive(false);
#endif    
        PauseButton.onClick.AddListener(PauseButtonTap);
    }      

    void Init()
    {
        _coins = 0;
        UpdateCoinsText();
    }
    void UpdateCoinsText()
    {
        TxtCoins.text = "Coins : " + _coins;
        Constant.Coins = _coins;
    }   

    private void OnEnable()
    {
        EventManager.OnCoinCollection += HandleCoinCollection;  
        EventManager.OnInit += Init;
    }
    private void OnDisable()
    {
        EventManager.OnCoinCollection -= HandleCoinCollection; 
        EventManager.OnInit -= Init;
    }
    void HandleCoinCollection(int coin)
    {
        _coins += coin;
        UpdateCoinsText();
    }
    void PauseButtonTap()
    {
        GameManager.Instance.ActivePauseScreen();
    }
    void LeftButtonTap()
    {
        EventManager.RaiseAddActions((int)Constant.HeroAction.TurnLeft);
    }
    void RightButtonTap()
    {
        EventManager.RaiseAddActions((int)Constant.HeroAction.TurnRight);
    }
    void JumpButtonTap()
    {
        EventManager.RaiseAddActions((int)Constant.HeroAction.Jump);
    }
    void SlideButtonTap()
    {
        EventManager.RaiseAddActions((int)Constant.HeroAction.Slide);
    }
}
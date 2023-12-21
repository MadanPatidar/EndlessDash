using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text TxtCoins;
    public Text TxtDistance;
    public Text TxtTime;
    public Button MainMenuButton;

    void Start()
    {
        MainMenuButton.onClick.AddListener(MainMenuButtonTap);
    }
    private void OnEnable()
    {
        TxtCoins.text = "Coin's : " + Constant.Coins;
        TxtDistance.text = "Distance : " + Constant.DistanceCover + "Mtr";
        TxtTime.text = "Time : " + Constant.TimeTaken + "s";
    }
    void MainMenuButtonTap()
    {
        GameManager.Instance.ActiveMainMenu();
    }
}
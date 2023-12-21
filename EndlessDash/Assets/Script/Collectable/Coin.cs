using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    public int CoinToCollect = 1;
    private void Start()
    {
        transform.DORotate(new Vector3(0, 180, 0), 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InSine);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.PLAYER_TAG))
        {
            // Handle coin collection
            EventManager.RaiseCoinCollection(CoinToCollect);
            gameObject.SetActive(false);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HeartHud : MonoBehaviour
{
    public Image RedOverLay;
    public Transform TrParentOfHeartHud;
    public GameObject PrefabHeart;
    public int MaxHealth = 3;

    private int _currentHealth;
    private List<Heart> _listHearts = new List<Heart>();    

    void Init()
    {        
        foreach (Heart heart in _listHearts)
        {
            Destroy(heart.gameObject);
        }
        _currentHealth = MaxHealth;
        DisplayHeart();
    }

    void DisplayHeart()
    {
        _listHearts.Clear();
        for (int i = 0; i < MaxHealth; i++)
        {
            GameObject goHeart = Instantiate(PrefabHeart, TrParentOfHeartHud);
            Heart heart = goHeart.GetComponent<Heart>();
            _listHearts.Add(heart);
        }
    }
    private void OnEnable()
    {
        EventManager.OnTakeDamage += TakeDamage;
        EventManager.OnInit += Init;
    }
    private void OnDisable()
    {
        EventManager.OnTakeDamage -= TakeDamage;
        EventManager.OnInit -= Init;
    }
    void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        int i = 0;
        foreach (Heart heart in _listHearts)
        {
            heart.CrossLineHide();
            if (i >= _currentHealth)
            {
                heart.CrossLineShow();
            }
            i++;
        }

        ShowCharacterHurtEffect();

        // Check for zero health
        if (_currentHealth <= 0)
        {
            StartCoroutine(EndGame());
        }
    }
    IEnumerator EndGame()
    {
       yield return new WaitForSeconds(0.75f);
       GameManager.Instance.ActiveGameOver();
    }

    void ShowCharacterHurtEffect()
    {
        RedOverLay.DOFade(0.5f, 0);
        RedOverLay.DOFade(0, 1);
    }
}
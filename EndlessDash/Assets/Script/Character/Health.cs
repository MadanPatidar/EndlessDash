using UnityEngine;

public class Health : MonoBehaviour
{
    public int Live = 3;
    public void TakeDamage(int damage)
    {
        Live--;
        EventManager.RaiseTakeDamage(damage);
        ShowDamageEffect();
    }
    void ShowDamageEffect()
    {
        // Implement damage effect
    }
    void Init()
    {
        Live = 3;
    }
    private void OnEnable()
    {
        EventManager.OnInit += Init;
    }
    private void OnDisable()
    {
        EventManager.OnInit -= Init;
    }
}
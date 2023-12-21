using UnityEngine;
[CreateAssetMenu(menuName = "HeroConfig", order = 0, fileName = "New HeroConfig")]
public class HeroConfig : ScriptableObject
{
    public float DefaultMoveSpeed = 11;
    public float MaxMoveSpeed = 30;
    public float MoveSpeedIncFactor = 0.2f;
}
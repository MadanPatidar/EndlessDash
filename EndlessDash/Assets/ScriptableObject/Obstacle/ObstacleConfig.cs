
using UnityEngine;

[CreateAssetMenu(menuName = "ObstacleConfig", order = 3, fileName = "New ObstacleConfig")]
public class ObstacleConfig : ScriptableObject
{
    public float PosY = 0;
    public int Damage = 1;

    [Range(0f, 10f)]
    public float Weight = 1;
}
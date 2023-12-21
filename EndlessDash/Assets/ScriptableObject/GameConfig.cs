using UnityEngine;
[CreateAssetMenu(menuName = "GameConfig", order = 1, fileName = "New GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Road")]
    public int NumRoads = 10;
    public int RoadGap = 56;
    public float RoadResetRate = 0.25f;

    [Header("Coin")]
    public int CoinInitZ = 50;
    public int NumCoins = 150;
    [Range(5, 20)]
    public int CoinGap = 7;
    public float CoinResetRate = 0.2f;

    [Header("Obstacles")]
    public int ObstacleInitZ = 150;
    public int NumObstacles = 15;
    [Range(40, 100)]
    public int ObstaclesGap = 60;
    public float ObstaclesResetRate = 0.2f;
}
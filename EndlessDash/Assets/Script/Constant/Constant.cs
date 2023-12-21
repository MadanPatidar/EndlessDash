using UnityEngine;
public class Constant
{
    public static string PLAYER_TAG = "Player";
    public enum HeroAction
    {
        Idle,
        Run,
        TurnLeft,
        TurnRight,
        Jump,
        Slide
    }
    public enum HeroAnimType
    {
        Idle = -1,
        Run = 0,
        Left = 3,
        Right = 4,
        Jump = 1,
        Slide = 2
    }
    //Time in milliseconds
    public enum AnimEndTime
    {
        Lefl = 645,
        Right = 645,
        Jump = 742,
        Slide = 773
    }

    public static int Coins
    {
        get
        {
            return PlayerPrefs.GetInt("coins");
        }
        set
        {
            PlayerPrefs.SetInt("coins", value);
        }
    }
    public static int DistanceCover
    {
        get
        {
            return PlayerPrefs.GetInt("distance");
        }
        set
        {
            PlayerPrefs.SetInt("distance", value);
        }
    }
    public static int TimeTaken
    {
        get
        {
            return PlayerPrefs.GetInt("time");
        }
        set
        {
            PlayerPrefs.SetInt("time", value);
        }
    }
}
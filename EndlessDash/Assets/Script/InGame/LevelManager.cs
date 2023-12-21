using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameConfig GameConfig;

    [SerializeField]
    private GameObject _roadPrefab;
    [SerializeField]
    private List<GameObject> _obstaclePrefabs = new List<GameObject>();

    [SerializeField]
    private GameObject _coinPrefab;
    public Transform PlayerTransform;

    [Header ("Road")]
    int NumRoads;
    int RoadGap;
    float RoadResetRate;  
    List<GameObject> _listRoadPrefab = new List<GameObject>();

    [Header("Coin")]
    int CoinInitZ;
    int NumCoins;
    int CoinGap;
    float CoinResetRate;
    List<GameObject> _listCoinPrefab = new List<GameObject>();

    [Header("Obstacles")]
    int ObstacleInitZ;
    int NumObstacles;
    int ObstaclesGap;
    float ObstaclesResetRate;
    List<GameObject> _listObstaclesPrefab = new List<GameObject>();

    IEnumerator _coroutineReSetRoadPos = null;
    IEnumerator _coroutineReSetCoinPos = null;
    IEnumerator _coroutineReSetObstaclePos = null;
    private void Awake()
    {
        NumRoads = GameConfig.NumRoads;
        RoadGap = GameConfig.RoadGap;
        RoadResetRate = GameConfig.RoadResetRate;

        CoinInitZ = GameConfig.CoinInitZ;
        NumCoins = GameConfig.NumCoins;
        CoinGap = GameConfig.CoinGap;
        CoinResetRate = GameConfig.CoinResetRate;

        ObstacleInitZ = GameConfig.ObstacleInitZ;
        NumObstacles = GameConfig.NumObstacles;
        ObstaclesGap = GameConfig.ObstaclesGap;
        ObstaclesResetRate = GameConfig.ObstaclesResetRate;
    }

    private void OnEnable()
    {
        EventManager.OnInit += Init;
        EventManager.OnTakeDamage += OnTakeDamage;
    }
    private void OnDisable()
    {
        EventManager.OnInit -= Init;
        EventManager.OnTakeDamage -= OnTakeDamage;

        if (_coroutineReSetRoadPos != null)
            StopCoroutine(_coroutineReSetRoadPos);

        if (_coroutineReSetCoinPos != null)
            StopCoroutine(_coroutineReSetCoinPos);

        if (_coroutineReSetObstaclePos != null)
            StopCoroutine(_coroutineReSetObstaclePos);
    }
    void Init()
    {
        foreach (GameObject _go in _listRoadPrefab)
        {
            Destroy(_go);
        }
        foreach (GameObject _go in _listCoinPrefab)
        {
            Destroy(_go);
        }
        foreach (GameObject _go in _listObstaclesPrefab)
        {
            Destroy(_go);
        }

        if (_coroutineReSetRoadPos != null)
            StopCoroutine(_coroutineReSetRoadPos);

        if (_coroutineReSetCoinPos != null)
            StopCoroutine(_coroutineReSetCoinPos);

        if (_coroutineReSetObstaclePos != null)
            StopCoroutine(_coroutineReSetObstaclePos);

        PlayerTransform.gameObject.SetActive(true);
        PlayerTransform.transform.localPosition = new Vector3(0, 1, 0);
        InitRoad();
        InitCoin();
        InitObstacles();
    }

    #region Road
    void InitRoad()
    {
        // Create road
        _listRoadPrefab.Clear();
        for (int i = 0; i < NumRoads; i++)
        {
           GameObject goRoad =  Instantiate(_roadPrefab, new Vector3(0, 0, i * RoadGap), Quaternion.identity);
            _listRoadPrefab.Add(goRoad);
        }

        StartRoadCoroutine();
    }

    void StartRoadCoroutine()
    {
        if (_coroutineReSetRoadPos != null)
            StopCoroutine(_coroutineReSetRoadPos);

        _coroutineReSetRoadPos = CheckAndReSetRoad();
        StartCoroutine(_coroutineReSetRoadPos);
    }

    IEnumerator CheckAndReSetRoad()
    {
        yield return new WaitForSeconds(RoadResetRate);

        foreach (GameObject _go in _listRoadPrefab)
        {
            if (_go.transform.localPosition.z < PlayerTransform.localPosition.z - RoadGap)
            {
                _listRoadPrefab.Remove(_go);
                Vector3 vector3 = _go.transform.localPosition;
                vector3.z = _listRoadPrefab[_listRoadPrefab.Count - 1].transform.localPosition.z + RoadGap;
                _go.transform.localPosition = vector3;
                _listRoadPrefab.Add(_go);
                break;
            }
        }

        StartRoadCoroutine();
    }
    #endregion

    #region Coin
    void InitCoin()
    {
        // Create Coins
        _listCoinPrefab.Clear();
        for (int i = 0; i < NumCoins; i++)
        {
            int posX = Random.Range(-1, 2)*2;
            GameObject goRoad = Instantiate(_coinPrefab, new Vector3(posX, 0.75f, i * CoinGap + CoinInitZ), Quaternion.identity);
            _listCoinPrefab.Add(goRoad);
        }

        StartCoinCoroutine();
    }

    void StartCoinCoroutine()
    {
        if (_coroutineReSetCoinPos != null)
            StopCoroutine(_coroutineReSetCoinPos);

        _coroutineReSetCoinPos = CheckAndReSetCoin();
        StartCoroutine(_coroutineReSetCoinPos);
    }
    IEnumerator CheckAndReSetCoin()
    {
        yield return new WaitForSeconds(CoinResetRate);

        foreach (GameObject _go in _listCoinPrefab)
        {
            if (_go.transform.localPosition.z < PlayerTransform.localPosition.z - CoinGap)
            {
                _listCoinPrefab.Remove(_go);
                Vector3 vector3 = _go.transform.localPosition;
                vector3.z = _listCoinPrefab[_listCoinPrefab.Count - 1].transform.localPosition.z + CoinGap;
                int posX = Random.Range(-1, 2) * 2;
                vector3.x = posX;
                _go.transform.localPosition = vector3;
                _listCoinPrefab.Add(_go);
                _go.SetActive(true);
                break;
            }
        }

        StartCoinCoroutine();
    }
    #endregion

    #region Obstacles

    private static System.Random random = new System.Random();
    // Get a random index based on weights
    public static int GetRandomIndex(List<float> weights)
    {
        // Calculate total weight
        float totalWeight = 0f;
        foreach (float weight in weights)
        {
            totalWeight += weight;
        }

        // Generate a random value between 0 and totalWeight
        float randomValue = (float)random.NextDouble() * totalWeight;

        // Find the index corresponding to the random value
        float cumulativeWeight = 0f;
        for (int i = 0; i < weights.Count; i++)
        {
            cumulativeWeight += weights[i];
            if (randomValue <= cumulativeWeight)
            {
                return i;
            }
        }

        // This should not happen, but just in case
        return weights.Count - 1;
    }

    void InitObstacles()
    {
        // Create Obstacless
        _listObstaclesPrefab.Clear();

        List<float> weights = new List<float>();
        foreach (GameObject _go in _obstaclePrefabs)
        {
            Obstacle obstacle = _go.GetComponent<Obstacle>();
            float weight = obstacle.Weight();
            weights.Add(weight);

          //  Debug.LogError(" weight : " + weight);
        }

        for (int i = 0; i < NumObstacles; i++)
        {
            int posX = Random.Range(-1, 2) * 2;            

            // Get a random index based on weights
            int randomIndex = GetRandomIndex(weights);
          //  Debug.LogError("Random Index: " + randomIndex);            
            //--
            GameObject goRoad = Instantiate(_obstaclePrefabs[randomIndex], new Vector3(posX, 0, i * ObstaclesGap + ObstacleInitZ), Quaternion.identity);
            goRoad.GetComponent<Obstacle>().SetPosY();
            _listObstaclesPrefab.Add(goRoad);
        }

        StartObstaclesCoroutine();
    }

    void StartObstaclesCoroutine()
    {
        if (_coroutineReSetObstaclePos != null)
            StopCoroutine(_coroutineReSetObstaclePos);

        _coroutineReSetObstaclePos = CheckAndReSetObstacles();
        StartCoroutine(_coroutineReSetObstaclePos);
    }
    IEnumerator CheckAndReSetObstacles()
    {
        yield return new WaitForSeconds(ObstaclesResetRate);

        foreach (GameObject _go in _listObstaclesPrefab)
        {
            if (_go.transform.localPosition.z < PlayerTransform.localPosition.z - ObstaclesGap)
            {
                _listObstaclesPrefab.Remove(_go);
                Vector3 vector3 = _go.transform.localPosition;
                vector3.z = _listObstaclesPrefab[_listObstaclesPrefab.Count - 1].transform.localPosition.z + ObstaclesGap;
                int posX = Random.Range(-1, 2) * 2;
                vector3.x = posX;
                _go.transform.localPosition = vector3;
                _listObstaclesPrefab.Add(_go);
                _go.SetActive(true);
                break;
            }
        }

        StartObstaclesCoroutine();
    }
    #endregion
    void OnTakeDamage(int damage)
    {
        HideCoinsNearByCharacter(5);
        HideObstaclesNearByCharacter(50);
    }
    void  HideCoinsNearByCharacter(int range)
    {
        foreach (GameObject _go in _listCoinPrefab)
        {
            if (_go.transform.localPosition.z > (PlayerTransform.localPosition.z - range) &&
                _go.transform.localPosition.z < (PlayerTransform.localPosition.z + range))
            {
                _go.SetActive(false);
            }
        }
    }

    void HideObstaclesNearByCharacter(int range)
    {
        foreach (GameObject _go in _listObstaclesPrefab)
        {
            if (_go.transform.localPosition.z > (PlayerTransform.localPosition.z - range) &&
                _go.transform.localPosition.z < (PlayerTransform.localPosition.z + range))
            {
                _go.SetActive(false);
            }
        }
    }
}
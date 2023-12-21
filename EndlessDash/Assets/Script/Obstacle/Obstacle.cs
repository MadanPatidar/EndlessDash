using UnityEngine;
public abstract class Obstacle : MonoBehaviour
{
    [SerializeField]
    private ObstacleConfig ObstacleConfig;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.PLAYER_TAG))
        {
            OnPlayerHit(other);          
        }
    }
    public virtual void OnPlayerHit(Collider other)
    {
        // Handle collision with obstacle
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(Damage());
            gameObject.SetActive(false);
        }
    }

    public int Damage()
    {
        return ObstacleConfig.Damage;
    }

    public float PosY()
    {
        return ObstacleConfig.PosY;
    }
    public float Weight()
    {
        return ObstacleConfig.Weight;
    }
    public void SetPosY()
    {
        Vector3 vector3 = transform.position;
        vector3.y += ObstacleConfig.PosY;
        transform.position = vector3;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogsObstacle : Obstacle
{
    /*  void OnTriggerEnter(Collider other)
      {
          if (other.CompareTag(Constant.PLAYER_TAG))
          {
              // Handle collision with obstacle
              Health health = other.GetComponent<Health>();
              if (health != null)
              {
                  health.TakeDamage(Damage());
                  gameObject.SetActive(false);
              }
          }
      }*/

    public override void OnPlayerHit(Collider other)
    {
        Debug.LogError("LogsObstacle");
        base.OnPlayerHit(other);
    }
}

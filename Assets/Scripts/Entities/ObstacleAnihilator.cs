using UnityEngine;

public class ObstacleAnihilator : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -20f) {
            Destroy(gameObject);
        }
    }
}

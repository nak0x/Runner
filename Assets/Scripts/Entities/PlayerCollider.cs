using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] PlayerMouvement playerMouvement;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Obstacle") {
           playerMouvement.enabled = false;
           FindFirstObjectByType<GameManager>().EndGame();
        }
    }
}

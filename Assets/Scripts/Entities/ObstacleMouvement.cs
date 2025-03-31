using UnityEngine;

public class ObstacleMouvement : MonoBehaviour
{
    public enum DirectionChoice
    {
        Backward = -1,
        Forward = 1
    }

    [SerializeField] public float distance = 1f;

    [SerializeField] public DirectionChoice direction = DirectionChoice.Backward;
    [SerializeField] ParticleSystem smokeParticlesSystem;
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject spawnOrigin;

    bool shouldMove = true;

    void FixedUpdate()
    {
        if (shouldMove) {
            float velocity = distance * (float)direction * Time.time;
            Vector3 position = spawnOrigin.transform.position + new Vector3(0, 0, velocity);
            rb.Move(position, Quaternion.identity);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player") {
            shouldMove = false;
            smokeParticlesSystem.transform.position = collision.contacts[0].point;
            smokeParticlesSystem.Play();
        }
    }
}

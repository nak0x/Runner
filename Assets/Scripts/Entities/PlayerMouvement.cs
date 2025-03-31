using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Unity.VisualScripting;

public class PlayerMouvement : MonoBehaviour
{
    [Header("Physics")]
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed = 500f;
    [SerializeField] float mouseSensitivity = 100f;

    [Header("Effects")]
    [SerializeField] float maxLinearVelocity = 15f;
    [SerializeField] Vector2 squeeznessX = new Vector2(0, 1);
    [SerializeField] Vector2 squeeznessY = new Vector2(0, 1);
    [SerializeField] Vector2 squeeznessZ = new Vector2(0, 1);

    [Header("Particules")]
    [SerializeField] ParticleSystem speedParticules;
    [SerializeField] float maxParticuleAmount = 150.7f;
    [SerializeField] Vector2 maxParticuleForce = new Vector2(2f, 5f);
    [SerializeField] float particulesAmountFactor = 100;

    float xAxis = 0f;
    float zAxis = 0f;
    float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Die if under -2y
        if (transform.position.y <= -2f) {
           FindFirstObjectByType<GameManager>().EndGame();
        }

        // Respond to user input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");
        yRotation += mouseX;

        // Mouvement effects
        float absVelocityX = Mathf.Abs(rb.linearVelocity.x);
        float absVelocityZ = Mathf.Abs(rb.linearVelocity.z);

        Vector3 squeeze = new Vector3(
            Mathf.Lerp(squeeznessX.x, squeeznessX.y, Mathf.InverseLerp(0, maxLinearVelocity, absVelocityX)),
            Mathf.Lerp(squeeznessY.x, squeeznessY.y, Mathf.InverseLerp(0, maxLinearVelocity, absVelocityX)),
            Mathf.Lerp(squeeznessZ.x, squeeznessZ.y, Mathf.InverseLerp(0, maxLinearVelocity, absVelocityZ))
        );
        transform.localScale = new Vector3(squeeze.x, squeeze.y, squeeze.z);

        float forwardParticuleAmount = Mathf.Lerp(0, maxParticuleAmount, Mathf.InverseLerp(0, maxLinearVelocity, absVelocityX));
        float sidewayParticuleAmount = Mathf.Lerp(0, maxParticuleAmount, Mathf.InverseLerp(0, maxLinearVelocity, absVelocityZ));
        float minStartParticuleForce = Mathf.Lerp(0, maxParticuleForce.x, Mathf.InverseLerp(0, maxLinearVelocity, absVelocityX));
        float maxStartParticuleForce = Mathf.Lerp(0, maxParticuleForce.y, Mathf.InverseLerp(0, maxLinearVelocity, absVelocityX));

         // Change emission rate
        var emission = speedParticules.emission;
        emission.rateOverTime = forwardParticuleAmount + sidewayParticuleAmount * particulesAmountFactor;

        // Change start speed (random range)
        var main = speedParticules.main;
        main.startSpeed = new ParticleSystem.MinMaxCurve(minStartParticuleForce, maxStartParticuleForce);

    }

    void FixedUpdate()
    {
        // Mouvements
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        rb.AddRelativeForce(new Vector3(speed * Time.deltaTime * xAxis, 0, 0), ForceMode.VelocityChange);
        rb.AddRelativeForce(new Vector3(0, 0, speed * Time.deltaTime * zAxis), ForceMode.VelocityChange);
    }
}

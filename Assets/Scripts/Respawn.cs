//using UnityEditor.Callbacks;
using UnityEngine;
public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] Vector2 respawnPosition;
    [SerializeField] Rigidbody2D rb;
    private Quaternion respawnRotation;
    void Start()

    {
        rb = GetComponent<Rigidbody2D>();
        respawnRotation = transform.rotation;
        respawnPosition = transform.position;
    }

    void Update()

    {

        Respawn();

    }

    void Respawn()

    {

        if(transform.position.y < -10f)

        {

            transform.rotation = respawnRotation;

            transform.position = respawnPosition;

            rb.linearVelocity = Vector2.zero;

//             rb.angularVelocity = Vector2.zero;

        }

    }

    public void SetCheckpoint(Vector2 newRespawnPosition, Quaternion newRespawnRotation)

    {

        respawnPosition = newRespawnPosition;

        respawnRotation = newRespawnRotation;

    }

}
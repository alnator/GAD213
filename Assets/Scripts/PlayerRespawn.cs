using UnityEngine;
public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] Vector2 respawnPosition;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerMovement playerMovement;
    private Quaternion respawnRotation;
    private GameObject respawnObjects;
    void Start()

    {
        rb = GetComponent<Rigidbody2D>();
        respawnRotation = transform.rotation;
        respawnPosition = transform.position;
        
    }

    void Update()

    {

        FallRespawn();

    }

    void FallRespawn()

    {

        if(transform.position.y < -10f)

        {

            transform.rotation = respawnRotation;

            transform.position = respawnPosition;

            rb.linearVelocity = Vector2.zero;
            respawnObjects = GameObject.Find("EverythingToRespawn");    
            respawnObjects.GetComponent<ObjectivesToRespawn>().RespawnObjects(); // searches the exact name of parent and calls for respawnObjects
            playerMovement.StartNewLevel();
//             rb.angularVelocity = Vector2.zero;

        }

    }
    public void DeadRespawn()

    {
            transform.rotation = respawnRotation;

            transform.position = respawnPosition;

            rb.linearVelocity = Vector2.zero;
            respawnObjects = GameObject.Find("EverythingToRespawn");    
            playerMovement.StartNewLevel();
            if(respawnObjects.GetComponent<ObjectivesToRespawn>()!= null)
            {
            
                respawnObjects.GetComponent<ObjectivesToRespawn>().RespawnObjects();
            }
            
    }


    public void SetCheckpoint(Vector2 newRespawnPosition, Quaternion newRespawnRotation)

    {

        respawnPosition = newRespawnPosition;

        respawnRotation = newRespawnRotation;

    }

}
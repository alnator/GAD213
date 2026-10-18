using UnityEngine;

public class Checkpoint : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
        {
            return;
        }
    PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();
        if(playerRespawn == null)
        {
            return;
        }
    playerRespawn.SetCheckpoint(transform.position, transform.rotation);
    //playsound
    Destroy(gameObject);
    }
}
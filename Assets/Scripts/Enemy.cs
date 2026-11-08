using UnityEngine;

public class Enemy : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("me worked");
        if(!other.gameObject.CompareTag("Player"))
        {
            return;
        }
        PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
        PlayerRespawn playerRespawn = other.gameObject.GetComponent<PlayerRespawn>();
        if(playerMovement !=null)
        {
            if(playerMovement.IsDashing())
            {
                gameObject.SetActive(false);
                playerMovement.AddDash();
            }
            else
            {
                playerRespawn.DeadRespawn();
            }
            
        }
    }
}

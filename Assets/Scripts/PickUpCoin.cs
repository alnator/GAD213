using UnityEngine;

public class PickUpCoin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("me worked");
        if(!other.gameObject.CompareTag("Player"))
        {
            return;
        }
        PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
        if(playerMovement !=null)
        {
            Debug.Log("adding");
            playerMovement.AddDash();
            //audiomanager.playsound(blalbalblablablalq)
            Destroy(gameObject);
        }
    }
}

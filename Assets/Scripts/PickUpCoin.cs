using UnityEngine;

public class PickUpCoin : MonoBehaviour // Coins aren't suppose to add dashes, they suppose to add balance.
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
            playerMovement.AddDash();// remove with shop.
            //audiomanager.playsound(blalbalblablablalq)
            Destroy(gameObject);
        }
    }
}

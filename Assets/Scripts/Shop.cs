using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerStats playerStats;
    public void ShopDashesUpgrade()
    {
        //if(currentCoins>= 1) 
        // coins--;
        playerStats.maxDashes++;
    }
}

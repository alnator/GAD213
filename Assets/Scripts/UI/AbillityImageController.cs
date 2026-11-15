using UnityEngine;
using UnityEngine.UI;

public class AbillityImageController : MonoBehaviour // bad decision, better would've been create event, when he casts abillity this script listens to it
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Image dashAbillityImage;
    public void ChangeDashIcon()
    {
        if(playerMovement.AmountOfDashes() ==0)
        {
            dashAbillityImage.color = Color.grey;
        }
        else
        {
            dashAbillityImage.color = Color.white;
        }
    }
}

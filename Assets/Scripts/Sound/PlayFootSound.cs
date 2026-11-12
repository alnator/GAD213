using UnityEngine;

public class PlayFootSound : MonoBehaviour
{
    public void PlaySound()
    {
        SoundManager.PlaySound(SoundType.Footstep);
    }
}

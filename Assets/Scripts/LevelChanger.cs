using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] GameObject cameraHolder;
    [SerializeField] GameObject level1Point;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
        {
            return;
        }
        cameraHolder.transform.position = level1Point.transform.position;
    }
}

using UnityEngine;

public class ObjectivesToRespawn : MonoBehaviour //picks all of objects in this PARENT and sets them as true
{
    public void RespawnObjects()
    {
            Transform[] allChildren = GetComponentsInChildren<Transform>(true);
            foreach(Transform child in allChildren)
            {
                if(child == this.transform)continue;
                child.gameObject.SetActive(true);
            }
    }
}

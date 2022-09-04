using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform endPoint;
    void Start()
    {
        WorldController.instance.OnPlatformMovement += TryDelAndAddPlatform;
    }
    private void OnDisable()
    {
        WorldController.instance.OnPlatformMovement -= TryDelAndAddPlatform;
    }
    private void TryDelAndAddPlatform()
    {
        if (transform.position.z < WorldController.instance.minZ)
        {
            WorldController.instance.worldBuilder.CreatePlatform();
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameraa : MonoBehaviour
{
    public Transform objToFollow;
    private Vector3 deltaPos;
    internal void Start()
    {
        deltaPos = transform.position - objToFollow.position;
    }
    internal void Update()
    {
        transform.position = new Vector3(
        Mathf.Clamp(objToFollow.position.x, 10f, 12f) + deltaPos.x,
        Mathf.Clamp(objToFollow.position.y, 8.654921f, 8.654923f),
        objToFollow.position.z + deltaPos.z);

        if (Input.GetKeyUp(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
    }
}

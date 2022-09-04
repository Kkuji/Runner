using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    void Update()
    {
        if (PlayerController.animator.enabled)
            transform.Rotate(Vector3.up * 180 * Time.deltaTime);
        if (PlayerController.EnviromentMoves)
        {
            transform.position -= Vector3.forward * WorldController.speed * Time.deltaTime;
            if (gameObject.transform.position.z < -22f)
                gameObject.SetActive(false);
        }
    }
}

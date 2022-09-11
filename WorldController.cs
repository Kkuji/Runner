using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public static float speed = 4f;
    public static float speedOfAnim = 1.7f;
    public static float speedOfRun = 1.4f;
    public WorldBuilder worldBuilder;
    public float minZ;
    public bool timer = true;

    public delegate void TryToDelAndAddPlatform();
    public event TryToDelAndAddPlatform OnPlatformMovement;
    public static WorldController instance;

    private void Awake()
    {
        if (WorldController.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        WorldController.instance = this;
    }

    private void OnDestroy()
    {
        WorldController.instance = null;
    }

    void Start()
    {
        StartCoroutine(OnPlatformMovementCorutine());
    }

    void Update()
    {
        if (PlayerController.EnviromentMoves)
        {
            if (timer)
                StartCoroutine(MakeHarder());

            transform.position -= Vector3.forward * speed * Time.deltaTime;
        }
    }

    IEnumerator OnPlatformMovementCorutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (OnPlatformMovement != null)
                OnPlatformMovement();
        }
    }
    IEnumerator MakeHarder()
    {
        timer = false;
        yield return new WaitForSeconds(10f);
        speed += 0.2f;
        speedOfAnim += 0.08f;
        speedOfRun += 0.05f;
        timer = true;
    }
}

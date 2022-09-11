using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    public GameObject[] freePlatforms;
    public GameObject[] obstaclePlatforms;
    public GameObject coin;
    public Transform platformContainer;

    private float dif = 1.38f;
    private MeshRenderer MeshRPlatf;
    private Vector3 delta;
    private Vector3 pos2;
    private bool isObstacle;
    private int NumberObstaclesInARow = -1;
    private bool firstPlatform = true;
    private Transform lastPlatform = null;
    private Transform platform;
    void Start()
    {
        platform = this.freePlatforms[0].transform.GetChild(0);
        MeshRPlatf = platform.GetComponent<MeshRenderer>();
        delta = MeshRPlatf.bounds.size;
        Init();
    }

    public void Init()
    {
        CreateFreePlatform();
        CreateFreePlatform();

        for (int i = 0; i < 7; i++)
            CreatePlatform();
    }
    public void CreatePlatform()
    {
        if (NumberObstaclesInARow == -1)
            NumberObstaclesInARow = Random.Range(2, 5);

        if (isObstacle && NumberObstaclesInARow < 1)
        {
            CreateFreePlatform();
            NumberObstaclesInARow--;
        }
        else
        {
            NumberObstaclesInARow--;
            CreateObstaclePlatform();
        }
    }
    private void CreateFreePlatform()
    {
        Vector3 pos = (lastPlatform == null) ?
            platformContainer.position :
            lastPlatform.GetComponent<PlatformController>().endPoint.position;

        int index = Random.Range(0, freePlatforms.Length);
        GameObject res = Instantiate(freePlatforms[index], pos, Quaternion.identity, platformContainer);
        lastPlatform = res.transform;
        lastPlatform.transform.position = new Vector3(res.transform.position.x, res.transform.position.y, res.transform.position.z + delta.z / 2);
        isObstacle = false;
        pos2 = pos;
        pos2.z += delta.z / 2;
        int value = Random.Range(-1, 2);
        float value1 = (float)value;
        float a = value1 * dif;
        if (!firstPlatform)
        {
            pos2.x = pos2.x + a;
            Instantiate(coin, pos2, Quaternion.identity);
        }
        else
            firstPlatform = !firstPlatform;
    }
    private void CreateObstaclePlatform()
    {
        Vector3 pos = lastPlatform.GetComponent<PlatformController>().endPoint.position;
        int index = Random.Range(0, obstaclePlatforms.Length);
        GameObject res = Instantiate(obstaclePlatforms[index], pos, Quaternion.identity, platformContainer);
        lastPlatform = res.transform;
        lastPlatform.transform.position = new Vector3(res.transform.position.x, res.transform.position.y, res.transform.position.z + delta.z / 2);
        isObstacle = true;
        pos2 = pos;
        pos2.z += delta.z / 2;
        if (index == 0)
        {
            int value = Random.Range(0, 2);
            float value1 = (float)value;
            float a = value1 * dif;
            pos2.x = pos2.x + a;
            Instantiate(coin, pos2, Quaternion.identity);
        }
        if (index == 1)
        {
            int value = Random.Range(-1, 1);
            float value1 = (float)value;
            float a = value1 * dif;
            pos2.x = pos2.x + a;
            Instantiate(coin, pos2, Quaternion.identity);
        }
        if (index == 2)
        {
            int value = Random.Range(-1, 2);
            while (value == 0)
                value = Random.Range(-1, 2);
            float value1 = (float)value;
            float a = value1 * dif;
            pos2.x = pos2.x + a;
            Instantiate(coin, pos2, Quaternion.identity);
        }
        if (index == 4)
        {
            int value = Random.Range(-1, 2);
            while (value == 0)
                value = Random.Range(-1, 2);
            float value1 = (float)value;
            float a = value1 * dif;
            pos2.x = pos2.x + a;
            Instantiate(coin, pos2, Quaternion.identity);
        }
        if (index == 5)
        {
            int value = 1;
            float value1 = (float)value;
            float a = value1 * dif;
            pos2.x = pos2.x + a;
            Instantiate(coin, pos2, Quaternion.identity);
        }
        if (index == 6)
        {
            pos2.x = 11f;
            Instantiate(coin, pos2, Quaternion.identity);
        }
    }
}

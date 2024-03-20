using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    private GameObject spawnedObject;
    void Start()
    {
        SpawnObject();
    }

    public void SpawnObject() {
        if (spawnedObject == null) {
            spawnedObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        }
    }
}

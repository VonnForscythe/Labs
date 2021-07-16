using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] prefab;
    //public GameObject prefab;

    void Start()
    {
        Instantiate(prefab[Random.Range(0, 3)], transform.position, Quaternion.identity);

   
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemies;

    // Start is called before the first frame update
    void Start()
    {
        Enemies.transform.position = new Vector3 (Random.Range(-1.4f, 0.6f), 0.25f, Random.Range(-1.245f,0.755f));
        Instantiate(Enemies);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

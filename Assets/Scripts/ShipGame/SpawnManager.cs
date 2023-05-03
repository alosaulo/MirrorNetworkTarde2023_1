using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame { 
    public class SpawnManager : NetworkBehaviour
    {

        public GameObject prefabEnemy;
        public Transform[] enemySpawns;

        public float spawnTime;
        [SyncVar] float spawnCounter;


        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (spawnCounter <= spawnTime)
            {
                spawnCounter += Time.deltaTime;
            }
            else 
            {
                spawnCounter = 0;
                int indice = Random.Range(0, enemySpawns.Length);
                GameObject gb = Instantiate(prefabEnemy,
                    enemySpawns[indice].position,
                    enemySpawns[indice].rotation);
                NetworkServer.Spawn(gb);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Examples.AdditiveLevels;

namespace ShipGame
{
    public class EnemyController : NetworkBehaviour
    {
        public float speed;

        [SyncVar]
        public int vida;

        Rigidbody rb;



        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();

        }

        // Update is called once per frame
        void Update()
        {
            rb.velocity = transform.forward * speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Tiro")
            {
                vida--;
                if (vida <= 0)
                {
                    DestroySelf();
                }
                TiroController tiro = other.GetComponent<TiroController>();
                tiro.DestroySelf();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Player")
            {
                collision.gameObject.GetComponent<ShipController>().LevarDano(1);
                DestroySelf();
            }
        }

        [Server]
        void DestroySelf()
        {
            NetworkServer.Destroy(gameObject);
        }

    }
}
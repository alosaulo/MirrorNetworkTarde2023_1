using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TiroController : NetworkBehaviour
{
    public float speed;
    Rigidbody rb;

    [SyncVar]
    float contadorMorte = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        contadorMorte+= Time.deltaTime;
        if (contadorMorte >= 3) {
            DestroySelf();
        }
    }

    [Server]
    void DestroySelf() {
        NetworkServer.Destroy(gameObject);
    }

}

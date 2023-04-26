using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using Telepathy;
using UnityEditor;
using UnityEngine;

public class ShipController : NetworkBehaviour
{
    public GameObject prefabTiro;
    public Transform origemTiro;
    public float speed;
    Rigidbody rb;

    float vAxis;
    float hAxis;

    [SyncVar]
    public float tiroCooldown;
    [SyncVar]
    float contadorTiro;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer) 
        { 
            vAxis = Input.GetAxis("Vertical");
            hAxis = Input.GetAxis("Horizontal");
            if (contadorTiro >= tiroCooldown)
            {
                if (Input.GetButtonDown("Jump")) 
                {
                    CmdAtirar();
                    contadorTiro = 0;
                }
            }
            else 
            {
                contadorTiro += Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            float newHPosition = transform.position.x + hAxis * Time.deltaTime * speed;
            newHPosition = Mathf.Clamp(newHPosition, -32f, 32f);

            float newVPosition = transform.position.z + vAxis * Time.deltaTime * speed;
            newVPosition = Mathf.Clamp(newVPosition, 0, 80f);

            rb.position = new Vector3(newHPosition, transform.position.y, newVPosition);
        }
    }

    [Command]
    void CmdAtirar() {
        GameObject tiro = Instantiate(prefabTiro, origemTiro.position, origemTiro.rotation);
        NetworkServer.Spawn(tiro);
    }

}

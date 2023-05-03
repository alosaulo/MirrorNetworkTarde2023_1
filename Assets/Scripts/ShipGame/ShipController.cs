using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using Telepathy;
using UnityEditor;
using UnityEngine;
using TMPro;

namespace ShipGame
{
    public class ShipController : NetworkBehaviour
    {

        private Vector3 startPos;

        public GameObject prefabTiro;
        public Transform origemTiro;
        public TextMeshProUGUI txtVida;
        public float speed;


        Rigidbody rb;

        float vAxis;
        float hAxis;

        [SyncVar]
        public int vida = 5;
        [SyncVar]
        public string vidaStr = string.Empty;

        [SyncVar]
        public float tiroCooldown;
        [SyncVar]
        float contadorTiro;

        // Start is called before the first frame update
        void Start()
        {
            startPos = transform.position;
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

            while (vida != vidaStr.Length)
            {
                if (vida < vidaStr.Length)
                {
                    vidaStr = vidaStr.Remove(0, 1);
                }
                else
                {
                    vidaStr += "♥";
                }
            }

            txtVida.text = vidaStr;

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
        void CmdAtirar()
        {
            GameObject tiro = Instantiate(prefabTiro, origemTiro.position, origemTiro.rotation);
            NetworkServer.Spawn(tiro);
        }

        public void LevarDano(int dano) {
            vida -= dano;
            if (vida <= 0) {
                RpcRespawn();
            }
        }

        [ClientRpc]
        void RpcRespawn() {
            transform.position = startPos;
            vida = 5;
        }

    }
}
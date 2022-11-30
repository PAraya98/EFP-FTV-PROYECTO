using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTCrashActivador : MonoBehaviour
{
    // Start is called before the first frame update

    private bool estaActivado = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            estaActivado = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            estaActivado = true;
        }
    }

    public bool GetEstaActivado()
    {
        return estaActivado;
    }
}

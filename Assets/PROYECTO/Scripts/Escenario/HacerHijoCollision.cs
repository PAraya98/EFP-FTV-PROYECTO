using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacerHijoCollision : MonoBehaviour
{
    private Transform jugadores;
    // Start is called before the first frame update
    void Start()
    {
        jugadores = GameObject.Find("Jugadores").transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        if(collision.gameObject.tag == "Player")
        collision.transform.parent = transform;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        collision.transform.parent = jugadores;
    }
}

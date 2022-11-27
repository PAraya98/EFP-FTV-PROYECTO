using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PisoTrampa : MonoBehaviour
{
    public GameObject contenedorTrampa;
    public Transform target;
    public float tiempoActivacion;
    public float velocidadSubida;
    public float velocidadBajada;

    private bool trampaActivada;
    private float tiempo;
    // Start is called before the first frame update
    void Start()
    {
        trampaActivada = false;
        tiempo = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Transform trampa = contenedorTrampa.GetComponent<Transform>();
        if (trampaActivada == true)
        {
            if(tiempo < tiempoActivacion)
            {
                tiempo += Time.deltaTime;
            }
            else
            {
                trampa.position = Vector2.MoveTowards(trampa.position, target.position, velocidadSubida * Time.deltaTime);
            }
        }
        else
        {
            trampa.position = Vector2.MoveTowards(trampa.position, transform.position, velocidadBajada * Time.deltaTime);
        }
        if(Vector2.Distance(trampa.position, target.position) == 0)
        {
            trampaActivada = false;
            tiempo = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            trampaActivada = true;
        }
    }
}

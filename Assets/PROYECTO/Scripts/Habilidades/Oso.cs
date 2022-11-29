using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oso : MonoBehaviour
{
    
    public float velocidad = 4f;
    public float tiempoDesaparicion = 15f;
    private PersonajeData personajeData;
    private Rigidbody2D rb;

    private bool inicializado = false;
    // Start is called before the first frame update

 
    void Start()
    {
        personajeData = gameObject.GetComponent<PersonajeData>();
        if (!inicializado && personajeData.ObtenerDireccionInicial() == -1)
        {
            Vector3 nuevaEscalaLocal = transform.localScale;
            nuevaEscalaLocal.x *= -1f;
            transform.localScale = nuevaEscalaLocal;
            inicializado = true;
        }
        rb = gameObject.GetComponent<Rigidbody2D>();

        StartCoroutine(Destruccion());
    }

    // Update is called once per frame
    void Update()
    {
        
        rb.velocity = new Vector2(velocidad * personajeData.ObtenerDireccionInicial(), rb.velocity.y);
    }
    IEnumerator Destruccion()
    {
        yield return new WaitForSeconds(tiempoDesaparicion);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Muerte"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Muerte"))
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerdoBomba : MonoBehaviour
{
    public float velocidad = 4f;
    public float tiempoDesaparicion = 15f;
    private PersonajeData personajeData;
    private Rigidbody2D rb;
    private Collider2D collider2D;
    private SpriteRenderer spriteRenderer;
    private Joint2D joint2D;
    private bool inicializado = false;
    private Transform nitroCrash;
    // Start is called before the first frame update


    void Start()
    {
        joint2D = gameObject.GetComponent<Joint2D>();
        personajeData = gameObject.GetComponent<PersonajeData>();
        collider2D = gameObject.GetComponent<Collider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        nitroCrash = transform.Find("NITROCrash");


        if (!inicializado && personajeData.ObtenerDireccionInicial() == -1)
        {
            Vector3 nuevaEscalaLocal = transform.localScale;
            nuevaEscalaLocal.x *= -1f;           
            transform.localScale = nuevaEscalaLocal;
            nuevaEscalaLocal = nitroCrash.localScale;
            nuevaEscalaLocal.x *= -1f;
            nitroCrash.localScale = nuevaEscalaLocal;

            inicializado = true;
        }
        rb = gameObject.GetComponent<Rigidbody2D>();
        
        StartCoroutine(Desaparicion());
    }

    // Update is called once per frame
    void Update()
    {
        if(nitroCrash == null) Destroy(gameObject);
        rb.velocity = new Vector2(velocidad * personajeData.ObtenerDireccionInicial(), rb.velocity.y);
    }
    IEnumerator Desaparicion()
    {
        yield return new WaitForSeconds(tiempoDesaparicion);
        spriteRenderer.enabled = false;
        collider2D.isTrigger = true;
        joint2D.enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Muerte"))
        {
            spriteRenderer.enabled = false;
            collider2D.isTrigger = true;
            joint2D.enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Muerte"))
        {
            spriteRenderer.enabled = false;
            collider2D.isTrigger = true;
            joint2D.enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NITROCrashSalto : MonoBehaviour
{
    [Range(0.0f, 9f)]
    public float fuerzaSaltoMin = 0.5f;
    [Range(0.0f, 9f)]
    public float fuerzaSaltoMax = 9f;
    private Rigidbody2D rb;
    private NITROCrash nitroCrash;
    private bool estaEnElPiso = false;
    // Start is called before the first frame update
    void Start()
    {
        nitroCrash = transform.parent.GetComponent<NITROCrash>();
        rb = transform.parent.GetComponent<Rigidbody2D>();
        StartCoroutine(SaltoNitro());
    }

 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Piso"))
        {
            estaEnElPiso = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Piso"))
        {
            estaEnElPiso = false;
        }
    }

    IEnumerator SaltoNitro()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 3));
        if (estaEnElPiso && !nitroCrash.GetExplosionEnCurso())
        {
            rb.velocity = new Vector2(rb.velocity.x, Random.Range(fuerzaSaltoMin, fuerzaSaltoMax));
        }

        StartCoroutine(SaltoNitro());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TNTCrash : MonoBehaviour
{
    public int TiempoActivacion = 4;
    private TNTCrashActivador tntCrashActivador;
    private Animator animator;
    private TextMeshPro textoContador;
    private Rigidbody2D rb;
    private bool explosionEnCurso = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        textoContador = gameObject.transform.Find("Texto - Contador").GetComponent<TextMeshPro>();
        tntCrashActivador = gameObject.transform.Find("Activador").GetComponent<TNTCrashActivador>();
    }


    private void Update()
    {
        if(tntCrashActivador.GetEstaActivado() && !explosionEnCurso)
        {
            explosionEnCurso = true;
            StartCoroutine(DestruccionInminente());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Muerte") && !explosionEnCurso)
        {
            explosionEnCurso = true;
            StartCoroutine(DestruccionInmediata());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Muerte") && !explosionEnCurso)
        {
            explosionEnCurso = true;
            StartCoroutine(DestruccionInmediata());
        }
    }

    IEnumerator DestruccionInminente()
    {   for(int i = TiempoActivacion; i > 0; i--)
        {   
            textoContador.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.Play("TNTExplosion");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Destroy(gameObject);
    }
    IEnumerator DestruccionInmediata()
    {   while(rb)
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.Play("TNTExplosion");
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle")) 
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Destroy(gameObject);
    }
}

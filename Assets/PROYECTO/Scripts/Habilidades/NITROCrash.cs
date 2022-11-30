using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NITROCrash : MonoBehaviour
{
    
    private Animator animator;
    private Rigidbody2D rb;
    private bool explosionEnCurso = false;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();   
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == LayerMask.NameToLayer("Muerte") || collision.gameObject.tag == "Player") && !explosionEnCurso)
        {
            explosionEnCurso = true;
            StartCoroutine(DestruccionInmediata());
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == LayerMask.NameToLayer("Muerte") || collision.gameObject.tag == "Player") && !explosionEnCurso)
        {
            explosionEnCurso = true;
            StartCoroutine(DestruccionInmediata());
        }
    }

    public bool GetExplosionEnCurso() 
    {
        return explosionEnCurso;
    }
    
    public void DetonarNitro()
    {
        explosionEnCurso = true;
        StartCoroutine(DestruccionInmediata());
    }

    IEnumerator DestruccionInmediata()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.Play("TNTExplosion");
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle")) 
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Destroy(gameObject);
    }
}

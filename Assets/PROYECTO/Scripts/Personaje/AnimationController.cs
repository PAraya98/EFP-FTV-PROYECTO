using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class AnimationController : MonoBehaviour
{
    //Constantes
    [BoxGroup("Constantes del personaje")]
    [ReadOnly]
    public float velocidadCaminando = 3f;
    [BoxGroup("Constantes del personaje")]
    [ReadOnly]
    public float velocidadCorriendo = 4f;

    //Variables del personaje en movimiento
    [BoxGroup("Variables en tiempo real")]
    [ReadOnly]
    public bool estaEnPiso;

    //Variables privadas obtenidas desde otros gameObject
    [BoxGroup("Dependencias")]
    [ReadOnly] [SerializeField] private Animator animator;
    [BoxGroup("Dependencias")]
    [ReadOnly] [SerializeField] private Rigidbody2D rb;
    [BoxGroup("Dependencias")]
    [ReadOnly] [SerializeField] private int layerPiso;
    void Start()
    {
        layerPiso = LayerMask.NameToLayer("Piso");
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {   
        
        if (Mathf.Abs(rb.velocity.x) <= velocidadCorriendo-0.1f && Mathf.Abs(rb.velocity.x) > 0.1f && estaEnPiso && !animator.GetCurrentAnimatorStateInfo(0).IsName("caminando")) 
            animator.Play("caminando");
        else if (Mathf.Abs(rb.velocity.x) >= velocidadCorriendo-0.1f && estaEnPiso && !animator.GetCurrentAnimatorStateInfo(0).IsName("corriendo")) 
            animator.Play("corriendo");
        else if (!estaEnPiso && rb.velocity.y > 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("salto")) 
            animator.Play("salto");
        else if (!estaEnPiso && rb.velocity.y < 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("caida")) 
            animator.Play("caida");
        else if (Mathf.Abs(rb.velocity.x) < 0.1f && estaEnPiso && !animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            animator.Play("idle");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == layerPiso) estaEnPiso = true;

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == layerPiso) estaEnPiso = false;
    }
}

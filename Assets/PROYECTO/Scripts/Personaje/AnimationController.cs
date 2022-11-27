using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;
using Cinemachine;

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
    [BoxGroup("Variables en tiempo real")][ReadOnly]
    public bool estaEnPiso;
    [BoxGroup("Variables en tiempo real")][ReadOnly]
    public bool estaMuerto;
    [BoxGroup("Variables en tiempo real")][ReadOnly]
    public double tiempoDeMuerte;
    [BoxGroup("Variables en tiempo real")][ReadOnly]
    public double deltaTimeMuerte;
    [BoxGroup("Variables en tiempo real")][ReadOnly]
    public float velocidadXReal;

    //Variables privadas obtenidas desde otros gameObject
    [BoxGroup("Dependencias")][ReadOnly][SerializeField]
    private Animator animator;
    [BoxGroup("Dependencias")][ReadOnly][SerializeField]
    private Rigidbody2D rb;
    [BoxGroup("Dependencias")][ReadOnly][SerializeField]
    private int layerPiso;
    [BoxGroup("Dependencias")][ReadOnly][SerializeField]
    private Collider2D pies;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField] 
    private Transform padreTransform;
    [BoxGroup("Dependencias")][ReadOnly][SerializeField]
    private Rigidbody2D padreRb;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField]
    private PisoController pisoController;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField]
    private CollisionController collisionController;
    [BoxGroup("Dependencias")][ReadOnly][SerializeField]
    private CinemachineTargetGroup cinemachinetargetgroup;
    void Start()
    {
        padreTransform = gameObject.transform.parent;
        tiempoDeMuerte = new TimeSpan(DateTime.Now.Ticks).TotalSeconds;
        layerPiso = LayerMask.NameToLayer("Piso");
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        pisoController = gameObject.transform.Find("pies").GetComponent<PisoController>();
        collisionController = gameObject.GetComponent<CollisionController>();
        cinemachinetargetgroup = GameObject.Find("TargetGroup - CamaraSeguimiento").GetComponent<CinemachineTargetGroup>();
    }

    void Update()
    {   
        if (collisionController.getEstaMuerto() && !estaMuerto)
        {
            transform.gameObject.layer = LayerMask.NameToLayer("Default");
            estaMuerto = true;
            animator.Play("muerte");
            tiempoDeMuerte = new TimeSpan(DateTime.Now.Ticks).TotalSeconds;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            
        }

        estaEnPiso = pisoController.GetEstaEnPiso();

        //FIXME: Ineficiente tal vez, ser�a mejor obtenerlo con un m�todo
        padreRb = pisoController.GetPadreRb();

        if (padreRb) velocidadXReal = Mathf.Abs(padreRb.velocity.x - rb.velocity.x);
        else velocidadXReal = Mathf.Abs(rb.velocity.x);


        //Animaciones de movimiento
        if (!estaMuerto)
        {
            if (velocidadXReal <= velocidadCorriendo - 0.1f && velocidadXReal > 0.1f && estaEnPiso && !animator.GetCurrentAnimatorStateInfo(0).IsName("caminando"))
                animator.Play("caminando");
            else if (velocidadXReal >= velocidadCorriendo - 0.1f && estaEnPiso && !animator.GetCurrentAnimatorStateInfo(0).IsName("corriendo"))
                animator.Play("corriendo");
            else if (!estaEnPiso && rb.velocity.y > 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("salto"))
                animator.Play("salto");
            else if (!estaEnPiso && rb.velocity.y < 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("caida"))
                animator.Play("caida");
            else if (velocidadXReal < 0.1f && estaEnPiso && !animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                animator.Play("idle");
        }
        //Muerte del personaje
        else
        {
            if (new TimeSpan(DateTime.Now.Ticks).TotalSeconds - tiempoDeMuerte > 3.5f)
            {
                Destroy(gameObject);
                cinemachinetargetgroup.RemoveMember(gameObject.transform);
            }

                

            //Missing Transform: https://forum.unity.com/threads/cinemachine-targetgroup-remove-member-issue-unity-2019-1-0f2.680692/
        }
        deltaTimeMuerte = new TimeSpan(DateTime.Now.Ticks).TotalSeconds - tiempoDeMuerte;

    }
}


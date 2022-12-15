using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class MovimientoController : MonoBehaviour
{
    // Inspector
    [Header("Constantes del personaje")]
    //Inputs del inspector
    //  Scale Inicial de los personajes
    private Vector3 constScale;
    //  Velocidades Iniciales que no se modifican
    public float constVelocidadCaminando = 3f;
    public float constVelocidadCorriendo = 4f;
    //  Velocidad que pueden ser modificadas
    public float velocidadCaminando = 3f;
    public float velocidadCorriendo = 4f;
    public float fuerzaDeSalto = 16f;
    public float alturaSalto = 7f;    
    public float tiempoSalto = 1f;
    public float fuerzaDeMA = 0.1f;
    // Nuevo Movimiento
    public float velPower = 1.2f;
    public float aceleracion = 9f;
    public float desaceleracion = 9f;
    public float frictionAmount = 0.2f;
    public float fallGravityMultiplier = 2;
    public float gravityScale = 3.8f;
    public float gravityStrength;
    public float masa = 1f;
    public float maximaVelocidadCaida = 5.5f;

    //Debuff 
    private bool controlLentitud = false;
    private bool controlReloj = false;

    // Variables en tiempo real
    private float velocidadHorizontal;
    private float velocidadVertical;
    private float mirandoHacia;
    private bool estaMirandoDerecha = true;
    private bool estaEnPiso;
    private bool corriendo = false;

    //Controles del personaje
    private float mover;
    private bool salto;
    private bool correr;
    //Variables privadas obtenidas desde otros gameObject
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private CompositeCollider2D checkPiso;
    private int layerPiso;
    private PlayerInput playerInput;
    private PisoController pisoController;
    void Start()
    {

        Application.targetFrameRate = 60;
        layerPiso = LayerMask.NameToLayer("Piso");
        checkPiso = GameObject.Find("Tilemap - Escenario").GetComponent<CompositeCollider2D>();
        pisoController = gameObject.transform.Find("pies").GetComponent<PisoController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.mass = masa;
        playerCollider = gameObject.GetComponent<Collider2D>();
        estaEnPiso = false;
        constScale = transform.localScale;

        //Calculate gravity strength using the formula (gravity = 2 * jumpHeight / timeToJumpApex^2) 
        gravityStrength = -(2 * alturaSalto) / (tiempoSalto * tiempoSalto);

        //Calculate the rigidbody's gravity scale (ie: gravity strength relative to unity's gravity value, see project settings/Physics2D)
        gravityScale = gravityStrength / Physics2D.gravity.y;
    }

    // Update is called once per frame
    void Update()
    {

        if (playerInput)
        {
            mover = playerInput.actions["mover"].ReadValue<Vector2>().x;
            mirandoHacia = mover == 0 ? 0 : mover > 0 ? 1 : -1;
            salto = playerInput.actions["salto"].IsPressed();
            correr = playerInput.actions["correr"].IsPressed();

            if (salto && estaEnPiso)
            {
                //rb.velocity = new Vector2(rb.velocity.x, fuerzaDeSalto);
                Saltar();
            }
            if (!salto && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
            if (correr && !controlLentitud && !controlReloj)
            {
                corriendo = true;
            }
            if (!correr && !controlLentitud && !controlReloj)
            {
                corriendo = false;
            }
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravityScale * fallGravityMultiplier;
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maximaVelocidadCaida));
            }
            else
            {
                rb.gravityScale = gravityScale;
            }

            if (corriendo && !controlLentitud && !controlReloj) Correr();
            else if(!corriendo && !controlLentitud && !controlReloj) Caminar();

            #region
            if (estaEnPiso || corriendo)
            {

                float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
                amount *= Mathf.Sign(rb.velocity.x);
                rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);                
            }
            #endregion

        }
        else
        {
            if (gameObject.transform.Find("PlayerInput")) playerInput = gameObject.transform.Find("PlayerInput").GetComponent<PlayerInput>();
        }
        GirarPersonaje();
    }
    public bool EstaSaltando()
    {
        if (playerInput) return playerInput.actions["salto"].IsPressed();
        else return false;
    }
     
    public void SetControlLentitud(bool control)
    {
        controlLentitud = control;
    }
    public void SetControlReloj(bool control)
    {
        controlReloj = control;
    }

    public int GetMirandoHacia()
    {
        return estaMirandoDerecha ? 1 : -1;
    }
    public float GetVelocidadCorriendo()
    {
        return constVelocidadCorriendo;
    }
    public void SetVelocidadCaminando(float NuevaVelocidadCaminando)
    {
        velocidadCaminando = NuevaVelocidadCaminando;
    }
    public float GetVelocidadCaminando()
    {
        return constVelocidadCaminando;
    }
    public void SetVelocidadCorriendo(float NuevaVelocidadCorriendo)
    {
        velocidadCorriendo = NuevaVelocidadCorriendo;
    }
    public Vector3 GetScales()
    {
        return constScale;
    }
    public void SetScales(Vector3 NuevaScale)
    {
        transform.localScale = NuevaScale;
    }

    private void FixedUpdate()
    {   // Se asigna la velocidad de movimiento

        estaEnPiso = pisoController.GetEstaEnPiso();
    }
    void Saltar()
    {
        rb.velocity = new Vector2(rb.velocity.x, fuerzaDeSalto);
        //rb.AddForce(Vector2.up * fuerzaDeSalto, ForceMode2D.Impulse);
    }
    void Correr()
    {
        float targetSpeed = mirandoHacia * velocidadCorriendo;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(speedDif) > 0.01f) ? aceleracion : desaceleracion;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        rb.AddForce(movement * Vector2.right);
    }
    void Caminar()
    {
        float targetSpeed = mirandoHacia * velocidadCaminando;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(speedDif) > 0.01f) ? aceleracion : desaceleracion;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        rb.AddForce(movement * Vector2.right);
    }
    void GirarPersonaje()
    {
        if (estaMirandoDerecha && mirandoHacia < 0f || !estaMirandoDerecha && mirandoHacia > 0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
            estaMirandoDerecha = !estaMirandoDerecha;
            //Vector3 nuevaEscalaLocal = transform.localScale;
            //nuevaEscalaLocal.x *= -1f;
            //transform.localScale = nuevaEscalaLocal;
            //Debug.Log("no gira");
        }
    }

}

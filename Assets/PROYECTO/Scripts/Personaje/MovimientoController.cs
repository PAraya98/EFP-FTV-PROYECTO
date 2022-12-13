using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class MovimientoController : MonoBehaviour
{
    // Inspector
    //Inputs del inspector
    //  Scale Inicial de los personajes
    private Vector3 constScale;
    //  Velocidades Iniciales que no se modifican
    [BoxGroup("Constantes del personaje")]
    public float constVelocidadCaminando = 3f;
    [BoxGroup("Constantes del personaje")]
    public float constVelocidadCorriendo = 4f;
    //  Velocidad que pueden ser modificadas
    [BoxGroup("Constantes del personaje")]
    public float velocidadCaminando = 3f;
    [BoxGroup("Constantes del personaje")]
    public float velocidadCorriendo = 4f;
    [BoxGroup("Constantes del personaje")]
    public float fuerzaDeSalto = 16f;

    [BoxGroup("Constantes del personaje")]
    public float fuerzaDeMA = 0.1f;
    
    // Nuevo Movimiento
    [BoxGroup("Constantes del personaje")]
    public float velPower = 1.2f;
    [BoxGroup("Constantes del personaje")]
    public float aceleracion = 9f;
    [BoxGroup("Constantes del personaje")]
    public float desaceleracion = 9f;

    // 


    [BoxGroup("Constantes del personaje")]
    [ReadOnly]
    public float masa = 1f;
    //Variables del personaje en movimiento
    [BoxGroup("Variables en tiempo real")]
    [ReadOnly]
    public float velocidadHorizontal;
    [BoxGroup("Variables en tiempo real")]
    [ReadOnly]
    public float velocidadVertical;
    [BoxGroup("Variables en tiempo real")]
    [ReadOnly]
    public float mirandoHacia;
    [BoxGroup("Variables en tiempo real")]
    [ReadOnly]
    public bool estaMirandoDerecha = true;
    [BoxGroup("Variables en tiempo real")]
    [ReadOnly]
    public bool estaEnPiso;
    [BoxGroup("Variables en tiempo real")]
    [ReadOnly]
    public bool corriendo = false;

    //Controles del personaje
    [BoxGroup("Controles del personaje")]
    [ReadOnly]
    public float mover;
    [BoxGroup("Controles del personaje")]
    [ReadOnly]
    public bool salto;
    [BoxGroup("Controles del personaje")]
    [ReadOnly]
    public bool correr;
    //Variables privadas obtenidas desde otros gameObject
    [BoxGroup("Dependencias")]
    [ReadOnly]
    [SerializeField]
    private Rigidbody2D rb;
    [BoxGroup("Dependencias")]
    [ReadOnly]
    [SerializeField]
    private Collider2D playerCollider;
    [BoxGroup("Dependencias")]
    [ReadOnly]
    [SerializeField]
    private CompositeCollider2D checkPiso;
    [BoxGroup("Dependencias")]
    [ReadOnly]
    [SerializeField]
    private int layerPiso;
    [BoxGroup("Dependencias")]
    [ReadOnly]
    [SerializeField]
    private PlayerInput playerInput;
    [BoxGroup("Dependencias")]
    [ReadOnly]
    [SerializeField]
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
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        velocidadHorizontal = rb.velocity.x;
        velocidadVertical = rb.velocity.y;
        rb.mass = masa;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
#endif
        if (playerInput)
        {
            mover = playerInput.actions["mover"].ReadValue<Vector2>().x;
            mirandoHacia = mover == 0 ? 0 : mover > 0 ? 1 : -1;
            salto = playerInput.actions["salto"].IsPressed();
            correr = playerInput.actions["correr"].IsPressed();

            if (salto && estaEnPiso)
            {
                //rb.velocity = new Vector2(rb.velocity.x, fuerzaDeSalto);
            }
            if (!salto && rb.velocity.y > 0f)
            {
                //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
            if (correr)
            {
                corriendo = true;
            }
            if (!correr)
            {
                corriendo = false;
            }
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

        if (corriendo && estaEnPiso) // Si esta corriendo en el piso
        {
            Correr();
            //rb.velocity = new Vector2(mirandoHacia * velocidadCorriendo, rb.velocity.y);
        }
        else if (corriendo && !estaEnPiso) // Si esta corriendo en el aire xD
        {
            rb.velocity = new Vector2(fuerzaDeMA * mirandoHacia * velocidadCorriendo, rb.velocity.y);
        }
        else if (!corriendo && estaEnPiso) // Si esta caminando en el piso
        {
            Caminar();
            //rb.velocity = new Vector2(mirandoHacia * velocidadCaminando, rb.velocity.y);
        }
        else if (!corriendo && !estaEnPiso) // Si esta caminando en el aire
        {
            rb.velocity = new Vector2(fuerzaDeMA * mirandoHacia * velocidadCaminando, rb.velocity.y);
        }
        else
        {
            Debug.Log("ENTRE AL ELSE UnU");
        }


        estaEnPiso = pisoController.GetEstaEnPiso();
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

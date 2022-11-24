using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;

public class MovimientoController : MonoBehaviour
{
    // Inspector
    //Inputs del inspector
    [BoxGroup("Constantes del personaje")]    
    public float velocidadCaminando = 3f;
    [BoxGroup("Constantes del personaje")]    
    public float velocidadCorriendo = 4f;
    [BoxGroup("Constantes del personaje")]    
    public float fuerzaDeSalto = 16f;

    [BoxGroup("Constantes del personaje")]
    public float fuerzaDeMA= 0.1f;

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

    //Variables privadas obtenidas desde otros gameObject
    [BoxGroup("Dependencias")]
    [ReadOnly] [SerializeField] private Rigidbody2D rb;
    [BoxGroup("Dependencias")]
    [ReadOnly] [SerializeField] private Collider2D playerCollider;
    [BoxGroup("Dependencias")]
    [ReadOnly] [SerializeField] private CompositeCollider2D checkPiso;
    [BoxGroup("Dependencias")]
    [ReadOnly] [SerializeField] private int layerPiso;
    [BoxGroup("Dependencias")]
    [ReadOnly] [SerializeField] private Collider2D pies;
    [BoxGroup("Dependencias")]
    [ReadOnly] [SerializeField] private PlayerInput playerInput;

    void Start()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();
        Application.targetFrameRate = 60;
        layerPiso = LayerMask.NameToLayer("Piso");
        checkPiso = GameObject.Find("Tilemap - Escenario").GetComponent<CompositeCollider2D>();
        pies = gameObject.transform.Find("pies").GetComponent<Collider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.mass = masa;
        playerCollider = gameObject.GetComponent<Collider2D>();
        estaEnPiso = false;
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        velocidadHorizontal = rb.velocity.x;
        velocidadVertical = rb.velocity.y;
        rb.mass = masa;
#endif
        
        Debug.Log(playerInput.actions["Move"].ReadValue<Vector2>());
        mirandoHacia = Input.GetAxisRaw("Horizontal");
        /*
        if (Input.GetButtonDown("Jump") && estaEnPiso)
        {
            rb.velocity = new Vector2(rb.velocity.x, fuerzaDeSalto);
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        if (Input.GetButtonDown("Fire3"))
        {
            corriendo = true;
        }
        if (Input.GetButtonUp("Fire3"))
        {
            corriendo = false;
        }
        */
        GirarPersonaje();        
    }

    private void FixedUpdate()
    {   // Se asigna la velocidad de movimiento
        if(corriendo && estaEnPiso) // Si esta corriendo en el piso
        {
            rb.velocity = new Vector2(mirandoHacia * velocidadCorriendo, rb.velocity.y);
        }
        else if (corriendo && !estaEnPiso) // Si esta corriendo en el aire xD
        {
            rb.velocity = new Vector2(fuerzaDeMA * mirandoHacia * velocidadCorriendo, rb.velocity.y);
        }
        else if (!corriendo && estaEnPiso) // Si esta caminando en el piso
        {
            rb.velocity = new Vector2(mirandoHacia * velocidadCaminando, rb.velocity.y);
        }
        else if (!corriendo && !estaEnPiso) // Si esta caminando en el aire
        {
            rb.velocity = new Vector2(fuerzaDeMA * mirandoHacia * velocidadCaminando, rb.velocity.y);
        }
        else
        {
            Debug.Log("ENTRE AL ELSE UnU");
        }
        //if (pies.IsTouchingLayers(layerPiso)) -> no funciona
        // https://answers.unity.com/questions/1321643/istouchinglayers-is-not-working.html
        //{
        Debug.Log(LayerMask.GetMask(new string[] { "Piso" }));
        if (pies.IsTouchingLayers(LayerMask.GetMask(new string[] { "Piso" })))
        { 
            estaEnPiso = true; 
        }
        else
        {
            estaEnPiso = false;
        }
    }


    void GirarPersonaje()
    {
        if (estaMirandoDerecha && mirandoHacia < 0f || !estaMirandoDerecha && mirandoHacia > 0f)
        {
            estaMirandoDerecha = !estaMirandoDerecha;
            Vector3 nuevaEscalaLocal = transform.localScale;
            nuevaEscalaLocal.x *= -1f;
            transform.localScale = nuevaEscalaLocal;
        }
    }

    public void MoviendoPersonaje(InputAction.CallbackContext ctx)
    {   Vector2 vector_mov = ctx.ReadValue<Vector2>();
        Debug.Log(vector_mov.x+ "||||| " + vector_mov.y);
    }

}

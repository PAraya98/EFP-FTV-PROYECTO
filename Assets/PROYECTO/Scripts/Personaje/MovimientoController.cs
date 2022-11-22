using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class MovimientoController : MonoBehaviour
{
    // Inspector
    //Inputs del inspector
    [BoxGroup("Constantes del personaje")]
    [ReadOnly]
    public float velocidadCaminando = 3f;
    [BoxGroup("Constantes del personaje")]
    [ReadOnly]
    public float velocidadCorriendo = 4f;
    [BoxGroup("Constantes del personaje")]
    [ReadOnly]
    public float fuerzaDeSalto = 16f;
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

    void Start()
    {
        Application.targetFrameRate = 60;
        layerPiso = LayerMask.NameToLayer("Piso");
        checkPiso = GameObject.Find("Tilemap - Escenario").GetComponent<CompositeCollider2D>();
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
        mirandoHacia = Input.GetAxisRaw("Horizontal");
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
        GirarPersonaje();
    }

    private void FixedUpdate()
    {   // Se asigna la velocidad de movimiento

        rb.velocity = new Vector2(
            (System.Convert.ToSingle(!corriendo) * mirandoHacia * velocidadCaminando) +
            (System.Convert.ToSingle(corriendo) * mirandoHacia * velocidadCorriendo)
            , rb.velocity.y);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == layerPiso) estaEnPiso = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == layerPiso) estaEnPiso = false;
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
}


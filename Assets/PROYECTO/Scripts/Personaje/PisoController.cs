using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;
public class PisoController : MonoBehaviour
{

    [BoxGroup("Constantes")] [SerializeField] [Range(0.0f, 1f)]
    private float porcentajeSalto = 1f;
    [BoxGroup("Variables en tiempo real")] [ReadOnly] [SerializeField]
    private bool estaEnPiso;
    [BoxGroup("Variables en tiempo real")] [ReadOnly] [SerializeField]
    private float fuerzaX;
    [BoxGroup("Variables en tiempo real")] [ReadOnly] [SerializeField]
    private float fuerzaY;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField]
    private Rigidbody2D personajeRb;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField]
    private Transform personaje;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField]
    private Rigidbody2D padreRb;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField] private Transform jugadores;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField] private Collider2D pies;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField] 
    private MovimientoController movimientoController;
    // Start is called before the first frame update
    void Start()
    {
        movimientoController = gameObject.transform.parent.GetComponent<MovimientoController>();
        personaje = gameObject.transform.parent;
        personajeRb = gameObject.transform.parent.GetComponent<Rigidbody2D>();
        jugadores = gameObject.transform.parent.transform.parent;
        pies = gameObject.GetComponent<Collider2D>();
    }

    public bool GetEstaEnPiso()
    {
        return estaEnPiso;
    }
    public Rigidbody2D GetPadreRb()
    {
        return padreRb;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Piso"))
        {
            //personaje.parent = jugadores;
            padreRb = null;
            estaEnPiso = false;
        }
    }

    private void FixedUpdate()
    {
        if (padreRb && padreRb.velocity.y > 0f && !movimientoController.EstaSaltando())
        {
            fuerzaY = padreRb.mass * (padreRb.velocity.y / Time.deltaTime) - personajeRb.mass * (personajeRb.velocity.y / Time.deltaTime);
            personajeRb.AddForce(new Vector2(0f, fuerzaY*porcentajeSalto));
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Piso"))
        {
            //personaje.parent = collision.transform;
            padreRb = collision.gameObject.GetComponent<Rigidbody2D>();
            personaje.position = new Vector3(personaje.position.x, personaje.position.y, 0f);
            fuerzaX = padreRb.mass * (padreRb.velocity.x / Time.deltaTime);
            personajeRb.AddForce(new Vector2(fuerzaX, 0f));
            estaEnPiso = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Piso"))
        {
            //personaje.parent = collision.transform;
            padreRb = collision.gameObject.GetComponent<Rigidbody2D>(); ;
            personaje.position = new Vector3(personaje.position.x, personaje.position.y, 0f);
            estaEnPiso = true;
        }
    }
}
//if (pies.IsTouchingLayers(layerPiso)) -> no funciona
// https://answers.unity.com/questions/1321643/istouchinglayers-is-not-working.html
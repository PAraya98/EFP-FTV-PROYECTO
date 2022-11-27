using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;
public class PisoController : MonoBehaviour
{
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


    private void Update()
    {
        /* 
        if (padreRb)
        {
            //Debug.Log(personaje.name + "->" + padreRb.name + "->" + getParentsVelocityY(personaje.transform.parent));

            Debug.Log(personaje.name + " sobre "+ padreRb.name + " =" + padreRb.mass + " * " + "(" + padreRb.velocity.x + " / " + Time.deltaTime + ")");
            fuerzaX = 0f;
            fuerzaY = 0f;
            if (Mathf.Abs(padreRb.velocity.x) > 0.1f)
            {
                //fuerzaX = padreRb.mass * (padreRb.velocity.x / Time.deltaTime);                
                //fuerzaX = getParentsVelocityX(personaje.transform.parent);
            }
            
            if (padreRb.velocity.y > 0f && !movimientoController.EstaSaltando())
            {
                fuerzaY = padreRb.mass * (padreRb.velocity.y / Time.deltaTime) - personajeRb.mass * (personajeRb.velocity.y / Time.deltaTime);
            }

            //float fuerzaX = getParentsVelocityX(personaje.transform.parent);
            //float fuerzaY = getParentsVelocityY(personaje.transform.parent) - (personajeRb.mass * (personajeRb.velocity.y / Time.deltaTime));
            //fuerzaY = (movimientoController.EstaSaltando() ? 0f : fuerzaY >= 0 ? fuerzaY : 0f);
            personajeRb.AddForce(new Vector2(fuerzaX, fuerzaY));
        }
        */
    }
    /*
    private void FixedUpdate()
    {
        if (false)
        {
           // personajeRb.AddForce(new Vector2(fuerzaX, fuerzaY));
        }
    }
    */

    private float getParentsVelocityX(Transform parent)
    {
        Rigidbody2D parentRb = parent.GetComponent<Rigidbody2D>();
        if (parentRb && parent.parent)
        {
            if (Mathf.Abs(parentRb.velocity.x) > 0.01f)
            {
                return parentRb.mass * (parentRb.velocity.x / Time.deltaTime) + getParentsVelocityX(parent.parent);
            }
            else return 0f + getParentsVelocityX(parent.parent);

        }
        else
        {
            return 0f;
        }        
    }

    private float getFirstParentVelocityX(Transform parent)
    {   
        Rigidbody2D parentCollider = parent.GetComponent<Rigidbody2D>();
        if (parentCollider && parent.parent.GetComponent<Rigidbody2D>() && parent.parent.name != "Tilemap - Escenario")
        {
            return getFirstParentVelocityX(parent.parent);
        }
        else
        {
            return parentCollider.mass * (parentCollider.velocity.x / Time.deltaTime);
        }
    }

    private float getParentsVelocityY(Transform parent)
    {
        Rigidbody2D parentCollider = parent.GetComponent<Rigidbody2D>();
        if (parentCollider && parent.parent)
        {
            return parentCollider.mass * (parentCollider.velocity.y / Time.deltaTime) + getParentsVelocityY(parent.parent);
        }
        else
        {
            return 0f;
        }
    }

    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.layer);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Piso"))
        {
            padreRb = collision.gameObject.GetComponent<Rigidbody2D>();

        }
    }
    */

    public bool GetEstaEnPiso()
    {
        return estaEnPiso;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Piso"))
        {
            personaje.parent = jugadores;
            padreRb = null;
            estaEnPiso = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Piso"))
        {
            personaje.parent = collision.transform;
            padreRb = collision.gameObject.GetComponent<Rigidbody2D>(); ;
            personaje.position = new Vector3(personaje.position.x, personaje.position.y, 0f);
            fuerzaX = padreRb.mass * (padreRb.velocity.x / Time.deltaTime);
            fuerzaY = padreRb.mass * (padreRb.velocity.y / Time.deltaTime) - personajeRb.mass * (personajeRb.velocity.y / Time.deltaTime);
            fuerzaY = fuerzaY >= 0 ? fuerzaY : 0f;
            Debug.Log(fuerzaY);
            personajeRb.AddForce(new Vector2(fuerzaX, fuerzaY));
            estaEnPiso = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Piso"))
        {
            personaje.parent = collision.transform;
            padreRb = collision.gameObject.GetComponent<Rigidbody2D>(); ;
            personaje.position = new Vector3(personaje.position.x, personaje.position.y, 0f);
            estaEnPiso = true;
        }
    }
}
//if (pies.IsTouchingLayers(layerPiso)) -> no funciona
// https://answers.unity.com/questions/1321643/istouchinglayers-is-not-working.html
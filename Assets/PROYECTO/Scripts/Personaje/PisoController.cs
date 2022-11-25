using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class PisoController : MonoBehaviour
{
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField]
    private Rigidbody2D personajeRb;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField]
    private Rigidbody2D padreRb;
    [BoxGroup("Dependencias")]
    [ReadOnly] [SerializeField] private Transform jugadores;
    [ReadOnly] [SerializeField] private Collider2D pies;
    // Start is called before the first frame update
    void Start()
    {
        personajeRb = gameObject.transform.parent.GetComponent<Rigidbody2D>();
        jugadores = gameObject.transform.parent.transform.parent;
        pies = gameObject.GetComponent<Collider2D>();
    }


    private void FixedUpdate()
    {
       
        if (padreRb)
        {   
            personajeRb.AddForce(new Vector2(personajeRb.mass * (padreRb.velocity.x/ Time.deltaTime), 0f));

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.layer);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Piso"))
        {
            padreRb = collision.gameObject.GetComponent<Rigidbody2D>();

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Piso"))
        {
            padreRb = null;
        }
    }

}

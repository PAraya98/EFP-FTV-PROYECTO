using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class CollisionController : MonoBehaviour
{ 
    [BoxGroup("Variables en tiempo real")] [ReadOnly] [SerializeField] 
    private bool estaMuerto = false;
    [BoxGroup("Variables en tiempo real")] [ReadOnly] [SerializeField] 
    private bool victoria = false;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField] 
    private Collider2D personaje;
    // Start is called before the first frame update
    void Start()
    {
        personaje = gameObject.GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Muerte"))
        {
            estaMuerto = true;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Victoria"))
        {
            victoria = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Muerte"))
        {
            estaMuerto = true;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Victoria"))
        {
            victoria = true;
        }
    }

    public bool getVictoria()
    {
        return victoria;
    }
    public bool getEstaMuerto()
    {
        return estaMuerto;
    }
}

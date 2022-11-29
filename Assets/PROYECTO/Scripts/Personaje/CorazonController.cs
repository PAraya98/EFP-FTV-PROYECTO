using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorazonController : MonoBehaviour
{
    private CollisionController collisionController;
    // Start is called before the first frame update
    void Start()
    {
        collisionController = transform.parent.GetComponent<CollisionController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Escenario")
            collisionController.SetEstaMuerto();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Escenario")
            collisionController.SetEstaMuerto();
    }

}

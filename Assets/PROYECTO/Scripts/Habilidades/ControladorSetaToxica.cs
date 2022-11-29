using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSetaToxica : MonoBehaviour
{
    // Start is called before the first frame update
    public float tiempoDesaparicion = 15f;
    void Start()
    {
        StartCoroutine(Destruccion());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            Destroy(gameObject);
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Muerte")) 
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            Destroy(gameObject);
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Muerte")) 
            Destroy(gameObject);
    }


    IEnumerator Destruccion ()
    {
        yield return new WaitForSeconds(tiempoDesaparicion);
        Destroy(gameObject);
    }
    
}

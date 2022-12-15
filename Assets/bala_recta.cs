using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala_recta : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public float velocidad;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        StartCoroutine(Destruccion());

    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2d.velocity = transform.right * velocidad;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Piso") || collision.gameObject.layer == LayerMask.NameToLayer("Muerte"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Piso") || collision.gameObject.layer == LayerMask.NameToLayer("Muerte"))
        {
            Destroy(gameObject);
        }
    }
    IEnumerator Destruccion()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}

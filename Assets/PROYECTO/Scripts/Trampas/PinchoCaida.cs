using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchoCaida : MonoBehaviour
{
    public float distanciaVision;
    public int tiempoRespawn;

    private Rigidbody2D rb;
    private bool caida;
    private Vector3 posicionInicial;
    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = transform.position;
        caida = false;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(0, -distanciaVision) + transform.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distanciaVision);
        if (hit.collider != null)
        {
            Debug.Log(hit.transform.tag);

            if (hit.transform.tag == "Player" && caida == false)
            {
                Debug.Log("?entre pincho2");
                rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                rb.AddForce(new Vector2(1, 0));
                caida = true;
                StartCoroutine(Respawn());
            }
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(tiempoRespawn);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.position = posicionInicial;
        caida = false;
    }
}

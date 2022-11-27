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
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2d.velocity = transform.right * velocidad;
        Destroy(gameObject, 3f);
    }
}

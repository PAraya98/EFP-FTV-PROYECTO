using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{
    public Transform[] posiciones;
    public int velocidad;
    public GameObject plataforma;
    public GameObject Target;
    private int indice;
    private bool incremento;
    // Start is called before the first frame update
    void Start()
    {
        GameObject inicio = Instantiate(Target, transform.position, transform.rotation);
        posiciones[0] = inicio.transform;
        indice = 0;
        incremento = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(posiciones.Length > 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, posiciones[indice].position, velocidad * Time.deltaTime);
            if (Vector2.Distance(transform.position, posiciones[indice].position) == 0f)
            {
                if (incremento)
                {
                    if(indice + 1 == posiciones.Length)
                    {
                        incremento = false;
                        indice--;
                    }
                    else
                    {
                        indice++;
                    }
                }
                else
                {
                    if (indice + 1 == 1)
                    {
                        incremento = true;
                        indice++;
                    }
                    else
                    {
                        indice--;
                    }
                }
            }
        }
    }
}

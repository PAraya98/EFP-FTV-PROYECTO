using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorBloqueActivador : MonoBehaviour
{
    public bool estado;

    private GameObject[] bloques1;
    private GameObject[] bloques2;

    // Start is called before the first frame update
    void Start()
    {
        bloques1 = GameObject.FindGameObjectsWithTag("Bloque 1");
        bloques2 = GameObject.FindGameObjectsWithTag("Bloque 2");
        foreach(GameObject bloque in bloques1)
        {
            bloque.SetActive(estado);
        }
        foreach (GameObject bloque in bloques2)
        {
            bloque.SetActive(!estado);
        }
    }

    public void CambiarEstadoBloques()
    {
        estado = !estado;
        foreach (GameObject bloque in bloques1)
        {
            bloque.SetActive(estado);
        }
        foreach (GameObject bloque in bloques2)
        {
            bloque.SetActive(!estado);
        }
    }
}

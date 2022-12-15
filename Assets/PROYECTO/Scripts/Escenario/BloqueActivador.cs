using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloqueActivador : MonoBehaviour
{
    public ControladorBloqueActivador controlador;
    public void Start()
    {
        controlador = GameObject.FindGameObjectWithTag("ControladorBloqueActivacion").GetComponent<ControladorBloqueActivador>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        controlador.CambiarEstadoBloques();
    }
}

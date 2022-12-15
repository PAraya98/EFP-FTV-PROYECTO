using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorObjetoReloj : MonoBehaviour
{
    // Start is called before the first frame update
    public float tiempoDesaparicion;
    private PersonajeData personajeData;
    private MovimientoController movimientoController;
    private List<GameObject> listaPlayer;
    void Start()
    {
        GameObject player1 = GameObject.Find("Player 1");
        GameObject player2 = GameObject.Find("Player 2");
        GameObject player3 = GameObject.Find("Player 3");
        GameObject player4 = GameObject.Find("Player 4");
        listaPlayer = new List<GameObject> { player1, player2, player3, player4 };
        personajeData = gameObject.GetComponent<PersonajeData>();
        for (int i = 0; i < listaPlayer.Count; i++)
        {
            if (listaPlayer[i])
            {
                if (personajeData.ObtenerPlayerName() != listaPlayer[i].name)
                {
                    movimientoController = listaPlayer[i].GetComponent<MovimientoController>();
                    movimientoController.SetVelocidadCaminando(0f);
                    movimientoController.SetVelocidadCorriendo(0f);
                    movimientoController.SetControlReloj(true);
                }
            }
        }
        StartCoroutine(Destruccion(listaPlayer));

    }

    // Update is called once per frame
    void Update()
    {   
    }
    IEnumerator Destruccion(List<GameObject> listaPlayer)
    {
        yield return new WaitForSeconds(tiempoDesaparicion);
        for (int i = 0; i < listaPlayer.Count; i++)
        {
            if (listaPlayer[i])
            {
                if (personajeData.ObtenerPlayerName() != listaPlayer[i].name)
                {
                    movimientoController.SetControlReloj(false);
                    movimientoController = listaPlayer[i].GetComponent<MovimientoController>();
                    movimientoController.SetVelocidadCaminando(movimientoController.GetVelocidadCaminando());
                    movimientoController.SetVelocidadCorriendo(movimientoController.GetVelocidadCorriendo());
                }
            }
        }
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorObjetoRayo : MonoBehaviour
{
    // Start is called before the first frame update
    public float tiempoDesaparicion;
    public float cambioScale;
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
                    movimientoController.SetScales(new Vector3(cambioScale, cambioScale, 0));
                }
            }
        }
        gameObject.transform.position = new Vector2(-2000, -2010);
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
                    movimientoController = listaPlayer[i].GetComponent<MovimientoController>();
                    movimientoController.SetScales(movimientoController.GetScales());
                }
            }
        }
        Destroy(gameObject);
    }
}

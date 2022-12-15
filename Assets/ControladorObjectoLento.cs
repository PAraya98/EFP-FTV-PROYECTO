using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorObjectoLento : MonoBehaviour
{
    // Start is called before the first frame update
    public float tiempoDesaparicion;
    public float lentitud;
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
                    //La lentitud se aplica en un 50% menos
                    movimientoController.SetVelocidadCaminando(movimientoController.GetVelocidadCaminando() * lentitud);
                    movimientoController.SetVelocidadCorriendo(movimientoController.GetVelocidadCorriendo() * lentitud);
                    movimientoController.SetControlLentitud(true);

                }
            }
        }
        gameObject.transform.position = new Vector2(-2000,-2010);
        // BUSCAR COMO HACER ANIMACIONES 
        // AGREGAR SONIDO???
        //+ achicar al personaje (scale)
        //+ slow  (cambiar el relog)
        //+ caparazon de mario ? caparazon espina matar (pa no saltar encima)
        //+ otro caparazon de mario para saltar encima.
        //+ un caparazon que rebote mas que otro y otro que sea mas rapido (EMPUJE XD)
        //+ rebote con la checkpoint (smash)  blanca de mario  (FISICA)
        // ^^estatica y grande, empuje random
        //+ Flecha canon (potencia = fija) -> habildiad


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
                    movimientoController.SetControlLentitud(false);
                    movimientoController.SetVelocidadCaminando(movimientoController.GetVelocidadCaminando());
                    movimientoController.SetVelocidadCorriendo(movimientoController.GetVelocidadCorriendo());
                }
            }
        }
        Destroy(gameObject);
    }
}

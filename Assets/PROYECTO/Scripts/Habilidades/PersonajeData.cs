using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class PersonajeData : MonoBehaviour
{
    [BoxGroup("Dependencias")] [ReadOnly]
    public GameObject player;

    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField]
    private MovimientoController movimientoController;

    [BoxGroup("Variables en tiempo real")] [ReadOnly] [SerializeField]
    private string playerName;
    [BoxGroup("Variables en tiempo real")] [ReadOnly] [SerializeField]
    private int direccionInicial;


    void Awake()
    {
        movimientoController = player.GetComponent<MovimientoController>();
        playerName = player.name;
        direccionInicial = movimientoController.GetMirandoHacia();
    }



    private void Update()
    {
        if(!player)
        {
            player = GameObject.Find(playerName);
            if(player)
            {
                movimientoController = player.GetComponent<MovimientoController>();
            }
        }
    }

    // Toda la información del Jugador 
    // Evitar utilizarlo, a no ser que sea algo muy especúƒico,
    // en vez de eso mejor agregar métodos genéricos
    public GameObject ObtenerPlayer() 
    {
        if(player) return player;
        return null;
    }

    public int ObtenerDireccionInicial()
    {
        return direccionInicial;
    }

    public int ObtenerDireccionActual()
    {
        return movimientoController.GetMirandoHacia();
    }

    public Rigidbody2D ObtenerPlayerRb()
    {
        if (player) return player.GetComponent<Rigidbody2D>();
        else return null;
    }

    public string ObtenerPlayerName()
    {
        return playerName;
    }
    public float ObtenerVelocidadCorriendo()
    {
        return movimientoController.GetVelocidadCorriendo();
    }
    public float ObtenerVelocidadCaminando()
    {
        return movimientoController.GetVelocidadCaminando();
    }
    public void CambiarVelocidadCorriendo(float NuevaVelocidad) 
    {
        movimientoController.SetVelocidadCorriendo(NuevaVelocidad);
    }
    public void CambiarVelocidadCaminando(float NuevaVelocidad)
    {
        movimientoController.SetVelocidadCaminando(NuevaVelocidad);
    }
}

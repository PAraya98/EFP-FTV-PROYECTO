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
    private int miraInicial;
    void Start()
    {
        movimientoController = player.GetComponent<MovimientoController>();
        playerName = player.name;
        miraInicial = movimientoController.GetMirandoHacia();
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
    // Evitar utilizarlo, a no ser que sea algo muy específico,
    // en vez de eso mejor agregar métodos genéricos
    public GameObject ObtenerPlayer() 
    {
        if(player) return player;
        return null;
    }

    public float ObtenerDireccionInicial()
    {
        return miraInicial;
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
}

using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffController : MonoBehaviour
{
    // Start is called before the first frame update

    private List<GameObject> listaPlayer;
    private PersonajeData personajeData;
    private MovimientoController movimientoController;

    void Start()
    {
        GameObject player1 = GameObject.Find("Player 1");
        GameObject player2 = GameObject.Find("Player 2");
        GameObject player3 = GameObject.Find("Player 3");
        GameObject player4 = GameObject.Find("Player 4");

        listaPlayer = new List<GameObject> { player1, player2, player3, player4 };
        
    }


    // Update is called once per frame
    void Update()
    {
        for (int i=0;i<listaPlayer.Count ;i++)
        {
            movimientoController = listaPlayer[i].GetComponent<MovimientoController
                >();
            
            
        }
    }
}

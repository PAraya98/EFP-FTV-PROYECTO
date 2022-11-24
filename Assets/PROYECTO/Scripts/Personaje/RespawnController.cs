using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class RespawnController : MonoBehaviour
{

    [BoxGroup("Valores requeridos")]
    public GameObject playerPrefab;
    //Variables del juego en movimiento
    [BoxGroup("Variables en tiempo real")]
    [ReadOnly]
    public bool tienePlayer1 = false;
    [BoxGroup("Variables en tiempo real")]
    [ReadOnly]
    public bool tienePlayer2 = false;
    [BoxGroup("Variables en tiempo real")]
    [ReadOnly]
    public bool tienePlayer3 = false;
    [BoxGroup("Variables en tiempo real")]
    [ReadOnly]
    public bool tienePlayer4 = false;

    [BoxGroup("Dependencias")]
    [ReadOnly] [SerializeField] private GameObject player1;
    private Vector3 player1Position;
    private Color player1Color;

    [BoxGroup("Dependencias")]
    [ReadOnly] [SerializeField] private GameObject player2;
    private Vector3 player2Position;
    private Color player2Color;

    [BoxGroup("Dependencias")]
    [ReadOnly] [SerializeField] private GameObject player3;
    private Vector3 player3Position;
    private Color player3Color;

    [BoxGroup("Dependencias")]
    [ReadOnly] [SerializeField] private GameObject player4;
    private Vector3 player4Position;
    private Color player4Color;

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("Player 1");
        if (player1)
        {   tienePlayer1 = true;
            player1Position = player1.GetComponent<Transform>().position;
            player1Color = player1.GetComponent<SpriteRenderer>().color;
        }

        player2 = GameObject.Find("Player 2");
        if (player2)
        {
            tienePlayer2 = true;
            player2Position = player2.GetComponent<Transform>().position;
            player2Color = player2.GetComponent<SpriteRenderer>().color;
        }

        player3 = GameObject.Find("Player 3");
        if (player3)
        {
            tienePlayer3 = true;
            player3Position = player3.GetComponent<Transform>().position;
            player3Color = player3.GetComponent<SpriteRenderer>().color;
        }

        player4 = GameObject.Find("Player 4");
        if (player4)
        {
            tienePlayer4 = true;
            player4Position = player4.GetComponent<Transform>().position;
            player4Color = player4.GetComponent<SpriteRenderer>().color;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        //TODO: CREAR EL JUGADOR -> HACERLO HIJO DE JUGADORES -> DEFINIR PARÁMETROS INICIALES
        if(tienePlayer1 && !player1)
        {
            player1 = Instantiate(playerPrefab);
            player1.transform.SetParent(gameObject.transform);
            player1.transform.position = player1Position;
            player1.GetComponent<SpriteRenderer>().color = player1Color;
            player1.name = "Player 1";
        }
        if (tienePlayer2 && !player2)
        {
            player2 = Instantiate(playerPrefab);
            player2.transform.SetParent(gameObject.transform);
            player2.transform.position = player2Position;
            player2.GetComponent<SpriteRenderer>().color = player2Color;
            player2.name = "Player 2";
        }
        if (tienePlayer3 && !player3)
        {
            player3 = Instantiate(playerPrefab);
            player3.transform.SetParent(gameObject.transform);
            player3.transform.position = player3Position;
            player3.GetComponent<SpriteRenderer>().color = player3Color;
            player3.name = "Player 3";
        }
        if (tienePlayer4 && !player4)
        {
            player4 = Instantiate(playerPrefab);
            player4.transform.SetParent(gameObject.transform);
            player4.transform.position = player4Position;
            player4.GetComponent<SpriteRenderer>().color = player4Color;
            player4.name = "Player 4";
        }
    }
}

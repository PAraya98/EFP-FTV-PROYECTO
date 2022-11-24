using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;
public class RespawnController : MonoBehaviour
{

    [BoxGroup("Valores requeridos")]
    public GameObject playerPrefab;
    [BoxGroup("Valores requeridos")]
    public GameObject playerInputPrefab; 

    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField]
    private List<GameObject> listaPlayer;

    private Vector3[] listaPlayerPosicion;
    private Color[] listaPlayerColor;
    private Gamepad[] listaPlayerMando;

    // Start is called before the first frame update

    void agregarInput(GameObject player, string constrolScheme, InputDevice[] pairWithDevices)
    {
        var instance = PlayerInput.Instantiate(playerInputPrefab, controlScheme: constrolScheme, pairWithDevices: pairWithDevices);
        instance.transform.SetParent(player.transform);
        instance.transform.position = player.transform.position;
        instance.name = "PlayerInput";
    }

    struct instanciaJugador 
    {
        GameObject  player;
        Color       color;
        Transform   posision;
        int         hashMando;
    }

    void Start()
    {
        GameObject player1 = GameObject.Find("Player 1");
        GameObject player2 = GameObject.Find("Player 2");
        GameObject player3 = GameObject.Find("Player 3");
        GameObject player4 = GameObject.Find("Player 4");

        listaPlayer = new List<GameObject> { player1, player2, player3, player4 };
        listaPlayerPosicion = new Vector3[] { player1.transform.position, player2.transform.position, player3.transform.position, player4.transform.position };
        listaPlayerColor = new Color[] { player1.GetComponent<SpriteRenderer>().color, player2.GetComponent<SpriteRenderer>().color, player3.GetComponent<SpriteRenderer>().color, player4.GetComponent<SpriteRenderer>().color };
        listaPlayerMando = new Gamepad[] {new Gamepad(), new Gamepad(), new Gamepad(), new Gamepad() };
        int i = 0;

        foreach (GameObject player in listaPlayer)
        {
            Debug.Log(Gamepad.all.Count);
            if(Gamepad.all.Count > i)
            {
                player.SetActive(true);
                listaPlayerMando[i] = Gamepad.all[i];
                agregarInput(listaPlayer[i], "Gamepad", new InputDevice[] { Gamepad.all[i] });
            }
            else
            {
                player.SetActive(false);
                GameObject.Find("Panel Info - Player "+ (i+1)).SetActive(false);
            }
            i++;
        }    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //TODO: CREAR EL JUGADOR -> HACERLO HIJO DE JUGADDORAES -> DEFINIR PARÁMETROS INICIALES

        for (int i = 0; i < listaPlayer.Count; i++)
        {
            if (!listaPlayer[i])
            {
                listaPlayer.RemoveAt(i);
                GameObject aux = Instantiate(playerPrefab);
                aux.transform.SetParent(gameObject.transform);
                aux.transform.position = listaPlayerPosicion[i];
                aux.GetComponent<SpriteRenderer>().color = listaPlayerColor[i];
                aux.name = "Player " + (i + 1);
                agregarInput(aux, "Gamepad", new InputDevice[] { listaPlayerMando[i] });
                listaPlayer.Insert(i, aux);
            }
        }
    }

    
}   

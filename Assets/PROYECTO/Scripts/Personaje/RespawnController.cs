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

    [BoxGroup("Dependencias")] [ReadOnly]
    public GameObject player1;
    [BoxGroup("Dependencias")] [ReadOnly]
    public GameObject player2;
    [BoxGroup("Dependencias")] [ReadOnly]
    public GameObject player3;  
    [BoxGroup("Dependencias")] [ReadOnly]
    public GameObject player4;

    [ReadOnly] [SerializeField] private PlayerInputManager jugadorControl;

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

        jugadorControl = gameObject.GetComponent<PlayerInputManager>();

        listaPlayer = new List<GameObject> { player1, player2, player3, player4 };
        listaPlayerPosicion = new Vector3[] { player1.transform.position, player2.transform.position, player3.transform.position, player4.transform.position };
        listaPlayerColor = new Color[] { player1.GetComponent<SpriteRenderer>().color, player2.GetComponent<SpriteRenderer>().color, player3.GetComponent<SpriteRenderer>().color, player4.GetComponent<SpriteRenderer>().color };
        listaPlayerMando = new Gamepad[] {new Gamepad(), new Gamepad(), new Gamepad(), new Gamepad() };
        int i = 0;

        foreach (Gamepad mando in Gamepad.all) {
            if (i < 4)
            {
                listaPlayer[i].SetActive(true);
                listaPlayerMando[i] = Gamepad.all[i];
                agregarInput(listaPlayer[i], "Gamepad", new InputDevice[] { Gamepad.all[i] }); 
            }
            i++;
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //TODO: CREAR EL JUGADOR -> HACERLO HIJO DE JUGADDORAES -> DEFINIR PARÁMETROS INICIALES
        int i = 0;
        foreach (GameObject player in listaPlayer)
        { 
            if(!player)
            {
                listaPlayer.RemoveAt(i);// ARREGLAR NO FUNCIONA LA ASIGNACIÓN DENTRO DEL ARRAY
                GameObject aux = Instantiate(playerPrefab);
                aux.transform.SetParent(gameObject.transform);
                aux.transform.position = listaPlayerPosicion[i];
                aux.GetComponent<SpriteRenderer>().color = listaPlayerColor[i];
                aux.name = "Player " + (i + 1);
                agregarInput(aux, "Gamepad", new InputDevice[] { listaPlayerMando[i]});
                listaPlayer.Insert(i, aux);
            }
        }
    }

    
}   

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;
using Cinemachine;
using System;
using static Unity.Burst.Intrinsics.X86;

public class RespawnController : MonoBehaviour
{

    [BoxGroup("Valores requeridos")]
    public GameObject playerPrefab;
    [BoxGroup("Valores requeridos")]
    public GameObject playerInputPrefab;
    [BoxGroup("Valores requeridos")]
    public bool contarEntradaTeclado = false;

    public GameObject sonidoRespawn;
    public float tiempoDesaparicion;

    [BoxGroup("Dependencias")]
    [ReadOnly]
    [SerializeField]
    private List<GameObject> listaPlayer;
    
    private Vector3[] listaPlayerPosicion;
    private Color[] listaPlayerColor;
    private InputDevice[] listaPlayerMando;
    
    [BoxGroup("Dependencias")]
    [ReadOnly]
    [SerializeField]
    private CinemachineTargetGroup cinemachinetargetgroup;

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
        GameObject player;
        Color color;
        Transform posision;
        int hashMando;
        
    }

    void Start()
    {
        // INICIA EL JUEGO ASIGNAR CAMARA LOS PLAYER ACTUALES..
        GameObject player1 = GameObject.Find("Player 1");
        GameObject player2 = GameObject.Find("Player 2");
        GameObject player3 = GameObject.Find("Player 3");
        GameObject player4 = GameObject.Find("Player 4");

        cinemachinetargetgroup = GameObject.Find("TargetGroup - CamaraSeguimiento").GetComponent<CinemachineTargetGroup>();

        listaPlayer = new List<GameObject> { player1, player2, player3, player4 };
        
        listaPlayerPosicion = new Vector3[] { player1.transform.position, player2.transform.position, player3.transform.position, player4.transform.position };
        listaPlayerColor = new Color[] { player1.GetComponent<SpriteRenderer>().color, player2.GetComponent<SpriteRenderer>().color, player3.GetComponent<SpriteRenderer>().color, player4.GetComponent<SpriteRenderer>().color };
        listaPlayerMando = new InputDevice[] { new Gamepad(), new Gamepad(), new Gamepad(), new Gamepad() };
        int i = 0;

        foreach (GameObject player in listaPlayer)
        {
            if (Gamepad.all.Count > i)
            {
                listaPlayerMando[i] = Gamepad.all[i];
                agregarInput(listaPlayer[i], "Gamepad", new InputDevice[] { Gamepad.all[i] });
            }
            else if (contarEntradaTeclado && i == Gamepad.all.Count)
            { 
                listaPlayerMando[i] = Keyboard.current;
                agregarInput(listaPlayer[i], "Keyboard", new InputDevice[] { Keyboard.current });
            }
            else
            {
                player.SetActive(false);
                GameObject.Find("Panel Info - Player " + (i + 1)).SetActive(false);
                cinemachinetargetgroup.RemoveMember(listaPlayer[i].transform);

            }
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: CREAR EL JUGADOR -> HACERLO HIJO DE JUGADDORAES -> DEFINIR PAR�METROS INICIALES
        for (int i = 0; i < listaPlayer.Count; i++)
        {
            if (!listaPlayer[i])
            {
                listaPlayer.RemoveAt(i);
                GameObject aux = Instantiate(playerPrefab);
                aux.transform.SetParent(gameObject.transform);
                aux.transform.position = listaPlayerPosicion[i];
                aux.GetComponent<SpriteRenderer>().color = listaPlayerColor[i];
                aux.transform.Find("muerte").GetComponent<SpriteRenderer>().color = listaPlayerColor[i];
                aux.name = "Player " + (i + 1);
                agregarInput(aux, "Gamepad", new InputDevice[] { listaPlayerMando[i] });
                listaPlayer.Insert(i, aux);
                cinemachinetargetgroup.AddMember(aux.transform, 1, 2);
                GameObject sonidorespawn = Instantiate(sonidoRespawn);
                StartCoroutine(SonidoRespawn(sonidorespawn));
                // ASIGNAR LACA AMRA PRIORIDAD 1 Y RAIDO
            }
        }
    }
    IEnumerator SonidoRespawn(GameObject sonidorespawn)
    {
        yield return new WaitForSeconds(tiempoDesaparicion);
        Destroy(sonidorespawn);
    }

}

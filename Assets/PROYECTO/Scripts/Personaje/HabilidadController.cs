using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;
using System.Linq;

[System.Serializable]
public class Habilidad
{
    public GameObject gameobject;
    [Range(0f, 100f)]
    public float probabilidad;
}

public class HabilidadController : MonoBehaviour
{

    [BoxGroup("Constantes del personaje")]
    public int tiempoDeCooldown = 3;

    public List<Habilidad> listaHabilidades;

    [BoxGroup("Variables en tiempo real")] [ReadOnly]
    public bool error = false;
    [BoxGroup("Variables en tiempo real")] [ReadOnly] [SerializeField]
    private int cooldown;
    [BoxGroup("Variables en tiempo real")] [ReadOnly] [SerializeField]
    private float habilidadObtenida;
    [BoxGroup("Variables en tiempo real")] [ReadOnly] [SerializeField] 
    private bool tieneHabilidad;
    [BoxGroup("Variables en tiempo real")] [ReadOnly] [SerializeField]
    private GameObject habilidadActual;

    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField] 
    private PlayerInput playerInput;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField] 
    private Transform habilidadSpawn;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField] 
    private CollisionController collisionController;
    
    // Start is called before the first frame update
    
    void Start()
    {
        cooldown = tiempoDeCooldown;
        float probabilidad = 100f;
        tieneHabilidad = false;
        collisionController = gameObject.GetComponent<CollisionController>();
        habilidadSpawn = transform.Find("habilidad spawn").transform;
        foreach (Habilidad habilidad in listaHabilidades)
        {
            probabilidad = probabilidad - habilidad.probabilidad;
        }
        if (probabilidad != 0f) error = true;
        if (!error) 
        {
            listaHabilidades = listaHabilidades.OrderBy(o => o.probabilidad).ToList();
            StartCoroutine(SetHabilidad());
        } 
        else Debug.Log("Las probabilidades no suman 100%");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerInput)
        {
            if (!error && playerInput.actions["habilidad"].IsPressed() && tieneHabilidad && !collisionController.getEstaMuerto() && !collisionController.getVictoria())
            {
                GameObject habilidad = Instantiate(habilidadActual, habilidadSpawn.position, Quaternion.identity);
                habilidad.SetActive(false);
                PersonajeData personajeData = habilidad.AddComponent<PersonajeData>();
                personajeData.player = gameObject;
                habilidad.transform.parent = GameObject.Find("Habilidades").transform;
                habilidad.SetActive(true);
                tieneHabilidad = false;
            }
        }
        else
        {
            if (gameObject.transform.Find("PlayerInput")) playerInput = gameObject.transform.Find("PlayerInput").GetComponent<PlayerInput>();
        }
    }

    public bool getTieneHabilidad() { return tieneHabilidad; }
    public Sprite getSpriteHabilidad() { return habilidadActual ? habilidadActual.GetComponent<SpriteRenderer>().sprite : null; }
    public int getCooldown() { return cooldown; }

    public Transform getTransformHabilidad() { return habilidadActual ? habilidadActual.transform : null; }
    private bool gastoHabilidad() { return !tieneHabilidad; }
    IEnumerator SetHabilidad()
    {
        //Cooldown de habilidad
        for(int i = tiempoDeCooldown; i > 0; i--)
        {
            cooldown = i;
            yield return new WaitForSeconds(1);
        }
        //Se asigna la habilidad
        habilidadObtenida = Random.Range(0f, 100f);
        Debug.Log("La habilidad obtenida es: "+ habilidadObtenida);
        float acumulador = 0f;

        for (int i = 0; i < listaHabilidades.Count; i++)
        {
            acumulador += listaHabilidades[i].probabilidad;

            if (habilidadObtenida <= acumulador)
            {
                habilidadActual = listaHabilidades[i].gameobject;
                break;
            }
        }
        //Espera a que el player gaste la habilidad
        tieneHabilidad = true;
        yield return new WaitUntil(gastoHabilidad);
        StartCoroutine(SetHabilidad());
    }
}

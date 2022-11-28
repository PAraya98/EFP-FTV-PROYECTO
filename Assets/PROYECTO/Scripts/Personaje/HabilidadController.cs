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
    private bool tieneHabilidad;
    [BoxGroup("Variables en tiempo real")] [ReadOnly] [SerializeField]
    private GameObject habilidadActual;

    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField] 
    private PlayerInput playerInput;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField] 
    private Transform habilidadSpawn;

    // Start is called before the first frame update

    void Start()
    {
        cooldown = tiempoDeCooldown;
        float probabilidad = 100f;
        tieneHabilidad = false;
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
    void Update()
    {
        if (playerInput)
        {
            if (!error && playerInput.actions["habilidad"].IsPressed() && tieneHabilidad)
            {
                GameObject habilidad = Instantiate(habilidadActual, habilidadSpawn.position, Quaternion.identity);
                habilidad.transform.parent = GameObject.Find("Habilidades").transform;
                tieneHabilidad = false;
                habilidadActual = null;
            }
        }
        else
        {
            if (gameObject.transform.Find("PlayerInput")) playerInput = gameObject.transform.Find("PlayerInput").GetComponent<PlayerInput>();
        }
    }

    public bool getTieneHabilidad() { return tieneHabilidad; }
    public Sprite getSpriteHabilidad() { return tieneHabilidad ? habilidadActual.GetComponent<SpriteRenderer>().sprite : null; }
    public int getCooldown() { return cooldown; }

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
        float habilidadObtenida = Random.Range(0f, 100f);
        float rango_inicial;
        float rango_final;
        for (int i = 0; i < listaHabilidades.Count; i++)
        {
            if (i == 0)
            {
                rango_inicial = 0f;
                rango_final = listaHabilidades[i].probabilidad;
            }
            else if (i == listaHabilidades.Count-1)
            {
                rango_inicial = listaHabilidades[i].probabilidad;
                rango_final = 100f;
            }
            else
            {
                rango_inicial = listaHabilidades[i].probabilidad;
                rango_final = listaHabilidades[i + 1].probabilidad;
            }

            if (habilidadObtenida >= rango_inicial && habilidadObtenida <= rango_final)
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

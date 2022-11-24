using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;
public class InfoPlayerUIController : MonoBehaviour
{
    [BoxGroup("Valores requeridos")]
    public GameObject player;

    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField]
    private string playerName;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField]
    private TextMeshProUGUI textMuertes;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField]
    private TextMeshProUGUI textVictorias;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField]
    private Collider2D playerCollider;

    [BoxGroup("Variables en tiempo real")] [ReadOnly] [SerializeField]
    private bool estaMuerto = false;
    [BoxGroup("Variables en tiempo real")] [ReadOnly] [SerializeField]
    private int contadorMuertes = 0;
    [BoxGroup("Variables en tiempo real")] [ReadOnly] [SerializeField]
    private int contadorVictorias = 0;


    void Start()
    {
        if(player)
        {
            playerName = player.name;
            playerCollider = player.GetComponent<Collider2D>();
            textMuertes = gameObject.transform.Find("Panel - Info Muertes")
                          .Find("Text - Contador Muertes")
                          .GetComponent<TextMeshProUGUI>();
            textVictorias = gameObject.transform.Find("Panel - Info Victorias")
                          .Find("Text - Contador Victorias")
                          .GetComponent<TextMeshProUGUI>();
            textMuertes.text = "x" + contadorMuertes;
            textVictorias.text = "x" + contadorVictorias;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (estaMuerto && !player)
        {
            player = GameObject.Find(playerName);
            if(player)
            {
                playerCollider = player.GetComponent<Collider2D>();
                estaMuerto = false;
            }            
        }
        if (!estaMuerto && player && playerCollider.IsTouchingLayers(LayerMask.GetMask(new string[] { "Muerte" })))
        {
            contadorMuertes++;
            textMuertes.text = "x" + contadorMuertes;
            estaMuerto = true;
        }
        
    }
}

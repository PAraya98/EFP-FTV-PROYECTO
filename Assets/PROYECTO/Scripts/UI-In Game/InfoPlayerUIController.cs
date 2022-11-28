using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;
using UnityEngine.UI;
public class InfoPlayerUIController : MonoBehaviour
{
    [BoxGroup("Valores requeridos")]
    public GameObject player;

    [BoxGroup("Variables en tiempo real")] [ReadOnly] [SerializeField]
    private bool estaMuerto = false;
    [BoxGroup("Variables en tiempo real")] [ReadOnly] [SerializeField]
    private int contadorMuertes = 0;
    [BoxGroup("Variables en tiempo real")] [ReadOnly] [SerializeField]
    private bool victoria = false;
    [BoxGroup("Variables en tiempo real")] [ReadOnly] [SerializeField]
    private int contadorVictorias = 0;

    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField]
    private string playerName;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField]
    private TextMeshProUGUI textMuertes;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField]
    private TextMeshProUGUI textVictorias;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField]
    private TextMeshProUGUI textCooldown;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField]
    private Image imageHabilidad;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField]
    private CollisionController collisionController;
    [BoxGroup("Dependencias")] [ReadOnly] [SerializeField]
    private HabilidadController habilidadController;
    


    void Start()
    {
        if(player)
        {
            victoria = false;
            estaMuerto = false;
            collisionController = player.GetComponent<CollisionController>();
            habilidadController = player.GetComponent<HabilidadController>();
            playerName = player.name;

            //Panel de muertes
            textMuertes = gameObject.transform
                          .Find("Panel - Info Muertes")
                          .Find("Text - Contador Muertes")
                          .GetComponent<TextMeshProUGUI>();
            textMuertes.text = "x" + contadorMuertes;

            // panel de victorias
            textVictorias = gameObject.transform
                          .Find("Panel - Info Victorias")
                          .Find("Text - Contador Victorias")
                          .GetComponent<TextMeshProUGUI>();
            textVictorias.text = "x" + contadorVictorias;

            // panel de habilidad

            textCooldown = gameObject.transform
                          .Find("Panel - Habilidad")
                          .Find("Text - Cooldown")
                          .GetComponent<TextMeshProUGUI>();

            imageHabilidad = gameObject.transform
                          .Find("Panel - Habilidad")
                          .Find("Image - Habilidad")
                          .GetComponent<Image>();

            imageHabilidad.enabled = false;

        }
        
    }

    // Update is called once per frame

    public int GetContadorVictorias()
    {
        return contadorVictorias;
    }
    void Update()
    {
        if (!player)
        {
            player = GameObject.Find(playerName);
            if (player)
            {
                habilidadController = player.GetComponent<HabilidadController>();
                collisionController = player.GetComponent<CollisionController>();
                victoria = false;
                estaMuerto = false;
            }
        }
        else
        {
            if (!victoria && collisionController.getVictoria())
            {
                contadorVictorias++;
                textVictorias.text = "x" + contadorVictorias;
                victoria = true;
            }
            if (!estaMuerto && collisionController.getEstaMuerto())
            {
                contadorMuertes++;
                textMuertes.text = "x" + contadorMuertes;
                estaMuerto = true;
            }          
            if(!habilidadController.getTieneHabilidad())
            {
                textCooldown.enabled = true;
                imageHabilidad.enabled = false;
                textCooldown.text = habilidadController.getCooldown().ToString();
            }
            else
            {   
                textCooldown.enabled = false;
                imageHabilidad.enabled = true;
                imageHabilidad.sprite = habilidadController.getSpriteHabilidad();
            }
        }
        
    }
}

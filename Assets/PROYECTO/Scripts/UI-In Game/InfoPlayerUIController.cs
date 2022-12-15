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

    //Variables en tiempo real
    private bool estaMuerto = false;
    private int contadorMuertes = 0;
    private bool victoria = false;
    private int contadorVictorias = 0;

    // Dependencias
    private string playerName;
    private TextMeshProUGUI textMuertes;
    private TextMeshProUGUI textVictorias;
    private TextMeshProUGUI textCooldown;
    private RawImage imageHabilidad;
    private CollisionController collisionController;
    private HabilidadController habilidadController;


    void Start()
    {
        RuntimePreviewGenerator.OrthographicMode = false;
        //Para que la imágen sea transparente  Pero da Lag 
        //RuntimePreviewGenerator.BackgroundColor = new Color(0, 0, 0, 0);
        RuntimePreviewGenerator.PreviewDirection = new Vector3(0, 0, 1);
        RuntimePreviewGenerator.OrthographicMode = true;

        if (player)
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
                          .GetComponent<RawImage>();

            imageHabilidad.enabled = false;

        }
        
    }

    public static Texture2D readableClone(Texture2D texture2D)
    {

        RenderTexture rt = new RenderTexture(texture2D.width, texture2D.height, 24);
        RenderTexture.active = rt;
        Graphics.Blit(texture2D, rt);

        Texture2D result = new Texture2D(texture2D.width, texture2D.height);
        result.ReadPixels(new Rect(0, 0, texture2D.width, texture2D.height), 0, 0);
        result.Apply();

        return result;
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
                imageHabilidad.texture = RuntimePreviewGenerator.GenerateModelPreview(habilidadController.getTransformHabilidad());
                //habilidadController.getSpriteHabilidad();
            }
        }
        
    }
}

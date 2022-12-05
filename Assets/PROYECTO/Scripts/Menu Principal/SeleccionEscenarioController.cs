using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SeleccionEscenarioController : MonoBehaviour
{
    // Start is called before the first frame update
    [BoxGroup("Valores Requeridos")]
    public GameObject escenario;
    [BoxGroup("Valores Requeridos")]
    public string nombreEscenario;
    [BoxGroup("Valores Requeridos")] [Scene]
    public string escena;


    void Start()
    {

        gameObject.transform
           .Find("Text - Titulo")
           .GetComponent<TextMeshProUGUI>()
           .text = nombreEscenario;

        // Dibujo del escenario como prefab
        RuntimePreviewGenerator.OrthographicMode = false;
        //RuntimePreviewGenerator.BackgroundColor = new Color(0, 170, 228, 0);
        RuntimePreviewGenerator.PreviewDirection = new Vector3(0, 0, 1);
        RuntimePreviewGenerator.OrthographicMode = true;
        RuntimePreviewGenerator.RenderSupersampling = 2.0f;

        gameObject.transform
            .Find("Image Container")
            .Find("Image - Escenario")
            .GetComponent<RawImage>()
            .texture = RuntimePreviewGenerator.GenerateModelPreview(escenario.transform, 400, 400);
    }

    public void CargarEscena()
    {
        StartCoroutine(LoadYourAsyncScene());
    }
    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(escena);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

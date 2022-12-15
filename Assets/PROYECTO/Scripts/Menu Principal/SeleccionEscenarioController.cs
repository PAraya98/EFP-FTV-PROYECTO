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
    [Header("Valores requeridos")]
    public Texture escenario;
    public string nombreEscenario;
    public bool tieneIntro = true;
    [Scene]
    public string escena;

    private Animator animatorIntro;
    void Start()
    {

        gameObject.transform
           .Find("Text - Titulo")
           .GetComponent<TextMeshProUGUI>()
           .text = nombreEscenario;


        gameObject.transform
            .Find("Image Container")
            .Find("Image - Escenario")
            .GetComponent<RawImage>()
            .texture = escenario;
        
        animatorIntro = GameObject.Find("Panel -Intro").GetComponent<Animator>();
    }

    public void CargarEscena()
    {
        if (!tieneIntro) StartCoroutine(LoadYourAsyncScene());
        else StartCoroutine(MostrarIntro());
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
    IEnumerator MostrarIntro()
    {
        animatorIntro.Play("intro");
        if (animatorIntro.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            yield return new WaitForSeconds(animatorIntro.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(animatorIntro.GetCurrentAnimatorStateInfo(0).length);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(escena);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

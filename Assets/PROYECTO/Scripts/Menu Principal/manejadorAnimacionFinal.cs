using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class manejadorAnimacionFinal : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator animator;
    public SpriteRenderer personaje;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public void InicializarTransisionFinal(Color color)
    {
        Color personaje_color = new Color(color.r, color.g, color.b, 100f);
        personaje.color = personaje_color;
        StartCoroutine(startAnimacionFinal());
    }

    IEnumerator startAnimacionFinal()
    {
        yield return new WaitForSeconds(5f);

        List<GameObject> rootObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);

        // iterate root objects and do something
        for (int i = 0; i < rootObjects.Count; ++i)
        {
            GameObject gameObject = rootObjects[i];
            if (gameObject.name != "Animacion_Final")
            {
                gameObject.SetActive(false);
            }
        }
        animator.Play("animatorfinalPlayer", 1);
        if (animator.GetCurrentAnimatorStateInfo(1).IsName("idle"))
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(1).length);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(1).length);
        SceneManager.LoadScene("MenuPrincipal");
    }  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorObjetoRayo : MonoBehaviour
{
    // Start is called before the first frame update
    public float tiempoDesaparicion;
    private PersonajeData personajeData;
    private List<GameObject> listaPlayer;
    private Animator animator;
    private HabilidadController habilidad;
    void Start()
    {
        GameObject player1 = GameObject.Find("Player 1");
        GameObject player2 = GameObject.Find("Player 2");
        GameObject player3 = GameObject.Find("Player 3");
        GameObject player4 = GameObject.Find("Player 4");
        listaPlayer = new List<GameObject> { player1, player2, player3, player4 };
        personajeData = gameObject.GetComponent<PersonajeData>();
        for (int i = 0; i < listaPlayer.Count; i++)
        {
            if (listaPlayer[i])
            {
                if (personajeData.ObtenerPlayerName() != listaPlayer[i].name)
                {
                    animator = listaPlayer[i].GetComponent<Animator>();
                    habilidad = listaPlayer[i].GetComponent<HabilidadController>();
                    habilidad.SetHabilidadControl(false);
                    animator.SetBool("chico",true);
                }
            }
        }
        StartCoroutine(Destruccion(listaPlayer));
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    IEnumerator Destruccion(List<GameObject> listaPlayer)
    {
        yield return new WaitForSeconds(tiempoDesaparicion);
        for (int i = 0; i < listaPlayer.Count; i++)
        {
            if (listaPlayer[i])
            {
                if (personajeData.ObtenerPlayerName() != listaPlayer[i].name)
                {
                    animator = listaPlayer[i].GetComponent<Animator>();
                    habilidad = listaPlayer[i].GetComponent<HabilidadController>();
                    StartCoroutine(AnimationPlayer(animator, habilidad));
                }
            }
        }
        yield return new WaitForSeconds(2.0f);
        habilidad.SetHabilidadControl(true); // Probar dps si funciona 
        Destroy(gameObject);

    }
    IEnumerator AnimationPlayer(Animator animator, HabilidadController habilidad)
    {
        // COMPROBAR DESPUES SI LOS PLAYER ESTAN CONECTADO SE ACHICAN AL MISMO TIEMPO
        //idle grande
        animator.SetBool("chico", false);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle grande"))
        {
            yield return new WaitForSeconds(
                animator.GetCurrentAnimatorStateInfo(3).length - animator.GetCurrentAnimatorStateInfo(3).normalizedTime);
        }
        // chico
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(3).length - animator.GetCurrentAnimatorStateInfo(3).normalizedTime);

        //grande
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("chico"))
        {
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(3).length - animator.GetCurrentAnimatorStateInfo(3).normalizedTime);
        }
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(3).length - animator.GetCurrentAnimatorStateInfo(3).normalizedTime);
    }
}

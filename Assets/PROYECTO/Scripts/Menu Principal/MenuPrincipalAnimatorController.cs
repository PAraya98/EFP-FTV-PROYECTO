using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using NaughtyAttributes;
public class MenuPrincipalAnimatorController : MonoBehaviour
{
    private Animator animator;
    private Animator animatorSeleccionEscenario;
    private Animator animatorCreditos;
    private PlayerInput playerInput;
    private bool presionoBoton = false;
    [BoxGroup("Botones Default")]
    public GameObject menu;
    [BoxGroup("Botones Default")]
    public GameObject seleccionEscenario;
    [BoxGroup("Botones Default")]
    public GameObject creditos;

    // Start is called before the first frame update
    void Start()
    {
        
        playerInput = GameObject.Find("EventSystem").GetComponent<PlayerInput>();
        animator = gameObject.GetComponent<Animator>();
        animatorSeleccionEscenario = GameObject.Find("Panel - Comenzar Partida").GetComponent<Animator>();
        animatorCreditos = GameObject.Find("Panel - Creditos").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (presionoBoton)
        {

        }
        else if (playerInput.actions["start"].IsPressed())
        { 
            animator.Play("InicioMenuPrincipalAnimation");
            gameObject.GetComponent<CanvasGroup>().interactable = true;
            presionoBoton = true;
        }
    }

    public void irSeleccionNivel()
    {
        animator.Play("IzquierdaOut");
        animatorSeleccionEscenario.Play("DerechaIn");
        EventSystem.current.SetSelectedGameObject(seleccionEscenario);
    }
    public void SeleccionNivelAMenuPrincipal()
    {        
        animator.Play("IzquierdaIn");
        animatorSeleccionEscenario.Play("DerechaOut");
        EventSystem.current.SetSelectedGameObject(menu);
    }
}

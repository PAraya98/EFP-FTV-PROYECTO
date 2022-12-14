using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarActivarEventSysyem : StateMachineBehaviour
{
    private GameObject eventSystem;
    private CanvasGroup canvasGroup;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   eventSystem = GameObject.Find("EventSystem");
        canvasGroup = animator.transform.parent.gameObject.GetComponent<CanvasGroup>();
        if (eventSystem) eventSystem.SetActive(false);

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        if(canvasGroup && (stateInfo.IsName("IzquierdaIn") || stateInfo.IsName("DerechaIn"))) canvasGroup.interactable = true;
        else if (canvasGroup && (stateInfo.IsName("IzquierdaOut") || stateInfo.IsName("DerechaOut"))) canvasGroup.interactable = false;
        if (eventSystem) eventSystem.SetActive(true);
        animator.transform.parent.gameObject.transform.position = animator.transform.position;
    }

}

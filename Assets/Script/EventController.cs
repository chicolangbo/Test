using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public Animator objAnimator;
    public Animator windowAnimator;

    void Start()
    {
        StartCoroutine(WaitObjAnimation());
    }

    void Update()
    {
        
    }

    IEnumerator WaitObjAnimation()
    {
        //originPosBtn.interactable = false;
        objAnimator.SetTrigger("start");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => objAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        windowAnimator.SetTrigger("Model");
        //originPosBtn.interactable = true;
        //isAnimWindow = false;
        //cameraViewController.isControlEnable = true;
    }

}

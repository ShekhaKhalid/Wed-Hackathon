using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class AnimateHandOutput : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;
    public Animator handAnimator;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float triggerValu = pinchAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValu);

        /* Debug.Log(triggerValu);*/

        float gripValu = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValu);

    }
}

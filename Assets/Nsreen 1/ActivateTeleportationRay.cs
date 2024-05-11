using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateTeleportationRay : MonoBehaviour
{
    public GameObject leftTelee;
    public GameObject RightTelee;


    public InputActionProperty leftActive;
    public InputActionProperty RightActive;

/*    public InputActionProperty leftCancel;
    public InputActionProperty RightCancel;
*/
    public XRRayInteractor leftRAY;
    public XRRayInteractor rightRAY;

   // leftCancel.action.ReadValue<float>() == 0 &&
    //&& RightCancel.action.ReadValue<float>() == 0
    // Update is called once per frame
    void Update()
    {
     
        bool isLeftRayHover = leftRAY.TryGetHitInfo(out Vector3 leftPos, out Vector3 leftNormal, out int leftNumber, out bool leftVaid);
        leftTelee.SetActive(!isLeftRayHover &&  leftActive.action.ReadValue<float>() > 0.1f);

     
        bool isRightRayHover = rightRAY.TryGetHitInfo(out Vector3 rightPos, out Vector3 rightNormal, out int rightNumber, out bool rightVaid);

        RightTelee.SetActive(!isRightRayHover && RightActive.action.ReadValue<float>() > 0.1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ActivateTeleportationRay : MonoBehaviour
{
    public GameObject leftTele;
    public GameObject RightTele;


    public InputActionProperty leftActive;
    public InputActionProperty RightActive;

    // Update is called once per frame
    void Update()
    {
        leftTele.SetActive(leftActive.action.ReadValue<float>() > 0.1f);
        RightTele.SetActive(RightActive.action.ReadValue<float>() > 0.1f);
    }
}

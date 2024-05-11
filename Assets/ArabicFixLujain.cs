using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArabicSupport;
using UnityEngine.UI;
using TMPro;


public class ArabicFixLujain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = ArabicFixer.Fix(GetComponent<TextMeshProUGUI>().text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

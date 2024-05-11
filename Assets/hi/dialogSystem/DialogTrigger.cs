using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private List<dialogString> dialogStrings = new List<dialogString>();
    [SerializeField] private Transform NPCTrasform;

    private bool hasSpoken = false;

    [SerializeField] private GameObject instructions;
    [SerializeField] private GameObject UI;
    [SerializeField] private float dialogDuration = 5f;
    private bool done = false;
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player") && !hasSpoken)
    //    {
    //        StartCoroutine(ShowDialogUI());


    //    }
    //}

    private void Start()
    {
        StartCoroutine(ShowDialogUI());
    }

    private IEnumerator ShowDialogUI()
    {
        //instructions.SetActive(true);
        UI.SetActive(false);
        yield return new WaitForSeconds(dialogDuration);
        UI.SetActive(true);
        //instructions.SetActive(false);
        DialogManager dialogManager = FindObjectOfType<DialogManager>();
        dialogManager.gameObject.GetComponent<DialogManager>().DialogueStart(dialogStrings, NPCTrasform);
        hasSpoken = true;

    }
    private IEnumerator DialogCoroutine(GameObject playerObject)
    {
        DialogManager dialogManager = playerObject.GetComponent<DialogManager>();

        dialogManager.DialogueStart(dialogStrings, NPCTrasform);
        hasSpoken = true;

        yield return StartCoroutine(ShowDialogUI());

        // Continue with the remaining code
    }

}


[System.Serializable]

public class dialogString
{
    public string text;
    public bool isEnd;
    public AudioClip clip; //what you will hear from the AI

    [Header("Branch")]
    public bool isQuestion;
    public string answerOption1;
    public AudioClip answerClip;


}



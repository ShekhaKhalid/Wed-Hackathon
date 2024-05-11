using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueParent;
    [SerializeField] private TMP_Text dialogueText;


    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float turnSpeed = 2f;

    private List<dialogString> dialogueList;

    //there is a player part to not make him move

    private int currentDialogueIndex = 0;

    [Header("Sound")]
   // [SerializeField] private Button option1Listen;
   // [SerializeField] private Button option1Speak;
    [SerializeField] private Button option1Stop;
    [SerializeField] private TMP_Text option1Text;
    //[SerializeField] private TMP_Text textSpoken;

    //[SerializeField] private AudioClip correct;
    //[SerializeField] private AudioClip wrong;
    //public AppVoiceExperience voiceRecognizer;
    public AudioSource audioSource;


    private string currentAnswerOption;
    [SerializeField] private Animator anim;
    //public ConvaiLipSync lips;
    private void Start()
    {
       // dialogueParent.SetActive(false);
        //voiceRecognizer.VoiceEvents.OnPartialResponse.AddListener(HandleSpeak);

    }
    public void DialogueStart(List<dialogString> textToprint, Transform NPC)
    {
        dialogueParent.SetActive(true);


        dialogueList = textToprint;
        currentDialogueIndex = 0;

        //DisableButtons();

        StartCoroutine(PrintDialogue());
    }

    private void DisableButtons()
    {


       // option1Listen.interactable = false;
       // option1Speak.interactable = false;
        option1Stop.interactable = false;


    }


    private bool spoken = false;
    private IEnumerator PrintDialogue()
    {
        while (currentDialogueIndex < dialogueList.Count)
        {
            dialogString line = dialogueList[currentDialogueIndex];
            print(currentDialogueIndex);

            if (line.isQuestion)
            {
                audioSource.PlayOneShot(line.clip);
                yield return StartCoroutine(TypeText(line.text));
                anim.SetBool("Talk", false);


                option1Text.text = line.answerOption1;
                currentAnswerOption = option1Text.text;
                audioSource.PlayOneShot(line.answerClip);
                RemoveListenSelectedListener();

                //option1Listen.onClick.AddListener(() => HandleListenSelected(line.answerClip));
                //option1Speak.onClick.AddListener(() => StartRecord());
                //option1Stop.onClick.AddListener(() => StopRecording(currentAnswerOption));
                option1Stop.onClick.AddListener(() => ContinuePressed());
                print(currentDialogueIndex + "o" + line.answerClip);
                print("!" + spoken);

                yield return new WaitUntil(() => spoken);
            }
            else
            {

                yield return StartCoroutine(TypeText(line.text));
                audioSource.PlayOneShot(line.clip);
            }

            option1Stop.onClick.AddListener(() => ContinuePressed());
            spoken = false; //on click continue make spoken true
            currentDialogueIndex++;
        }
        print("done11111");
        DialogueStop();
    }

    private void ContinuePressed() {
        print("im being pressed here !!");
        spoken = true;



    }

    //private void HandleListenSelected(AudioClip audio)
    //{
    //    DisableButtons();

    //    audioSource.PlayOneShot(audio);
    //}

    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            anim.SetBool("Talk", true);
            //lips.LipSyncCharacter();
            yield return new WaitForSeconds(typingSpeed);
        }
        if (!dialogueList[currentDialogueIndex].isQuestion)
        {
            //yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }
        if (dialogueList[currentDialogueIndex].isEnd)
            DialogueStop();
        //currentDialogueIndex++;
    }

    public void DialogueStop()
    {
        StopAllCoroutines();
        dialogueText.text = "";
        dialogueParent.SetActive(false);
    }


  

    


    

   
    private void RemoveListenSelectedListener()
    {
        //option1Listen.onClick.RemoveAllListeners();
    }
}

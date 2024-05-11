using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonsone : MonoBehaviour
{
    [SerializeField] private Button btnStart;
    [SerializeField] private Button btninstruct;


    [SerializeField] private GameObject UIstart;
    [SerializeField] private GameObject UIinstruct;




    private void Update()
    {
        btnStart.onClick.AddListener(() => changeTheUI());
        btninstruct.onClick.AddListener(() => changeScene());
        //option1Stop.onClick.AddListener(() => StopRecording(currentAnswerOption));
    }

    private void changeTheUI()
    {
        UIstart.SetActive
            (false);

        UIinstruct.SetActive(true); 
    }


    private void changeScene()
    {
        SceneManager.LoadSceneAsync("FatimaS");
    }
}

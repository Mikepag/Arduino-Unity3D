using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public Button NaturalEvaluationButton;      // I use this to be able to know when the NaturalEvaluationButton gets clicked.
    public Button IsomorphicEvaluationButton;   // I use this to be able to know when the IsomorphicEvaluationButton gets clicked.
    public Button NaturalFieldStudyButton;      // I use this to be able to know when the NaturalFieldStudyButton gets clicked.
    public Button IsomorphicFieldStudyButton;   // I use this to be able to know when the IsomorphicFieldStudyButton gets clicked.
    public Button QuitButton;                   // I use this to be able to know when the QuitButton gets clicked.

    // Start is called before the first frame update
    void Start()
    {
        NaturalEvaluationButton.onClick.AddListener(TaskOnNaturalEvaluationButtonClick);        // Whenever the NaturalEvaluationButton is clicked, call the TaskOnNaturalEvaluationButtonClick() function.
        IsomorphicEvaluationButton.onClick.AddListener(TaskOnIsomorphicEvaluationButtonClick);  // Whenever the IsomorphicEvaluationButton is clicked, call the TaskOnIsomorphicEvaluationButtonClick() function.
        NaturalFieldStudyButton.onClick.AddListener(TaskOnNaturalFieldStudyButtonClick);        // Whenever the NaturalFieldStudyButton is clicked, call the TaskOnNaturalFieldStudyButtonClick() function.
        IsomorphicFieldStudyButton.onClick.AddListener(TaskOnIsomorphicFieldStudyButtonClick);  // Whenever the IsomorphicFieldStudyButton is clicked, call the TaskOnIsomorphicFieldStudyButtonClick() function.
        QuitButton.onClick.AddListener(TaskOnQuitButtonClick);                                  // Whenever the Quit1Button is clicked, call the TaskOnQuitButtonClick() function.
    }

    void TaskOnNaturalEvaluationButtonClick()
    {   // The following code is executed only when the NaturalEvaluationButton gets clicked.
        SceneManager.LoadScene("Assets/Scenes/NaturalEvaluation.unity");    // Loads NaturalEvaluation scene.
    }

    void TaskOnIsomorphicEvaluationButtonClick()
    {   // The following code is executed only when the IsomorphicEvaluationButton gets clicked.
        SceneManager.LoadScene("Assets/Scenes/IsomorphicEvaluation.unity"); // Loads IsomorphicEvaluation scene.
    }

    void TaskOnNaturalFieldStudyButtonClick()
    {   // The following code is executed only when the NaturalFieldStudyButton gets clicked.
        SceneManager.LoadScene("Assets/Scenes/NaturalFieldStudy.unity");    // Loads NaturalFieldStudy scene.
    }

    void TaskOnIsomorphicFieldStudyButtonClick()
    {   // The following code is executed only when the IsomorphicFieldStudyButton gets clicked.
        SceneManager.LoadScene("Assets/Scenes/IsomorphicFieldStudy.unity"); // Loads IsomorphicFieldStudy scene.
    }

    void TaskOnQuitButtonClick()
    {   // The following code is executed only when the QuitButton gets clicked.
        //UnityEditor.EditorApplication.isPlaying = false;                  // [IMPORTANT: COMMENT IT OUT BEFORE BUILD!] Quit the application (works when playing on the Editor).
        Application.Quit();                                                 // Quit the application (works when playing on a Build).
    }
}

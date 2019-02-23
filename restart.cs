using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class restart : MonoBehaviour {
    public GameObject Compass;
    public GameObject resButObject; // I use this to hide the restart button when it is clicked.
    public Button restartButton;    // I use this to know when the restart button gets clicked.
    public int resBtnClicked;
    public int roundNum;

    void Start()
    {
        restartButton.onClick.AddListener(TaskOnClick); // Whenever the restart button is clicked, call the TaskOnClick() function.
        resButObject.SetActive(true);
        roundNum = 0;
    }

    void Update()
    {
        resBtnClicked = 0;
        //resBtnClicked = Compass.GetComponent<timer>().unfinCD;  // For as long as the countdown takes place in timer.cs, I want the variable to restart to be ==1.

        if (Compass.GetComponent<compassRotation>().goalReached == 1)  // If the goal had been reached and round 10 hasn't started yet...
        {
            resButObject.SetActive(true);   // Show the restart button to start the next round.

            if (roundNum == 1)
            {
                resButObject.GetComponentInChildren<Text>().text = "Quit";
            }
        }
        
    }


    void TaskOnClick(){

        if (roundNum == 1)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
        else
        {
            //Debug.Log("You have clicked the button!");
            resBtnClicked = 1;
            resButObject.SetActive(false);
            //restartButton.enabled = false;
            //restartButton.interactable = false;
            roundNum++;
        }
        
    }

}



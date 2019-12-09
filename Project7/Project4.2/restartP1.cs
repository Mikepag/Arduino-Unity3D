using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class restartP1 : MonoBehaviour
{
    public GameObject Disk;
    public GameObject resButObject; // I use this to be able to hide the restart button when it is clicked.
    public Button restartButton;    // I use this to be able to know when the restart button gets clicked.
    public Text roundText;          // Declaring a public Text called roundText

    public int resBtnClicked;       // ==1 when the restart button gets clicked.
    public int roundNum;            // Is equal to the current round's number.

    private int goalReached;  // Getting the value of goalReached from the diskRotationP1.cs script.

    void Start()
    {
        restartButton.onClick.AddListener(TaskOnClick); // Whenever the restart button is clicked, call the TaskOnClick() function.
        resButObject.SetActive(true);                   // Set the restart button active (and visible).
        roundNum = 0;                                   // Starting from round 0. This number will increase each time the restart button gets clicked.
        roundText.color = Color.black;                  // Setting the UI roundtext's colour to black.
    }

    void Update()
    {
        goalReached = Disk.GetComponent<diskRotationP1>().goalReached;

        //resBtnClicked = 0;                                          // It is equal to 0 unless it gets clicked. ! ! ! ! ! DELETE IF NEW METHOD WORKS ! ! ! ! !
        roundText.color = Color.black;                              // It is black unless the goal is reached.
        roundText.text = "Round: " + roundNum.ToString() + " /10";  // Printing the round's number to the UI roundText.

        if (roundNum == 0)                                                          // If the current round is 0 (If the player has not clicked the button to start the first round)...
        {
            resButObject.GetComponentInChildren<Text>().text = "Start!";            //...Set the buttons text to "Start!".
        }
        else                                                                        // Else, if the player has started at least the first round...
        {
            resButObject.GetComponentInChildren<Text>().text = "Start next round";  //...Set the buttons text to "Start next round".
        }

        if (goalReached == 1 || roundNum == 0)       // If the goal has been reached, OR the first round has not yet started...
        {
            resButObject.SetActive(true);                                   // Set the restart button active (and visible) for the player to start the next round.

            if (roundNum == 0)
            {
                roundText.color = Color.black;
            }
            else
            {
                roundText.color = Color.green;                                  // If the goal is reached for this round, set roundText colour to green.
            }

            if (roundNum == 10)                                             // If the current round is 10 and the goal has been reached (parent if)...
            {
                resButObject.GetComponentInChildren<Text>().text = "Return to Menu";  //...Set the buttons text to "Quit".
            }
        }
        else
        {
            resButObject.SetActive(false);  // Disable the restart button.
        }
    }


    void TaskOnClick()
    { // The following code is executed only when the restart button gets clicked.

        if (roundNum == 10)                                     // If the button gets clicked in the final round...
        {
            SceneManager.LoadScene("Assets/Scenes/Menu.unity"); // Loads Menu Scene.
            //UnityEditor.EditorApplication.isPlaying = false;  // [IMPORTANT: COMMENT IT OUT BEFORE BUILD!] Quit the application (works when playing on the Editor).
            //Application.Quit();                               // Quit the application (works when playing on a Build).
        }
        else                                // Else, if not in final round yet...
        {
            //resBtnClicked = 1;              // Set resBtnClicked equal to 1. ! ! ! ! ! DELETE IF NEW METHOD WORKS ! ! ! ! !
            roundNum++;                     // Increment the current round number by one because the next round starts.
            resBtnClicked = roundNum;              // Set resBtnClicked equal to the current round number.
            resButObject.SetActive(false);  // Set the restart button inactive (and non-visible).
        }
    }
}

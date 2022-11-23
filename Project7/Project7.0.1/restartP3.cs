using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class restartP3 : MonoBehaviour
{
    public GameObject Disk;
    public GameObject resButObject;     // I use this to be able to hide the restart button when it is clicked.
    public Button restartButton;        // I use this to be able to know when the restart button gets clicked.
    public Text roundText;              // Declaring a public Text called roundText

    public int resBtnClicked;           // ==1 when the restart button gets clicked.
    public int roundNum;                // Is equal to the current round's number.
    private const int FinalRound = 3;   // Constant variable to save the total number of rounds.

    private int goalReached;            // Getting the value of goalReached from the diskRotationP2.cs script.

    void Start()
    {
        restartButton.onClick.AddListener(TaskOnClick); // Whenever the restart button is clicked, call the TaskOnClick() function.
        resButObject.SetActive(true);                   // Set the restart button active (and visible).
        roundNum = 0;                                   // Starting from round 0. This number will increase each time the restart button gets clicked.
        roundText.color = Color.black;                  // Setting the UI roundtext's colour to black.
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {                                                                                   // When the Backspace button gets clicked:
            SceneManager.LoadScene("Assets/Scenes/Menu.unity");                             // Load Menu Scene.
        }

        goalReached = Disk.GetComponent<diskRotationP3>().goalReached;                      // Getting the value of goalReached from the diskRotationP3.cs script.
        roundText.text = "Round: " + roundNum.ToString() + " /" + FinalRound.ToString();    // Setting the current round's number to the UI roundText.

        if (goalReached == 0)
        {                                                                                   // If the goal has not been reached in this round yet:
            roundText.color = Color.black;                                                  // Set the roundText color to black.
            if (roundNum == 0)
            {                                                                               // If not even the first round has started yet:
                resButObject.SetActive(true);                                               // Activate the restart button to be able to start the first round.
                resButObject.GetComponentInChildren<Text>().text = "Start!";                // Set the button's text to "Start".
            }
            else
            {                                                                               // Else if roundNum !=0 && the goal has not been reached yet:
                resButObject.SetActive(false);                                              // Disable the restart button.
            }
        }
        else
        {                                                                                   // Else if the goal has been reached:
            resButObject.SetActive(true);                                                   // Activate the restart button.
            roundText.color = Color.green;                                                  // Set the roundText's color to green to show that the round has finished successfully.
            if (roundNum == FinalRound)
            {                                                                               // When the goal is reached in the Final round:
                resButObject.GetComponentInChildren<Text>().text = "Return to Menu";        // Set the button's text to "Return to Menu".
            }
            else
            {                                                                               // Else,  if the round, in which the goal has been reached, is not the final one:
                resButObject.GetComponentInChildren<Text>().text = "Start Next Round";      // Set the button's text to "Start Next Round".
            }
        }
    }


    void TaskOnClick()
    { // The following code is only executed when the restart button gets clicked.

        if (roundNum == FinalRound)
        {                                                       // If the button gets clicked in the final round...
            SceneManager.LoadScene("Assets/Scenes/Menu.unity"); // Load Menu Scene.
        }
        else
        {                                                       // Else, if not in final round yet...
            roundNum++;                                         // Increment the current round number by one because the next round starts.
            resBtnClicked = roundNum;                           // Set resBtnClicked equal to the current round number.
            resButObject.SetActive(false);                      // Set the restart button inactive (and non-visible).
        }
    }
}

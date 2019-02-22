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

    void Start()
    {
        restartButton.onClick.AddListener(TaskOnClick); // Whenever the restart button is clicked, call the TaskOnClick() function.
        resButObject.SetActive(true);
    }

    void Update()
    {
        resBtnClicked = 0;
        //resBtnClicked = Compass.GetComponent<timer>().unfinCD;  // For as long as the countdown takes place in timer.cs, I want the variable to restart to be ==1.

        if (Compass.GetComponent<compassRotation>().goalReached == 1)
        {
            resButObject.SetActive(true);
        }
        
    }


    void TaskOnClick(){
        //Debug.Log("You have clicked the button!");
        resBtnClicked = 1;
        resButObject.SetActive(false);
        //restartButton.enabled = false;
        //restartButton.interactable = false;
    }

}



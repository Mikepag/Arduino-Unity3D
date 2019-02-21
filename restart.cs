using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class restart : MonoBehaviour {
    public GameObject Compass;

    public Button restartButton;
    public int toRestart;

    void Start()
    {
        restartButton.onClick.AddListener(TaskOnClick);
    }

    void Update()
    {
        toRestart = Compass.GetComponent<timer>().unfinCD;
    }

    void TaskOnClick(){
        //Debug.Log("You have clicked the button!");
        toRestart = 1;
    }
}

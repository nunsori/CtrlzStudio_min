
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Click : MonoBehaviour
{
    public GameObject NewGameImage;
    public GameObject LoadImage;
    public GameObject OptionImage;
   

    void Start()
    {
        NewGameImage.SetActive(false);
        LoadImage.SetActive(false);
        OptionImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Start
    public void NewGameButton() 
    {
        NewGameImage.SetActive(true);
    }

    public void Load()
    {
        LoadImage.SetActive(true);
    }
   

    //option
    public void Option()
    {
        OptionImage.SetActive(true);
    }



    //Exit
    public void Exit()
    {
        Application.Quit();
    }

}
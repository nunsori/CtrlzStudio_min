
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

    //NewGame
    public void NewGameButton() 
    {
        NewGameImage.SetActive(true);
    }
    //Load
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



    //부가기능
    
    //Load 뒤로가기
    public void LoadBack()
    {
        LoadImage.SetActive(false);
    }

    //Option 뒤로가기
    public void OptionBack()
    {
        OptionImage.SetActive(false);
    }

    //NewGamePopup 다음씬으로 전환
    public void NewGamePopup()
    {
        SceneManager.LoadScene(2);
    }

}

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

    public Slider bgm_slider;
    public Slider narr_slider;
   

    void Start()
    {
        NewGameImage.SetActive(false);
        LoadImage.SetActive(false);
        OptionImage.SetActive(false);

        bgm_slider.value = save_load_Data.Instance.play_data.BGM_Volume;
        narr_slider.value = save_load_Data.Instance.play_data.Narr_Volume;
        //save_load_Data.Instance.play_data.BGM_Volume = bgm_slider.value;
        //save_load_Data.Instance.play_data.Narr_Volume = narr_slider.value;
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

        save_load_Data.Instance.save();
    }

    //NewGamePopup 다음씬으로 전환
    public void NewGamePopup()
    {
        //SceneManager.LoadScene(2);

        //로딩씬 불러오는 함수 인자로 씬이름
        loading_scene_controller.Instance.load_new_scene("1NabiScene");
    }

    public void change_slider(string slider_name)
    {
        switch (slider_name)
        {
            case ("BGM"):
                //BGM volume 조절
                save_load_Data.Instance.play_data.BGM_Volume = bgm_slider.value;

                break;


            case ("NARR"):
                //narration volume 조절
                save_load_Data.Instance.play_data.Narr_Volume = narr_slider.value;


                break;


            default:

                break;
        }
    }
}

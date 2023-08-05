
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Click : MonoBehaviour
{
    public GameObject start_game;
    public GameObject NewGameImage;
    public GameObject LoadImage;
    public GameObject OptionImage;

    public Slider bgm_slider;
    public Slider narr_slider;

    public Text[] load_game_text;
   

    void Start()
    {
        start_game.SetActive(true);
        NewGameImage.SetActive(false);
        LoadImage.SetActive(false);
        OptionImage.SetActive(false);

        
        save_load_Data.Instance.load();


        bgm_slider.value = save_load_Data.Instance.play_data.BGM_Volume;
        narr_slider.value = save_load_Data.Instance.play_data.Narr_Volume;

        for (int i =0; i < load_game_text.Length; i++)
        {
            if (save_load_Data.Instance.play_data.progress_slot[i] == -1)
            {
                load_game_text[i].text = "null";
            }
            else
            {
                load_game_text[i].text = save_load_Data.Instance.play_data.progress_slot[i].ToString() + " 번째 밤";
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //NewGame
    public void NewGameButton() 
    {
        //production_controller.call_production(production_controller.Instance.fade_production(0, start_game, false, 0.2f));
        production_controller.call_production(production_controller.Instance.fade_production(0, NewGameImage, true, 0.2f));
        //NewGameImage.SetActive(true);
    }
    //Load
    public void Load()
    {
        production_controller.call_production(production_controller.Instance.fade_production(0, start_game, false, 0.2f));
        production_controller.call_production(production_controller.Instance.fade_production(0.2f, LoadImage, true, 0.2f));
        //LoadImage.SetActive(true);
    }
   

    //option
    public void Option()
    {
        production_controller.call_production(production_controller.Instance.fade_production(0, start_game, false, 0.2f));
        production_controller.call_production(production_controller.Instance.fade_production(0.2f, OptionImage, true, 0.2f));
        //OptionImage.SetActive(true);
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
        production_controller.call_production(production_controller.Instance.fade_production(0, LoadImage, false, 0.2f));
        production_controller.call_production(production_controller.Instance.fade_production(0.2f, start_game, true, 0.2f));
        //LoadImage.SetActive(false);
    }

    //Option 뒤로가기
    public void OptionBack()
    {
        production_controller.call_production(production_controller.Instance.fade_production(0, OptionImage, false, 0.2f));
        production_controller.call_production(production_controller.Instance.fade_production(0.2f, start_game, true, 0.2f));
        //OptionImage.SetActive(false);

        save_load_Data.Instance.save();
    }

    //NewGamePopup 다음씬으로 전환
    public void NewGamePopup()
    {
        //SceneManager.LoadScene(2);
        save_load_Data.Instance.play_data.cur_progress = 0;

        save_load_Data.Instance.save();

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

                sound_sr.Instance.change_sound();

                break;


            case ("NARR"):
                //narration volume 조절
                save_load_Data.Instance.play_data.Narr_Volume = narr_slider.value;

                sound_sr.Instance.change_sound();

                break;


            default:

                break;
        }
    }

    public void loadgame(int temp)
    {
        if (save_load_Data.Instance.play_data.progress_slot[temp] == -1)
        {
            return;
        }
        save_load_Data.Instance.play_data.cur_progress = save_load_Data.Instance.play_data.progress_slot[temp];

        save_load_Data.Instance.save();

        loading_scene_controller.Instance.load_new_scene("1NabiScene");
    }
}

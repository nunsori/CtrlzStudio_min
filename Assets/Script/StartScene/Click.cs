
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
    public GameObject CreditImage;

    public Slider bgm_slider;
    public Slider narr_slider;

    public Text[] load_game_text;

    public string empty_data_name = "null";

    private string[] char_list = new string[6] { "����", "��ȣ", "�� ��", "����", "�� ��", "����" };
   

    void Start()
    {
        SetResolution();


        start_game.SetActive(true);
        NewGameImage.SetActive(false);
        LoadImage.SetActive(false);
        OptionImage.SetActive(false);
        CreditImage.SetActive(false);
        
        save_load_Data.Instance.load();


        bgm_slider.value = save_load_Data.Instance.play_data.BGM_Volume;
        narr_slider.value = save_load_Data.Instance.play_data.Narr_Volume;

        for (int i =0; i < load_game_text.Length; i++)
        {
            if (save_load_Data.Instance.play_data.progress_slot[i] == -1)
            {
                load_game_text[i].text = empty_data_name;
            }
            else
            {
                load_game_text[i].text = ((save_load_Data.Instance.play_data.progress_slot[i])/2 + 1).ToString() + "����-" + char_list[save_load_Data.Instance.play_data.progress_slot[i]/2] + "�� �̾߱�" + ((save_load_Data.Instance.play_data.progress_slot[i]%2 == 1) ? "(���� ���� ��)" : "");
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetResolution();
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

    public void Credit()
    {
        production_controller.call_production(production_controller.Instance.fade_production(0, start_game, false, 0.2f));
        production_controller.call_production(production_controller.Instance.fade_production(0.2f, CreditImage, true, 0.2f));
    }



    //Exit
    public void Exit()
    {
        Application.Quit();
    }



    //�ΰ����
    
    //Load �ڷΰ���
    public void LoadBack()
    {
        production_controller.call_production(production_controller.Instance.fade_production(0, LoadImage, false, 0.2f));
        production_controller.call_production(production_controller.Instance.fade_production(0.2f, start_game, true, 0.2f));
        //LoadImage.SetActive(false);
    }

    public void CreditBack()
    {
        production_controller.call_production(production_controller.Instance.fade_production(0, CreditImage, false, 0.2f));
        production_controller.call_production(production_controller.Instance.fade_production(0.2f, start_game, true, 0.2f));
    }

    //Option �ڷΰ���
    public void OptionBack()
    {
        production_controller.call_production(production_controller.Instance.fade_production(0, OptionImage, false, 0.2f));
        production_controller.call_production(production_controller.Instance.fade_production(0.2f, start_game, true, 0.2f));
        //OptionImage.SetActive(false);

        save_load_Data.Instance.save();
    }

    //NewGamePopup ���������� ��ȯ
    public void NewGamePopup()
    {
        //SceneManager.LoadScene(2);
        save_load_Data.Instance.play_data.cur_progress = 0;

        save_load_Data.Instance.save();

        //�ε��� �ҷ����� �Լ� ���ڷ� ���̸�
        loading_scene_controller.Instance.load_new_scene("1NabiScene");
    }

    public void change_slider(string slider_name)
    {
        switch (slider_name)
        {
            case ("BGM"):
                //BGM volume ����
                save_load_Data.Instance.play_data.BGM_Volume = bgm_slider.value;

                sound_sr.Instance.change_sound();

                break;


            case ("NARR"):
                //narration volume ����
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

    //private void Start()
    //{
        //SetResolution(); // �ʱ⿡ ���� �ػ� ����
    //}

    /* �ػ� �����ϴ� �Լ� */
    public void SetResolution()
    {
        int setWidth = 1920; // ����� ���� �ʺ�
        int setHeight = 1080; // ����� ���� ����

        int deviceWidth = Screen.width; // ��� �ʺ� ����
        int deviceHeight = Screen.height; // ��� ���� ����

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution �Լ� ����� ����ϱ�

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
        }
        else // ������ �ػ� �� �� ū ���
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
        }
    }
}

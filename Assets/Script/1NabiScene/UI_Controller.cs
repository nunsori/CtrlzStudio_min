using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
    /* 0 - start_btn_ui
     * 1 - 2-1 Dialog
     * 2 - 2-2 UI
    */
    [SerializeField]
    public GameObject[] ui_objs;

    //0 - make 1 - finish
    public GameObject[] drink_ui_set;

    [SerializeField]
    private GameObject dim_dialog_obj;


    public DialogManager dialogManager;

    public SmoothCamera smoothCamera;

    public Image dim;

    delegate void temp_fun();

    public Animator window_animator = null;

    public GameObject[] make_drink_objs;

    public GameObject finish_title;

    public Animator[] drink_animator;

    private bool is_progress = false;

    private bool is_drink = false;

    [Header("연출이미지 리스트")]
    public Sprite[] image_set;

    public GameObject image_obj;

    [Header("스티커 리스트")]
    public GameObject[] sticker_set;

    [Header("비디오 플레이어")]
    public GameObject video_player_obj;
    public VideoPlayer videoPlayer = null;
    private bool is_video_on = false;


    [Header("UI 모음")]
    public GameObject check_home_obj;
    public GameObject option_popup_obj;

    //0 - bgm , 1 - effect
    public Slider[] option_slider_set;

    public Image day_img;
    public Sprite[] day_img_list;
    public Image menu_img;
    public Sprite[] menu_img_list;
    public Image complete_text_img;
    public Sprite[] complete_text_img_list;

    [Header("특수 랜더경우")]
    public MeshRenderer nabi_cup;
    public Material[] nabi_cup_mat;
    public MeshRenderer soombi_cup;
    public Material[] soombi_cup_mat;


    public delegate void temp_fuc_ui();

    // Start is called before the first frame update
    void Start()
    {

        
        //초기 ui active 설정
        ui_objs[0].SetActive(true);
        ui_objs[1].SetActive(false);
        ui_objs[2].SetActive(false);
        video_player_obj.SetActive(false);

        check_home_obj.SetActive(false);
        option_popup_obj.SetActive(false);

        dim_dialog_obj.SetActive(false);

        dim.gameObject.SetActive(false);

        change_animation_state(window_animator, "cafe_init");

        for(int i =0; i<make_drink_objs.Length; i++)
        {
            //make_drink_objs[i].SetActive(false);
        }

        finish_title.SetActive(false);
        finish_title.GetComponent<CanvasGroup>().alpha = 0f;

        image_obj.SetActive(false);

        for(int i =0; i<sticker_set.Length; i++)
        {
            sticker_set[i].SetActive(false);
        }

        is_progress = false;
        is_drink = false;
        is_video_on = false;

        videoPlayer.loopPointReached += video_over;

        

        //save_load_Data.Instance.load();
        //Debug.Log(save_load_Data.play_data.cur_progress);
        //Debug.Log(save_load_Data.Instance);
        Debug.Log(save_load_Data.Instance.play_data);
        //DialogManager.currentDialogIndex = save_load_Data.Instance.play_data.cur_progress;

        


        
    }

    // Update is called once per frame
    void Update()
    {
        if (is_drink)
        {
            if (is_progress)
            {
                drink_animator[Mathf.FloorToInt(DialogManager.current_scene_page / 2)].speed = 1f;
                change_animation_state(drink_animator[Mathf.FloorToInt(DialogManager.current_scene_page / 2)], "animated_drink");
                if(Mathf.FloorToInt(DialogManager.current_scene_page / 2) == 0)
                {
                    nabi_cup.material = nabi_cup_mat[1];
                }else if(Mathf.FloorToInt(DialogManager.current_scene_page / 2) == 3)
                {
                    soombi_cup.material = soombi_cup_mat[1];
                }


            }
            else
            {
                drink_animator[Mathf.FloorToInt(DialogManager.current_scene_page / 2)].speed = 0f;

                if (Mathf.FloorToInt(DialogManager.current_scene_page / 2) == 0)
                {
                    nabi_cup.material = nabi_cup_mat[0];
                }
                else if (Mathf.FloorToInt(DialogManager.current_scene_page / 2) == 3)
                {
                    soombi_cup.material = soombi_cup_mat[0];
                }
            }

            if(drink_animator[Mathf.FloorToInt(DialogManager.current_scene_page / 2)].GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {

                if (Mathf.FloorToInt(DialogManager.current_scene_page / 2) == 0)
                {
                    nabi_cup.material = nabi_cup_mat[0];
                }
                else if (Mathf.FloorToInt(DialogManager.current_scene_page / 2) == 3)
                {
                    soombi_cup.material = soombi_cup_mat[0];
                }

                is_progress = false;
                is_drink = false;
                Debug.Log("진행완료");
                //함수실행
                finish_drink();
            }
        }

        if (is_video_on)
        {
            //if(!videoPlayer.isPlaying)
            //{
                //is_video_on = false;
                //SceneManager.LoadScene("StartScene");
            //}
        }
        
    }

    public void start_btn_clicked()
    {
        Debug.Log("start_clicked------------------------");
        //ui 활성화 변경
        ui_objs[0].SetActive(false);
        ui_objs[1].SetActive(true);
        ui_objs[2].SetActive(false);
        dim_dialog_obj.SetActive(false);

        //다이얼로그 활성화
        //dialogManager.currentDialogIndex = 0;



        envirnment_active();



        sound_sr.Instance.Play_BGM("1", save_load_Data.Instance.play_data.BGM_Volume, true);

        dialogManager.DisplayDialog();
    }

    //음료 만들기 active true
    public void make_ui_active()
    {
        //ui_objs[1].SetActive(false);
        //ui_objs[2].SetActive(true);
        //dim_dialog_obj.SetActive(false);
        //ui_objs[1].SetActive(false);
        //dialogManager.Makingbutton.SetActive(false);
        //make_drink_objs[DialogManager.current_scene_page].SetActive(true);
        sound_sr.Instance.Play_Effect("2", save_load_Data.Instance.play_data.Narr_Volume, false);
        sound_sr.Instance.Play_BGM("2", save_load_Data.Instance.play_data.BGM_Volume, false);

        //call_function_delay_ui(0.8f, new UI_Controller.temp_fuc_ui(envirnment_active));
        //call_function_delay_ui(1f, envirnment_active);
        envirnment_active();

        is_drink = true;
        drink_ui_set[0].SetActive(true);
        drink_ui_set[1].SetActive(false);

        finish_title.SetActive(false);
        finish_title.GetComponent<CanvasGroup>().alpha = 0f;


        drink_animator[Mathf.FloorToInt(DialogManager.current_scene_page / 2)].speed = 0f;
        drink_animator[Mathf.FloorToInt(DialogManager.current_scene_page/2)].Play("animated_drink");
        
        drink_animator[Mathf.FloorToInt(DialogManager.current_scene_page / 2)].Rebind();

        Debug.Log("current page index : " + DialogManager.current_scene_page);

        //fade in
        production_controller.call_production(production_controller.Instance.fade_production(0, dim.gameObject, true, 0.7f));

        //fade out 함수 호출
        production_controller.call_production(production_controller.Instance.fade_production(1.1f, dim.gameObject, false, 0.7f));

        

        production_controller.call_production(smooth_camera_call(1f, new SmoothCamera.temp_fuc(smoothCamera.MoveCupScene)));



        //ui 조절
        production_controller.call_production(active_delay(1f, dim_dialog_obj, false));
        production_controller.call_production(active_delay(1f, ui_objs[1], false));
        production_controller.call_production(active_delay(1f, ui_objs[2], true));
        production_controller.call_production(active_delay(1f, dialogManager.Makingbutton, false));








    }

    public void dim_dialog(bool is_true)
    {
        if (is_true)
        {
            dim_dialog_obj.SetActive(true);
            ui_objs[1].SetActive(false);

        }
        else
        {
            dim_dialog_obj.SetActive(false);
            ui_objs[1].SetActive(true);
        }
    }


    //음료 다만들기 버튼 클릭되었을 때, 다음 ui로
    public void make_finish_clicked()
    {
        //ui 활성화 변경
        //ui_objs[0].SetActive(false);
        //ui_objs[1].SetActive(true);
        //ui_objs[2].SetActive(false);

        //dialogManager.next_page_dialog();

        //smoothCamera.MoveMainScene();
        //call_function_delay_ui(0.8f, new UI_Controller.temp_fuc_ui(envirnment_active_add));
        production_controller.call_production(call_function_delay_ui(1.2f, new UI_Controller.temp_fuc_ui(envirnment_active_add)));
        //envirnment_active_add();

        //fade in
        production_controller.call_production(production_controller.Instance.fade_production(0,dim.gameObject, true, 0.7f));

        production_controller.call_production(call_function_cafe_init(1f));

        //next dialog 함수 호출
        production_controller.call_production(call_function_delay(2f, new DialogManager.temp_fun(dialogManager.next_page_dialog)));

        production_controller.call_production(call_function_delay(1f, new DialogManager.temp_fun(dialogManager.reset_dialog)));
        
        production_controller.call_production(smooth_camera_call(1f, new SmoothCamera.temp_fuc(smoothCamera.MoveMainScene)));

        //fade out 함수 호출
        production_controller.call_production(production_controller.Instance.fade_production(1.1f, dim.gameObject, false, 0.7f));

        

        //ui 조절
        production_controller.call_production(active_delay(1f, ui_objs[0], false));
        production_controller.call_production(active_delay(1f, ui_objs[1], true));
        production_controller.call_production(active_delay(1f, ui_objs[2], false));
    }

    public void next_scene()
    {
        //DialogManager.current_scene_page++;
        //DialogManager.currentDialogIndex = 0;

        //call_function_delay_ui(0.8f, new UI_Controller.temp_fuc_ui(envirnment_active_add));
        production_controller.call_production(call_function_delay_ui(1.2f, new UI_Controller.temp_fuc_ui(envirnment_active_add)));
        //call_function_delay_ui(1f, envirnment_active_add);
        //envirnment_active_add();



        //fade in
        production_controller.call_production(production_controller.Instance.fade_production(0, dim.gameObject, true, 0.7f));

        //
        production_controller.call_production(call_function_cafe_init(1f));

        //next dialog 함수 호출
        if (dialogManager.dialogs[DialogManager.current_scene_page + 1][0].production == "1")
        {
            dim_dialog_obj.SetActive(true);
            dialogManager.characterNameText_list[0].text = "";
            dialogManager.dialogText_list[0].text = "";
            dialogManager.characterNameText_list[1].text = "";
            dialogManager.dialogText_list[1].text = "";

            production_controller.call_production(active_delay(1f, ui_objs[0], false));
            production_controller.call_production(active_delay(1f, ui_objs[1], false));
            production_controller.call_production(active_delay(1f, ui_objs[2], false));

            production_controller.call_production(active_delay(1f, dim_dialog_obj, true));
        }
        else
        {
            production_controller.call_production(active_delay(1f, ui_objs[0], false));
            production_controller.call_production(active_delay(1f, ui_objs[1], true));
            production_controller.call_production(active_delay(1f, ui_objs[2], false));
        }
        production_controller.call_production(call_function_delay(2f, new DialogManager.temp_fun(dialogManager.next_page_dialog)));

        production_controller.call_production(call_function_delay(1f, new DialogManager.temp_fun(dialogManager.reset_dialog)));

        production_controller.call_production(smooth_camera_call(1f, new SmoothCamera.temp_fuc(smoothCamera.MoveMainScene)));

        //fade out 함수 호출
        production_controller.call_production(production_controller.Instance.fade_production(1.1f, dim.gameObject, false, 0.7f));


        is_drink = false;
        //ui 조절
        
    }


    public void finish_drink()
    {
        drink_ui_set[0].SetActive(false);
        //drink_ui_set[1].SetActive(true);

        //이후 연출추가
        production_controller.call_production(production_controller.Instance.fade_production(0, finish_title, true, 1.5f));
        change_animation_state(drink_animator[Mathf.FloorToInt(DialogManager.current_scene_page / 2)], "fin");

        production_controller.call_production(active_delay(2f, drink_ui_set[1], true));


    }

    IEnumerator smooth_camera_call(float delay_, SmoothCamera.temp_fuc function)
    {
        yield return new WaitForSeconds(delay_);

        function();
    }


    IEnumerator call_function_delay(float delay_, DialogManager.temp_fun function)
    {
        yield return new WaitForSeconds(delay_);

        function();
    }

    IEnumerator call_function_delay_ui(float delay_, temp_fuc_ui function)
    {
        yield return new WaitForSeconds(delay_);

        function();
    }

    IEnumerator call_function_cafe_init(float delay_)
    {
        yield return new WaitForSeconds(delay_);

        change_animation_state(window_animator, "cafe_init");
    }

    IEnumerator active_delay(float delay_, GameObject obj, bool is_active)
    {
        yield return new WaitForSeconds(delay_);

        obj.SetActive(is_active);
    }

    //public void 


    public void change_animation_state(Animator animator, string state_name)
    {
        // stop from interrupting by same animation
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(state_name)) return;

        //animation play
        animator.Play(state_name);

        

    }

    public void pointer_down()
    {
        is_progress = true;
    }

    public void pointer_up()
    {
        is_progress=false;
    }

    public void ending()
    {
        video_player_obj.SetActive(true);

        dim.gameObject.SetActive(true);

        videoPlayer.Play();

        //is_video_on = true;
    }

    public void video_over(UnityEngine.Video.VideoPlayer vp)
    {
        if(!videoPlayer.isPlaying)
        {
            is_video_on = false;
            SceneManager.LoadScene("StartScene");
        }
    }

    public void call_home()
    {
        for(int i =0; i<save_load_Data.Instance.play_data.progress_slot.Length; i++)
        {
            if(save_load_Data.Instance.play_data.progress_slot[i] == -1)
            {
                save_load_Data.Instance.play_data.progress_slot[i] = DialogManager.current_scene_page;
                break;
            }

            if(i == save_load_Data.Instance.play_data.progress_slot.Length - 1 && save_load_Data.Instance.play_data.progress_slot[i] != -1)
            {
                save_load_Data.Instance.play_data.progress_slot[0] = DialogManager.current_scene_page;
            }
        }

        save_load_Data.Instance.play_data.cur_progress = DialogManager.current_scene_page;

        save_load_Data.Instance.save();

        SceneManager.LoadScene("StartScene");
    }

    public void open_ui(int temp)
    {
        if(temp == 0)
        {
            production_controller.call_production(production_controller.Instance.fade_production(0, check_home_obj, true, 0.2f));
        }
        else if(temp == 1)
        {
            production_controller.call_production(production_controller.Instance.fade_production(0, option_popup_obj, true, 0.2f));
        }
    }

    public void close_ui(int temp)
    {
        if (temp == 0)
        {
            production_controller.call_production(production_controller.Instance.fade_production(0, check_home_obj, false, 0.2f));
        }
        else if (temp == 1)
        {
            production_controller.call_production(production_controller.Instance.fade_production(0, option_popup_obj, false, 0.2f));
        }
    }


    public void change_slider(string slider_name)
    {
        switch (slider_name)
        {
            case ("BGM"):
                //BGM volume 조절
                save_load_Data.Instance.play_data.BGM_Volume = option_slider_set[0].value;

                sound_sr.Instance.change_sound();

                break;


            case ("NARR"):
                //narration volume 조절
                save_load_Data.Instance.play_data.Narr_Volume = option_slider_set[1].value;

                sound_sr.Instance.change_sound();

                break;


            default:

                break;
        }
    }

    public void envirnment_active()
    {
        //day_img.sprite = day_img_list[Mathf.FloorToInt(DialogManager.current_scene_page / 2)];
        //complete_text_img.sprite = complete_text_img_list[Mathf.FloorToInt(DialogManager.current_scene_page / 2)];
        //menu_img.sprite = menu_img_list[Mathf.FloorToInt(DialogManager.current_scene_page / 2)];

        

        for (int i = 0; i < make_drink_objs.Length; i++)
        {
            make_drink_objs[i].SetActive(Mathf.FloorToInt(DialogManager.current_scene_page / 2) == i);


            if (make_drink_objs[i].activeSelf == true && make_drink_objs[i].transform.childCount >=2)
            {
                if (Mathf.FloorToInt(DialogManager.current_scene_page % 2) == 0)
                {
                    make_drink_objs[i].transform.GetChild(0).gameObject.SetActive(true);
                    make_drink_objs[i].transform.GetChild(1).gameObject.SetActive(false);
                }
                else if (Mathf.FloorToInt(DialogManager.current_scene_page % 2) == 1)
                {
                    make_drink_objs[i].transform.GetChild(0).gameObject.SetActive(false);
                    make_drink_objs[i].transform.GetChild(1).gameObject.SetActive(true);
                }

            }
        }
    }

    public void envirnment_active_add()
    {
        /*
        if(Mathf.FloorToInt((DialogManager.current_scene_page + 1) / 2) < 6)
        {
            if(Mathf.FloorToInt((DialogManager.current_scene_page + 1) / 2) < 5)
            {
                complete_text_img.sprite = complete_text_img_list[Mathf.FloorToInt((DialogManager.current_scene_page + 1) / 2)];
                menu_img.sprite = menu_img_list[Mathf.FloorToInt((DialogManager.current_scene_page + 1) / 2)];
            }
            day_img.sprite = day_img_list[Mathf.FloorToInt((DialogManager.current_scene_page + 1) / 2)];
            
        }*/
        

        for (int i = 0; i < make_drink_objs.Length; i++)
        {
            make_drink_objs[i].SetActive(Mathf.FloorToInt((DialogManager.current_scene_page + 1) / 2) == i);

            if (make_drink_objs[i].activeSelf == true && make_drink_objs[i].transform.childCount >= 2)
            {
                if (Mathf.FloorToInt((DialogManager.current_scene_page + 1) % 2) == 0)
                {
                    make_drink_objs[i].transform.GetChild(0).gameObject.SetActive(true);
                    make_drink_objs[i].transform.GetChild(1).gameObject.SetActive(false);
                }
                else if (Mathf.FloorToInt((DialogManager.current_scene_page + 1) % 2) == 1)
                {
                    make_drink_objs[i].transform.GetChild(0).gameObject.SetActive(false);
                    make_drink_objs[i].transform.GetChild(1).gameObject.SetActive(true);
                }

            }
        }
    }

    public void ui_update()
    {
        if (Mathf.FloorToInt((DialogManager.current_scene_page + 1) / 2) < 6)
        {
            if (Mathf.FloorToInt((DialogManager.current_scene_page + 1) / 2) < 5)
            {
                complete_text_img.sprite = complete_text_img_list[Mathf.FloorToInt((DialogManager.current_scene_page + 1) / 2)];
                menu_img.sprite = menu_img_list[Mathf.FloorToInt((DialogManager.current_scene_page + 1) / 2)];
            }
            day_img.sprite = day_img_list[Mathf.FloorToInt((DialogManager.current_scene_page + 1) / 2)];

        }
    }



    public void ui_update_minus()
    {
        if (Mathf.FloorToInt((DialogManager.current_scene_page) / 2) < 6)
        {
            if (Mathf.FloorToInt((DialogManager.current_scene_page) / 2) < 5)
            {
                complete_text_img.sprite = complete_text_img_list[Mathf.FloorToInt((DialogManager.current_scene_page) / 2)];
                menu_img.sprite = menu_img_list[Mathf.FloorToInt((DialogManager.current_scene_page) / 2)];
            }
            day_img.sprite = day_img_list[Mathf.FloorToInt((DialogManager.current_scene_page) / 2)];

        }
    }




}

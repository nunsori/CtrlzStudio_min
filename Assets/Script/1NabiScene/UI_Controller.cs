using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject[] finish_titles;

    public Animator[] drink_animator;

    private bool is_progress = false;

    private bool is_drink = false;

    // Start is called before the first frame update
    void Start()
    {
        //�ʱ� ui active ����
        ui_objs[0].SetActive(true);
        ui_objs[1].SetActive(false);
        ui_objs[2].SetActive(false);

        dim_dialog_obj.SetActive(false);

        dim.gameObject.SetActive(false);

        change_animation_state(window_animator, "cafe_init");

        for(int i =0; i<make_drink_objs.Length; i++)
        {
            make_drink_objs[i].SetActive(false);
        }

        for(int i =0; i<finish_titles.Length; i++)
        {
            finish_titles[i].SetActive(false);
        }

        is_progress = false;
        is_drink = false;

        save_load_Data.Instance.load();
        //Debug.Log(save_load_Data.play_data.cur_progress);
        //Debug.Log(save_load_Data.Instance);
        Debug.Log(save_load_Data.Instance.play_data);
        DialogManager.currentDialogIndex = save_load_Data.Instance.play_data.cur_progress;

        


        
    }

    // Update is called once per frame
    void Update()
    {
        if (is_drink)
        {
            if (is_progress)
            {
                drink_animator[DialogManager.current_scene_page].speed = 1f;
                change_animation_state(drink_animator[DialogManager.current_scene_page], "animated_drink|CircleAction");
                
            }
            else
            {
                drink_animator[DialogManager.current_scene_page].speed = 0f;
                
            }

            if(drink_animator[DialogManager.current_scene_page].GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                is_progress = false;
                is_drink = false;
                Debug.Log("����Ϸ�");
                //�Լ�����
                finish_drink();
            }
        }
        
    }

    public void start_btn_clicked()
    {
        Debug.Log("start_clicked------------------------");
        //ui Ȱ��ȭ ����
        ui_objs[0].SetActive(false);
        ui_objs[1].SetActive(true);
        ui_objs[2].SetActive(false);
        dim_dialog_obj.SetActive(false);

        //���̾�α� Ȱ��ȭ
        //dialogManager.currentDialogIndex = 0;
        dialogManager.DisplayDialog();
    }

    //���� ����� active true
    public void make_ui_active()
    {
        //ui_objs[1].SetActive(false);
        //ui_objs[2].SetActive(true);
        //dim_dialog_obj.SetActive(false);
        //ui_objs[1].SetActive(false);
        //dialogManager.Makingbutton.SetActive(false);
        //make_drink_objs[DialogManager.current_scene_page].SetActive(true);
        for(int i = 0; i<make_drink_objs.Length; i++)
        {
            make_drink_objs[i].SetActive(DialogManager.current_scene_page == i);
        }

        is_drink = true;
        drink_ui_set[0].SetActive(true);
        drink_ui_set[1].SetActive(false);

        drink_animator[DialogManager.current_scene_page].speed = 0f;
        drink_animator[DialogManager.current_scene_page].Play("animated_drink|CircleAction");

        Debug.Log("current page index : " + DialogManager.current_scene_page);

        //fade in
        production_controller.call_production(production_controller.Instance.fade_production(0, dim.gameObject, true, 2f));

        //fade out �Լ� ȣ��
        production_controller.call_production(production_controller.Instance.fade_production(2.5f, dim.gameObject, false, 2f));

        

        production_controller.call_production(smooth_camera_call(2.5f, new SmoothCamera.temp_fuc(smoothCamera.MoveCupScene)));



        //ui ����
        production_controller.call_production(active_delay(2.1f, dim_dialog_obj, false));
        production_controller.call_production(active_delay(2.1f, ui_objs[1], false));
        production_controller.call_production(active_delay(2.1f, ui_objs[2], true));
        production_controller.call_production(active_delay(2.1f, dialogManager.Makingbutton, false));








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


    //���� �ٸ���� ��ư Ŭ���Ǿ��� ��, ���� ui��
    public void make_finish_clicked()
    {
        //ui Ȱ��ȭ ����
        //ui_objs[0].SetActive(false);
        //ui_objs[1].SetActive(true);
        //ui_objs[2].SetActive(false);

        //dialogManager.next_page_dialog();

        //smoothCamera.MoveMainScene();

        //fade in
        production_controller.call_production(production_controller.Instance.fade_production(0,dim.gameObject, true, 2f));

        production_controller.call_production(call_function_cafe_init(2.1f));

        //next dialog �Լ� ȣ��
        production_controller.call_production(call_function_delay(4.5f, new DialogManager.temp_fun(dialogManager.next_page_dialog)));

        production_controller.call_production(call_function_delay(2.1f, new DialogManager.temp_fun(dialogManager.reset_dialog)));
        
        production_controller.call_production(smooth_camera_call(2.1f, new SmoothCamera.temp_fuc(smoothCamera.MoveMainScene)));

        //fade out �Լ� ȣ��
        production_controller.call_production(production_controller.Instance.fade_production(2.5f, dim.gameObject, false, 2f));

        

        //ui ����
        production_controller.call_production(active_delay(2.1f, ui_objs[0], false));
        production_controller.call_production(active_delay(2.1f, ui_objs[1], true));
        production_controller.call_production(active_delay(2.1f, ui_objs[2], false));
    }

    public void next_scene()
    {

        
        //fade in
        production_controller.call_production(production_controller.Instance.fade_production(0, dim.gameObject, true, 2f));

        //
        production_controller.call_production(call_function_cafe_init(2.1f));

        //next dialog �Լ� ȣ��
        production_controller.call_production(call_function_delay(4.5f, new DialogManager.temp_fun(dialogManager.next_page_dialog)));

        production_controller.call_production(call_function_delay(2.1f, new DialogManager.temp_fun(dialogManager.reset_dialog)));

        production_controller.call_production(smooth_camera_call(2.1f, new SmoothCamera.temp_fuc(smoothCamera.MoveMainScene)));

        //fade out �Լ� ȣ��
        production_controller.call_production(production_controller.Instance.fade_production(2.5f, dim.gameObject, false, 2f));


        is_drink = false;
        //ui ����
        production_controller.call_production(active_delay(2.1f, ui_objs[0], false));
        production_controller.call_production(active_delay(2.1f, ui_objs[1], true));
        production_controller.call_production(active_delay(2.1f, ui_objs[2], false));
    }


    public void finish_drink()
    {
        drink_ui_set[0].SetActive(false);
        drink_ui_set[1].SetActive(true);

        //���� �����߰�


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





}

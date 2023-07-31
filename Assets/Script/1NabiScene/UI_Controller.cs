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

    [SerializeField]
    private GameObject dim_dialog_obj;


    public DialogManager dialogManager;

    public SmoothCamera smoothCamera;

    public Image dim;

    delegate void temp_fun();

    public Animator window_animator = null;

    // Start is called before the first frame update
    void Start()
    {
        //초기 ui active 설정
        ui_objs[0].SetActive(true);
        ui_objs[1].SetActive(false);
        ui_objs[2].SetActive(false);

        dim_dialog_obj.SetActive(false);

        dim.gameObject.SetActive(false);

        change_animation_state(window_animator, "cafe_init");
        

        save_load_Data.Instance.load();
        //Debug.Log(save_load_Data.play_data.cur_progress);
        //Debug.Log(save_load_Data.Instance);
        Debug.Log(save_load_Data.Instance.play_data);
        DialogManager.currentDialogIndex = save_load_Data.Instance.play_data.cur_progress;

        


        
    }

    // Update is called once per frame
    void Update()
    {
        
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

        //fade in
        production_controller.call_production(production_controller.Instance.fade_production(0, dim.gameObject, true, 2f));

        //fade out 함수 호출
        production_controller.call_production(production_controller.Instance.fade_production(2.5f, dim.gameObject, false, 2f));

        

        production_controller.call_production(smooth_camera_call(2.5f, new SmoothCamera.temp_fuc(smoothCamera.MoveCupScene)));



        //ui 조절
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


    //음료 다만들기 버튼 클릭되었을 때, 다음 ui로
    public void make_finish_clicked()
    {
        //ui 활성화 변경
        //ui_objs[0].SetActive(false);
        //ui_objs[1].SetActive(true);
        //ui_objs[2].SetActive(false);

        //dialogManager.next_page_dialog();

        //smoothCamera.MoveMainScene();

        //fade in
        production_controller.call_production(production_controller.Instance.fade_production(0,dim.gameObject, true, 2f));

        production_controller.call_production(call_function_cafe_init(2.1f));

        //next dialog 함수 호출
        production_controller.call_production(call_function_delay(4.5f, new DialogManager.temp_fun(dialogManager.next_page_dialog)));

        production_controller.call_production(call_function_delay(2.1f, new DialogManager.temp_fun(dialogManager.reset_dialog)));
        
        production_controller.call_production(smooth_camera_call(2.1f, new SmoothCamera.temp_fuc(smoothCamera.MoveMainScene)));

        //fade out 함수 호출
        production_controller.call_production(production_controller.Instance.fade_production(2.5f, dim.gameObject, false, 2f));

        

        //ui 조절
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

        //next dialog 함수 호출
        production_controller.call_production(call_function_delay(4.5f, new DialogManager.temp_fun(dialogManager.next_page_dialog)));

        production_controller.call_production(call_function_delay(2.1f, new DialogManager.temp_fun(dialogManager.reset_dialog)));

        production_controller.call_production(smooth_camera_call(2.1f, new SmoothCamera.temp_fuc(smoothCamera.MoveMainScene)));

        //fade out 함수 호출
        production_controller.call_production(production_controller.Instance.fade_production(2.5f, dim.gameObject, false, 2f));



        //ui 조절
        production_controller.call_production(active_delay(2.1f, ui_objs[0], false));
        production_controller.call_production(active_delay(2.1f, ui_objs[1], true));
        production_controller.call_production(active_delay(2.1f, ui_objs[2], false));
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





}

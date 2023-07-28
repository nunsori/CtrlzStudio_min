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
    private GameObject[] ui_objs;


    public DialogManager dialogManager;

    public Image dim;

    delegate void temp_fun();

    // Start is called before the first frame update
    void Start()
    {
        save_load_Data.Instance.load();
        //Debug.Log(save_load_Data.play_data.cur_progress);
        //Debug.Log(save_load_Data.Instance);
        Debug.Log(save_load_Data.Instance.play_data);
        DialogManager.currentDialogIndex = save_load_Data.Instance.play_data.cur_progress;

        dim.gameObject.SetActive(false);


        //�ʱ� ui active ����
        ui_objs[0].SetActive(true);
        ui_objs[1].SetActive(false);
        ui_objs[2].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void start_btn_clicked()
    {
        Debug.Log("start_clicked------------------------");
        //ui Ȱ��ȭ ����
        ui_objs[0].SetActive(false);
        ui_objs[1].SetActive(true);
        ui_objs[2].SetActive(false);

        //���̾�α� Ȱ��ȭ
        //dialogManager.currentDialogIndex = 0;
        dialogManager.DisplayDialog();
    }

    //���� ����� active true
    public void make_ui_active()
    {
        //ui_objs[1].SetActive(false);
        ui_objs[2].SetActive(true);
    }


    //���� �ٸ���� ��ư Ŭ���Ǿ��� ��, ���� ui��
    public void make_finish_clicked()
    {
        //ui Ȱ��ȭ ����
        //ui_objs[0].SetActive(false);
        //ui_objs[1].SetActive(true);
        //ui_objs[2].SetActive(false);


        production_controller.call_production(production_controller.Instance.fade_production(0,dim.gameObject, true, 2f));

        //production_controller.Production_list.Add(call_function_delay(2f, new DialogManager.temp_fun(dialogManager.next_page_dialog)));
        production_controller.call_production(call_function_delay(2f, new DialogManager.temp_fun(dialogManager.next_page_dialog)));

        production_controller.call_production(production_controller.Instance.fade_production(2.5f, dim.gameObject, false, 2f));

        //production_controller.Production_list.Add(active_delay(0, dim.gameObject, true));

        //production_controller.Production_list.Add(active_delay(4.5f, dim.gameObject, false));
        production_controller.call_production(active_delay(4.5f, ui_objs[0], false));
        production_controller.call_production(active_delay(4.5f, ui_objs[1], true));
        production_controller.call_production(active_delay(4.5f, ui_objs[2], false));
        //production_controller.Production_list.Add(active_delay(4.5f, ui_objs[0], false));
        //production_controller.Production_list.Add(active_delay(4.5f, ui_objs[1], true));
        //production_controller.Production_list.Add(active_delay(4.5f, ui_objs[2], false));
        //dialogManager.NextDialog();
        //dialogManager.next_page_dialog();
    }


    IEnumerator call_function_delay(float delay_, DialogManager.temp_fun function)
    {
        yield return new WaitForSeconds(delay_);

        function();
    }

    IEnumerator active_delay(float delay_, GameObject obj, bool is_active)
    {
        yield return new WaitForSeconds(delay_);

        obj.SetActive(is_active);
    }

    //public void 






}

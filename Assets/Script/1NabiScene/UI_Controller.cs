using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    /* 0 - start_btn_ui
     * 1 - 2-1 Dialog
     * 2 - 2-2 UI
    */
    [SerializeField]
    private GameObject[] ui_objs;


    public DialogManager dialogManager;

    // Start is called before the first frame update
    void Start()
    {
        save_load_Data.Instance.load();

        DialogManager.currentDialogIndex = save_load_Data.play_data.cur_progress;

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
        ui_objs[2].SetActive(true);
    }


    //���� �ٸ���� ��ư Ŭ���Ǿ��� ��, ���� ui��
    public void make_finish_clicked()
    {
        //ui Ȱ��ȭ ����
        ui_objs[0].SetActive(false);
        ui_objs[1].SetActive(true);
        ui_objs[2].SetActive(false);

        //dialogManager.NextDialog();
        dialogManager.next_page_dialog();
    }






}

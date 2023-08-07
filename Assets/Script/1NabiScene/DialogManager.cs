using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{   
    [Header("CSV ���� ���")]
    [Tooltip("CSV������ Asset/Dialog/�����̸�.csv�� �Է����ֽð�, \n ������ project>Asset>Dialog�� ����!")]
    //Assets/Dialog/Nabi_1_1.csv
    public string[] filePath; // CSV ���� ���

    // ��ȭ �����͸� ������ ����Ʈ
    public List<List<Dialog>> dialogs = new List<List<Dialog>>();


    [Header("ĳ���� �𵨸�")]
    public GameObject[] char_model;
    private Animator[] model_animator;
    public string[] state_name;




    [Header("Dialog UI Text")]
    public Text characterNameText; // ĳ���� �̸��� ����� UI �ؽ�Ʈ
    public TextMeshProUGUI dialogText; // ��縦 ����� UI �ؽ�Ʈ

    [Header("Dialog UI Text_list")]
    //0 - default 1 - dim
    public Text[] characterNameText_list; // ĳ���� �̸��� ����� UI �ؽ�Ʈ
    public TextMeshProUGUI[] dialogText_list; // ��縦 ����� UI �ؽ�Ʈ

    [Header("Dialog ������ ���Ḹ��� ��ư")]
    public GameObject Makingbutton;

    [Header("��� ��µǴ� �ӵ�")]
    public float typingSpeed; // ��簡 ������ ��µǴ� �ӵ�

    [Header("�̰� �ǵ��� ������!")]
    public bool isRunning = true;


    public static int current_scene_page = 0;

    public static int currentDialogIndex = 0; // ���� ��� ���� ��ȭ �ε���
    private bool isTyping = false; // ��� ��� ������ ����

    private UI_Controller uI_Controller = null;


    private string state = "";

    public delegate void temp_fun();

    private void Awake()
    {
        for (current_scene_page = 0; current_scene_page < filePath.Length; current_scene_page++)
        {
            LoadDialogsFromCSV();
        }
    }

    void Start()
    {
        
        //current_scene_page = 10;
        //currentDialogIndex = save_load_Data.Instance.play_data.cur_progress;
        save_load_Data.Instance.load();
        currentDialogIndex = 0;
        Makingbutton.SetActive(false);
        //LoadDialogsFromCSV();

        uI_Controller = gameObject.GetComponent<UI_Controller>();
        //DisplayDialog();

        for (int i = 0; i < uI_Controller.make_drink_objs.Length; i++)
        {
            uI_Controller.make_drink_objs[i].SetActive(Mathf.FloorToInt(DialogManager.current_scene_page / 2) == i);
        }

        for(int i = 0; i<char_model.Length; i++)
        {
            char_model[i].SetActive(false);
        }

        current_scene_page = save_load_Data.Instance.play_data.cur_progress;

        if(current_scene_page == -1)
        {
            current_scene_page = 0;
        }
        //current_scene_page = 8;

        uI_Controller.option_slider_set[0].value = save_load_Data.Instance.play_data.BGM_Volume;

        uI_Controller.option_slider_set[1].value = save_load_Data.Instance.play_data.Narr_Volume;


    }

    void LoadDialogsFromCSV()
    {
        string[] lines = File.ReadAllLines(filePath[current_scene_page]); // CSV ���� �б�

        for (int i = 1; i < lines.Length; i++) // ù ��° ���� ����̹Ƿ� �����ϰ� �� ��° �ٺ��� �����͸� ����
        {
            dialogs.Add(new List<Dialog>());
            string[] fields = lines[i].Split(','); // ��ǥ�� ���е� ������ �迭�� �о��
            Dialog dialog = new Dialog(fields[0], fields[1].Replace("\\n", "\n"), fields[2], fields[3]); // ĳ���� �̸��� ��縦 Dialog Ŭ������ ����
            dialogs[current_scene_page].Add(dialog); // ��ȭ �����͸� ����Ʈ�� �߰�
        }

        //current_scene_page++;
    }

    public void DisplayDialog()
    {
        //Debug.Log("dis@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
        //if (currentDialogIndex >= dialogs[current_scene_page].Count) // ��ȭ�� �������� �Լ��� ����, ��ư Ȱ��ȭ
        //{
            //Makingbutton.SetActive(true);
            
            //return;
        //}

        

        


        Dialog dialog = dialogs[current_scene_page][currentDialogIndex]; // ����� ��ȭ ��������

        if(dialog.production == "1")
        {
            characterNameText = characterNameText_list[1];
            dialogText = dialogText_list[1];
        }
        else
        {
            characterNameText = characterNameText_list[0];
            dialogText = dialogText_list[0];
        }

        characterNameText.text = dialog.characterName; // ĳ���� �̸� ���

        for(int i =0; i<char_model.Length; i++)
        {
            char_model[i].SetActive(false);
        }

        switch (dialog.characterName) {
            case "����":
                char_model[0].SetActive(true);
                //uI_Controller.change_animation_state(model_animator[0], state_name[0]);

                break;

            case "��ȣ":
                char_model[1].SetActive(true);
                //uI_Controller.change_animation_state(model_animator[1], state_name[0]);

                break;

            case "�� ��":
                char_model[2].SetActive(true);
                //uI_Controller.change_animation_state(model_animator[2], state_name[0]);

                break;

            case "����":
                char_model[3].SetActive(true);
                //uI_Controller.change_animation_state(model_animator[3], state_name[0]);

                break;

            case "�� ��":
                char_model[4].SetActive(true);
                //uI_Controller.change_animation_state(model_animator[4], state_name[0]);

                break;
        
        
        }



        dialogText.text = "";
        Makingbutton.SetActive(false);


        Debug.Log(dialog.production);

        //������� �Լ�
        switch (dialog.production) {
            case "-3":
                state = dialog.production;

                break;
            case "-2":
                //making drink
                state = dialog.production;

                break;
            case "-1":
                //ending
                state = dialog.production;

                break;
            case "0":
                //default
                state = dialog.production;
                uI_Controller.dim_dialog(false);
                uI_Controller.ui_objs[1].SetActive(true);

                uI_Controller.ui_objs[1].transform.GetChild(0).gameObject.SetActive(true);
                uI_Controller.ui_objs[1].transform.GetChild(1).gameObject.SetActive(true);


                break;
            case "11":
                state = dialog.production;
                uI_Controller.dim_dialog(true);
                uI_Controller.ui_objs[1].SetActive(true);

                uI_Controller.ui_objs[1].transform.GetChild(0).gameObject.SetActive(false);
                uI_Controller.ui_objs[1].transform.GetChild(1).gameObject.SetActive(true);

                dialogText_list[1].text = "";
                characterNameText_list[1].text = "";


                break;

            case "1":
                //dim_production
                state = dialog.production;
                uI_Controller.dim_dialog(true);
                uI_Controller.ui_objs[1].SetActive(false);

                break;


            default:
                //nonde

                Debug.Log("none");

                break;




        }

        string[] production_additional = dialog.production_additional.Split('%');

        Debug.Log(production_additional);

        uI_Controller.image_obj.SetActive(false);
        for (int i = 0; i < uI_Controller.sticker_set.Length; i++)
        {
            uI_Controller.sticker_set[i].SetActive(false);
        }

        switch (production_additional[0]) {
            case "0":
                //�ƹ��͵� �ȶ���
                Debug.Log("default_on");
                uI_Controller.image_obj.SetActive(false);
                for(int i =0; i<uI_Controller.sticker_set.Length; i++)
                {
                    uI_Controller.sticker_set[i].SetActive(false);
                }
                break;

            case "1":
                Debug.Log("image_on");
                //image
                if(uI_Controller.image_obj.activeSelf == true)
                {
                    break;
                }

                uI_Controller.image_obj.SetActive(false);
                uI_Controller.image_obj.GetComponent<Image>().sprite = uI_Controller.image_set[int.Parse(production_additional[1])];

                

                production_controller.call_production(production_controller.Instance.fade_production(0, uI_Controller.image_obj, true, 0.4f));
                break;


            case "2":
                Debug.Log("sticker_on");
                uI_Controller.sticker_set[int.Parse(production_additional[1])].SetActive(true);

                break;
            case "3":
                Debug.Log("change_BGM");

                sound_sr.Instance.Play_BGM(production_additional[1], save_load_Data.Instance.play_data.BGM_Volume, true);

                break;

            case "4":
                Debug.Log("play_effect");

                sound_sr.Instance.Play_Effect(production_additional[1], save_load_Data.Instance.play_data.Narr_Volume, false);

                break;


            default:

                break;

        }



        // ��� ��� �Լ� ȣ��
        if (isTyping == false)
        {
            StartCoroutine(TypeDialogText(dialog.text));
        }



        // ĳ�����̸� ĭ�� ����־ ��µǴ� �Լ�
        if (dialog.characterName != "")
        { // ĳ���� �̸��� ������� ������ ĳ���� �̸� ���
            characterNameText.text = dialog.characterName;
        }
        else
        { // ĳ���� �̸��� ��������� ���� ĳ���� �̸��� ���� �̸����� ���
            //characterNameText.text = dialogs[current_scene_page][currentDialogIndex - 1].characterName;
            characterNameText.text = "";
        }

    }

    // ��簡 ������ ��µǴ� �ڷ�ƾ
    IEnumerator TypeDialogText(string text)
    {
        isTyping = true;


        if(state == "0" && currentDialogIndex == 0)
        {
            //ȭ�� ���ۿ��� ����ϱ�
            uI_Controller.change_animation_state(uI_Controller.window_animator, "cafe_motion");
            yield return new WaitForSeconds(uI_Controller.window_animator.GetCurrentAnimatorClipInfo(0).Length);
        }
        else
        {
            //ȭ�� ���� init ����ϱ�
            //uI_Controller.change_animation_state(uI_Controller.window_animator, "cafe_init");
            
        }

        

        Debug.Log("typing_start");
        dialogText.text = "";

        string temp_str = "";

        foreach (char c in text)
        {
            if(c == '<' && temp_str == "")
            {
                temp_str += c;
            }else if(c == '>' && temp_str != "")
            {
                temp_str += c;
                dialogText.text += temp_str;
                Debug.Log(temp_str);
                temp_str = "";
            }
            else if(temp_str != "")
            {
                //dialogText.text += temp_str;
                temp_str += c;
            }
            else if(temp_str == "")
            {
                dialogText.text += c;
            }

            

            yield return new WaitForSeconds(typingSpeed);
        }

        Debug.Log("typing_end");

        isTyping = false;
    }

    // ���� ��ȭ�� �Ѿ�� �Լ�
    public void NextDialog()
    {
        
        Debug.Log("next_clicked");
        // ��� ��� ���� ���� Ŭ���� ���õǵ��� ��
        if (isTyping)
        {
            return;
        }

        sound_sr.Instance.Play_Effect("1", save_load_Data.Instance.play_data.Narr_Volume, false);

        if (state == "-2")
        {
            Debug.Log("making_btn");
            Makingbutton.SetActive(true);
            

            return;
        }
        else if(state == "-1")
        {
            //��������
            Debug.Log("next_stage@@@@@@@@@@@@@@@@");
            //next_page_dialog();
            uI_Controller.next_scene();

            return;
        }
        else if(state == "0")
        {

            Debug.Log("default");
            // ���� ��ȭ�� �̵�
            currentDialogIndex++;
            //uI_Controller.dim_dialog(false);

            // ��ȭ ��� �Լ� ȣ��
            DisplayDialog();
        }
        else if(state == "1" || state == "11")
        {
            currentDialogIndex++;
            //uI_Controller.dim_dialog(true);
            // ��ȭ ��� �Լ� ȣ��
            DisplayDialog();
        }else if(state == "-3")
        {
            //�����ϱ�
            uI_Controller.ending();
        }


        
    }

    

    void Update()
    {
        //���콺Ŭ��, space ��ư ������ ���� ��ȭ�� �̵�
        if (isRunning && Input.GetMouseButtonDown(0))
        {
            //NextDialog();
        }

        if (isRunning && Input.GetKeyDown(KeyCode.Space))
        {
            //NextDialog();
        }
    }

    public void next_page_dialog()
    {
        current_scene_page++;
        currentDialogIndex = 0;

        //for (int i = 0; i < uI_Controller.make_drink_objs.Length; i++)
        //{
            //uI_Controller.make_drink_objs[i].SetActive(Mathf.FloorToInt(DialogManager.current_scene_page / 2) == i);
        //}

        //LoadDialogsFromCSV();

        DisplayDialog();
    }

    public void reset_dialog()
    {
        dialogText.text = "";
        characterNameText.text = "";
    }
}

public class Dialog
{
    public string characterName; // ĳ���� �̸�
    public string text; // ���
    public string production; // ���� ���
    public string production_additional;

    //�ؽ�Ʈ ǥ�� UI
    public Dialog(string characterName, string text, string production, string production_additional)
    {
        this.characterName = characterName;
        this.text = text;
        this.production = production;
        this.production_additional = production_additional;
    }
}
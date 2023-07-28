using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{    [Header("CSV ���� ���")]
    [Tooltip("CSV������ Asset/Dialog/�����̸�.csv�� �Է����ֽð�, \n ������ project>Asset>Dialog�� ����!")]
    //Assets/Dialog/Nabi_1_1.csv
    public string[] filePath; // CSV ���� ���

    // ��ȭ �����͸� ������ ����Ʈ
    public List<List<Dialog>> dialogs = new List<List<Dialog>>();

    


    [Header("Dialog UI Text")]
    public Text characterNameText; // ĳ���� �̸��� ����� UI �ؽ�Ʈ
    public TextMeshProUGUI dialogText; // ��縦 ����� UI �ؽ�Ʈ

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

    void Start()
    {
        current_scene_page = 0;
        currentDialogIndex = 0;
        Makingbutton.SetActive(false);
        LoadDialogsFromCSV();

        uI_Controller = gameObject.GetComponent<UI_Controller>();
        //DisplayDialog();
        

    
    }

    void LoadDialogsFromCSV()
    {
        string[] lines = File.ReadAllLines(filePath[current_scene_page]); // CSV ���� �б�

        for (int i = 1; i < lines.Length; i++) // ù ��° ���� ����̹Ƿ� �����ϰ� �� ��° �ٺ��� �����͸� ����
        {
            dialogs.Add(new List<Dialog>());
            string[] fields = lines[i].Split(','); // ��ǥ�� ���е� ������ �迭�� �о��
            Dialog dialog = new Dialog(fields[0], fields[1].Replace("\\n", "\n"), fields[2]); // ĳ���� �̸��� ��縦 Dialog Ŭ������ ����
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

        

        dialogText.text = "";
        Makingbutton.SetActive(false);


        Dialog dialog = dialogs[current_scene_page][currentDialogIndex]; // ����� ��ȭ ��������
        characterNameText.text = dialog.characterName; // ĳ���� �̸� ���


        // ��� ��� �Լ� ȣ��
        if(isTyping == false)
        {
            StartCoroutine(TypeDialogText(dialog.text));
        }

        Debug.Log(dialog.production);

        //������� �Լ�
        switch (dialog.production) {
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



                break;

            case "1":
                //other production
                

                break;


            default:
                //nonde

                Debug.Log("none");

                break;
        
        }



        // ĳ�����̸� ĭ�� ����־ ��µǴ� �Լ�
        if (dialog.characterName != "")
        { // ĳ���� �̸��� ������� ������ ĳ���� �̸� ���
            characterNameText.text = dialog.characterName;
        }
        else
        { // ĳ���� �̸��� ��������� ���� ĳ���� �̸��� ���� �̸����� ���
            characterNameText.text = dialogs[current_scene_page][currentDialogIndex - 1].characterName;
        }

    }

    // ��簡 ������ ��µǴ� �ڷ�ƾ
    IEnumerator TypeDialogText(string text)
    {
        isTyping = true;
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

        isTyping = false;
    }

    // ���� ��ȭ�� �Ѿ�� �Լ�
    public void NextDialog()
    {
        // ��� ��� ���� ���� Ŭ���� ���õǵ��� ��
        if (isTyping)
        {
            return;
        }

        if(state == "-2")
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
            uI_Controller.make_finish_clicked();

            return;
        }
        else if(state == "0")
        {

            Debug.Log("default");
            // ���� ��ȭ�� �̵�
            currentDialogIndex++;

            // ��ȭ ��� �Լ� ȣ��
            DisplayDialog();
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

        LoadDialogsFromCSV();

        DisplayDialog();
    }
}

public class Dialog
{
    public string characterName; // ĳ���� �̸�
    public string text; // ���
    public string production; // ���� ���

    //�ؽ�Ʈ ǥ�� UI
    public Dialog(string characterName, string text, string production)
    {
        this.characterName = characterName;
        this.text = text;
        this.production = production;
    }
}
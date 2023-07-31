using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{    [Header("CSV 파일 경로")]
    [Tooltip("CSV파일은 Asset/Dialog/파일이름.csv로 입력해주시고, \n 파일은 project>Asset>Dialog에 저장!")]
    //Assets/Dialog/Nabi_1_1.csv
    public string[] filePath; // CSV 파일 경로

    // 대화 데이터를 저장할 리스트
    public List<List<Dialog>> dialogs = new List<List<Dialog>>();

    


    [Header("Dialog UI Text")]
    public Text characterNameText; // 캐릭터 이름을 출력할 UI 텍스트
    public TextMeshProUGUI dialogText; // 대사를 출력할 UI 텍스트

    [Header("Dialog UI Text_list")]
    //0 - default 1 - dim
    public Text[] characterNameText_list; // 캐릭터 이름을 출력할 UI 텍스트
    public TextMeshProUGUI[] dialogText_list; // 대사를 출력할 UI 텍스트

    [Header("Dialog 끝나고 음료만들기 버튼")]
    public GameObject Makingbutton;

    [Header("대사 출력되는 속도")]
    public float typingSpeed; // 대사가 서서히 출력되는 속도

    [Header("이건 건들지 마세요!")]
    public bool isRunning = true;


    public static int current_scene_page = 0;

    public static int currentDialogIndex = 0; // 현재 출력 중인 대화 인덱스
    private bool isTyping = false; // 대사 출력 중인지 여부

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
        string[] lines = File.ReadAllLines(filePath[current_scene_page]); // CSV 파일 읽기

        for (int i = 1; i < lines.Length; i++) // 첫 번째 줄은 헤더이므로 무시하고 두 번째 줄부터 데이터를 읽음
        {
            dialogs.Add(new List<Dialog>());
            string[] fields = lines[i].Split(','); // 쉼표로 구분된 값들을 배열로 읽어옴
            Dialog dialog = new Dialog(fields[0], fields[1].Replace("\\n", "\n"), fields[2]); // 캐릭터 이름과 대사를 Dialog 클래스에 저장
            dialogs[current_scene_page].Add(dialog); // 대화 데이터를 리스트에 추가
        }

        //current_scene_page++;
    }

    public void DisplayDialog()
    {
        //Debug.Log("dis@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
        //if (currentDialogIndex >= dialogs[current_scene_page].Count) // 대화가 끝났으면 함수를 종료, 버튼 활성화
        //{
            //Makingbutton.SetActive(true);
            
            //return;
        //}

        

        


        Dialog dialog = dialogs[current_scene_page][currentDialogIndex]; // 출력할 대화 가져오기

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

        characterNameText.text = dialog.characterName; // 캐릭터 이름 출력


        dialogText.text = "";
        Makingbutton.SetActive(false);


        Debug.Log(dialog.production);

        //연출관련 함수
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
                uI_Controller.dim_dialog(false);


                break;

            case "1":
                //dim_production
                state = dialog.production;
                uI_Controller.dim_dialog(true);

                break;


            default:
                //nonde

                Debug.Log("none");

                break;



        
        }


        // 대사 출력 함수 호출
        if (isTyping == false)
        {
            StartCoroutine(TypeDialogText(dialog.text));
        }



        // 캐릭터이름 칸이 비어있어도 출력되는 함수
        if (dialog.characterName != "")
        { // 캐릭터 이름이 비어있지 않으면 캐릭터 이름 출력
            characterNameText.text = dialog.characterName;
        }
        else
        { // 캐릭터 이름이 비어있으면 이전 캐릭터 이름과 같은 이름으로 출력
            characterNameText.text = dialogs[current_scene_page][currentDialogIndex - 1].characterName;
        }

    }

    // 대사가 서서히 출력되는 코루틴
    IEnumerator TypeDialogText(string text)
    {
        isTyping = true;


        if(state == "0" && currentDialogIndex == 0)
        {
            //화면 시작연출 재생하기
            uI_Controller.change_animation_state(uI_Controller.window_animator, "cafe_init");
            uI_Controller.change_animation_state(uI_Controller.window_animator, "cafe_motion");
            yield return new WaitForSeconds(uI_Controller.window_animator.GetCurrentAnimatorClipInfo(0).Length);
        }
        else
        {
            //화면 연출 init 재생하기
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

    // 다음 대화로 넘어가는 함수
    public void NextDialog()
    {
        Debug.Log("next_clicked");
        // 대사 출력 중일 때는 클릭이 무시되도록 함
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
            //다음연출
            Debug.Log("next_stage@@@@@@@@@@@@@@@@");
            //next_page_dialog();
            uI_Controller.next_scene();

            return;
        }
        else if(state == "0")
        {

            Debug.Log("default");
            // 다음 대화로 이동
            currentDialogIndex++;
            //uI_Controller.dim_dialog(false);

            // 대화 출력 함수 호출
            DisplayDialog();
        }
        else if(state == "1")
        {
            currentDialogIndex++;
            //uI_Controller.dim_dialog(true);
            // 대화 출력 함수 호출
            DisplayDialog();
        }


        
    }

    

    void Update()
    {
        //마우스클릭, space 버튼 누르면 다음 대화씬 이동
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

    public void reset_dialog()
    {
        dialogText.text = "";
        characterNameText.text = "";
    }
}

public class Dialog
{
    public string characterName; // 캐릭터 이름
    public string text; // 대사
    public string production; // 연출 목록

    //텍스트 표시 UI
    public Dialog(string characterName, string text, string production)
    {
        this.characterName = characterName;
        this.text = text;
        this.production = production;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{    [Header("CSV 파일 경로")]
    [Tooltip("CSV파일은 Asset/Dialog/파일이름.csv로 입력해주시고, \n 파일은 project>Asset>Dialog에 저장!")]
    public string filePath; // CSV 파일 경로
    // 대화 데이터를 저장할 리스트
    public List<Dialog> dialogs = new List<Dialog>();


    [Header("Dialog UI Text")]
    public Text characterNameText; // 캐릭터 이름을 출력할 UI 텍스트
    public TextMeshProUGUI dialogText; // 대사를 출력할 UI 텍스트

    [Header("Dialog 끝나고 음료만들기 버튼")]
    public GameObject Makingbutton;

    [Header("대사 출력되는 속도")]
    public float typingSpeed; // 대사가 서서히 출력되는 속도

    [Header("이건 건들지 마세요!")]
    public bool isRunning = true;


    public int currentDialogIndex = 0; // 현재 출력 중인 대화 인덱스
    private bool isTyping = false; // 대사 출력 중인지 여부

    private UI_Controller uI_Controller = null;

    void Start()
    {
        currentDialogIndex = 0;
        Makingbutton.SetActive(false);
        LoadDialogsFromCSV();

        uI_Controller = gameObject.GetComponent<UI_Controller>();
        //DisplayDialog();
        

    
    }

    void LoadDialogsFromCSV()
    {
        string[] lines = File.ReadAllLines(filePath); // CSV 파일 읽기

        for (int i = 1; i < lines.Length; i++) // 첫 번째 줄은 헤더이므로 무시하고 두 번째 줄부터 데이터를 읽음
        {
            string[] fields = lines[i].Split(','); // 쉼표로 구분된 값들을 배열로 읽어옴
            Dialog dialog = new Dialog(fields[0], fields[1].Replace("\\n", "\n")); // 캐릭터 이름과 대사를 Dialog 클래스에 저장
            dialogs.Add(dialog); // 대화 데이터를 리스트에 추가
        }
    }

    public void DisplayDialog()
    {
        if (currentDialogIndex >= dialogs.Count) // 대화가 끝났으면 함수를 종료, 버튼 활성화
        {
            Makingbutton.SetActive(true);
            
            return;
        }

        Dialog dialog = dialogs[currentDialogIndex]; // 출력할 대화 가져오기
        characterNameText.text = dialog.characterName; // 캐릭터 이름 출력


        // 대사 출력 함수 호출
        StartCoroutine(TypeDialogText(dialog.text));

        // 캐릭터이름 칸이 비어있어도 출력되는 함수
        if (dialog.characterName != "")
        { // 캐릭터 이름이 비어있지 않으면 캐릭터 이름 출력
            characterNameText.text = dialog.characterName;
        }
        else
        { // 캐릭터 이름이 비어있으면 이전 캐릭터 이름과 같은 이름으로 출력
            characterNameText.text = dialogs[currentDialogIndex - 1].characterName;
        }

    }

    // 대사가 서서히 출력되는 코루틴
    IEnumerator TypeDialogText(string text)
    {
        isTyping = true;
        dialogText.text = "";

        foreach (char c in text)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    // 다음 대화로 넘어가는 함수
    public void NextDialog()
    {
        // 대사 출력 중일 때는 클릭이 무시되도록 함
        if (isTyping)
        {
            return;
        }

        // 다음 대화로 이동
        currentDialogIndex++;

        // 대화 출력 함수 호출
        DisplayDialog();
    }

    

    void Update()
    {
        //마우스클릭, space 버튼 누르면 다음 대화씬 이동
        if (isRunning && Input.GetMouseButtonDown(0))
        {
            NextDialog();
        }

        if (isRunning && Input.GetKeyDown(KeyCode.Space))
        {
            NextDialog();
        }
    }
}

public class Dialog
{
    public string characterName; // 캐릭터 이름
    public string text; // 대사

    //텍스트 표시 UI
    public Dialog(string characterName, string text)
    {
        this.characterName = characterName;
        this.text = text;
    }
}
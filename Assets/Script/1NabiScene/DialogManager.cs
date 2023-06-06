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
            Dialog dialog = new Dialog(fields[0], fields[1].Replace("\\n", "\n")); // ĳ���� �̸��� ��縦 Dialog Ŭ������ ����
            dialogs[current_scene_page].Add(dialog); // ��ȭ �����͸� ����Ʈ�� �߰�
        }

        //current_scene_page++;
    }

    public void DisplayDialog()
    {
        if (currentDialogIndex >= dialogs[current_scene_page].Count) // ��ȭ�� �������� �Լ��� ����, ��ư Ȱ��ȭ
        {
            Makingbutton.SetActive(true);
            
            return;
        }

        dialogText.text = "";
        Makingbutton.SetActive(false);


        Dialog dialog = dialogs[current_scene_page][currentDialogIndex]; // ����� ��ȭ ��������
        characterNameText.text = dialog.characterName; // ĳ���� �̸� ���


        // ��� ��� �Լ� ȣ��
        StartCoroutine(TypeDialogText(dialog.text));

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

        foreach (char c in text)
        {
            dialogText.text += c;
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

        // ���� ��ȭ�� �̵�
        currentDialogIndex++;

        // ��ȭ ��� �Լ� ȣ��
        DisplayDialog();
    }

    

    void Update()
    {
        //���콺Ŭ��, space ��ư ������ ���� ��ȭ�� �̵�
        if (isRunning && Input.GetMouseButtonDown(0))
        {
            NextDialog();
        }

        if (isRunning && Input.GetKeyDown(KeyCode.Space))
        {
            NextDialog();
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

    //�ؽ�Ʈ ǥ�� UI
    public Dialog(string characterName, string text)
    {
        this.characterName = characterName;
        this.text = text;
    }
}
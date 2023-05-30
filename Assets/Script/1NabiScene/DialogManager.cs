using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{    [Header("CSV ���� ���")]
    [Tooltip("CSV������ Asset/Dialog/�����̸�.csv�� �Է����ֽð�, \n ������ project>Asset>Dialog�� ����!")]
    public string filePath; // CSV ���� ���
    // ��ȭ �����͸� ������ ����Ʈ
    public List<Dialog> dialogs = new List<Dialog>();


    [Header("Dialog UI Text")]
    public Text characterNameText; // ĳ���� �̸��� ����� UI �ؽ�Ʈ
    public TextMeshProUGUI dialogText; // ��縦 ����� UI �ؽ�Ʈ

    [Header("Dialog ������ ���Ḹ��� ��ư")]
    public GameObject Makingbutton;

    [Header("��� ��µǴ� �ӵ�")]
    public float typingSpeed; // ��簡 ������ ��µǴ� �ӵ�

    [Header("�̰� �ǵ��� ������!")]
    public bool isRunning = true;


    private int currentDialogIndex = 0; // ���� ��� ���� ��ȭ �ε���
    private bool isTyping = false; // ��� ��� ������ ����

    void Start()
    {
        LoadDialogsFromCSV();
        DisplayDialog();
        Makingbutton.SetActive(false);

    
    }

    void LoadDialogsFromCSV()
    {
        string[] lines = File.ReadAllLines(filePath); // CSV ���� �б�

        for (int i = 1; i < lines.Length; i++) // ù ��° ���� ����̹Ƿ� �����ϰ� �� ��° �ٺ��� �����͸� ����
        {
            string[] fields = lines[i].Split(','); // ��ǥ�� ���е� ������ �迭�� �о��
            Dialog dialog = new Dialog(fields[0], fields[1].Replace("\\n", "\n")); // ĳ���� �̸��� ��縦 Dialog Ŭ������ ����
            dialogs.Add(dialog); // ��ȭ �����͸� ����Ʈ�� �߰�
        }
    }

    void DisplayDialog()
    {
        if (currentDialogIndex >= dialogs.Count) // ��ȭ�� �������� �Լ��� ����, ��ư Ȱ��ȭ
        {
            Makingbutton.SetActive(true);
            return;
        }

        Dialog dialog = dialogs[currentDialogIndex]; // ����� ��ȭ ��������
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
            characterNameText.text = dialogs[currentDialogIndex - 1].characterName;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Click : MonoBehaviour
{
    public GameObject startButton; //���۹�ư
    public Image fadeimage; //fade in out �� �� ���� ���� �̹���
    public Image optionimage; //optionâ Ȱ��ȭ

    void Start()
    {
        fadeimage.enabled = false;
        optionimage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Start
    public void StartScene() //�� ���� �޼ҵ� // public���� �����ؾ� ui�� ���� �� �ֽ��ϴ�
    {
        startButton.SetActive(false); //��ư ��Ȱ��ȭ
        fadeimage.enabled = true; // Fade �̹��� Ȱ��ȭ Fade in out ����
        StartCoroutine(Fade()); //�ڷ�ƾ���� �ð� ������
        Invoke("Scene", 2f);


    }

    IEnumerator Fade() //fade in out
    {
        float fadeCount = 0;
        while (fadeCount < 1.1f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f); //0.01�� ���� ����
            fadeimage.color = new Color(0, 0, 0, fadeCount);
            if (fadeCount == 1f)
            {
                break;
            }
        }

        while (fadeCount > 0)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f); //0.01�� ���� ����
            fadeimage.color = new Color(0, 0, 0, fadeCount);
        }
    }

    void Scene()  //Nabi ������ �Ѿ��
    {
        SceneManager.LoadScene(2);
    }

    //option
    public void Option()
    {
        optionimage.enabled = true;
        StartCoroutine(Fade2());
    }

    IEnumerator Fade2()
    {
        float fadeCount = 0;
        while (fadeCount < 1f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f); //0.01�� ���� ����
            optionimage.color = new Color(47f / 255f, 79f / 255f, 108f / 255f, fadeCount);

        }
    }

    //Exit
    public void Exit()
    {
        Application.Quit();
    }

}
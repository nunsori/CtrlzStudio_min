using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class intro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("IsFirstRun", 0) == 0)
        {
            // ù ��° ���� �ÿ��� ����Ǵ� �ڵ�
            PlayerPrefs.SetInt("IsFirstRun", 1);
        }

        if (PlayerPrefs.GetInt("IsFirstRun", 0) == 1)
        {
            // �� ��° ������ʹ� ������� �ʴ� �ڵ�
        }
        else
        {
            // ù ��° ���� �ÿ��� ����Ǵ� �ڵ�
            PlayerPrefs.SetInt("IsFirstRun", 1);
        }

        Invoke("nextScene", 3f);
    }

    void nextScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}

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
            // 첫 번째 실행 시에만 실행되는 코드
            PlayerPrefs.SetInt("IsFirstRun", 1);
        }

        if (PlayerPrefs.GetInt("IsFirstRun", 0) == 1)
        {
            // 두 번째 실행부터는 실행되지 않는 코드
        }
        else
        {
            // 첫 번째 실행 시에만 실행되는 코드
            PlayerPrefs.SetInt("IsFirstRun", 1);
        }

        Invoke("nextScene", 3f);
    }

    void nextScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}

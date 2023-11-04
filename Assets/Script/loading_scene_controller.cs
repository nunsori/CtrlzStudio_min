using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loading_scene_controller : MonoBehaviour
{
    /*
    [Header("title_btns")]
    [SerializeField]
    private GameObject[] btn_list;
    [SerializeField]
    private string[] btn_fadeout_motion_name;*/

    [Header("loading scene manage")]
    [SerializeField]
    private CanvasGroup sceneloader;
    [SerializeField]
    private Image progressBar;
    [SerializeField]
    private float fade_effect_time;
    [SerializeField]
    private string loadscene_name;
    

    //Animator[] btn_animator_list;

    protected static loading_scene_controller instance;
    public static loading_scene_controller Instance {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<loading_scene_controller>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = Create();
                }
            }
            return instance;
        }

            private set { instance = value; }

        }

    private static loading_scene_controller Create()
    {
        var SceneLoaderPrefab = Resources.Load<loading_scene_controller>("loading_ui");
        return Instantiate(SceneLoaderPrefab);
    }


    // Start is called before the first frame update

    private void Awake()
    {
        //singleton
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        /*
        for(int i =0; i<btn_list.Length; i++)
        {
            btn_animator_list[i] = btn_list[i].GetComponent<Animator>();
        }*/
        Screen.SetResolution(1920, 1080, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void load_new_scene(string scene_name)
    {
        //fade out effect start
        /*
        for(int i =0; i<btn_animator_list.Length; i++)
        {
            change_animation_state(btn_animator_list[i], btn_fadeout_motion_name[i]);
        }


        //wait time

        wait_time(1f);
        */


        //generate loading scene
        gameObject.SetActive(true);
        
        SceneManager.sceneLoaded += load_scene_end;
        loadscene_name = scene_name;
        StartCoroutine(load_scene(scene_name));



    }




    private void load_scene_end(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.name == loadscene_name)
        {
            StartCoroutine(fade(false));
            SceneManager.sceneLoaded -= load_scene_end;
        }
    }

    private IEnumerator load_scene(string scene_name)
    {
        progressBar.fillAmount = 0f;
        yield return StartCoroutine(fade(true));

        AsyncOperation op = SceneManager.LoadSceneAsync(scene_name);
        op.allowSceneActivation = false;

        float temp = 0f;
        while (!op.isDone)
        {
            yield return null;
            temp += Time.unscaledDeltaTime;

            if(op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, temp); //진행상황 나타내줌
                if(progressBar.fillAmount >= op.progress)
                {
                    temp = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, temp); //진행상황 나타내줌

                if(progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }

        }
    }


    private IEnumerator fade(bool is_fade_in)
    {
        float temp = 0f;

        while(temp <= fade_effect_time)
        {
            yield return null;
            temp += Time.unscaledDeltaTime * 2f;
            sceneloader.alpha = Mathf.Lerp(is_fade_in ? 0 : 1, is_fade_in ? 1 : 0, temp);
        }

        if (!is_fade_in)
        {
            gameObject.SetActive(false);
        }

    }
}

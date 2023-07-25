using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class production_controller : MonoBehaviour
{
    private static production_controller instance;

    private static List<IEnumerator> Production_list; //= new List<IEnumerator>();

    public GameObject temp;

    static IEnumerator temp_cor;

    private void Awake()
    {
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

    public static production_controller Instance
    {
        get
        {
            if(null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Start()
    {
        Production_list = new List<IEnumerator>();


        //함수 부르는 예시
        //원하는 코루틴을 인자로 주어 호출하면 된다
        //call_production(fade_production(temp, false, 0.4f));

        //다른 스크립트에서 호출 예시
        //production_controller.call_production(production_controller.fade_production(gameObject, true, 0.4f);



    }

    
    //모든 연출 멈추는 함수
    public static void stop_all_prodution()
    {
        Production_list.ForEach((production) =>
        {
            instance.StopCoroutine(production);
        });

        Production_list.Clear();
    }

    //연출 부르는 함수
    public static void call_production(IEnumerator coroutine_)
    {
        Production_list.Add(coroutine_);
        instance.StartCoroutine(coroutine_);
    }


    //fade 함수
    //obj - 대상 오브젝트 (반드시 canvas group 컴포넌트를 가지고 있어야함)
    //fade_in - true일시 fade in / false일시 fade out
    //delay_ - fade in/out 에 걸리는 시간
    //active - fade in/out 시 오브젝트의 ative값도 조정해줄지 여부 결정, 기본 true로 되어있음
    //term - 그냥 두기 (몬가 렉 많이 걸린다 싶으면 0.1아니면 0.05 정도로 조정해보기)
    public static IEnumerator fade_production(GameObject obj, bool fade_in, float delay_, bool acitve = true,float term = 0.01f)
    {
        CanvasGroup canvasGroup = null;
        float progress = 0f;
        float time_check = 0f;

        Debug.Log("start cor");

        
        canvasGroup = obj.GetComponent<CanvasGroup>();



        if (fade_in)
        {
            //fade in 이므로 무조건 알파값 0으로 고정시켜서 시작
            canvasGroup.alpha = 0f;
            if (acitve)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            //fade out 이므로 무조건 알파값 1로 고정시키고 시작
            canvasGroup.alpha = 1f;

            if (acitve)
            {
                obj.SetActive(true);
            }
        }

        while(progress <= 1)
        {
            

            yield return new WaitForSeconds(term);

            time_check += term;

            progress = time_check / delay_;

            //canvasGroup.alpha = progress;


            //반복하며 알파값 조절
            canvasGroup.alpha = Mathf.Lerp((fade_in)? 0 : 1, (fade_in) ? 1 : 0, progress);
            //canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, (fade_in) ? 1 : 0, 0.5f);

            //delay_ / term = 반복횟수
            //1/반복횟수
            //term / delay_

        }

        Debug.Log("cor end");

        if (acitve && fade_in == true)
        {
            obj.SetActive(true);
        }else if(acitve && fade_in == false)
        {
            obj.SetActive(false);
        }





    }
}


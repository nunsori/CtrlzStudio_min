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


        //�Լ� �θ��� ����
        //���ϴ� �ڷ�ƾ�� ���ڷ� �־� ȣ���ϸ� �ȴ�
        //call_production(fade_production(temp, false, 0.4f));

        //�ٸ� ��ũ��Ʈ���� ȣ�� ����
        //production_controller.call_production(production_controller.fade_production(gameObject, true, 0.4f);



    }

    
    //��� ���� ���ߴ� �Լ�
    public static void stop_all_prodution()
    {
        Production_list.ForEach((production) =>
        {
            instance.StopCoroutine(production);
        });

        Production_list.Clear();
    }

    //���� �θ��� �Լ�
    public static void call_production(IEnumerator coroutine_)
    {
        Production_list.Add(coroutine_);
        instance.StartCoroutine(coroutine_);
    }


    //fade �Լ�
    //obj - ��� ������Ʈ (�ݵ�� canvas group ������Ʈ�� ������ �־����)
    //fade_in - true�Ͻ� fade in / false�Ͻ� fade out
    //delay_ - fade in/out �� �ɸ��� �ð�
    //active - fade in/out �� ������Ʈ�� ative���� ���������� ���� ����, �⺻ true�� �Ǿ�����
    //term - �׳� �α� (�� �� ���� �ɸ��� ������ 0.1�ƴϸ� 0.05 ������ �����غ���)
    public static IEnumerator fade_production(GameObject obj, bool fade_in, float delay_, bool acitve = true,float term = 0.01f)
    {
        CanvasGroup canvasGroup = null;
        float progress = 0f;
        float time_check = 0f;

        Debug.Log("start cor");

        
        canvasGroup = obj.GetComponent<CanvasGroup>();



        if (fade_in)
        {
            //fade in �̹Ƿ� ������ ���İ� 0���� �������Ѽ� ����
            canvasGroup.alpha = 0f;
            if (acitve)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            //fade out �̹Ƿ� ������ ���İ� 1�� ������Ű�� ����
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


            //�ݺ��ϸ� ���İ� ����
            canvasGroup.alpha = Mathf.Lerp((fade_in)? 0 : 1, (fade_in) ? 1 : 0, progress);
            //canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, (fade_in) ? 1 : 0, 0.5f);

            //delay_ / term = �ݺ�Ƚ��
            //1/�ݺ�Ƚ��
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


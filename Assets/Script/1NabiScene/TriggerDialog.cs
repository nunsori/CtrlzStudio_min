using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDialog : MonoBehaviour
{
    public GameObject nabiObject;
    //public GameObject mihoObject;
    public Text text;

    public GameObject TriggerImage;

    
    public Vector3 offset;

    private RectTransform rectTransform;

  

    private List<string> nabiList = new List<string>();
    private List<string> mihoList = new List<string>();

    

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();



        nabiList.Add("�ֿ�-(�ȳ��ϼ���.)");
        nabiList.Add("�ٷ�-(���� �ٺ�����)");
        nabiList.Add("��-.(������ �����ּ���.\n���ϴ� ���̿���)");
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            int randomIndex = Random.Range(0, nabiList.Count);
            string Dialog = nabiList[randomIndex];
           

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == nabiObject)
            {
                text.text = Dialog.ToString();
                
                Debug.Log("�ϸ���Ŭ��");
                rectTransform.position = nabiObject.transform.position + offset;

            }
           

        }


    }

  
}
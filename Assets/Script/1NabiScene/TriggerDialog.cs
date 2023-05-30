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



        nabiList.Add("애옹-(안녕하세요.)");
        nabiList.Add("꾸룩-(일이 바빠서요)");
        nabiList.Add("먘-.(만지지 말아주세요.\n일하는 중이에요)");
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
                
                Debug.Log("하르방클릭");
                rectTransform.position = nabiObject.transform.position + offset;

            }
           

        }


    }

  
}
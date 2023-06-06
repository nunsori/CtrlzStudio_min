
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupUI : MonoBehaviour
{
    public GameObject cupObject;
    void Start()
    {
        cupObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickbutton()
    {
        cupObject.SetActive(true);
    }
}

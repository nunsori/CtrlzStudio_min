using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Controller : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


[System.Serializable]
public abstract class Production_Controller {

    public sound_sr sound_controller = null;



    public void Init()
    {

    }
    public abstract void product_init();
}

public class page1 : production_controller { 

    

    public  void product_init()
    {

    }

}

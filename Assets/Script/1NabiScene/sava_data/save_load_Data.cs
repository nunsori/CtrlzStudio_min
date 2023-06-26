using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;



public class data_
{

    /*
     *  �ٸ� ��ũ��Ʈ, �Լ����� �� �ؿ� ������ �������� ��
     * 
     *  ex) temp ���� �������� �� 
     *      save_load_Data.play_data.temp
     *      �� �̿��ؼ� temp ������ ���ٰ���
     * 
     */
    public int temp = 0;

    public int cur_progress = 0;

    public bool[] progress_arr = { false, false, false, false, false };


    //���� ���� ����
    public float BGM_Volume;
    public float Narr_Volume;



    //������ �Լ�
    public data_()
    {
        temp = 0;
        cur_progress = 0;
        progress_arr = new bool[5];

        //
        BGM_Volume = 0.5f;
        Narr_Volume = 0.5f;
        Debug.Log("created data_ @@@@");
    }



}


public class save_load_Data : MonoBehaviour
{

    public static save_load_Data Instance;

    [Header("data_path_name")]
    [SerializeField]
    private string data_file_name = "";
    //[SerializeField]
    //private string json_file_name = "";

    private string data_path;

    private string data_string_temp;

    public data_ play_data;


    private void Awake()
    {
        //������ ���� ���� ��� ����
        //data_file_name = "/" + data_file_name + "/" ;
        data_file_name = data_file_name + ".json";
        data_path = Application.persistentDataPath + "/data/";

        

        //singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        //�÷��� ������ ����
        play_data = new data_();
        Debug.Log(play_data);

        load();


    }


    //���� �Լ�
    public void save()
    {
        data_string_temp = JsonUtility.ToJson(play_data, true);
        //Debug.Log(data_string_temp);
        
        File.WriteAllText(data_path + data_file_name, data_string_temp);

        Debug.Log("save complete");
    }

    //�ε� �Լ�
    public void load()
    {
        //if file does not exist
        if (!File.Exists(data_path))
        {
            //make file
            //File.Create(data_path);
            Directory.CreateDirectory(data_path);
            Debug.Log("data file created");
        }


        //data file does not exist
        if (!File.Exists(data_path + data_file_name))
        {
            //make data file

            FileStream temp = File.Create(data_path + data_file_name);

            Debug.Log("data_json created");

            play_data = new data_();

            temp.Close();

            save();
        }
        else //else data file exist
        {

            data_string_temp = File.ReadAllText(data_path + data_file_name);
            play_data = JsonUtility.FromJson<data_>(data_string_temp);
            Debug.Log(data_string_temp);
            Debug.Log(play_data);
        }
    }


    //��������� �ڵ����� �����ϱ�
    private void OnApplicationQuit()
    {

        save();
    }


}




using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Camera myCamera;
    public Transform Camera1;
    public Transform Camera2;
    public float CameraSpeed;
    public bool isActivated;

    public GameObject Dialog;

    private float t = 0f;
    
    void Start()
    {
        isActivated = false;
        
    }
    void Update()
    {

       

    }

    //��������� �Ѿ��
    public void MoveCupScene()
    {
        // �̵��� ��ġ�� Ȯ���ϰ� �غ� �Ǹ� ī�޶� �̵�
        myCamera.transform.position = Camera2.position;
        myCamera.transform.rotation = Camera2.rotation;
        myCamera.orthographicSize = Camera2.GetComponent<Camera>().orthographicSize;
        isActivated = false; // ���� ��� ����
        Dialog.SetActive(false);
    }
}

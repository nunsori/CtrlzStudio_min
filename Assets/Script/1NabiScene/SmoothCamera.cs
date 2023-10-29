using UnityEngine;
using UnityEngine.Rendering;

public class SmoothCamera : MonoBehaviour
{
    public Camera myCamera;
    public Transform Camera1;
    public Transform Camera2;
    public float CameraSpeed;
    public bool isActivated;

    private Vector3 camera_init_pos;
    private Quaternion camera_init_rotation;

    public GameObject Dialog;

    public delegate void temp_fuc();

    private float t = 0f;


    [Header("volume_profile")]
    public Volume volume;
    public VolumeProfile[] volume_profile;


    private void Awake()
    {
        camera_init_pos = gameObject.transform.position;
        camera_init_rotation = gameObject.transform.rotation;
        Camera1 = gameObject.transform;
    }

    void Start()
    {
        isActivated = false;
        
    }
    void Update()
    {


    }

    //음료씬으로 넘어가기
    public void MoveCupScene()
    {
        volume.profile = volume_profile[1];

        // 이동할 위치를 확인하고 준비가 되면 카메라 이동
        myCamera.transform.position = Camera2.position;
        myCamera.transform.rotation = Camera2.rotation;
        myCamera.orthographicSize = Camera2.GetComponent<Camera>().orthographicSize;
        isActivated = false; // 보간 계산 중지
        Dialog.SetActive(false);
    }

    public void MoveMainScene()
    {
        volume.profile = volume_profile[0];

        myCamera.transform.position = camera_init_pos;
        myCamera.transform.rotation = camera_init_rotation;

    }
}

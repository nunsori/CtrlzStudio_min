using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_controller : MonoBehaviour
{
    private Animator char_animator;
    private SkinnedMeshRenderer char_renderer;

    private Material[] temp_material;

    [Header("캐릭터 얼굴 materail 넣어놓기")]
    public Material[] character_face_materal;
    [Header("캐릭터 Skinned Mesh Renderer에서 캐릭터 얼굴부분에 해당하는 material의 인덱스")]
    public int character_face_material_index = 0;

    public static string[] char_motion_name = { "wait_vmd", "drink_vmd", "nod the head_vmd", "read_vmd", "shake hand_vmd", "sigh_vmd", "worry_vmd", "optimization" };

    // Start is called before the first frame update
    void Start()
    {
        char_animator = gameObject.GetComponent<Animator>();
        char_renderer = gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();

        change_charcter_face(1);

        //Debug.Log(character_materials[0].GetTexturePropertyNames().Length);
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(character_materials[0].GetTexturePropertyNames());
    }

    public void change_charcter_face(int index)
    {
        temp_material = char_renderer.materials;

        temp_material[character_face_material_index] = character_face_materal[index];
        //char_renderer.materials[character_face_material_index] = character_face_materal[index];
        char_renderer.materials = temp_material;
        Debug.Log("changed_character_face");
    }

    public void change_motion(string state_name)
    {
        if (char_animator.GetCurrentAnimatorStateInfo(0).IsName(state_name)) return;


        char_animator.Play(state_name);
    }

}

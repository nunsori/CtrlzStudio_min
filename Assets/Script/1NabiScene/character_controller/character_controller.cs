using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_controller : MonoBehaviour
{
    private Animator char_animator;
    private SkinnedMeshRenderer char_renderer;

    private Material[] temp_material;

    [Header("ĳ���� �� materail �־����")]
    public Material[] character_face_materal;
    [Header("ĳ���� Skinned Mesh Renderer���� ĳ���� �󱼺κп� �ش��ϴ� material�� �ε���")]
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

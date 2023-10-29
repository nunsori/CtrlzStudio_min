using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    [SerializeField]
    private string[] animation_clip_name;

    //public AnimationClip animation1;
    //public AnimationClip animation2;
    private Animation animationComponent;

    public Animator animator;


    private int clickCount = 0;

    private void Start()
    {
        //animationComponent = GetComponent<Animation>();
        //animationComponent.Play(animation_clip_name[0]);
        change_animation_state(animator, animation_clip_name[0]);
    }

    public void OnButtonClick()
    {
        clickCount++;

        if (clickCount % 2 == 1)
        {
            change_animation_state(animator, animation_clip_name[1]);
        }
        else
        {
            change_animation_state(animator, animation_clip_name[2]);
        }

        Debug.Log("btn_clicked");
    }

    private void PlayAnimation(AnimationClip animationClip)
    {
        animationComponent.clip = animationClip;
        animationComponent.Play();
    }


    public void change_animation_state(Animator animator, string state_name)
    {
        // stop from interrupting by same animation
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(state_name)) return;

        //animation play
        animator.Play(state_name);

    }
}

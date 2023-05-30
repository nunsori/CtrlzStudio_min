using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public AnimationClip animation1;
    public AnimationClip animation2;
    private Animation animationComponent;
    private int clickCount = 0;

    private void Start()
    {
        animationComponent = GetComponent<Animation>();
    }

    public void OnButtonClick()
    {
        clickCount++;

        if (clickCount % 2 == 1)
        {
            PlayAnimation(animation1);
        }
        else
        {
            PlayAnimation(animation2);
        }
    }

    private void PlayAnimation(AnimationClip animationClip)
    {
        animationComponent.clip = animationClip;
        animationComponent.Play();
    }
}

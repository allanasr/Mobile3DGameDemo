using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;
    public List<AnimatorSetup> animatorSetups;

    public enum AnimationType
    {
        IDLE,
        RUN,
        DEAD
    }

    public void SetTrigger(AnimationType type, float currentSpeedScale = 1f)
    {
        foreach(var animation in animatorSetups)
        {
            if(animation.type == type)
            {
                animator.SetTrigger(animation.trigger);
                animator.speed = animation.speed * currentSpeedScale;
                break;
            }
        }
    }
}

[System.Serializable]
public class AnimatorSetup
{
    public AnimationManager.AnimationType type;
    public string trigger;
    public float speed;
}

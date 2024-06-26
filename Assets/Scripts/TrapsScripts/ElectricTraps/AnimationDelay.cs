using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDelay : MonoBehaviour
{

    public Animator animator;
    public string triggerName;
    public float delay = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayAnimationWithDelay());
    }

    // Update is called once per frame
   
    IEnumerator PlayAnimationWithDelay()
    {
        while(true)
        {
            animator.SetTrigger(triggerName);
            // get the current time of the animation + delay
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + delay);
        }
    }

}

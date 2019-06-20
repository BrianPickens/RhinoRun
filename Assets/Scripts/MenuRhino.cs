using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRhino : MonoBehaviour
{

    [SerializeField]
    private Animator myAnimator = null;

    [SerializeField]
    private float timeBetweenAnimations = 7f;

    private float currentTime = 0f;

    private void Start()
    {
        currentTime = timeBetweenAnimations;   
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            PlayRandomAnimation();
            currentTime = timeBetweenAnimations;
        }
    }

    private void PlayRandomAnimation()
    {
        int animation = Random.Range(0, 4);

        switch (animation)
        {
            case 0:
                myAnimator.SetTrigger("ThumbsUp");
                break;

            case 1:
                myAnimator.SetTrigger("Shake");
                break;

            case 2:
                myAnimator.SetTrigger("Flex");
                break;

            case 3:
                myAnimator.SetTrigger("Buck");
                break;

            default:
                myAnimator.SetTrigger("Shake");
                break;
        }
    }

}

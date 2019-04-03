using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardedAdResult : MonoBehaviour
{

    [SerializeField]
    private Text infoText;

    [SerializeField]
    private Animator myAnimator;

    [SerializeField]
    private AudioClip rewardGivenSound;

    private bool rewardGiven;

    public void SetInfo(string _text, bool _rewardGiven)
    {
        infoText.text = _text;
        rewardGiven = _rewardGiven;
        gameObject.SetActive(true);
    }

    public void CloseRewardedAdResult()
    {
        gameObject.SetActive(false);
    }

    public void AcceptButton()
    {
        myAnimator.SetTrigger("Exit");

        if (rewardGiven)
        {
            PlaySound(rewardGivenSound);
        }
        else
        {
            PlayClickSound();
        }

    }

    private void PlaySound(AudioClip _clip)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySoundEffect(_clip);
        }
    }

    private void PlayClickSound()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayClickSound();
        }
    }
}

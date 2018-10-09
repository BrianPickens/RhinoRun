using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    [SerializeField]
    private ObjectPooler soundObjectPooler;

    [SerializeField]
    private AudioSource backgroundMusicSource;

    [SerializeField]
    private float minPitch;

    [SerializeField]
    private float maxPitch;

    private IEnumerator backgroundMusicFadeOutRoutine;
    private IEnumerator backgroundMusicFadeInRoutine;
    private IEnumerator backgroundMusicTransitionRoutine;

    private bool initialized;
    public bool Initialized
    {
        get { return initialized; }
    }

    public void Initialize()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        initialized = true;
    }

    public void PlaySoundEffect(AudioClip _clip)
    {
        GameObject soundObj = soundObjectPooler.GetPooledObject();
        AudioSource soundSource = soundObj.GetComponent<AudioSource>();
        soundSource.clip = _clip;
        soundSource.pitch = Random.Range(minPitch, maxPitch);
        soundObj.SetActive(true);
        soundSource.Play();
        StartCoroutine(ResetSoundObject(soundObj, _clip.length));
    }

    public void FadeOutBackgroundMusic(float _fadeSpeed)
    {
        if (backgroundMusicFadeOutRoutine != null)
        {
            StopCoroutine(backgroundMusicFadeOutRoutine);
        }
        backgroundMusicFadeOutRoutine = FadeRoutine(0, _fadeSpeed);
        StartCoroutine(backgroundMusicFadeOutRoutine);
    }

    public void FadeInBackgroundMusic(float _fadeSpeed)
    {
        if (backgroundMusicFadeInRoutine != null)
        {
            StopCoroutine(backgroundMusicFadeInRoutine);
        }
        backgroundMusicFadeInRoutine = FadeRoutine(1, _fadeSpeed);
        StartCoroutine(backgroundMusicFadeInRoutine);
    }

    public void ChangeBackgroundMusic(AudioClip _clip)
    {
        if (backgroundMusicTransitionRoutine != null)
        {
            StopCoroutine(backgroundMusicTransitionRoutine);
        }

        if (backgroundMusicFadeInRoutine != null)
        {
            StopCoroutine(backgroundMusicFadeInRoutine);
        }

        if (backgroundMusicFadeOutRoutine != null)
        {
            StopCoroutine(backgroundMusicFadeOutRoutine);
        }

        backgroundMusicTransitionRoutine = ChangeBackgroundFadeRoutine(_clip);
        StartCoroutine(backgroundMusicTransitionRoutine);
    }

    private IEnumerator ResetSoundObject(GameObject _soundObject, float _delay)
    {
        yield return new WaitForSeconds(_delay);
        _soundObject.SetActive(false);
    }

    private IEnumerator FadeRoutine(float _targetVolume, float _speed)
    {
        while (Mathf.Abs(backgroundMusicSource.volume - _targetVolume) > 0.01f)
        {
            backgroundMusicSource.volume = Mathf.MoveTowards(backgroundMusicSource.volume, _targetVolume, Time.deltaTime * _speed);
            yield return null;
        }
        backgroundMusicSource.volume = _targetVolume;
    }

    private IEnumerator ChangeBackgroundFadeRoutine(AudioClip _clip)
    {
        backgroundMusicFadeInRoutine = FadeRoutine(0, 1f);
        backgroundMusicFadeOutRoutine = FadeRoutine(1f, 1f);
        yield return StartCoroutine(backgroundMusicFadeOutRoutine);
        backgroundMusicSource.Stop();
        backgroundMusicSource.clip = _clip;
        backgroundMusicSource.volume = 0;
        backgroundMusicSource.Play();
        yield return StartCoroutine(backgroundMusicFadeInRoutine);

    }





}

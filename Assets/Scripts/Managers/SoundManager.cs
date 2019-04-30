using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;
    public static SoundManager Instance
    {
        get { return instance; }
    }

    [SerializeField]
    private ObjectPooler soundObjectPooler = null;

    [SerializeField]
    private AudioSource backgroundMusicSource = null;

    [SerializeField]
    private float minPitch = 0f;

    [SerializeField]
    private float maxPitch = 0f;

    [SerializeField]
    private AudioClip clickSound = null;

    private bool musicOn;
    public bool MusicOn
    {
        get { return musicOn; }
    }
    private bool soundEffectsOn;
    public bool SoundEffectsOn
    {
        get { return soundEffectsOn; }
    }

    private const string musicString = "music";
    private const string soundEffectsString = "soundEffects";

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
        

        if (PlayerPrefs.HasKey(musicString))
        {
            musicOn = PlayerPrefs.GetInt(musicString) == 1 ? true : false;
        }
        else
        {
            musicOn = true;
        }

        if (PlayerPrefs.HasKey(soundEffectsString))
        {
            soundEffectsOn = PlayerPrefs.GetInt(soundEffectsString) == 1 ? true : false;
        }
        else
        {
            soundEffectsOn = true;
        }

        initialized = true;
    }

    public void UpdateMusicPreference(bool _preference)
    {
        musicOn = _preference;
        PlayerPrefs.SetInt(musicString, _preference ? 1 : 0);
        if (!_preference)
        {
            backgroundMusicSource.Stop();
        }
        else
        {
            backgroundMusicSource.Play();
        }
    }

    public void UpdateSoundEffectPreference(bool _preference)
    {
        soundEffectsOn = _preference;
        PlayerPrefs.SetInt(soundEffectsString, _preference ? 1 : 0);
    }

    public void PlaySoundEffect(AudioClip _clip)
    {
        if (soundEffectsOn)
        {
            GameObject soundObj = soundObjectPooler.GetPooledObject();
            AudioSource soundSource = soundObj.GetComponent<AudioSource>();
            soundSource.clip = _clip;
            soundSource.pitch = Random.Range(minPitch, maxPitch);
            soundObj.SetActive(true);
            soundSource.Play();
            StartCoroutine(ResetSoundObject(soundObj, _clip.length));
        }
    }

    public void PlaySoundNoPitchShift(AudioClip _clip, float _volume = 1)
    {
        if (soundEffectsOn)
        {
            GameObject soundObj = soundObjectPooler.GetPooledObject();
            AudioSource soundSource = soundObj.GetComponent<AudioSource>();
            soundSource.clip = _clip;
            soundSource.volume = _volume;
            soundObj.SetActive(true);
            soundSource.Play();
            StartCoroutine(ResetSoundObject(soundObj, _clip.length));
        }
    }

    public void PlayClickSound()
    {
        PlaySoundEffect(clickSound);
    }

    public void FadeOutBackgroundMusic(float _fadeSpeed)
    {
        if (musicOn)
        {
            StopCoroutines();
            backgroundMusicFadeOutRoutine = FadeRoutine(0, _fadeSpeed);
            StartCoroutine(backgroundMusicFadeOutRoutine);
        }
    }

    public void FadeInBackgroundMusic(float _fadeSpeed)
    {
        if (musicOn)
        {
            StopCoroutines();
            backgroundMusicFadeInRoutine = FadeRoutine(1, _fadeSpeed);
            StartCoroutine(backgroundMusicFadeInRoutine);
        }
    }

    public void ChangeBackgroundMusic(AudioClip _clip)
    {
        if (musicOn)
        {
            if (backgroundMusicSource.clip != _clip)
            {
                StopCoroutines();
                backgroundMusicTransitionRoutine = ChangeBackgroundFadeRoutine(_clip);
                StartCoroutine(backgroundMusicTransitionRoutine);
            }
        }
        else
        {
            backgroundMusicSource.clip = _clip;
        }
    }

    private void StopCoroutines()
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
        backgroundMusicFadeInRoutine = FadeRoutine(1f, 1f);
        backgroundMusicFadeOutRoutine = FadeRoutine(0, 1f);
        yield return StartCoroutine(backgroundMusicFadeOutRoutine);
        backgroundMusicSource.Stop();
        backgroundMusicSource.clip = _clip;
        backgroundMusicSource.volume = 0;
        backgroundMusicSource.Play();
        yield return StartCoroutine(backgroundMusicFadeInRoutine);

    }





}

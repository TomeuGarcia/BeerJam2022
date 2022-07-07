using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicPlay : MonoBehaviour
{
    public AudioSource _audioSource;
    private GameObject[] other;
    private bool NotFirst = false;
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip canonMusic;
    private float baseMusicVolume;
    private int playersUsingCanon;

    private void Awake()
    {

        other = GameObject.FindGameObjectsWithTag("Music");

        foreach (GameObject oneOther in other)
        {
            if (oneOther.scene.buildIndex == -1)
            {
                NotFirst = true;
            }
        }

        if (NotFirst == true)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();

        baseMusicVolume = _audioSource.volume;
    }

    private void OnEnable()
    {
        CannonUse.OnPlayerMount += PlayerEntersCanon;
        CannonUse.OnPlayerDismount += PlayerExitsCanon;
    }

    private void OnDisable()
    {
        CannonUse.OnPlayerMount -= PlayerEntersCanon;
        CannonUse.OnPlayerDismount -= PlayerExitsCanon;
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

    public void TransitionToGameMusic()
    {
        _audioSource.DOFade(0f, 1f).OnComplete(() => PlayMusic(gameMusic));
    }

    public void TransitionToMainMenuMusic()
    {
        _audioSource.DOFade(0f, 1f).OnComplete(() => PlayMusic(menuMusic));
    }

    public void PauseMusicVolume()
    {
        _audioSource.DOFade(0.1f, 1f);
    }

    public void ResumeMusicVolume()
    {
        _audioSource.DOFade(baseMusicVolume, 1f);
    }

    private void PlayMusic(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.DOFade(baseMusicVolume, 1f);
        _audioSource.Play();
    }

    public void TransitionToCanonMusic()
    {
        _audioSource.DOFade(0f, 1f).OnComplete(() => PlayMusic(canonMusic));
    }

    private void PlayerEntersCanon()
    {
        if(++playersUsingCanon == 2)
        {
            TransitionToCanonMusic();
        }
    }
    private void PlayerExitsCanon()
    {
        if (--playersUsingCanon == 1)
        {
            TransitionToGameMusic();
        }
    }
}

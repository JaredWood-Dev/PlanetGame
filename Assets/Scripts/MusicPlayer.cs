using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public List<AudioClip> Playlist = new List<AudioClip>();
    public bool shuffle = false;
    public GameObject target;
    public bool followTarget = false;

    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
        _audioSource.clip = Playlist[0];
        
        _audioSource.Play();
    }

    void Update()
    {
        if (followTarget)
        {
            transform.position = target.transform.position;
        }
    }

    public void QueueSong(AudioClip clip, bool immediately = false)
    {
        if (immediately)
        {
            _audioSource.clip = clip;
        }
        else
        {
            Playlist.Add(clip);
        }
    }

    public void StopPlaying()
    {
        _audioSource.Stop();
    }

    public void StartPlaying()
    {
        _audioSource.Play();
    }
}

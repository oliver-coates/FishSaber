using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    public BeatVisualiser beatVisualiser;

    public AudioClip song;

    public float firstBeatOffset;

    public float songBpm;

    public float songPositonInBeats = -1f;

    public float secPerBeat;

    float songPosition;

    float dspSongTime;

    public bool debugPlaySongDelayed;

    public bool isPlaying;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        secPerBeat = 60f / songBpm;

          
        if (debugPlaySongDelayed)
        {
            StartCoroutine(playDelayed(1.2f));
        }
        
    }

    public void PlaySong()
    {
        audioSource.PlayOneShot(song);
        isPlaying = true;
        beatVisualiser.StartVisualisation();
        dspSongTime = (float)AudioSettings.dspTime;
    }

    public void StopPlayingSong()
    {
        songPositonInBeats = -1f;
        isPlaying = false;
        audioSource.Stop();
    }

    private void Update() {
        if (isPlaying)
        {
            songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

            songPositonInBeats = songPosition / secPerBeat;
        }
        
    }

    private IEnumerator playDelayed(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        PlaySong();
    }
}

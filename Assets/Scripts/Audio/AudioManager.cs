using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Chords")]
    [SerializeField] Chords chords = null;
    [SerializeField] AudioSource chordAudio = null;
    [SerializeField] AudioSource distordedChordAudio = null;

    [Header("Ambiences")]
    [SerializeField] AudioAmbience[] ambiences = null;
    public Dictionary<AudioAmbience.Type, AudioClip> ambiencesDic = null;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

        ambiencesDic = new Dictionary<AudioAmbience.Type, AudioClip>();
        foreach (AudioAmbience ambience in ambiences)
        {
            ambiencesDic.Add(ambience.type, ambience.clip);
        }
    }

    public void PlayChord(int id, float volume = 1)
    {
        chordAudio.PlayOneShot(chords.clips[Mathf.Abs(id) % chords.clips.Length], volume);
    }

    public void PlayDistordedChord(int id, float volume =1)
    {
        distordedChordAudio.PlayOneShot(chords.clips[Mathf.Abs(id) % chords.clips.Length], volume);
    }
}

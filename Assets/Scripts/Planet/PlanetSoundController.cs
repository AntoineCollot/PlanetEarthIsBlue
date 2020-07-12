using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSoundController : MonoBehaviour
{
     new AudioSource audio;

    [SerializeField] float lerpVolumeInTime = 1;
    static int planetSoundCount = 0;
    int soundId;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        PlanetGravity planet = GetComponent<PlanetGravity>();
        audio.minDistance = planet.radius;
        audio.maxDistance = planet.gravityRadius + 3;

        //Delay the start on frame to let the other components initialize
        Invoke("SetUpAmbienceSound", 0.01f);

        StartCoroutine(LerpVolumeIn());

        //Play the chord sound
        soundId = planetSoundCount;
        planetSoundCount++;
        AudioManager.Instance.PlayChord(soundId);
    }

    void SetUpAmbienceSound()
    {
        //Find the type of ambience based on the color
        Color c = GetComponent<PlanetRendering>().mainColor;
        AudioAmbience.Type ambienceType = AudioAmbience.Type.Ice;

        //If white
        if (c.grayscale > 0.8f)
        {
            ambienceType = AudioAmbience.Type.Blizzard;
        }
        else if(c.grayscale < 0.2f)
        {
            ambienceType = AudioAmbience.Type.Rock;
        }
        //if mostly red
        else if(c.r > c.g + c.b)
        {
            ambienceType = AudioAmbience.Type.Fire;
        }
        //if mostly green
        else if (c.g > c.r +c.b)
        {
            ambienceType = AudioAmbience.Type.Forest;
        }
        //if mostly blue
        else if (c.b > c.r + c.g)
        {
            ambienceType = AudioAmbience.Type.Water;
        }

        audio.clip = AudioManager.Instance.ambiencesDic[ambienceType];
        audio.Play();
    }

    IEnumerator LerpVolumeIn()
    {
        float t= 0;
        float maxVolume = audio.volume;
        while(t<1)
        {
            t += Time.deltaTime / lerpVolumeInTime;

            audio.volume = Mathf.Lerp(0, maxVolume,t);

            yield return null;
        }
    }

    private void OnDestroy()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayDistordedChord(soundId, 0.5f);
    }
}

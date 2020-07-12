using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AudioAmbience", order = 1, fileName = "AudioAmbience")]
public class AudioAmbience : ScriptableObject
{
    public enum Type { Fire, Ice, Water, Blizzard, Forest,Rock}
    public Type type;
    public AudioClip clip;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Chords", order = 2, fileName = "Chords")]
public class Chords : ScriptableObject
{
    public AudioClip[] clips = null;
}

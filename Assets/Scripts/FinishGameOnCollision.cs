using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGameOnCollision : MonoBehaviour
{
    [SerializeField] SceneLoader.SceneName sceneToLoad = SceneLoader.SceneName.Level_Earth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneLoader.Instance.GetComponent<AudioSource>().Play();
        SceneLoader.Instance.LoadScene(sceneToLoad);
    }
}

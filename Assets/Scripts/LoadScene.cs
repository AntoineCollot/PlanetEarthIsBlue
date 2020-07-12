using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public SceneLoader.SceneName scene;

    public void Load()
    {
        SceneLoader.Instance.LoadScene(scene);
    }
}

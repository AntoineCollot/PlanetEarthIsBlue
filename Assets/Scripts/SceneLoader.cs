using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    PostProcessDrawColor transition;
    [SerializeField] float transistionTime = 0.5f;
    bool isLoading = false;

    public enum SceneName { Menu, Level_Earth, Level_Relax, Level_Tutorial }

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance!=null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;

        transition = FindObjectOfType<PostProcessDrawColor>();
    }

    public void LoadScene(SceneName scene)
    {
        if(!isLoading)
            StartCoroutine(LoadSceneWithTransition(scene));
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isLoading = false;
        transition = FindObjectOfType<PostProcessDrawColor>();

        StartCoroutine(AnimTransition(1, 0));
    }

    IEnumerator LoadSceneWithTransition(SceneName scene)
    {
        isLoading = true;

        yield return StartCoroutine(AnimTransition(0, 1));

        SceneManager.LoadScene(scene.ToString());
    }

    IEnumerator AnimTransition(float start, float end)
    {
        float t = 0;

        while(t<1)
        {
            t += Time.deltaTime / transistionTime;

            transition.intensity = Mathf.Lerp(start, end, t);

            yield return null;
        }
    }
}

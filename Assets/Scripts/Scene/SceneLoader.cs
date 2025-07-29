using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SceneReference
{
    [SerializeField] private string sceneName;
    public string SceneName { get => sceneName; set => sceneName = value; }
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
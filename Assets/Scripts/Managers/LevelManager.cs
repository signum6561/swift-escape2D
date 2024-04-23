using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    private int currentLevel;
    private GameObject playerPrefab;
    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCo(sceneName));
    }
    public void LoadLevelScene(int levelIndex)
    {
        string sceneName = "Level" + levelIndex.ToString();
        currentLevel = levelIndex;
        StartCoroutine(LoadSceneCo(sceneName));
    }
    public void ReloadScene()
    {
        StartCoroutine(LoadSceneCo("Level" + currentLevel));
    }
    public void NextLevel()
    {
        currentLevel++;
        LoadLevelScene(currentLevel);
    }
    public GameObject PlayerPrefab
    {
        get { return playerPrefab; }
        set
        {
            if (value != null)
            {
                playerPrefab = value;
            }
        }
    }
    private IEnumerator LoadSceneCo(string sceneName)
    {
        Transitor.Instance.StartTransition();
        Time.timeScale = 1f;
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        do
        {
            yield return new WaitForSeconds(0.1f);
        }
        while (scene.progress < 0.9f);
        yield return new WaitForSeconds(1f);
        Transitor.Instance.EndTransition();
        scene.allowSceneActivation = true;
        GameManager.Instance.StartEnterLevel();
    }
}

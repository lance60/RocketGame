using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject landingpad;

    [SerializeField]
    private string nextScene;

    [SerializeField]
    private float sceneLoadDelay = 2f;

    private Rocket rocketComponent;
    private Landingpad landingpadComponent;
    private string currentScene;

    private void Start()
    {
        rocketComponent = player.GetComponent<Rocket>();
        landingpadComponent = landingpad.GetComponent<Landingpad>();
        currentScene = SceneManager.GetActiveScene().name;
    }
    // Update is called once per frame
    void Update()
    {
        if(landingpadComponent.isSuccessful())
        {
            StartCoroutine(LoadSceneDelayed(nextScene, sceneLoadDelay));
        }
        else if(!rocketComponent.isAlive())
        {
            StartCoroutine(LoadSceneDelayed(currentScene, sceneLoadDelay));
        }
    }

    IEnumerator LoadSceneDelayed(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}

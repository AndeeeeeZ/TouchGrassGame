using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private string nextScene; 

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single); 
    }

    public void LoadSceneWithName(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single); 
    }
}

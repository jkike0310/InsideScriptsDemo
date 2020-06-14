using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject LoadingScreen;
    public Slider slider;
    public void LoadLevel(int SceneIndex)
    {
        LoadingScreen.SetActive(true);
        
        StartCoroutine(LoadAsynchronus(SceneIndex));
    }

   
    IEnumerator LoadAsynchronus(int SceneIndex)
    {
        yield return new WaitForSeconds(2f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneIndex);
               
        while (!operation.isDone)
        {
            //float progress = Mathf.Clamp01(operation.progress/0.9f);
            slider.value = operation.progress;
            yield return null;
        }

        
    }
   
}

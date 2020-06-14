using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AudioButton : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audio;
   
    //[SerializeField] AudioClip[] sonidos;
    
    [SerializeField] AudioClip butonIn;
    [SerializeField] AudioClip butonOut;
    void Start()
    {
        audio = GetComponent<AudioSource>();
       
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PlayHoverIn()
    {
        audio.PlayOneShot(butonIn);
    }

    public void PlayHoverOut()
    {
        audio.PlayOneShot(butonOut);
    }

    public void ClickPlay()
    {
        SceneManager.LoadScene("Level1");
    }

    

    public void ClickExit()
    {
        Application.Quit();
    }

    
    
}

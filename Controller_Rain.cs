using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Controller_Rain : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<RainCameraController> rainControllers;
    public GameObject rainObject;
    public GameObject audioObject;
    [SerializeField] GameObject flashButton;
    [SerializeField] public GameObject botonPick;
    [SerializeField] GameObject DialogoCuadro;
    AmbientalAudioManager audioManager;
    AudioSource audio;
    ParticleSystem rain;

    enum PlayMode
    {
        //Normal = 0,
        Rain = 0,
    };

    PlayMode playMode = 0;

    void Start()
    {
        audio = audioObject.GetComponent<AudioSource>();
        audioManager = FindObjectOfType<AmbientalAudioManager>();
        rainControllers[0].Play();
        rain = rainObject.GetComponent<ParticleSystem>();
    }

    private void StopAll() {
        foreach (var con in rainControllers)
        {
            con.StopImmidiate();
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            rainControllers[0].Play();
            audio.Stop();
            rain.Play(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            rainControllers[0].Stop();
            rain.Stop(true);
            audio.clip = audioManager.audioCastle;
            audio.Play();
            StartCoroutine(ShowFlashLButton());
        }
    }

    IEnumerator ShowFlashLButton()
    {
        yield return new WaitForSeconds(5f);
        flashButton.SetActive(true);
        yield return new WaitForSeconds(4f);
        flashButton.SetActive(false);
        Destroy(flashButton,1);
        yield return new WaitForSeconds(3f);
        botonPick.SetActive(true);
        yield return new WaitForSeconds(4f);
        botonPick.SetActive(false);
        yield return new WaitForSeconds(7f);
        DialogoCuadro.SetActive(true);
        yield return new WaitForSeconds(8f);
        DialogoCuadro.SetActive(false);
        Destroy(DialogoCuadro,2);
        
    }
}

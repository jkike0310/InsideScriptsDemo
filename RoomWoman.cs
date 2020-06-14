using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWoman : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject PostPNormal;
    [SerializeField] GameObject PostPWomanRoom;
    [SerializeField] GameObject DirectionalLightNormal;
    [SerializeField] GameObject DirectionalLightRoom;
    [SerializeField] GameObject AudioSource;
    AudioSource audio;
    AmbientalAudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AmbientalAudioManager>();
        audio = AudioSource.GetComponent<AudioSource>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            RenderSettings.fog = false;
            PostPNormal.SetActive(false);
            PostPWomanRoom.SetActive(true);
            DirectionalLightNormal.SetActive(false);
            DirectionalLightRoom.SetActive(true);
            audio.clip = audioManager.audioRoom;
            audio.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            RenderSettings.fog = true;
            PostPNormal.SetActive(true);
            PostPWomanRoom.SetActive(false);
            DirectionalLightNormal.SetActive(true);
            DirectionalLightRoom.SetActive(false);
            audio.clip = audioManager.audioCastle;
            audio.Play();
        }
    }
}

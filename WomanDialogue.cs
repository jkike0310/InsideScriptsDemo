using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanDialogue : MonoBehaviour
{
    [SerializeField] public GameObject DialogueWoman;
    [SerializeField] public GameObject AdvertKey;
    [SerializeField] public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject key;


    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            StartCoroutine(Dialogue());
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            StartCoroutine(Dialogue());
            StartCoroutine(Desplegar());
        }
    }


    IEnumerator Dialogue()
    {
        DialogueWoman.SetActive(true);
        yield return new WaitForSeconds(13f);
        Destroy(DialogueWoman, 1);
        yield return new WaitForSeconds(4f);
        AdvertKey.SetActive(true);
        yield return new WaitForSeconds(8f);
        Destroy(AdvertKey, 2);

    }

    IEnumerator Desplegar()
    {
        yield return new WaitForSeconds(1f);
        Enemy1.SetActive(true);
        Enemy2.SetActive(true);
        Enemy3.SetActive(true);
        key.SetActive(true);
        
    }
}

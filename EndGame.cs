using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pantallaEndGame;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player")) ;
        {
            StartCoroutine(EndGameScreen());
        }
    }

    IEnumerator EndGameScreen()
    {
        pantallaEndGame.SetActive(true);
        yield return new WaitForSeconds(17f);
        SceneManager.LoadScene(0);
    }


}
        

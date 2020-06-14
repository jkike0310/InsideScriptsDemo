using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamClick : MonoBehaviour
{
    [SerializeField] GameObject credits;
    [SerializeField] GameObject canvas;
    Animator anim;
    // Start is called before the first frame update

    void Start()
    {
        anim = credits.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickTeam()
    {
        canvas.SetActive(true);
        anim.SetBool("CreditsTransition", true);
        StartCoroutine(ConsumeCredits());

    }

    IEnumerator ConsumeCredits()
    {
        if (Input.anyKeyDown)
        {
            anim.SetBool("CreditsTransition", false);
            canvas.SetActive(false);
            //anim.SetBool("CreditsTransition", false);

        }
        else
        {
           yield return new WaitForSeconds(12.5f);
           canvas.SetActive(false);
           anim.SetBool("CreditsTransition", false);
        }
        
    }

}

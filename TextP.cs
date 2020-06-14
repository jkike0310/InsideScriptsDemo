using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class TextP : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textToUse;
    [SerializeField] public GameObject image;
    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOnStart = false;
    [SerializeField] private float timeMultiplier;
    private bool FadeIncomplete = false;
    bool ciclo = true;

    private void Start()
    {

       
       StartCoroutine(IntroFade(textToUse));
    }

    private void Update()
    {
        
    }

    private IEnumerator FadeInText(float timeSpeed, TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime * timeSpeed));
            yield return null;
        }
    }
    private IEnumerator FadeOutText(float timeSpeed, TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * timeSpeed));
            yield return null;
        }
    }
  

    private IEnumerator IntroFade(TextMeshProUGUI textToUse)
    {
        yield return new WaitForSeconds(3.0f);
        textToUse.enabled = true;
        while (ciclo) {
            yield return StartCoroutine(FadeInText(1f, textToUse));
            
            yield return StartCoroutine(FadeOutText(1f, textToUse));
        }
        
       // yield return new WaitForSeconds(2f);
        //End of transition, do some extra stuff!!
    }

    public void HideText()
    {
        textToUse.enabled = false;
    }
}

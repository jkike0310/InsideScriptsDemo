using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stadistics : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject post;
    PostProcessVolume postProcess;
    [SerializeField] int fear;
    [SerializeField] int life;
    [SerializeField] AudioClip susto;
    [SerializeField] AudioClip alivio;
    [SerializeField] public GameObject FearBarObject;
    [SerializeField] public GameObject LifeBarObject;
    [SerializeField] public GameObject GameOverScreen;

    Slider FearBar;
    Slider LiferBar;

    AudioSource audio;
    float fearAmount;
    float lifeAmount;
    
    ChromaticAberration chromaticLayer;
    LensDistortion lensLayer;
    DepthOfField depthLayer;
    public int contador;
    bool isInvulnerable;
    public bool haveKey;

    float chromatic;
    float lens;
    int temp;
    int temp2;
    bool isDead;
    public bool persecution;
    void Start()
    {
        //
        haveKey = false;
        chromaticLayer = ScriptableObject.CreateInstance<ChromaticAberration>();
        lensLayer = ScriptableObject.CreateInstance<LensDistortion>();
        depthLayer = ScriptableObject.CreateInstance<DepthOfField>();
        postProcess = post.GetComponent<PostProcessVolume>();
        audio = GetComponent<AudioSource>();

        FearBar = FearBarObject.GetComponent<Slider>();
        LiferBar = LifeBarObject.GetComponent<Slider>();

        persecution = false;
        depthLayer.active = false;        
        postProcess.profile.TryGetSettings (out chromaticLayer);
        postProcess.profile.TryGetSettings(out lensLayer);
        postProcess.profile.TryGetSettings(out depthLayer);
        contador = 0;
        isDead = false;
        life = 100;
        fear = 0;
        temp = 0;
        temp2 = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(chromaticLayer.intensity.value);
        chromatic = (fear / 100);
        lens = (0 - fear) / 2;
        chromaticLayer.intensity.value = (chromatic);
        lensLayer.intensity.value = (lens);
        fearAmount = fear / 100;
        lifeAmount = life / 100;
        FearBar.value = (fearAmount);
        LiferBar.value = (lifeAmount);
    }
    void FixedUpdate()
    {
        //Debug.Log(chromaticLayer.intensity.value);
        
        
    }

    public void PlusFear(int adition)
    {
        if ((fear+adition) <= 100)
        {
            fear += adition;
            if (fear+adition > 80)
            {
                FearSound();
                depthLayer.active = true;
            }
        }
        if ((fear + adition) > 100)
        {
            FearSound();
            fear = 100;
            /*fear += adition;
            temp = fear - 100;
            fear -= temp;
            temp = 0;*/
            depthLayer.active = true;

        }

        
    }
    public void MinusFear(int rest)
    {
        if ((fear - rest) >= 0)
        {
            fear -= rest;
            if(fear-rest < 80)
            {
                PotionSound();
                depthLayer.active = false;
            }
        }
        if((fear - rest) < 0)
        {
            PotionSound();
            depthLayer.active = false;
            fear = 0;
            /*fear -= rest;
            temp = 0 - fear;            
            fear += temp;
            temp = 0;*/
        }
    }

    public void addLife(int health)
    {
        if ((life + health) <= 100)
        {
            life += health;
        }
        if ((life + health) > 100)
        {
            life = 100;
            /*life += health;
            temp2 = health - 100;
            life -= temp2;
            temp2 = 0;*/
        }
    }

    public void restLife(int damage)
    {
        if (!isInvulnerable)
        {
            life -= damage;
            if (life <= 0)
            {
                life = 0;
                isDead = true;
                StartCoroutine(GameOver());
            }
            StartCoroutine(Invulnerabilidad());
        }
        
    }

    

    IEnumerator Invulnerabilidad()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(3f);
        isInvulnerable = false;
    }

    private void FearSound()
    {
        //audio.clip = susto;
        audio.PlayOneShot(susto);
    }

    private void PotionSound()
    {
        audio.PlayOneShot(alivio);
    }

    IEnumerator GameOver()
    {
        GameOverScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        //Destroy(this.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}

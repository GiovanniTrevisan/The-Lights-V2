using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class VidaPlayer : MonoBehaviour
{

    public int StartHealth = 10;
    public int CurrentHealth;
    public bool PlayerisDead = false;
    public AudioClip DeadClip;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public AudioClip[] SonsDano;
    public GameObject FPSController;
    public int QtdeCura = 5;
    public float timeToHeal = 3f;
    public float TimeTic = 0.2f;
    float Timer;
    bool isHealing = false;

    bool damaged;
    AudioSource AudioSourcePlayer;


    void Awake()
    {
        CurrentHealth = StartHealth;
        AudioSourcePlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        damaged = false;
        Timer += Time.deltaTime;

        if (isHealing == false && CurrentHealth < StartHealth && Timer >= timeToHeal && CurrentHealth > 0)
        {
            isHealing = true;
            StartCoroutine(Heal());
        }
    }

    public void TakeDamage(int amount)
    {
        if (CurrentHealth > 0)
        {
            Timer = 0f;
            isHealing = false;
            damageImage.color = flashColour;
            CurrentHealth = CurrentHealth - amount;

            int IndexSomDano = Random.Range(0, (SonsDano.Length));
            AudioSourcePlayer.PlayOneShot(SonsDano[IndexSomDano]);
        }

        if (CurrentHealth <= 0 && PlayerisDead == false)
        {
            Death();
        }
    }

    void Death()
    {
        PlayerisDead = true;

        FPSController.SetActive(false);

        GameOver.gameover = true;


        //Animacao de morte
        //Som de morte
        //Desabilitar movimento
        //Desabilitar armas
        //Mensagem de morte
    }

    public IEnumerator Heal()
    {
        do
        {
            CurrentHealth += QtdeCura;
            if (CurrentHealth >= StartHealth)
            {
                CurrentHealth = StartHealth;
                isHealing = false;
            }
            yield return new WaitForSeconds(TimeTic);
        } while (isHealing == true);
    }

    public void ShakeScreen()
    {
        CameraShaker.Instance.ShakeOnce(14f, 4f, .1f, 1f);
    }

}

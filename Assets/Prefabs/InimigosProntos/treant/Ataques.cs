using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataques : MonoBehaviour {

    public string[] Attacks;
    public float[] Range;
    public float[] timeBetweenAttacks;
    public int[] Dano;
    public bool[] PrecisaEstarNaFrente;
    public GameObject[] Projetil;
    public bool[] ShakeCamera;
    public float[] timer;
    public float tempo = 1f;
    public Transform PointProjetil;
    
    float timerGeneric;
    int IndexAttack;
    bool isAttacking;
    bool isFoward;

    Animator anim;
    GameObject player;
    VidaPlayer VidaPlayer;
    VidaInimigo VidaInimigo;
    //GameObject Camera;

    UnityEngine.AI.NavMeshAgent nav;
   
    void Start () {
        //Camera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        VidaPlayer = player.GetComponent<VidaPlayer>();
        anim = GetComponent<Animator>();
        VidaInimigo = GetComponent<VidaInimigo>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        isFoward = false;
        isAttacking = false;
    }
	
	void Update () {
        for (int i =0; i< timer.Length; i++)
        {
            timer[i] += Time.deltaTime;
        }
        timerGeneric += Time.deltaTime;

        if (isAttacking == false)
        {
            isAttacking = true;
            IndexAttack = Random.Range(0, (Attacks.Length));

            if (Attacks[IndexAttack] == "" || Attacks[IndexAttack] == null)
            {
                do
                {
                    IndexAttack = Random.Range(0, (Attacks.Length));
                } while (Attacks[IndexAttack] == "" || Attacks[IndexAttack] == null);
            }
            Atacar();
        }
    }

    void Atacar()
    {
        if ((PrecisaEstarNaFrente[IndexAttack] == true && isFoward == true) || PrecisaEstarNaFrente[IndexAttack] == false)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= Range[IndexAttack] && timerGeneric >= tempo && timer[IndexAttack] >= timeBetweenAttacks[IndexAttack] && VidaInimigo.CurrentHealth > 0 && VidaPlayer.CurrentHealth > 0)
            {
                timerGeneric = 0f;
                timer[IndexAttack] = 0f;
                anim.SetTrigger(Attacks[IndexAttack]);
                nav.enabled = false;
            }
            else
            {
                isAttacking = false;
            }
        }
        else
        {
            isAttacking = false;
        }
    }

    public void DarDano() //animation event
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= Range[IndexAttack])
        {
            if ((PrecisaEstarNaFrente[IndexAttack] == true && isFoward == true) || PrecisaEstarNaFrente[IndexAttack] == false)
            {
                VidaPlayer.TakeDamage(Dano[IndexAttack]);
                if (ShakeCamera[IndexAttack] == true)
                {
                    VidaPlayer.ShakeScreen();
                }
            }
        }
    }

    public void ReiniciarAttack() //animation event
    {
        isAttacking = false;
        nav.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isFoward = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isFoward = false;
        }
    }

    void InstanciarProjetil(int speedUp) //Animation Event
    {
        GameObject obj = Instantiate(Projetil[IndexAttack], PointProjetil.position, PointProjetil.rotation);
        Rigidbody objRigidBody = obj.GetComponent<Rigidbody>();
        objRigidBody.AddForce(transform.forward * 1000);
        objRigidBody.AddForce(transform.up * speedUp);
    }

}

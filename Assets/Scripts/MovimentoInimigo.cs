using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovimentoInimigo : MonoBehaviour {

    public float speedRotation = 1f;
    public float FatorSpeed = 0.02f;
    public float Maxspeed = 6;

    Transform player;
    VidaPlayer VidaPlayer;
    VidaInimigo VidaInimigo;
    NavMeshAgent nav;
    Animator anim;
    Rigidbody Rigidbody;
    GameObject GameController;
    RoundManager RoundManager;


    float velocidadeAnterior;

    void Start () {
        GameController = GameObject.FindGameObjectWithTag("GameController");
        RoundManager = GameController.GetComponent<RoundManager>();
        

        player = GameObject.FindGameObjectWithTag("Player").transform;
        VidaPlayer = player.GetComponent <VidaPlayer> ();
        VidaInimigo = GetComponent <VidaInimigo> ();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();

        float newSpeed = nav.speed + (FatorSpeed * RoundManager.CurrentRound);
        if (newSpeed > Maxspeed)
        {
            newSpeed = Maxspeed;
        }
        velocidadeAnterior = newSpeed;
        nav.speed = newSpeed;
       
    }

    void Update () {

        if (nav.enabled == true && VidaPlayer.CurrentHealth > 0 && VidaInimigo.CurrentHealth > 0)
        { 
            nav.SetDestination(player.position);
            var rotation = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speedRotation);

            float velocity = nav.velocity.sqrMagnitude;
            if (velocity > 0)
            {   
                anim.SetBool("Walk Forward", true);
            }
            else
            {
                anim.SetBool("Walk Forward", false);
            }            
        }
        else{
            anim.SetBool("Walk Forward", false);
        }

    }

    public void RunSpeed(float Speed)
    {
        nav.speed = Speed;
    }

    public void WalkSpeed()
    {
        nav.speed = velocidadeAnterior;
    }

}

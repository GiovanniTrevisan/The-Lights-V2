using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour {

    public float TimeBetweenAttacks = 1f;
    float timer = 0f;

    Animator anim;
    GameObject player;
    VidaPlayer VidaPlayer;
    VidaInimigo VidaInimigo;

    public class Attack
    {

        public string Name { get; set; }
        public int Damage { get; set; }
        public float TimeBetweenAttack { get; set; }
        public float Range { get; set; }
        public bool NeedBeForward { get; set; }

        public Attack(string name, int damage, float time, float range, bool forward)
        {
            Name = name;
            Damage = damage;
            TimeBetweenAttack = time;
            Range = range;
            NeedBeForward = forward;
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

    }
}

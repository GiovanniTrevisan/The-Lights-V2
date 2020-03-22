using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour {

    public int dano = 30;

    bool inRange = false;
    GameObject player;
    VidaPlayer VidaPlayer;

    void Start () {
        
        
    }
	
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        if (inRange == true)
        {
            
            VidaPlayer.TakeDamage(dano);
        }
        if (collision.gameObject.tag != "Treant")
        {
            Destroy(gameObject);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            VidaPlayer = player.GetComponent<VidaPlayer>();
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }

}

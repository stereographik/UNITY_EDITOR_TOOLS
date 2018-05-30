using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(AudioSource))]
public class ground_soundfx : MonoBehaviour {


    public AudioClip sample;
    public float min_impactforce;
    AudioSource soundplayer;

	// Use this for initialization
	void Start () {
        soundplayer = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude>min_impactforce) {
            soundplayer.PlayOneShot(sample);

        }

    }
    }

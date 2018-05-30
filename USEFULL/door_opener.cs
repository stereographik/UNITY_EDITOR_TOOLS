using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


[RequireComponent(typeof(AudioSource))]
public class door_opener : MonoBehaviour {


    public GameObject porte_droite;
    public GameObject porte_gauche;
    public float animation_length = 1.5f;
    public Vector3 rotation_values;
    bool etat = false;
    bool is_locked = false;
    public bool play_sound = false;
    public AudioClip door_sound_open;
    public AudioClip door_sound_close;

    // Use this for initialization
    void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {

		
	}

    public void switch_door() {
        if (!is_locked) {
            if (!etat) {
                porte_droite.transform.DOLocalRotate(rotation_values * -1, animation_length);
                porte_gauche.transform.DOLocalRotate(rotation_values, animation_length);
                if(play_sound){GetComponent<AudioSource>().PlayOneShot(door_sound_open)};
                etat = true;

            } else {
                porte_droite.transform.DOLocalRotate(Vector3.zero, animation_length);
                porte_gauche.transform.DOLocalRotate(Vector3.zero, animation_length);
                if(play_sound){GetComponent<AudioSource>().PlayOneShot(door_sound_close)};
                etat = false;
            }
        }
    }


}

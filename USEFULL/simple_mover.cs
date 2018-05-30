using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class simple_mover : MonoBehaviour {


    public Vector3 ouverture_value;
    public float animation_time = 1.0f;
    public bool etat = false;
    public Vector3 position_initiale;
    public AudioClip tiroir_sound_open;
    public AudioClip tiroir_sound_close;

    // Use this for initialization
    void Start () {
        position_initiale = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
  
  public void switch_anim(){
  if (etat)
                {                   
                    GetComponent<AudioSource>().PlayOneShot(tiroir_sound_open);
                   transform.DOMove(hit.collider.gameObject.transform.position + ouverture_value, te.animation_time);
                    etat = true;
                }
                else
                {
                    GetComponent<AudioSource>().PlayOneShot(tiroir_sound_open);
                    transform.DOMove(te.position_initiale, animation_time);
                    etat = false;
                }
  }
}

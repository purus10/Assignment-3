using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour 
{

    public AudioSource Musical;
    public AudioSource Bettle;
    public AudioSource Ambi2;


	// Use this for initialization
	void Awake () 
    {
        Musical.Play();
        Musical.volume = 0.5f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        Musical.Play();
        Musical.volume = 0.5f;
	}
}

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
        DontDestroyOnLoad(transform.gameObject);
        OnLevelWasLoaded(0);
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            Ambi2.Stop();
            Musical.Stop();
            Bettle.Play();
            Bettle.volume = 0.5f;
        }
        if (level == 1)
        {
            Ambi2.Stop();
            Bettle.Stop();
            Musical.Play();
            Musical.volume = 0.5f;
        }
        if (level == 0)
        {
            Musical.Stop();
            Bettle.Stop();
            Ambi2.Play();
            Ambi2.volume = 0.5f;
        }
    }
}

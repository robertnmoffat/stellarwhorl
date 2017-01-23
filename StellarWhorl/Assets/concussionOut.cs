using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class concussionOut : MonoBehaviour {

    public GameObject sphere1;
    public GameObject sphere2;
    public GameObject sphere3;
    const int DURATION = 60;
    int lifetime = 0;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (lifetime <= DURATION/2-8)
        {
            expand();
            lifetime++;
        }
        else if (lifetime > DURATION/2-8 && lifetime <= DURATION)
        {
            print("conditional");
            contract();
            lifetime++;
        }
        else if (lifetime > DURATION)
        {
            Destroy(gameObject);
        }
    }
    void expand()
    {
        sphere1.transform.localScale = new Vector3(sphere1.transform.localScale.x + 4f, sphere1.transform.localScale.y + 4f, sphere1.transform.localScale.z + 4f);
        sphere2.transform.localScale = new Vector3(sphere1.transform.localScale.x * 0.66f, sphere1.transform.localScale.y * 0.66f, sphere1.transform.localScale.z * 0.66f);
        sphere3.transform.localScale = new Vector3(sphere1.transform.localScale.x * 0.33f, sphere1.transform.localScale.y * 0.33f, sphere1.transform.localScale.z * 0.33f);
    }
    void contract()
    {
        print("contracting");
        sphere1.transform.localScale = new Vector3(sphere1.transform.localScale.x - 1f, sphere1.transform.localScale.y - 1f, sphere1.transform.localScale.z - 1f);
        sphere2.transform.localScale = new Vector3(sphere1.transform.localScale.x * 0.66f, sphere1.transform.localScale.y * 0.66f, sphere1.transform.localScale.z * 0.66f);
        sphere3.transform.localScale = new Vector3(sphere1.transform.localScale.x * 0.33f, sphere1.transform.localScale.y * 0.33f, sphere1.transform.localScale.z * 0.33f);
    }
}

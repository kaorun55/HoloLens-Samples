using UnityEngine;
using System.Collections;
using System;

public class PlaySound : MonoBehaviour {

    public int delay = 0;

	// Use this for initialization
	void Start () {
        StartCoroutine(Play());
	}

    private IEnumerator Play()
    {
        var audio = GetComponent<AudioSource>();

        yield return new WaitForSeconds(delay);

        while (true)
        {
            audio.Play();

            yield return new WaitForSeconds(8);
        }
    }
}

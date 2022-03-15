using UnityEngine;

// The Audio Source component has an AudioClip option.  The audio
// played in this example comes from AudioClip and is called audioData.

[RequireComponent(typeof(AudioSource))]
public class StoryMusic : MonoBehaviour
{
    private float delay = 90f;
    private float timeElapsed;

    AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        // audioData.Play();
        Debug.Log("started");
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > delay || Input.GetMouseButtonDown(0))
        {
            audioData.Stop();
        }
    }
}
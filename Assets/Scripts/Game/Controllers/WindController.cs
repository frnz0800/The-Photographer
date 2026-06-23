using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    [SerializeField] List<AudioClip> audioClips;

    private AudioSource audioSource;

    private IEnumerator PlayRandomClip()
    {
        while (true)
        {
            int random = Random.Range(0, 2);

            audioSource.clip = audioClips[random];
            audioSource.Play();

            yield return new WaitForSecondsRealtime(90);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(PlayRandomClip());
    }
}

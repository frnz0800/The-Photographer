using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerSnapshot defaultSnapshot;
    [SerializeField] private AudioMixerSnapshot dangerSnapshot;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform enemyTransform;

    [SerializeField] private AudioSource musicAudioSource;

    [SerializeField] private float maxDistance;

    private void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, enemyTransform.position);

        if (distance < maxDistance)
        {
            dangerSnapshot.TransitionTo(1f);

            if (!musicAudioSource.isPlaying)
            {
                musicAudioSource.Play();
            }
        }
        else
        {
            defaultSnapshot.TransitionTo(1.5f);

            if (musicAudioSource.isPlaying)
            {
                musicAudioSource.Stop();
            }
        }
    }
}

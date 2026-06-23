using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource footstepsAudioSource;

    private EnemyController enemyController;

    private IEnumerator PlayRandom()
    {
        while (true)
        {
            float randomTime = Random.Range(10, 30);
            float randomPitch = Random.Range(.5f, 1.25f);

            sfxAudioSource.pitch = randomPitch;
            sfxAudioSource.Play();

            yield return new WaitForSecondsRealtime(randomTime);
        }
    }

    private void OnMove()
    {
        if (!enemyController.CanChase)
        {
            footstepsAudioSource.Stop();
        }

        if (!footstepsAudioSource.isPlaying && enemyController.CanChase)
        {
            footstepsAudioSource.Play();
        }

        sfxAudioSource.Play();

        StartCoroutine(PlayRandom());
    }

    private void OnEnable()
    {
        enemyController.IsMoving += OnMove;
    }

    private void OnDisable()
    {
        enemyController.IsMoving -= OnMove;
    }

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }
}

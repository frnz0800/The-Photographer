using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerController))]

[RequireComponent(typeof(PlayerActionsController))]
public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clips;
    
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource footstepsAudioSource;

    private PlayerController playerController;
    private PlayerActionsController playerActionsController;

    private void OnMove(bool isWalking, bool isRunning)
    {
        if (isWalking && isRunning)
        {
            footstepsAudioSource.pitch = 1.25f;
        }
        else if (isWalking && !isRunning)
        {
            footstepsAudioSource.pitch = 1f;
        }
        else if (!isWalking)
        {
            footstepsAudioSource.Stop();
        }

        if (!footstepsAudioSource.isPlaying && isWalking)
        {
            footstepsAudioSource.Play();
        }
    }

    private void OnUse()
    {
        sfxAudioSource.clip = clips[0];
        sfxAudioSource.loop = false;
        sfxAudioSource.Play();
    }

    private void OnToggle()
    {
        sfxAudioSource.clip = clips[1];
        sfxAudioSource.loop = false;
        sfxAudioSource.Play();
    }

    private void OnEnable()
    {
        playerController.IsMoving += OnMove;
        playerActionsController.IsCameraUsed += OnUse;
        playerActionsController.IsFlashlightToggled += OnToggle;
    }

    private void OnDisable()
    {
        playerController.IsMoving -= OnMove;
        playerActionsController.IsCameraUsed -= OnUse;
        playerActionsController.IsFlashlightToggled -= OnToggle;
    }

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerActionsController = GetComponent<PlayerActionsController>();
    }
}

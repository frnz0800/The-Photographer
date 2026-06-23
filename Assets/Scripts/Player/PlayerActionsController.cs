using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionsController : MonoBehaviour
{
    [SerializeField] private float cameraCooldownTime;

    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private Light flashlight;
    [SerializeField] private Light cameraFlash;

    private bool isAiming = false;
    private bool onCooldown = false;

    public event Action<bool> IsAiming;
    public event Action IsCameraUsed;
    public event Action IsFlashlightToggled;

    private IEnumerator TakePhoto()
    {
        cameraFlash.enabled = true;
        onCooldown = true;

        IsCameraUsed?.Invoke();

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.collider.gameObject.TryGetComponent(out IFlashable newFlashable))
            {
                newFlashable.Flash();
            }
        }

        yield return new WaitForSeconds(.15f);

        cameraFlash.enabled = false;

        yield return new WaitForSeconds(cameraCooldownTime);

        onCooldown = false;
    }

    public void OnFlashlightToggled(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            flashlight.enabled = !flashlight.enabled;

            IsFlashlightToggled?.Invoke();
        }
    }

    public void OnCameraAimed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isAiming = true;
        }
        else if (context.canceled)
        {
            isAiming = false;
        }

        IsAiming?.Invoke(isAiming);
    }

    public void OnCameraUsed(InputAction.CallbackContext context)
    {
        if (context.performed && isAiming && !onCooldown)
        {
            StartCoroutine(TakePhoto());
        }
    }

    public void OnGamePaused(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Time.timeScale == 1f)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}

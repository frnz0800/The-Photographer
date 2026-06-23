using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ViewmodelController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerActionsController playerActionsController;

    private Animator animator;

    private void OnRun(bool _, bool isRunning)
    {
        animator.SetBool("IsRunning", isRunning);
    }

    private void OnAim(bool value)
    {
        animator.SetBool("IsAiming", value);
    }

    private void OnEnable()
    {
        playerController.IsMoving += OnRun;
        playerActionsController.IsAiming += OnAim;
    }

    private void OnDisable()
    {
        playerController.IsMoving -= OnRun;
        playerActionsController.IsAiming -= OnAim;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
}

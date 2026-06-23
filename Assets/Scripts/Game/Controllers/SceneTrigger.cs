using System;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName;

    [SerializeField] private bool requiresObjectiveCompletion;
    [SerializeField] private bool useTrigger;

    [Serializable]
    public struct ObjectiveProperties
    {
        public string objectiveName;
    }

    [SerializeField] private ObjectiveProperties objectiveProperties;

    private void OnObjectiveCompleted(string name)
    {
        if (!requiresObjectiveCompletion || useTrigger || objectiveProperties.objectiveName != name) return;

        LoadingManager.Manager.StartLoading(sceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!useTrigger) return;
        if (requiresObjectiveCompletion && objectiveProperties.objectiveName != name) return;

        if (other.gameObject.CompareTag("Player"))
        {
            LoadingManager.Manager.StartLoading(sceneName);
        }
    }

    private void OnEnable()
    {
        ObjectiveManager.OnObjectiveDeleted += OnObjectiveCompleted;
    }

    private void OnDisable()
    {
        ObjectiveManager.OnObjectiveDeleted -= OnObjectiveCompleted;
    }
}

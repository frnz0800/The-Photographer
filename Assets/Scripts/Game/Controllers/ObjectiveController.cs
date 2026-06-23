using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ObjectiveController : MonoBehaviour, IFlashable
{
    [SerializeField] private ObjectiveType objectiveType;

    [SerializeField] private string objectiveToUpdate;

    [SerializeField] private bool createsObjective;

    [Serializable]
    public struct ObjectiveProperties
    {
        public string objectiveName;
        public string objectiveDescription;
        public int objectiveTarget;
    }

    [SerializeField] private ObjectiveProperties objectiveProperties;

    private Collider collider_;

    public void Flash()
    {
        OnFlashed();
    }

    private void CreateObjective()
    {
        if (objectiveProperties.objectiveName == "") return;

        ObjectiveManager.CreateObjective(objectiveProperties.objectiveName, objectiveProperties.objectiveDescription, objectiveProperties.objectiveTarget);
    }

    private void CompleteObjective(string name)
    {
        if (name != "" && ObjectiveManager.Objectives[name]["Target"] == ObjectiveManager.Objectives[name]["Amount"])
        {
            ObjectiveManager.DeleteObjective(name);
        }
    }

    private void OnFlashed()
    {
        if (objectiveType != ObjectiveType.Photo || !ObjectiveManager.Objectives.ContainsKey(objectiveToUpdate)) return;

        if (createsObjective) CreateObjective();

        ObjectiveManager.UpdateObjective(objectiveToUpdate, 1);
        CompleteObjective(objectiveToUpdate);

        collider_.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (objectiveType != ObjectiveType.Trigger || !ObjectiveManager.Objectives.ContainsKey(objectiveToUpdate)) return;

        if (other.gameObject.CompareTag("Player"))
        {
            if (createsObjective) CreateObjective();

            ObjectiveManager.UpdateObjective(objectiveToUpdate, 1);
            CompleteObjective(objectiveToUpdate);

            collider_.enabled = false;
        }
    }

    private void Start()
    {
        collider_ = GetComponent<Collider>();
    }
}

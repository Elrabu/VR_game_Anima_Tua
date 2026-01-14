using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WhispererDetector", story: "Update [Detector] and assign [Target]", category: "Action", id: "28ff2f53bb9e21a9d75830f1faa03f1b")]
public partial class WhispererDetectorAction : Action
{
    [SerializeReference] public BlackboardVariable<WatcherDetect> Detector;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnUpdate()
    {
        Target.Value = Detector.Value.Detector();
        return Target.Value == null ? Status.Failure : Status.Success;
    }
}


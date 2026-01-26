using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Range Detector", story: "Update [Detector] and assign [Target]", category: "Action", id: "980c9ed16578b7a3c5ae29ed63650971")]
public partial class RangeDetectorAction : Action
{
    [SerializeReference] public BlackboardVariable<WatcherDetect> Detector;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnUpdate()
    {
        Target.Value = Detector.Value.Detector();
        return Target.Value == null? Status.Failure : Status.Success;
    }
}


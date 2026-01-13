using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Line of Sight check", story: "Check [Target] with Sight [Detector]", category: "Conditions", id: "fdaf62de8a8e5775d0d2807dc9538a65")]
public partial class LineOfSightCheckCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<WatcherSight> Detector;

    public override bool IsTrue()
    {
        return Detector.Value.PerformDetection(Target.Value) != null;
    }
}

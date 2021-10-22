using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Hand : XRDirectInteractor
{
    private SkinnedMeshRenderer meshRenderer = null;
    protected override void Awake()
    {
        base.Awake();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public void SetVisibility(bool value)
    {
        meshRenderer.enabled = value;
    }
}
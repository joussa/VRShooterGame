using UnityEngine.XR.Interaction.Toolkit;

public class HandHold : XRBaseInteractable
{
    void Update()
    {
        
    }

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        bool isalreadygrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);
        return base.IsSelectableBy(interactor);
    }
}
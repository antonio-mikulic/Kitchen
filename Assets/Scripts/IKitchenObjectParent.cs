using UnityEngine;

namespace Assets.Scripts
{
    public interface IKitchenObjectParent
    {
        Transform GetKitchenObjectFollowTransform();
        void SetKitchenObject(KitchenObject kitchenObject);
        KitchenObject GetKitchenObject();
        void ClearKitchenObject();
        bool HasKitchenObject();
    }
}

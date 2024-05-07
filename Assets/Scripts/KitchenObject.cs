using Assets.Scripts;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent parent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetParent(IKitchenObjectParent parent)
    {
        if (this.parent != null)
            this.parent.ClearKitchenObject();

        this.parent = parent;

        if (this.parent.HasKitchenObject())
            Debug.LogError("KitchenParent already has a kitchen object");

        this.parent.SetKitchenObject(this);

        transform.parent = this.parent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetParent()
    {
        return parent;
    }

    public void DestoySelf()
    {
        parent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetParent(kitchenObjectParent);

        return kitchenObject;
    }
}


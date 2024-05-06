using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter counter)
    {
        if (clearCounter != null)
            clearCounter.ClearKitchenObject();

        clearCounter = counter;

        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("Counter already has a kitchen object");
        }

        clearCounter.SetKitchenObject(this);

        transform.parent = counter.GetKitchenObjectoFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }

}
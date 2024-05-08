using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
                player.GetKitchenObject().SetParent(this);
        }
        else
        {
            if (!player.HasKitchenObject())
                GetKitchenObject().SetParent(player);
            else if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                bool isAdded = plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO());
                if (isAdded) GetKitchenObject().DestoySelf();
            }
            else if (GetKitchenObject().TryGetPlate(out PlateKitchenObject counterPlateKitchenObject))
            {
                bool isAdded = counterPlateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO());
                if (isAdded) player.GetKitchenObject().DestoySelf();
            }
        }
    }
}

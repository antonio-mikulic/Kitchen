using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                    player.GetKitchenObject().SetParent(this);
        }
        else
        {
            if (!player.HasKitchenObject())
                GetKitchenObject().SetParent(player);
        }
    }

    public override void InteractAlternative(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

            GetKitchenObject().DestoySelf();

            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
                return cuttingRecipeSO.output;

        return null;
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
                return true;

        return false;
    }
}

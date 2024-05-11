using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;


    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private int succeededRecipesAmount = 0;

    public void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            if (waitingRecipeSOList.Count < waitingRecipesMax)
            {
                spawnRecipeTimer = spawnRecipeTimerMax;
                RecipeSO waiting = recipeListSO.recipes[UnityEngine.Random.Range(0, recipeListSO.recipes.Count)];
                waitingRecipeSOList.Add(waiting);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
                Debug.Log("Recipe spawned: " + waiting.name);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipe = waitingRecipeSOList[i];
            List<KitchenObjectSO> kitchenObjectsOnPlate = plateKitchenObject.GetKitchenObjectSOList();

            if (waitingRecipe.kitchenObjectSOList.Count != kitchenObjectsOnPlate.Count)
                continue;

            int matching = 0;
            foreach (KitchenObjectSO onPlate in kitchenObjectsOnPlate)
                if (waitingRecipe.kitchenObjectSOList.Contains(onPlate))
                    matching++;

            if (matching == waitingRecipe.kitchenObjectSOList.Count)
            {
                waitingRecipeSOList.RemoveAt(i);
                succeededRecipesAmount++;
                Debug.Log("Recipe delivered: " + waitingRecipe.name);
                OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                return;
            }
        }

        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSucceededRecipesAmount()
    {
        return succeededRecipesAmount;
    }
}

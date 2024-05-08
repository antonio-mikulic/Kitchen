using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipes;


    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

    public void Awake()
    {
        Instance = this;
        waitingRecipes = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            if (waitingRecipes.Count < waitingRecipesMax)
            {
                spawnRecipeTimer = spawnRecipeTimerMax;
                RecipeSO waiting = recipeListSO.recipes[Random.Range(0, recipeListSO.recipes.Count)];
                Debug.Log("New recipe: " + waiting.name);
                waitingRecipes.Add(waiting);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipes.Count; i++)
        {
            RecipeSO waitingRecipe = waitingRecipes[i];
            List<KitchenObjectSO> kitchenObjectsOnPlate = plateKitchenObject.GetKitchenObjectSOList();

            if (waitingRecipe.kitchenObjectSOList.Count != kitchenObjectsOnPlate.Count)
                continue;

            int matching = 0;
            foreach (KitchenObjectSO onPlate in kitchenObjectsOnPlate)
                if (waitingRecipe.kitchenObjectSOList.Contains(onPlate))
                    matching++;

            if (matching == waitingRecipe.kitchenObjectSOList.Count)
            {
                waitingRecipes.RemoveAt(i);
                Debug.Log("Recipe delivered: " + waitingRecipe.name);
                return;
            }
            else
                Debug.Log("Delivery not matching recipe: " + waitingRecipe.name);
        }

        Debug.Log("Recipe not found");
    }
}

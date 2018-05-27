using Pipliz;
using System;
using System.Collections.Generic;

namespace ExtendedAPI.Recipes
{
    [ModLoader.ModManager]
    public static class RecipeManager
    {
        private static Dictionary<string, BaseRecipe> recipes = new Dictionary<string, BaseRecipe>();

        public static void Add(Type recipe)
        {
            BaseRecipe newRecipe = Activator.CreateInstance(recipe) as BaseRecipe;

            recipes.Add(newRecipe.key, newRecipe);
        }

        public static bool TryGet(string key, out BaseRecipe recipe)
        {
            return recipes.TryGetValue(key, out recipe);
        }

        [ModLoader.ModCallback(ModLoader.EModCallbackType.OnPlayerRecipeSettingChanged, "Khanx.ExtendedAPI.OnPlayerRecipeSettingChanged")]
        public static void OnPlayerRecipeSettingChanged(RecipeStorage.PlayerRecipeStorage storage, Recipe recipe, Box<RecipeStorage.RecipeSetting> recipeSetting)
        {
            BaseRecipe bRecipe;

            if(recipes.TryGetValue(recipe.Name, out bRecipe))
                bRecipe.OnPlayerRecipeSettingChanged(storage, recipeSetting);
        }

        [ModLoader.ModCallback(ModLoader.EModCallbackType.OnNPCCraftedRecipe, "Khanx.ExtendedAPI.OnNPCCraftedRecipe")]
        public static void OnNPCCraftedRecipe(NPC.IJob job, Recipe recipe, List<InventoryItem> results)
        {
            BaseRecipe bRecipe;

            if(recipes.TryGetValue(recipe.Name, out bRecipe))
                bRecipe.OnNPCCraftedRecipe(job, results);
        }
    }
}

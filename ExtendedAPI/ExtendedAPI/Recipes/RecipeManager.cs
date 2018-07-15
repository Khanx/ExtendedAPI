using Pipliz;
using System;
using System.Collections.Generic;

namespace ExtendedAPI.Recipes
{
    [ModLoader.ModManager]
    public static class RecipeManager
    {
        private static Dictionary<string, BaseRecipe> recipes = new Dictionary<string, BaseRecipe>();

        [ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined, "Khanx.ExtendedAPI.LoadBaseRecipes")]
        public static void LoadRecipes()
        {
            foreach(var modAssembly in ModLoader.LoadedMods)
            {
                if(modAssembly.HasAssembly)
                {
                    foreach(Type type in modAssembly.LoadedAssemblyTypes)
                    {
                        if(type.IsDefined(typeof(AutoLoadRecipeAttribute), true))
                        {
                            BaseRecipe newRecipe = Activator.CreateInstance(type) as BaseRecipe;

                            if(newRecipe.key.Equals("NOT_INIZILIZED"))
                            {
                                Log.Write("<color=red>Trying to add a BaseRecipe without defining the key property.</color>");
                                return;
                            }

                            if(!recipes.ContainsKey(newRecipe.key))
                                recipes.Add(newRecipe.key, newRecipe);
                            else
                                Log.Write(string.Format("<color=red>{0} already has a callback registered in ExtendedAPI.</color>", newRecipe.key));
                        }
                    }
                }
            }
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

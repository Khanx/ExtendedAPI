using Pipliz;
using System;
using System.Collections.Generic;

namespace ExtendedAPI.Recipes
    {
    public static class RecipeManager
        {
        private static List<Type> toRegister = new List<Type>();
        private static Dictionary<string, BaseRecipe> toCall = new Dictionary<string, BaseRecipe>();

        public static void Add(Type type)
            {
            toRegister.Add(type);
            }

        public static void RegisterCallBacks()
            {
            for(int i = 0; i < toRegister.Count; i++)
                {
                var recipe = Activator.CreateInstance(toRegister[i]) as Recipes.BaseRecipe;

                toCall.Add(recipe.key, recipe);
                }
            }

        public static void OnPlayerRecipeSettingChanged(RecipeStorage.PlayerRecipeStorage storage, string name, Box<RecipeStorage.RecipeSetting> recipeSetting)
            {
            BaseRecipe recipe;

            if(toCall.TryGetValue(name, out recipe))
                recipe.OnPlayerRecipeSettingChanged(storage, recipeSetting);
            }

        public static void OnNPCCraftedRecipe(NPC.IJob job, string name, List<InventoryItem> results)
            {
            BaseRecipe recipe;

            if(toCall.TryGetValue(name, out recipe))
                recipe.OnNPCCraftedRecipe(job, results);
            }
        }
    }

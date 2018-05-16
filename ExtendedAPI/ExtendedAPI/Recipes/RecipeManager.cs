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

        public static void Register()
            {
            for(int i = 0; i < toRegister.Count; i++)
                {
                var recipe = Activator.CreateInstance(toRegister[i]) as Recipes.BaseRecipe;
                if(recipe != null)
                    {
                    if(recipe.IsOptional)
                        {
                        if(recipe.PlayerCanMakeIt)
                            RecipePlayer.AddOptionalRecipe(recipe.GetRecipe());
                        RecipeStorage.AddOptionalLimitTypeRecipe(recipe.ProducedByJob, recipe.GetRecipe());
                        }
                    else
                        {
                        if(recipe.PlayerCanMakeIt)
                            RecipePlayer.AddDefaultRecipe(recipe.GetRecipe());
                        RecipeStorage.AddDefaultLimitTypeRecipe(recipe.ProducedByJob, recipe.GetRecipe());
                        }
                    if(recipe.OnChangingSetting || recipe.OnProducingRecipeByNPC)
                        toCall.Add(recipe.Name, recipe);
                    }
                }
            toRegister.Clear();
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

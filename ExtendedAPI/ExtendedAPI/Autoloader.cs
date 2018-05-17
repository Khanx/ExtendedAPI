using Pipliz;
using System.Collections.Generic;

namespace ExtendedAPI
    {
    [ModLoader.ModManager]
    public static class Autoloader
        {

        //Busca todos las clases con metatags y las anyade a su manager correspondiente
        [ModLoader.ModCallback(ModLoader.EModCallbackType.AfterModsLoaded, "Khanx.ExtendedAPI.ParseMods")]
        public static void AfterModsLoaded(List<ModLoader.ModDescription> assemblies)
            {
            foreach(var modAssembly in assemblies)
                {
                if(modAssembly.HasAssembly)
                    {
                    foreach(System.Type type in modAssembly.LoadedAssemblyTypes)
                        {
                        try
                            {
                            if(type.IsDefined(typeof(Recipes.AutoLoadRecipeAttribute), true))       //Recipes
                                Recipes.RecipeManager.Add(type);
                            else if(type.IsDefined(typeof(Types.AutoLoadTypeAttribute), true))      //Types
                                Types.TypeManager.Add(type);
                            else if(type.IsDefined(typeof(Commands.AutoLoadCommandAttribute), true))   //Commands
                                Commands.CommandManager.Add(type);
                            }
                        catch(System.Exception e)
                            {
                            Log.WriteException("APIProvider threw exception parsing dll {0}, type {1}", e, System.IO.Path.GetFileName(modAssembly.LoadedAssembly.Location), type.FullName);
                            }
                        }
                    }
                }
            }

        [ModLoader.ModCallback(ModLoader.EModCallbackType.AfterStartup, "Khanx.ExtendedAPI.RegisterCommand")]
        public static void RegisterCommand()
            {
            Commands.CommandManager.Register();
            }

        //Add Recipes Callbacks
        [ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined, "Khanx.ExtendedAPI.RegisterCallBacksOfRecipes")]
        [ModLoader.ModCallbackDependsOn("pipliz.server.recipeplayerload")]  //After loading the NPC & player recipes
        public static void RegisterCallBacksOfRecipes()
            {
            Recipes.RecipeManager.RegisterCallBacks();
            }

        [ModLoader.ModCallback(ModLoader.EModCallbackType.OnPlayerRecipeSettingChanged, "Khanx.ExtendedAPI.OnPlayerRecipeSettingChanged")]
        public static void OnPlayerRecipeSettingChanged(RecipeStorage.PlayerRecipeStorage storage, Recipe recipe, Box<RecipeStorage.RecipeSetting> recipeSetting)
            {
            Recipes.RecipeManager.OnPlayerRecipeSettingChanged(storage, recipe.Name, recipeSetting);
            }

        [ModLoader.ModCallback(ModLoader.EModCallbackType.OnNPCCraftedRecipe, "Khanx.ExtendedAPI.OnPlayerRecipeSettingChanged")]
        public static void OnNPCCraftedRecipe(NPC.IJob job, Recipe recipe, List<InventoryItem> results)
            {
            Recipes.RecipeManager.OnNPCCraftedRecipe(job, recipe.Name, results);
            }

        //Register OnAdd / On RemoveBlock
        [ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined, "Khanx.ExtendedAPI.RegisterCallBacksOfTypes")]
        public static void RegisterCallBacksOfTypes()
            {
            Types.TypeManager.RegisterCallBacks();
            }

        [ModLoader.ModCallback(ModLoader.EModCallbackType.OnPlayerClicked, "Khanx.ExtendedAPI.OnPlayerClickedTypes")]
        public static void OnPlayerClickedTypes(Players.Player player, Box<Shared.PlayerClickedData> boxedData)
            {
            Types.TypeManager.OnPlayerClicked(player, boxedData);
            }


        }
    }

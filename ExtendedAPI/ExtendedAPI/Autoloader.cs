using Pipliz;
using System.Collections.Generic;

namespace ExtendedAPI
{
    [ModLoader.ModManager]
    public static class Autoloader
    {
        //Search by metatags and add them to the corresponding manager
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
    }
}

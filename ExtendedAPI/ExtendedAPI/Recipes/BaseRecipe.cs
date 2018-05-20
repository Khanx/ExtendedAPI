using NPC;
using System.Collections.Generic;


namespace ExtendedAPI.Recipes
{
    public class BaseRecipe
    {
        public string key { get; protected set; }

        /// <summary>
        /// Don't store the Box<>, it's re-used.
        /// See storage.Player for the owner of the recipe
        /// This callback is also called upon loading the settings from json - but only for non-default values (defaults aren't stored)
        /// No registered uses
        /// </summary>
        /// <param name="storage">the players' recipe settings storage</param>
        /// <param name="recipeSetting">the new setting for the recipe</param>
        public virtual void OnPlayerRecipeSettingChanged(RecipeStorage.PlayerRecipeStorage storage, Pipliz.Box<RecipeStorage.RecipeSetting> recipeSetting) { }

        /// <summary>
        /// Triggered when an npc doing {job} crafts this recipe, creating {results}
        /// The results are re-used, don't store it.
        /// Results can be edited.After the callback they'll be added to the npc/block's inventory
        /// If the results are not empty, the npc will show a npc indicator with a weighted random type from the results
        /// No registered uses
        /// </summary>
        /// <param name="job">Job that has used this recipe</param>
        /// <param name="results">Product of the recipe</param>
        public virtual void OnNPCCraftedRecipe(IJob job, List<InventoryItem> results) { }
    }
}

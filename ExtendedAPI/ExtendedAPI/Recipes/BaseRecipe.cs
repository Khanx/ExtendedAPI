using NPC;
using System.Collections.Generic;


namespace ExtendedAPI.Recipes
    {

    public class BaseRecipe
        {
        public string Name;
        public bool PlayerCanMakeIt = false;
        public string ProducedByJob = "";
        public List<InventoryItem> Requirements;
        public List<InventoryItem> Results;
        public bool IsOptional = false;
        public int DefaultLimit = 2000000000;
        public int DefaultPriority = 0;
        public bool OnChangingSetting = false;        //Put true if you will extend OnPlayerRecipeSettingChanged
        public bool OnProducingRecipeByNPC = false;   //Put true if you will extend OnNPCCraftedRecipe

        protected void AddRequirement(InventoryItem item)
            {
            if(Requirements == null)
                Requirements = new List<InventoryItem>();
            Requirements.Add(item);
            }

        protected void AddResult(InventoryItem item)
            {
            if(Results == null)
                Results = new List<InventoryItem>();
            Results.Add(item);
            }

        public Recipe GetRecipe()
            {
            return new Recipe(Name, Requirements, Results, DefaultLimit, IsOptional, DefaultPriority);
            }

        /// <summary>
        /// Don't store the Box<>, it's re-used.
        /// See storage.Player for the owner of the recipe
        /// This callback is also called upon loading the settings from json - but only for non-default values (defaults aren't stored)
        /// No registered uses
        /// </summary>
        /// <param name="storage">the players' recipe settings storage</param>
        /// <param name="recipeSetting">the new setting for the recipe</param>
        public virtual void OnPlayerRecipeSettingChanged(RecipeStorage.PlayerRecipeStorage storage, Pipliz.Box<RecipeStorage.RecipeSetting> recipeSetting)
            {
            }

        /// <summary>
        /// Triggered when an npc doing {job} crafts this recipe, creating {results}
        /// The results are re-used, don't store it.
        /// Results can be edited.After the callback they'll be added to the npc/block's inventory
        /// If the results are not empty, the npc will show a npc indicator with a weighted random type from the results
        /// No registered uses
        /// </summary>
        /// <param name="job">Job that has used this recipe</param>
        /// <param name="results">Product of the recipe</param>
        public virtual void OnNPCCraftedRecipe(IJob job, List<InventoryItem> results)
            {
            }
        }

    }

using BlockTypes.Builtin;
using ExtendedAPI.Recipes;
using NPC;
using Pipliz;
using System.Collections.Generic;

namespace ExtendedAPI
    {
    [AutoLoadRecipe]
    class TestRecipe : BaseRecipe
        {
        public TestRecipe()
            {
            Name = "New Recipe";
            PlayerCanMakeIt = true;
            ProducedByJob = "pipliz.crafter";
            IsOptional = false;
            DefaultLimit = 50;
            DefaultPriority = 0;
            OnChangingSetting = true;       //Put true if you will extend OnPlayerRecipeSettingChanged
            OnProducingRecipeByNPC = true; //Put true if you will extend OnNPCCraftedRecipe

            var iron = new InventoryItem(BuiltinBlocks.BronzeIngot, 2);
            var planks = new InventoryItem(BuiltinBlocks.Planks, 4);

            AddRequirement(iron);
            AddResult(planks);
            }

        public override void OnNPCCraftedRecipe(IJob job, List<InventoryItem> results)
            {
            Pipliz.Chatting.Chat.SendToAll("Testing OnNPCCraftedRecipe");
            }

        public override void OnPlayerRecipeSettingChanged(RecipeStorage.PlayerRecipeStorage storage, Box<RecipeStorage.RecipeSetting> recipeSetting)
            {
            Pipliz.Chatting.Chat.SendToAll("Testing OnPlayerRecipeSettingChanged");
            }
        }
    }

//#define EXAMPLES
using ExtendedAPI.Recipes;
using NPC;
using Pipliz;
using System.Collections.Generic;

#if EXAMPLES
namespace ExtendedAPI
{
    [AutoLoadRecipe]
    class TestRecipe : BaseRecipe
    {
        public TestRecipe()
        {
            key = "pipliz.crafter.plankstemperate";
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
#endif
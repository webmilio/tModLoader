﻿using Terraria.ModLoader.Exceptions;

namespace Terraria.ModLoader.Recipes
{
	public class RecipeBuilder
	{
		private readonly ModRecipe _recipe;

		/// <summary>
		/// Returns a new instance of <see cref="RecipeBuilder"/>
		/// </summary>
		/// <returns></returns>
		public static RecipeBuilder New() => new RecipeBuilder();

		/// <summary>Creates a new instance with no ingredients and no result.</summary>
		/// <seealso cref="Requires(int,int)"/>
		/// <seealso cref="Build"/>
		public RecipeBuilder()
		{
		}

		/// <summary>Creates a new instance with no ingredients with the given item and stack as a result.</summary>
		/// <param name="mod">The mod who owns the recipe.</param>
		/// <param name="item">The item type.</param>
		/// <param name="stack">The stack.</param>
		/// <seealso cref="Requires(int,int)"/>
		/// <seealso cref="Build"/>
		public RecipeBuilder(Mod mod, int item, int stack = 1)
		{
			_recipe = new ModRecipe(mod);
			_recipe.SetResult(item, stack);
		}

		/// <summary>Creates a new instance with no ingredients with the given item and stack as a result.</summary>
		/// <param name="mod">The mod who owns the recipe.</param>
		/// <param name="itemName">Name of the item.</param>
		/// <param name="stack">The stack.</param>
		/// <seealso cref="Requires(int,int)"/>
		/// <seealso cref="Build"/>
		public RecipeBuilder(Mod mod, string itemName, int stack = 1)
		{
			_recipe = new ModRecipe(mod);
			_recipe.SetResult(mod, itemName, stack);
		}

		/// <summary>Creates a new instance with no ingredients with the given item and stack as a result.</summary>
		/// <param name="item"></param>
		/// <param name="stack"></param>
		/// <seealso cref="Requires(int,int)"/>
		/// <seealso cref="Build"/>
		public RecipeBuilder(ModItem item, int stack = 1)
		{
			_recipe = new ModRecipe(item.mod);
			_recipe.SetResult(item, stack);
		}

		/// <summary>
		/// Adds an ingredient to this recipe with the given item type and stack size.
		/// Ex.: 
		/// <example>recipe.AddIngredient(ItemID.IronAxe)</example>
		/// </summary>
		/// <param name="type">The item type.</param>
		/// <param name="stack">The stack.</param>
		/// <returns></returns>
		public RecipeBuilder AddIngredient(int type, int stack = 1)
		{
			_recipe.AddIngredient(type, stack);
			return this;
		}

		/// <summary>
		/// Adds given ingredients to the recipe.
		/// </summary>
		/// <param name="ingredients">The ingredients value tuples</param>
		/// <returns></returns>
		public RecipeBuilder AddIngredients(params (short itemType, int stack)[] ingredients)
		{
			foreach (var (itemType, stack) in ingredients)
				AddIngredient(itemType, stack);
			return this;
		}

		/// <summary>
		/// Adds the specified ingredients to this recipe with the given item types.
		/// Ex.: 
		/// <example>recipe.AddIngredient(ItemID.IronAxe, ItemID.IronPickaxe)</example>
		/// </summary>
		/// <param name="types">The item types.</param>
		/// <returns></returns>
		public RecipeBuilder AddIngredients(params int[] types)
		{
			for (int i = 0; i < types.Length; i++)
				AddIngredient(types[i]);

			return this;
		}

		/// <summary>Adds an ingredient to this recipe with the given item name from the given mod, and with the given stack stack. If the mod parameter is null, then it will automatically use an item from the mod creating this recipe.</summary>
		/// <param name="mod">The mod.</param>
		/// <param name="itemName">Name of the item.</param>
		/// <param name="stack">The stack.</param>
		/// <returns></returns>
		/// <exception cref="RecipeException">The item " + itemName + " does not exist in mod " + mod.Name + ". If you are trying to use a vanilla item, try removing the first argument.</exception>
		public RecipeBuilder AddIngredient(Mod mod, string itemName, int stack = 1)
		{
			_recipe.AddIngredient(mod, itemName, stack);
			return this;
		}

		/// <summary>Adds an ingredient to this recipe of the given type and stack size.</summary>
		/// <typeparam name="T">The type.</typeparam>
		/// <param name="stack">The stack.</param>
		/// <returns></returns>
		public RecipeBuilder AddIngredient<T>(int stack = 1) where T : ModItem => AddIngredient(ModContent.ItemType<T>(), stack);

		/// <summary>Adds an ingredient to this recipe of the given type of item and stack size.</summary>
		/// <param name="modItem">The item.</param>
		/// <param name="stack">The stack.</param>
		/// <returns></returns>
		public RecipeBuilder AddIngredient(ModItem modItem, int stack = 1)
		{
			_recipe.AddIngredient(modItem, stack);
			return this;
		}

		/// <summary>Adds a recipe group ingredient to this recipe with the given RecipeGroup name and stack size. Vanilla recipe groups consist of "Wood", "IronBar", "PresurePlate", "Sand", and "Fragment".</summary>
		/// <param name="recipeGroup">The name.</param>
		/// <param name="stack">The stack.</param>
		/// <returns></returns>
		/// <exception cref="RecipeException"></exception>
		public RecipeBuilder AddRecipeGroup(string recipeGroup, int stack = 1)
		{
			_recipe.AddRecipeGroup(recipeGroup, stack);
			return this;
		}

		/// <summary>
		/// Adds one or many required crafting station(s) with the given tile type(s) to the recipe being built.
		/// Ex.:
		/// <example>At(TileID.WorkBenches, TileID.Anvils)</example>
		/// </summary>
		/// <param name="tileTypes"></param>
		/// <returns></returns>
		public RecipeBuilder RequiresTiles(params int[] tileTypes)
		{
			for (int i = 0; i < tileTypes.Length; i++)
				_recipe.AddTile(tileTypes[i]);

			return this;
		}

		/// <summary>
		/// Makes the recipe require lava
		/// </summary>
		/// <returns></returns>
		public RecipeBuilder RequiresLava()
		{
			_recipe.needLava = true;
			return this;
		}

		/// <summary>
		/// Makes the recipe require honey
		/// </summary>
		/// <returns></returns>
		public RecipeBuilder RequiresHoney()
		{
			_recipe.needHoney = true;
			return this;
		}

		/// <summary>
		/// Makes the recipe require water
		/// </summary>
		/// <returns></returns>
		public RecipeBuilder RequiresWater()
		{
			_recipe.needWater = true;
			return this;
		}

		/// <summary>
		/// Makes the recipe require the snow biome
		/// </summary>
		/// <returns></returns>
		public RecipeBuilder RequiresSnowBiome()
		{
			_recipe.needSnowBiome = true;
			return this;
		}

		/// <summary>Adds this recipe to the game. Call this after you have finished setting the result, ingredients, etc.</summary>
		/// <returns><see cref="ModRecipe"/></returns>
		/// <exception cref="RecipeException">A recipe without any result has been added.</exception>
		public ModRecipe Build()
		{
			_recipe.AddRecipe();
			return _recipe;
		}
	}
}

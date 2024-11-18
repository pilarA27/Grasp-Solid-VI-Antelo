//-------------------------------------------------------------------------
// <copyright file="Recipe.cs" company="Universidad Católica del Uruguay">
// Copyright (c) Programación II. Derechos reservados.
// </copyright>
//-------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Full_GRASP_And_SOLID
{
    public class Recipe : IRecipeContent // Modificado por DIP
    {
        // Cambiado por OCP
        private IList<BaseStep> steps = new List<BaseStep>();

        public Recipe()
        {
            Cooked = false;
        }

        public int CookTime { get; set; }
        public bool Cooked { get; set; }
        public CountdownTimerForRecipe RecipeTimer { get; set; }
        private CountdownTimer _countdownTimer;
        public Product FinalProduct { get; set; }

        // Agregado por Creator
        public void AddStep(Product input, double quantity, Equipment equipment, int time)
        {
            Step step = new Step(input, quantity, equipment, time);
            this.steps.Add(step);
        }

        // Agregado por OCP y Creator
        public void AddStep(string description, int time)
        {
            WaitStep step = new WaitStep(description, time);
            this.steps.Add(step);
        }

        public void RemoveStep(BaseStep step)
        {
            this.steps.Remove(step);
        }

        // Agregado por SRP
        public string GetTextToPrint()
        {
            string result = $"Receta de {this.FinalProduct.Description}:\n";
            foreach (BaseStep step in this.steps)
            {
                result = result + step.GetTextToPrint() + "\n";
            }

            // Agregado por Expert
            result = result + $"Costo de producción: {this.GetProductionCost()}";

            return result;
        }

        // Agregado por Expert
        public double GetProductionCost()
        {
            double result = 0;

            foreach (BaseStep step in this.steps)
            {
                result = result + step.GetStepCost();
            }

            return result;
        }

        public void GetCookTime(IList<BaseStep> recipeSteps)
        {
            int cookTime = 0;
            foreach (var varStep in recipeSteps)
            {
                cookTime += varStep.Time;
            }

            this.CookTime = cookTime * 1000;
        }

        public void Cook()
        {
            this.RecipeTimer = new CountdownTimerForRecipe(this._countdownTimer);
            this.RecipeTimer.RegisterAdapter(this.CookTime, this);
            this.Cooked = true;
        }
    }
}


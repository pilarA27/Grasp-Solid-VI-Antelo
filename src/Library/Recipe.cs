using System;
using System.Collections.Generic;
using System.Threading;

namespace Full_GRASP_And_SOLID
{
    public class Recipe : IRecipeContent, TimerClient
    {
        private IList<BaseStep> steps = new List<BaseStep>();

        private bool cooked = false;
        private CountdownTimer countdownTimer;
        private int cookTimeInMilliseconds;

        public Product FinalProduct { get; set; }

        public bool Cooked
        {
            get { return this.cooked; }
        }

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

            result = result + $"Costo de producción: {this.GetProductionCost()}";
            return result;
        }

        public double GetProductionCost()
        {
            double result = 0;

            foreach (BaseStep step in this.steps)
            {
                result = result + step.GetStepCost();
            }

            return result;
        }

        // Método para obtener el tiempo total de cocción
        public int GetCookTime()
        {
            int totalCookTime = 0;

            foreach (BaseStep step in this.steps)
            {
                totalCookTime += step.Time;
            }

            this.cookTimeInMilliseconds = totalCookTime * 1000;
            return totalCookTime;
        }

        // Método para cocinar la receta
        public void Cook()
        {
            if (this.cooked)
            {
                throw new InvalidOperationException("La receta ya está cocinada.");
            }
            this.countdownTimer = new CountdownTimer();
            this.countdownTimer.Register(this.cookTimeInMilliseconds, this);
        }

        // Uso de CountDownTimer
        public void TimeOut()
        {
            this.cooked = true;
            Console.WriteLine("La receta ahora está cocinada.");
        }
    }
}


using System;
using System.Text;
namespace FuelCalculator {
    public class Program {
        public static void Main(string[] args) {
            if (args == null || args.Length != 2) {
                Console.WriteLine("Program use: FuelCalculator.exe [FuelCapacity] [DistanceToTravel]");
                Console.WriteLine("Example: FuelCalculator.exe 500 800");
                return;
            }

            double fuelCapacity = 500;
            double.TryParse(args[0], out fuelCapacity);
            double distanceToTravel = 800;
            double.TryParse(args[1], out distanceToTravel);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("--Discrete Values--");
            CalculatorDiscrete discrete = new CalculatorDiscrete((int)fuelCapacity, (int)distanceToTravel);
            sb.Append($"(Distance To Travel = {(int)distanceToTravel}) (Stops = ");
            AppendList(discrete.Distances, sb);
            sb.AppendLine($") (Fuel Needed = {discrete.CalculateFuel()})");

            sb.AppendLine("--Real Values--");
            CalculatorReal real = new CalculatorReal(fuelCapacity, distanceToTravel);
            sb.Append($"(Distance To Travel = {distanceToTravel}) (Stops = ");
            AppendList(real.Distances, sb);
            sb.AppendLine($") (Fuel Needed = {real.CalculateFuel():0.##})");

            Console.WriteLine(sb.ToString());
            Console.ReadLine();
        }
        private static void AppendList(int[] distances, StringBuilder builder) {
            int distance = 0;
            foreach (int stop in distances) {
                distance += stop;
                builder.Append($"{distance}, ");
            }
            builder.Length -= 2;
        }
        private static void AppendList(double[] distances, StringBuilder builder) {
            double distance = 0;
            foreach (double stop in distances) {
                distance += stop;
                builder.Append($"{distance:0.###}, ");
            }
            builder.Length -= 2;
        }
    }
}
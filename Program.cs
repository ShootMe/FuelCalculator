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

            CalculatorReal real = new CalculatorReal(fuelCapacity, distanceToTravel);
            if (real.Distances.Length <= 2050) {
                sb.AppendLine("--Real Values--");
                sb.Append($"(Distance To Travel = {distanceToTravel}) (Stops = ");
                AppendList(real.Distances, sb);
                sb.AppendLine($") (Fuel Needed = {real.CalculateFuel():0.###})");
            }

            Console.WriteLine(sb.ToString());
            Console.ReadLine();
        }
        private static void AppendList(int[] distances, StringBuilder builder) {
            int distance = 0;
            if (distances.Length > 100) {
                builder.Append("..., ");
            }
            for (int i = distances.Length > 100 ? distances.Length - 100 : 0; i < distances.Length; i++) {
                distance += distances[i];
                builder.Append($"{distance}, ");
            }
            builder.Length -= 2;
        }
        private static void AppendList(double[] distances, StringBuilder builder) {
            double distance = 0;
            if (distances.Length > 100) {
                builder.Append("..., ");
            }
            for (int i = distances.Length > 100 ? distances.Length - 100 : 0; i < distances.Length; i++) {
                distance += distances[i];
                builder.Append($"{distance:0.###}, ");
            }
            builder.Length -= 2;
        }
    }
}
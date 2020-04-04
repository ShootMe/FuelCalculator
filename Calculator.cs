using System;
using System.Collections.Generic;
using System.Text;
namespace FuelCalculator {
    public class Calculator {
        public static void Main(string[] args) {
            if (args == null || args.Length != 2) {
                Console.WriteLine("Program use: FuelCalculator.exe [FuelCapacity] [DistanceToTravel]");
                Console.WriteLine("Example: FuelCalculator.exe 500 800");
                return;
            }

            int fuelCap = int.Parse(args[0]);
            int distanceToTravel = int.Parse(args[1]);

            int[] distances = GetOptimalDistances(fuelCap, distanceToTravel);

            StringBuilder sb = new StringBuilder();
            sb.Append($"(Distance To Travel = {distanceToTravel}) (Stops = ");
            int distance = 0;
            for (int i = 0; i < distances.Length; i++) {
                distance += distances[i];
                sb.Append($"{distance}, ");
            }
            sb.Length -= 2;

            long calculatedAmount = CalculateFuel(distances, fuelCap, distanceToTravel);
            sb.Append($") (Fuel Needed = {calculatedAmount})");
            Console.WriteLine(sb.ToString());
            Console.ReadLine();
        }
        private static int[] GetOptimalDistances(int fuelCapacity, int distanceToTravel) {
            int distance = fuelCapacity;
            if (distance > distanceToTravel) {
                distance = distanceToTravel;
            }
            int ratio = 3;
            int leftOver = 0;

            List<int> distanceList = new List<int>();
            distanceList.Add(distance);
            while (distance < distanceToTravel) {
                int travel = (fuelCapacity + leftOver) / ratio;
                if (travel <= 0) { travel = 1; }
                leftOver = (fuelCapacity + leftOver) - travel * ratio;
                if (travel + distance > distanceToTravel) {
                    travel = distanceToTravel - distance;
                }
                distanceList.Insert(0, travel);
                distance += travel;
                ratio += 2;
            }

            return distanceList.ToArray();
        }
        public static long CalculateFuel(int[] distances, int fuelCapacity, int distanceToTravel) {
            int index = distances.Length - 1;
            long fuelAmount = distances[index--];
            int distance = (int)fuelAmount;
            while (distance < distanceToTravel) {
                int toMove = distances[index--];
                fuelAmount = GetNextAmount(fuelCapacity, fuelAmount, toMove);
                distance += toMove;
            }
            return fuelAmount;
        }
        private static long GetNextAmount(int fuelCapacity, long startingAmount, int toMove) {
            int increase = fuelCapacity - 2 * toMove;
            long nextAmount = (startingAmount - toMove) / increase;
            if (nextAmount == 0 || nextAmount * (fuelCapacity - 2 * toMove) != startingAmount - toMove) {
                nextAmount = nextAmount * fuelCapacity + (startingAmount - toMove - nextAmount * increase) + 2 * toMove;
            } else {
                nextAmount = nextAmount * fuelCapacity;
            }
            return nextAmount;
        }
    }
}
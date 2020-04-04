using System.Collections.Generic;
namespace FuelCalculator {
    public class CalculatorDiscrete {
        public int FuelCapacity;
        public int DistanceToTravel;
        public int[] Distances;
        public CalculatorDiscrete(int fuelCapacity, int distanceToTravel) {
            FuelCapacity = fuelCapacity;
            DistanceToTravel = distanceToTravel;
            Distances = GetOptimalDistances();
        }
        private int[] GetOptimalDistances() {
            int distance = FuelCapacity;
            if (distance > DistanceToTravel) {
                distance = DistanceToTravel;
            }
            int ratio = 3;
            int leftOver = 0;

            List<int> distanceList = new List<int>();
            distanceList.Add(distance);

            while (distance < DistanceToTravel) {
                int travel = (FuelCapacity + leftOver) / ratio;
                if (travel <= 0) { travel = 1; }

                leftOver = (FuelCapacity + leftOver) - travel * ratio;
                if (travel + distance > DistanceToTravel) {
                    travel = DistanceToTravel - distance;
                }

                distanceList.Insert(0, travel);
                distance += travel;
                ratio += 2;
            }

            return distanceList.ToArray();
        }
        public long CalculateFuel(int[] distances = null) {
            if (distances == null) {
                distances = Distances;
            }

            int index = distances.Length - 1;
            long fuelAmount = distances[index--];
            int distance = (int)fuelAmount;
            while (distance < DistanceToTravel) {
                int toMove = distances[index--];
                fuelAmount = GetNextAmount(fuelAmount, toMove);
                distance += toMove;
            }

            return fuelAmount;
        }
        private long GetNextAmount(long startingAmount, int toMove) {
            int increase = FuelCapacity - 2 * toMove;
            long nextAmount = (startingAmount - toMove) / increase;

            if (nextAmount == 0 || nextAmount * (FuelCapacity - 2 * toMove) != startingAmount - toMove) {
                nextAmount = nextAmount * FuelCapacity + (startingAmount - toMove - nextAmount * increase) + 2 * toMove;
            } else {
                nextAmount = nextAmount * FuelCapacity;
            }

            return nextAmount;
        }
    }
}
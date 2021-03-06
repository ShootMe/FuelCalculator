﻿using System.Collections.Generic;
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
            long ratio = 3;
            long leftOver = 0;

            List<int> distanceList = new List<int>();
            distanceList.Add(distance);

            while (distance < DistanceToTravel) {
                int travel = (int)((FuelCapacity + leftOver) / ratio);
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
            while (index >= 0) {
                int toMove = distances[index--];
                fuelAmount = GetNextAmount(fuelAmount, toMove);
            }
            return fuelAmount;
        }
        private long GetNextAmount(long startingAmount, int toMove) {
            long increase = FuelCapacity - 2 * toMove;
            long nextAmount = (startingAmount - toMove) / increase;

            if (nextAmount == 0 || nextAmount * increase != startingAmount - toMove) {
                nextAmount = nextAmount * FuelCapacity + (startingAmount - toMove - nextAmount * increase) + 2 * toMove;
            } else {
                nextAmount = nextAmount * FuelCapacity;
            }

            return nextAmount;
        }
    }
}
using System.Collections.Generic;
namespace FuelCalculator {
    public class CalculatorReal {
        public double FuelCapacity;
        public double DistanceToTravel;
        public double[] Distances;
        public CalculatorReal(double fuelCapacity, double distanceToTravel) {
            FuelCapacity = fuelCapacity;
            DistanceToTravel = distanceToTravel;
            Distances = GetOptimalDistances();
        }
        private double[] GetOptimalDistances() {
            double distance = FuelCapacity;
            if (distance > DistanceToTravel) {
                distance = DistanceToTravel;
            }
            double ratio = 3;

            List<double> distanceList = new List<double>();
            distanceList.Add(distance);

            while (distance < DistanceToTravel) {
                double travel = FuelCapacity / ratio;
                double leftOver = FuelCapacity - travel * ratio;
                if (travel + distance > DistanceToTravel) {
                    travel = DistanceToTravel - distance;
                }

                distanceList.Insert(0, travel);
                distance += travel;
                ratio += 2;
            }

            return distanceList.ToArray();
        }
        public double CalculateFuel(double[] distances = null) {
            if (distances == null) {
                distances = Distances;
            }

            return (distances.Length - 1) * FuelCapacity + (distances[0] * (distances.Length * 2 - 1));
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;

namespace AutonomousRobot.AI
{
    public class SensorReading
    {
        public int SensorId { get; set; }
        public string Type { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
        public double Confidence { get; set; }
    }
    public enum RobotAction
    {
        Stop,
        SlowDown,
        Reroute,
        Continue

    }
    public class DecisionEngine
    {
        public static List<SensorReading> GetRecentReadings(List<SensorReading> sensorHistory, DateTime fromTime)
        {
            return sensorHistory.Where(s => s.Timestamp >= fromTime).ToList();
        }
        public static bool IsBatteryCritical(List<SensorReading> readings)
        {
            return readings.Any(s => s.Type == "Battery" && s.Value < 20);
        }
        public static double GetNearestObstacleDistance(List<SensorReading> readings)
        {
            var distanceReadings = readings.Where(s => s.Type == "Distance").Select(s => s.Value);
            return distanceReadings.Any() ? distanceReadings.Min() : double.MaxValue;
        }
        public static bool IsTemperatureSafe(List<SensorReading> readings)
        {
            return readings.Where(s => s.Type == "Temperature").All(s => s.Value < 90);
        }
        public static double GetAverageVibration(List<SensorReading> readings)
        {
            var vibrations = readings.Where(s => s.Type == "Vibration").Select(s => s.Value);
            return vibrations.Any() ? vibrations.Average() : 0;
        }
        public static Dictionary<string, double> CalculateSensorHealth(List<SensorReading> sensorHistory)
        {
            return sensorHistory
                .GroupBy(s => s.Type)
                .ToDictionary(g => g.Key, g => g.Average(s => s.Confidence));
        }
        public static List<string> DetectFaultySensors(List<SensorReading> sensorHistory)
        {
            return sensorHistory
                .GroupBy(s => s.Type)
                .Where(g => g.Count(s => s.Confidence < 0.4) > 2)
                .Select(g => g.Key)
                .ToList();
        }
        public static bool IsBatteryDrainingFast(List<SensorReading> sensorHistory)
        {
            var batteryValues = sensorHistory
                .Where(s => s.Type == "Battery")
                .OrderBy(s => s.Timestamp)
                .Select(s => s.Value)
                .ToList();

            if (batteryValues.Count < 2) return false;

            return batteryValues.Zip(batteryValues.Skip(1), (prev, curr) => prev > curr).All(x => x);
        }
        public static double GetWeightedDistance(List<SensorReading> readings)
        {
            var distanceSensors = readings.Where(s => s.Type == "Distance").ToList();
            double totalConfidence = distanceSensors.Sum(s => s.Confidence);

            if (totalConfidence == 0) return double.MaxValue;

            double weightedSum = distanceSensors.Sum(s => s.Value * s.Confidence);
            return weightedSum / totalConfidence;
        }
        public static RobotAction DecideRobotAction(List<SensorReading> recentReadings, List<SensorReading> sensorHistory)
        {
            if (IsBatteryCritical(recentReadings)) 
                return RobotAction.Stop;

            if (GetNearestObstacleDistance(recentReadings) < 1.0) 
                return RobotAction.Reroute;

            if (!IsTemperatureSafe(recentReadings) || GetAverageVibration(recentReadings) > 10.0) 
                return RobotAction.SlowDown;

            return RobotAction.Continue;
        }


    }

    class Program
    {
        static void Main()
        {
            DateTime now = DateTime.Now;
            List<SensorReading> sensorHistory = new List<SensorReading>
            {
                new SensorReading { SensorId = 1, Type = "Distance", Value = 0.8, Confidence = 0.9, Timestamp = now.AddSeconds(-5) },
                new SensorReading { SensorId = 2, Type = "Battery", Value = 18, Confidence = 0.8, Timestamp = now.AddSeconds(-4) },
                new SensorReading { SensorId = 3, Type = "Temperature", Value = 92, Confidence = 0.7, Timestamp = now.AddSeconds(-3) },
                new SensorReading { SensorId = 4, Type = "Vibration", Value = 8.2, Confidence = 0.6, Timestamp = now.AddSeconds(-2) },
                new SensorReading { SensorId = 5, Type = "Battery", Value = 75, Confidence = 0.9, Timestamp = now.AddSeconds(-1) },
                new SensorReading { SensorId = 6, Type = "Distance", Value = 2.5, Confidence = 0.5, Timestamp = now }
            };

            var recentReadings = DecisionEngine.GetRecentReadings(sensorHistory, now.AddSeconds(-10));
            foreach (var i in recentReadings)   
            {
                Console.WriteLine($"{i.SensorId} : {i.Type} : {i.Value} : {i.Confidence} : {i.Timestamp}");
            }

            bool batteryCritical = DecisionEngine.IsBatteryCritical(recentReadings);
            Console.WriteLine($"{batteryCritical} ");
            
            double nearestObstacle = DecisionEngine.GetNearestObstacleDistance(recentReadings);
            Console.WriteLine($"{nearestObstacle} ");

            bool tempSafe = DecisionEngine.IsTemperatureSafe(recentReadings);
            Console.WriteLine($"{tempSafe} ");

            double avgVibration = DecisionEngine.GetAverageVibration(recentReadings);
            Console.WriteLine($"{avgVibration} ");

            var sensorHealth = DecisionEngine.CalculateSensorHealth(sensorHistory);
            foreach (var i in recentReadings)   
            {
                Console.WriteLine($"{i.SensorId} : {i.Type} : {i.Value} : {i.Confidence} : {i.Timestamp}");
            }

            var faultySensors = DecisionEngine.DetectFaultySensors(sensorHistory);
            foreach (var i in recentReadings)   
            {
                Console.WriteLine($"{i.SensorId} : {i.Type} : {i.Value} : {i.Confidence} : {i.Timestamp}");
            }
            
            bool drainingFast = DecisionEngine.IsBatteryDrainingFast(sensorHistory);
            Console.WriteLine($"{drainingFast} ");

            double weightedDist = DecisionEngine.GetWeightedDistance(recentReadings);
            Console.WriteLine($"{weightedDist} ");

            RobotAction finalDecision = DecisionEngine.DecideRobotAction(recentReadings, sensorHistory);
            Console.WriteLine($"Robot Action : {finalDecision}");

            


        }
    }
}
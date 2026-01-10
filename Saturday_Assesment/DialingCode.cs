
using System;
using System.Collections.Generic;

namespace DialingCodesApp
{
    public static class DialingCodes
    {
        public static Dictionary<int, string> GetEmptyDictionary()
        {
            return new Dictionary<int, string>();
        }


        public static Dictionary<int, string> GetExistingDictionary()
        {
            return new Dictionary<int, string>
            {
                {1, "United States of America"},
                {55, "Brazil"},
                {91, "India"}
            };
        }


        public static Dictionary<int, string> AddCountryToEmptyDictionary(int countryCode, string countryName)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict.Add(countryCode, countryName);
            return dict;
        }


        public static Dictionary<int, string> AddCountryToExistingDictionary(Dictionary<int, string> existingDictionary, int countryCode, string countryName)
        {
            existingDictionary[countryCode] = countryName;
            return existingDictionary;
        }
        
        public static string GetCountryNameFromDictionary(Dictionary<int, string> existingDictionary, int countryCode)
        {
            if(existingDictionary.ContainsKey(countryCode))
            {
                return existingDictionary[countryCode];
            }
            return string.Empty;
        }


        public static bool CheckCodeExists(Dictionary<int, string> existingDictionary, int countryCode)
        {
            return existingDictionary.ContainsKey(countryCode);
        }


        public static Dictionary<int, string> UpdateDictionary(Dictionary<int, string> existingDictionary, int countryCode, string countryName)
        {
            if(existingDictionary.ContainsKey(countryCode))
            {
                existingDictionary[countryCode] = countryName;
            }
            return existingDictionary;
        }


        public static Dictionary<int, string> RemoveCountryFromDictionary(Dictionary<int, string> existingDictionary, int countryCode)
        {
            if(existingDictionary.ContainsKey(countryCode))
            {
                existingDictionary.Remove(countryCode);
            }
            return existingDictionary;
        }


        public static string FindLongestCountryName(Dictionary<int, string> existingDictionary)
        {
            string longestName = "";
            foreach(var item in existingDictionary.Values)
            {
                if(item.Length > longestName.Length)
                {
                    longestName = item;
                }
            }
            return longestName;
        }


        public static void PrintDictionary(Dictionary<int, string> dictionary)
        {
            foreach (var item in dictionary)
            {
                Console.WriteLine($"Code: {item.Key}, Country: {item.Value}");
            }
        }

    }


    public class TeleCaller
    {
        public static void TeleCallerM()
        {
            // task 1
            Dictionary<int, string> emptyDict = DialingCodes.GetEmptyDictionary(); 
            Console.WriteLine($"Empty Dictionary Count: {emptyDict.Count}");

            // task 2 
            Dictionary<int, string> existingDict = DialingCodes.GetExistingDictionary();
            Console.WriteLine("\nExisting Dictionary:");
            DialingCodes.PrintDictionary(existingDict);

            // task 3
            Dictionary<int, string> singleEntryDict = DialingCodes.AddCountryToEmptyDictionary(81,"Japan");
            Console.WriteLine("\nSingle Entry Dictionary:");
            DialingCodes.PrintDictionary(singleEntryDict);

            // task 4
            DialingCodes.AddCountryToExistingDictionary(existingDict, 44, "United Kingdom");
            // DialingCodes.PrintDictionary(existingDict);

            // task 5
            string country = DialingCodes.GetCountryNameFromDictionary(existingDict, 91);
            Console.WriteLine($"\nCountry for code 91: {country}");

            // task 6
            bool exist = DialingCodes.CheckCodeExists(existingDict, 911);
            Console.WriteLine($"Does code 911 exist? {exist}");

            // task 7
            DialingCodes.UpdateDictionary(existingDict, 91, "New India");
            // DialingCodes.PrintDictionary(existingDict);

            // task 8
            DialingCodes.RemoveCountryFromDictionary(existingDict, 91);
            // DialingCodes.PrintDictionary(existingDict);

            // task 9
            string longestName = DialingCodes.FindLongestCountryName(existingDict);
            Console.WriteLine($"\nLongest Country Name: {longestName}");
        }
    }
}

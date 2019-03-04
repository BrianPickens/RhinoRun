using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Utilities
{
    public static class Utility
    {
        public static List<string> ConvertStringToStringList(string _string)
        {
            List<string> stringList = new List<string>();

            string[] splitString = _string.Split(new string[] { "," }, StringSplitOptions.None);
            foreach (string newString in splitString)
            {
                stringList.Add(newString);
            }

            return stringList;
        }

        public static string ConvertStringListToString(List<string> _stringList)
        {
            string seperator = ",";
            string resultString = "";

            resultString = string.Join(seperator, _stringList.ToArray());

            return resultString;
        }

        public static int StringToInt(string _string)
        {
            int newInt = 0;

            try
            {
                newInt = Convert.ToInt32(_string);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Trying to convert string to int, but value is not an int");
                Debug.Log(e);
            }

            return newInt;
        }

        public static string IntToString(int _int)
        {
            string newString = "";
            newString = _int.ToString();
            return newString;
        }
    }
}

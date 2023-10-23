/* 
 
YOU ARE NOT ALLOWED TO MODIFY ANY FUNCTION DEFINATION's PROVIDED.
WRITE YOUR CODE IN THE RESPECTIVE QUESTION FUNCTION BLOCK


*/

using System.Text;
using System.Text.RegularExpressions;

namespace ISM6225_Fall_2023_Assignment_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Question 1:
            Console.WriteLine("Question 1:");
            int[] nums1 = { 0,1, 3, 50,75 };
            int upper = 99, lower = 0;
            IList<IList<int>> missingRanges = FindMissingRanges(nums1, lower, upper);
            string result = ConvertIListToNestedList(missingRanges);
            Console.WriteLine(result);
            Console.WriteLine();
            Console.WriteLine();

            //Question2:
            Console.WriteLine("Question 2");
            string parenthesis = "()[]{}";
            bool isValidParentheses = IsValid(parenthesis);
            Console.WriteLine(isValidParentheses);
            Console.WriteLine();
            Console.WriteLine();

            //Question 3:
            Console.WriteLine("Question 3");
            int[] prices_array = { 7, 6, 4, 3, 1 };
            int max_profit = MaxProfit(prices_array);
            Console.WriteLine(max_profit);
            Console.WriteLine();
            Console.WriteLine();

            //Question 4:
            Console.WriteLine("Question 4");
            string s1 = "8";
            bool IsStrobogrammaticNumber = IsStrobogrammatic(s1);
            Console.WriteLine(IsStrobogrammaticNumber);
            Console.WriteLine();
            Console.WriteLine();

            //Question 5:
            Console.WriteLine("Question 5");
            int[] numbers = { 1, 1, 1, 1 };
            int noOfPairs = NumIdenticalPairs(numbers);
            Console.WriteLine(noOfPairs);
            Console.WriteLine();
            Console.WriteLine();

            //Question 6:
            Console.WriteLine("Question 6");
            int[] maximum_numbers = { 3,2,1};
            int third_maximum_number = ThirdMax(maximum_numbers);
            Console.WriteLine(third_maximum_number);
            Console.WriteLine();
            Console.WriteLine();

            //Question 7:
            Console.WriteLine("Question 7:");
            string currentState = "+++";
            IList<string> combinations = GeneratePossibleNextMoves(currentState);
            string combinationsString = ConvertIListToArray(combinations);
            Console.WriteLine(combinationsString);
            Console.WriteLine();
            Console.WriteLine();

            //Question 8:
            Console.WriteLine("Question 8:");
            string longString = "hello how are you";
            string longStringAfterVowels = RemoveVowels(longString);
            Console.WriteLine(longStringAfterVowels);
            Console.WriteLine();
            Console.WriteLine();
        }

        /*
        
        Question 1:
        You are given an inclusive range [lower, upper] and a sorted unique integer array nums, where all elements are within the inclusive range. A number x is considered missing if x is in the range [lower, upper] and x is not in nums. Return the shortest sorted list of ranges that exactly covers all the missing numbers. That is, no element of nums is included in any of the ranges, and each missing number is covered by one of the ranges.
        Example 1:
        Input: nums = [0,1,3,50,75], lower = 0, upper = 99
        Output: [[2,2],[4,49],[51,74],[76,99]]  
        Explanation: The ranges are:
        [2,2]
        [4,49]
        [51,74]
        [76,99]
        Example 2:
        Input: nums = [-1], lower = -1, upper = -1
        Output: []
        Explanation: There are no missing ranges since there are no missing numbers.

        Constraints:
        -109 <= lower <= upper <= 109
        0 <= nums.length <= 100
        lower <= nums[i] <= upper
        All the values of nums are unique.

        Time complexity: O(n), space complexity:O(1)
        */

        public static IList<IList<int>> FindMissingRanges(int[] numbers, int lowerBound, int upperBound)
        {
            try
            {
                // Ensuring that the constraint is met: -109 <= lowerBound <= upperBound <= 109
                if (lowerBound > upperBound || lowerBound < -109 || upperBound > 109)
                {
                    throw new ArgumentException("Invalid lower or upper bounds.");
                }

                // Ensuring that the length of the array is between 0 and 100 (inclusive)
                if (numbers.Length > 100 || numbers.Length < 0)
                {
                    throw new ArgumentException("The length of the numbers array should be between 0 and 100");
                }

                // Handle duplicate values
                HashSet<int> uniqueValues = new HashSet<int>();
                foreach (int num in numbers)
                {
                    if (!uniqueValues.Add(num))
                    {
                        throw new ArgumentException("Duplicate values found in the numbers array.");
                    }
                }

                IList<IList<int>> result = new List<IList<int>>();

                // Check if the input array 'numbers' is empty or null
                if (numbers == null || numbers.Length == 0)
                {
                    if (lowerBound == upperBound)
                    {
                        // If the lower and upper bounds are the same, add a single-element range
                        result.Add(new List<int> { lowerBound });
                    }
                    else
                    {
                        // If the lower and upper bounds are different, add a range spanning from lowerBound to upperBound
                        result.Add(new List<int> { lowerBound, upperBound });
                    }
                }
                else
                {
                    // Check if there is a missing range before the first element
                    if (lowerBound < numbers[0])
                    {
                        if (lowerBound == numbers[0] - 1)
                        {
                            // If the missing range is just one element, add it to 'result'
                            result.Add(new List<int> { lowerBound });
                        }
                        else
                        {
                            // If the missing range is more than one element, add it as a range
                            result.Add(new List<int> { lowerBound, numbers[0] - 1 });
                        }
                    }

                    for (int i = 1; i < numbers.Length; i++)
                    {
                        long diff = (long)numbers[i] - numbers[i - 1];
                        if (diff > 1)
                        {
                            if (diff == 2)
                            {
                                // If there is exactly one missing element, add it to 'result'
                                result.Add(new List<int> { numbers[i - 1] + 1 });
                            }
                            else
                            {
                                // If there is more than one missing element, add the range to 'result'
                                result.Add(new List<int> { numbers[i - 1] + 1, numbers[i] - 1 });
                            }
                        }
                    }

                    // Check if there is a missing range after the last element
                    if (upperBound > numbers[numbers.Length - 1])
                    {
                        if (upperBound == numbers[numbers.Length - 1] + 1)
                        {
                            // If the missing range is just one element, add it to 'result'
                            result.Add(new List<int> { upperBound });
                        }
                        else
                        {
                            // If the missing range is more than one element, add it as a range
                            result.Add(new List<int> { numbers[numbers.Length - 1] + 1, upperBound });
                        }
                    }
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }



        /*
         
        Question 2

        Given a string s containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid.An input string is valid if:
        Open brackets must be closed by the same type of brackets.
        Open brackets must be closed in the correct order.
        Every close bracket has a corresponding open bracket of the same type.
 
        Example 1:

        Input: s = "()"
        Output: true
        Example 2:

        Input: s = "()[]{}"
        Output: true
        Example 3:

        Input: s = "(]"
        Output: false

        Constraints:

        1 <= s.length <= 104
        s consists of parentheses only '()[]{}'.

        Time complexity:O(n^2), space complexity:O(1)
        */

        public static bool IsValid(string s)
        {
            try
            {
                if (s.Length < 1 || s.Length > 104)
                {
                    throw new ArgumentException("Invalid input: s length out of bounds");
                }

                Stack<char> stack = new Stack<char>();

                foreach (char c in s)
                {
                    if (c == '(' || c == '[' || c == '{')
                    {
                        stack.Push(c);
                    }
                    else if (c == ')' || c == ']' || c == '}')
                    {
                        if (stack.Count == 0)
                        {
                            return false;
                        }

                        char top = stack.Pop();

                        if ((c == ')' && top != '(') || (c == ']' && top != '[') || (c == '}' && top != '{'))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Invalid input: s contains characters other than '()[]{}'");
                    }
                }

                if (stack.Count > 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }




        /*

        Question 3:
        You are given an array prices where prices[i] is the price of a given stock on the ith day.You want to maximize your profit by choosing a single day to buy one stock and choosing a different day in the future to sell that stock.Return the maximum profit you can achieve from this transaction. If you cannot achieve any profit, return 0.
        Example 1:
        Input: prices = [7,1,5,3,6,4]
        Output: 5
        Explanation: Buy on day 2 (price = 1) and sell on day 5 (price = 6), profit = 6-1 = 5.
        Note that buying on day 2 and selling on day 1 is not allowed because you must buy before you sell.

        Example 2:
        Input: prices = [7,6,4,3,1]
        Output: 0
        Explanation: In this case, no transactions are done and the max profit = 0.
 
        Constraints:
        1 <= prices.length <= 105
        0 <= prices[i] <= 104

        Time complexity: O(n), space complexity:O(1)
        */

        public static int MaxProfit(int[] prices)
        {
            try
            {
                int maxProfit = 0;
                int minPrice = int.MaxValue;

                foreach (int price in prices)
                {
                    if (price < minPrice)
                    {
                        minPrice = price; // Update the minimum price
                    }
                    else if (price - minPrice > maxProfit)
                    {
                        maxProfit = price - minPrice; // Update the maximum profit
                    }
                }

                return maxProfit;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /*
        
        Question 4:
        Given a string num which represents an integer, return true if num is a strobogrammatic number.A strobogrammatic number is a number that looks the same when rotated 180 degrees (looked at upside down).
        Example 1:

        Input: num = "69"
        Output: true
        Example 2:

        Input: num = "88"
        Output: true
        Example 3:

        Input: num = "962"
        Output: false

        Constraints:
        1 <= num.length <= 50
        num consists of only digits.
        num does not contain any leading zeros except for zero itself.

        Time complexity:O(n), space complexity:O(1)
        */

        public static bool IsStrobogrammatic(string num)
        {
            try
            {
                // Create a dictionary to map strobogrammatic pairs
                Dictionary<char, char> strobogrammaticMap = new Dictionary<char, char>
        {
            {'0', '0'},
            {'1', '1'},
            {'6', '9'},
            {'8', '8'},
            {'9', '6'}
        };

                int left = 0;
                int right = num.Length - 1;

                while (left <= right)
                {
                    char leftChar = num[left];
                    char rightChar = num[right];

                    if (!strobogrammaticMap.ContainsKey(leftChar) || strobogrammaticMap[leftChar] != rightChar)
                    {
                        return false;
                    }

                    left++;
                    right--;
                }

                // Ensure that there are no leading zeros (except for zero itself)
                if (num.Length > 1 && num[0] == '0')
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }





        /*

        Question 5:
        Given an array of integers nums, return the number of good pairs.A pair (i, j) is called good if nums[i] == nums[j] and i < j. 

        Example 1:

        Input: nums = [1,2,3,1,1,3]
        Output: 4
        Explanation: There are 4 good pairs (0,3), (0,4), (3,4), (2,5) 0-indexed.
        Example 2:

        Input: nums = [1,1,1,1]
        Output: 6
        Explanation: Each pair in the array are good.
        Example 3:

        Input: nums = [1,2,3]
        Output: 0

        Constraints:

        1 <= nums.length <= 100
        1 <= nums[i] <= 100

        Time complexity:O(n), space complexity:O(n)

        */

        public static int NumIdenticalPairs(int[] nums)
        {
            try
            {
                int[] count = new int[101]; // As per the constraints (1 <= nums[i] <= 100)

                foreach (int num in nums)
                {
                    count[num]++;
                }

                int goodPairs = 0;

                for (int i = 1; i <= 100; i++)
                {
                    if (count[i] > 1)
                    {
                        goodPairs += (count[i] * (count[i] - 1)) / 2;
                    }
                }

                return goodPairs;
            }
            catch (Exception)
            {
                throw;
            }
        }



        /*
        Question 6

        Given an integer array nums, return the third distinct maximum number in this array. If the third maximum does not exist, return the maximum number.

        Example 1:

        Input: nums = [3,2,1]
        Output: 1
        Explanation:
        The first distinct maximum is 3.
        The second distinct maximum is 2.
        The third distinct maximum is 1.
        Example 2:

        Input: nums = [1,2]
        Output: 2
        Explanation:
        The first distinct maximum is 2.
        The second distinct maximum is 1.
        The third distinct maximum does not exist, so the maximum (2) is returned instead.
        Example 3:

        Input: nums = [2,2,3,1]
        Output: 1
        Explanation:
        The first distinct maximum is 3.
        The second distinct maximum is 2 (both 2's are counted together since they have the same value).
        The third distinct maximum is 1.
        Constraints:

        1 <= nums.length <= 104
        -231 <= nums[i] <= 231 - 1

        Time complexity:O(nlogn), space complexity:O(n)
        */
        public static int ThirdMax(int[] nums)
        {
            try
            {
                long max1 = long.MinValue;  // Initialize to the smallest possible value
                long max2 = long.MinValue;
                long max3 = long.MinValue;

                foreach (int num in nums)
                {
                    if (num < -231 || num > 230)
                    {
                        throw new ArgumentException("Invalid input: num[i] is out of range.");
                    }

                    if (num == max1 || num == max2 || num == max3)
                    {
                        continue;  // Skip duplicates
                    }

                    if (num > max1)
                    {
                        max3 = max2;
                        max2 = max1;
                        max1 = num;
                    }
                    else if (num > max2)
                    {
                        max3 = max2;
                        max2 = num;
                    }
                    else if (num > max3)
                    {
                        max3 = num;
                    }
                }

                // If a third maximum exists, return it; otherwise, return the maximum
                return max3 == long.MinValue ? (int)max1 : (int)max3;
            }
            catch (Exception)
            {
                throw;
            }
        }




        /*
        
        Question 7:

        You are playing a Flip Game with your friend. You are given a string currentState that contains only '+' and '-'. You and your friend take turns to flip two consecutive "++" into "--". The game ends when a person can no longer make a move, and therefore the other person will be the winner.Return all possible states of the string currentState after one valid move. You may return the answer in any order. If there is no valid move, return an empty list [].
        Example 1:
        Input: currentState = "++++"
        Output: ["--++","+--+","++--"]
        Example 2:

        Input: currentState = "+"
        Output: []
 
        Constraints:
        1 <= currentState.length <= 500
        currentState[i] is either '+' or '-'.

        Timecomplexity:O(n), Space complexity:O(n)
        */
        public static IList<string> GeneratePossibleNextMoves(string currentState)
        {
            try
            {
                if (string.IsNullOrEmpty(currentState) || currentState.Length > 500 || !currentState.All(c => c == '+' || c == '-'))
                {
                    throw new ArgumentException("Invalid input");
                }

                IList<string> possibleMoves = new List<string>();

                char[] currentStateArray = currentState.ToCharArray();

                for (int i = 0; i < currentStateArray.Length - 1; i++)
                {
                    if (currentStateArray[i] == '+' && currentStateArray[i + 1] == '+')
                    {
                        // Flip "++" to "--"
                        currentStateArray[i] = '-';
                        currentStateArray[i + 1] = '-';
                        possibleMoves.Add(new string(currentStateArray));
                        // Revert the change for backtracking
                        currentStateArray[i] = '+';
                        currentStateArray[i + 1] = '+';
                    }
                }

                return possibleMoves;
            }
            catch (Exception)
            {
                throw;
            }
        }





        /*

        Question 8:

        Given a string s, remove the vowels 'a', 'e', 'i', 'o', and 'u' from it, and return the new string.
        Example 1:

        Input: s = "leetcodeisacommunityforcoders"
        Output: "ltcdscmmntyfrcdrs"

        Example 2:

        Input: s = "aeiou"
        Output: ""

        Timecomplexity:O(n), Space complexity:O(n)
        */

        public static string RemoveVowels(string s)
        {
            // Use a regular expression to remove vowels 'a', 'e', 'i', 'o', and 'u'
            return Regex.Replace(s, "[aeiouAEIOU]", "");
        }


        /* Inbuilt Functions - Don't Change the below functions */
        static string ConvertIListToNestedList(IList<IList<int>> input)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("["); // Add the opening square bracket for the outer list

            for (int i = 0; i < input.Count; i++)
            {
                IList<int> innerList = input[i];
                sb.Append("[" + string.Join(",", innerList) + "]");

                // Add a comma unless it's the last inner list
                if (i < input.Count - 1)
                {
                    sb.Append(",");
                }
            }

            sb.Append("]"); // Add the closing square bracket for the outer list

            return sb.ToString();
        }


        static string ConvertIListToArray(IList<string> input)
        {
            // Create an array to hold the strings in input
            string[] strArray = new string[input.Count];

            for (int i = 0; i < input.Count; i++)
            {
                strArray[i] = "\"" + input[i] + "\""; // Enclose each string in double quotes
            }

            // Join the strings in strArray with commas and enclose them in square brackets
            string result = "[" + string.Join(",", strArray) + "]";

            return result;
        }
    }
}

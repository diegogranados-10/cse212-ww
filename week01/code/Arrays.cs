public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Step 1: Create a new array of type double with size equal to 'length'
        // This array will store all the multiples we need to generate
        double[] multiples = new double[length];

        // Step 2: Use a for loop to iterate through each position in the array
        // The loop will run 'length' times, from index 0 to (length - 1)
        for (int i = 0; i < length; i++)
        {
            // Step 3: Calculate the multiple for the current position
            // Formula: number * (i + 1)
            // We use (i + 1) because:
            //   - When i = 0, we want the 1st multiple: number * 1
            //   - When i = 1, we want the 2nd multiple: number * 2
            //   - When i = 2, we want the 3rd multiple: number * 3
            //   - And so on...
            // Example: If number = 3 and i = 0, then 3 * (0 + 1) = 3
            //          If number = 3 and i = 1, then 3 * (1 + 1) = 6
            multiples[i] = number * (i + 1);

            // Step 4: The calculated multiple is now stored in the array at position i
            // The loop continues until all positions in the array are filled
        }

        // Step 5: Return the completed array containing all the multiples
        // Example result for MultiplesOf(3, 5): {3, 6, 9, 12, 15}
        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // PLAN: Rotating right means taking elements from the end and moving them to the beginning
        // Example: {1, 2, 3, 4, 5, 6, 7, 8, 9} rotated right by 3
        //          Take last 3: {7, 8, 9}
        //          Take remaining: {1, 2, 3, 4, 5, 6}
        //          Result: {7, 8, 9, 1, 2, 3, 4, 5, 6}

        // Step 1: Calculate the split point (where to divide the list)
        // The split point is where the "remaining" portion starts
        // Formula: data.Count - amount
        // Example: If list has 9 elements and amount is 3, split point is 9 - 3 = 6
        //          This means index 6 is where we start taking elements to move to the front
        int splitPoint = data.Count - amount;

        // Step 2: Extract the "tail" portion (elements that will move to the front)
        // Use GetRange(startIndex, count) to get elements from splitPoint to the end
        // GetRange parameters:
        //   - startIndex: splitPoint (where the tail begins)
        //   - count: amount (how many elements to take from the end)
        // Example: For {1, 2, 3, 4, 5, 6, 7, 8, 9} with splitPoint=6 and amount=3
        //          GetRange(6, 3) returns {7, 8, 9}
        List<int> tail = data.GetRange(splitPoint, amount);

        // Step 3: Extract the "head" portion (elements that will stay but move to the right)
        // Use GetRange(startIndex, count) to get elements from the beginning to splitPoint
        // GetRange parameters:
        //   - startIndex: 0 (start from the beginning)
        //   - count: splitPoint (how many elements to take from the beginning)
        // Example: For {1, 2, 3, 4, 5, 6, 7, 8, 9} with splitPoint=6
        //          GetRange(0, 6) returns {1, 2, 3, 4, 5, 6}
        List<int> head = data.GetRange(0, splitPoint);

        // Step 4: Clear the original list
        // We need to empty it so we can rebuild it in the new order
        // After this step, data will be an empty list: {}
        data.Clear();

        // Step 5: Add the tail portion first (elements that rotated to the front)
        // AddRange adds all elements from the tail list to data
        // Example: data becomes {7, 8, 9}
        data.AddRange(tail);

        // Step 6: Add the head portion second (elements that moved to the back)
        // AddRange adds all elements from the head list to the end of data
        // Example: data becomes {7, 8, 9, 1, 2, 3, 4, 5, 6}
        data.AddRange(head);

        // Step 7: The list has now been rotated right by 'amount' positions
        // The function modifies the original list (no return needed)
        // Final result example: {7, 8, 9, 1, 2, 3, 4, 5, 6}
    }
}

public static class ComplexStack {
    
    public static void Main()
    {
        var lines = new string[]
        {
            "(a == 3 or (b == 5 and c == 6))",
            "(students]i].Grade > 80 and students[i].Grade < 90)",
            "(robot[id + 1].Execute(.Pass() || (!robot[id * (2 + i)].Alive && stormy) || (robot[id - 1].Alive && lavaFlowing))",

        };

        foreach (var line in lines)
        {
            var result = DoSomethingComplicated(line);
            Console.WriteLine($"{line} -> {result}");
        }
    }
    public static bool DoSomethingComplicated(string line)
    {
        var stack = new Stack<char>();
        foreach (var item in line)
        {
            if (item is '(' or '[' or '{')
            {
                stack.Push(item);
            }
            else if (item is ')')
            {
                if (stack.Count == 0 || stack.Pop() != '(')
                    return false;
            }
            else if (item is ']')
            {
                if (stack.Count == 0 || stack.Pop() != '[')
                    return false;
            }
            else if (item is '}')
            {
                if (stack.Count == 0 || stack.Pop() != '{')
                    return false;
            }
        }

        return stack.Count == 0;
    }
}
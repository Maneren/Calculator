using System;
using System.Collections.Generic;

namespace Calculator
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine(PrettifyArray(args));
      if (Array.Exists(args, x => x == "-d")) Test();
      else Run();
    }

    static void Test()
    {
      var input = "2+2^(1/2)";
      var result = RunOnce(input);
      Console.WriteLine(result);
    }

    static void Run()
    {
      while (true)
      {
        Console.Write("calc > ");
        string input = Console.ReadLine();
        if (input == "q") return;
        var result = RunOnce(input);
        if (result == null) continue;
        Console.WriteLine($"{input} = {result}");
      }
    }

#nullable enable
    static Values.IFraction? RunOnce(string input)
    {
      Token[] tokens = new Lexer(input).Tokenize();
      Console.WriteLine(PrettifyArray(tokens));
      if (tokens.Length < 1) return null;
      var parser = new Parser(tokens);
      var tree = parser.parse();
      Console.WriteLine(tree);
      return Interpreter.Visit(tree);
    }

    static string PrettifyArray(object[] array)
    {
      return "[" + string.Join(",", array) + "]";
    }
  }
}

using System;
using System.Collections.Generic;

public abstract class Shape
{
    public abstract double CalculateArea();

    public abstract bool IsValid();
}

public class Circle : Shape
{
    public double Radius { get; set; }

    public Circle(double radius)
    {
        Radius = radius;
    }

    public override bool IsValid()
    {
        return Radius > 0;
    }

    public override double CalculateArea()
    {
        if (!IsValid()) return 0;
        return Math.PI * Math.Pow(Radius, 2);
    }
}

public class Rectangle : Shape
{
    public double Length { get; set; }
    public double Width { get; set; }

    public Rectangle(double length, double width)
    {
        Length = length;
        Width = width;
    }

    public override bool IsValid()
    {
        return Length > 0 && Width > 0;
    }

    public override double CalculateArea()
    {
        if (!IsValid()) return 0;
        return Length * Width;
    }
}

public class Square : Rectangle
{
    public Square(double side) : base(side, side)
    {
    }
}

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
        List<Shape> shapes = new List<Shape>();

        for (int i = 0; i < 10; i++)
        {
            int shapeType = random.Next(3);
            Shape shape = null;

            switch (shapeType)
            {
                case 0:
                    shape = new Circle(random.NextDouble() * 10 + 0.1);
                    break;
                case 1:
                    shape = new Rectangle(random.NextDouble() * 10 + 0.1, random.NextDouble() * 10 + 0.1);
                    break;
                case 2:
                    shape = new Square(random.NextDouble() * 10 + 0.1);
                    break;
            }

            if (shape != null && shape.IsValid())
            {
                shapes.Add(shape);
            }
        }

        double totalArea = 0;
        Console.WriteLine("===== 随机生成的形状信息 =====");
        foreach (var s in shapes)
        {
            string shapeName = s.GetType().Name;
            double area = s.CalculateArea();
            Console.WriteLine($"{shapeName} - 面积: {area:F2}");
            totalArea += area;
        }

        Console.WriteLine($"\n10个合法形状的总面积: {totalArea:F2}");
        Console.ReadLine();
    }
}
.Net Core library example project for 2d figure area calculation

Usage example:

```C#
var triangle = new Triangle();

triangle.A = new Point2D(-1, -3);
triangle.B = new Point2D(3, 4);
triangle.C = new Point2D(5, -5);

Console.WriteLine("Triangle ([-1;-3][3;4][5;-5]) area: " + triangle.GetArea());
```

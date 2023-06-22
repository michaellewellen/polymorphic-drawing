namespace Poly;

public abstract class AbstractGraphic2D : IGraphic2D
{
    // returns true if the given point is within (including the border) the shape
    public abstract bool ContainsPoint(decimal x, decimal y);

    // The following indicate a (possiblly loose) bouding box for the element
    public abstract decimal LowerBoundX{get; protected set;}
    public abstract decimal UpperBoundX{get; protected set;}
    public abstract decimal LowerBoundY{get; protected set;}
    public abstract decimal UpperBoundY{get; protected set;}
    
    // The character to be displayed on cells within the image
    public char DisplayChar{get;set;}
    // The color of the character 
    public ConsoleColor ForegroundColor{get;set;}
    // the background color of the character
    public ConsoleColor BackgroundColor{get;set;}

    // displays the given list of shapes
    public static void Display(List<IGraphic2D> shapes)
    {
        bool skippedSome = false;
        foreach(IGraphic2D shape in shapes)
        {
            if(!shape.Display())
                skippedSome = true;                
        }
        if(skippedSome)
            Console.WriteLine("WARNING: some cells skipped because of too small buffer");
    }

    public bool Display()
    {
        Console.ForegroundColor = ForegroundColor;
        Console.BackgroundColor = BackgroundColor;
        int lowX = (int)decimal.Floor(LowerBoundX);
        int lowY = (int)decimal.Floor(LowerBoundY);
        int highX = (int)decimal.Floor(UpperBoundX);
        int highY = (int)decimal.Floor(UpperBoundY);
        bool skippedSome = false;
        for (int row = lowY; row <= highY;row ++ )
        {
            for (int col = lowX; col <=highX; col++)
            {
                if(ContainsPoint(col,row))
                {
                Console.SetCursorPosition(col,row);
                Console.Write(DisplayChar);
                }
                else
                    skippedSome = true;
            }
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Black;   
        Console.SetCursorPosition(0,0);
        return !skippedSome;
    }

}

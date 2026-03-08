namespace backend_challenge.Modules.refactor;

public class Calculator
{
    public int Calc(int x, int y, int z)
    {
        int result = 0;

        AdjustValues(ref x, ref y, ref z);
        result += ApplyFirstMultiplier(ref x, ref y, ref z);

        AdjustValues(ref x, ref y, ref z);
        result += ApplySecondMultiplier(ref x, ref y, ref z);

        return result;
    }

    private void AdjustValues(ref int x, ref int y, ref int z)
    {
        if (x > y)
        {
            x += 10;
            return;
        }

        if (y > z)
        {
            y -= 5;
            return;
        }

        z += 15;
    }

    private int ApplyFirstMultiplier(ref int x, ref int y, ref int z)
    {
        if (x > y)
        {
            x *= 2;
            return x;
        }

        if (y > z)
        {
            y *= 3;
            return y;
        }

        z *= 4;
        return z;
    }

    private int ApplySecondMultiplier(ref int x, ref int y, ref int z)
    {
        if (x < y)
        {
            x *= 2;
            return x;
        }

        if (y < z)
        {
            y *= 3;
            return y;
        }

        z *= 4;
        return z;
    }
}
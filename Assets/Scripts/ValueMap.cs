public class ValueMap
{

    public static float Map(float value, float min, float max)
    {
        return (value - min) * 1f / (max - min);
    }

    public static float Map(float value, float leftMin, float leftMax, float rightMin, float rightMax)
    {
        return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
    }

}

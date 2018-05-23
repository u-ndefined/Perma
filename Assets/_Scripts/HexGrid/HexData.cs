[System.Serializable]
public struct HexData
{
    public int light;
    public int humidity;
    public int energy;

    public HexData(int _light, int _humidity, int _energy)
    {
        light = _light;
        humidity = _humidity;
        energy = _energy;
    }

    public bool IsPositive()
    {
        if (light < 0)
        {
            return false;
        }
        if (humidity < 0)
        {
            return false;
        }
        if (energy < 0)
        {
            return false;
        }
        return true;
    }

    public static HexData operator +(HexData a, HexData b)
    {
        HexData result = new HexData();
        result.light = a.light + b.light;
        result.humidity = a.humidity + b.humidity;
        result.energy = a.energy + b.energy;
        return result;
    }

    public static HexData operator -(HexData a, HexData b)
    {
        HexData result = new HexData();
        result.light = a.light - b.light;
        result.humidity = a.humidity - b.humidity;
        result.energy = a.energy - b.energy;
        return result;
    }

    public override string ToString()
    {
        return "HexData: light = " + light + ", humidity = " + humidity + ", energy = " + energy;
    }

}
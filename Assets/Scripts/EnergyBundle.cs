using System;

[Serializable]
public class EnergyBundle
{
    public int blue;
    public int red;
    public int green;
	public int brown;
    public static EnergyBundle operator+ (EnergyBundle a, EnergyBundle b){
        EnergyBundle result;
		if (a is null){
            return b;
        }
        else {
            result = a;
        }
        if (b != null){
            result.blue += b.blue;
            result.red += b.red;
            result.green += b.green;
            result.brown += b.brown;
        }
        return result;
    }
    public static EnergyBundle operator- (EnergyBundle a, EnergyBundle b){
        EnergyBundle result;
		if (a is null){
            result = b;
            result.blue = -result.blue;
            result.red = -result.red;
            result.green = -result.green;
            result.brown = -result.brown;
            return result;
        }
        else {
            result = a;
        }
        if (b != null){
            result.blue -= b.blue;
            result.red -= b.red;
            result.green -= b.green;
            result.brown -= b.brown;
        }
        return result;
    }
    public static bool operator> (EnergyBundle a, EnergyBundle b){
		if (a is null || b is null){
            return false;
        }
        return (a.blue>b.blue && a.red > b.red && a.green > b.green && a.brown > b.brown);
    }
    public static bool operator>= (EnergyBundle a, EnergyBundle b){
		if (a is null || b is null){
            return false;
        }
        return (a.blue >= b.blue && a.red >= b.red && a.green >= b.green && a.brown >= b.brown);
    }
    public static bool operator<= (EnergyBundle a, EnergyBundle b){
		if (a is null || b is null){
            return false;
        }
        return (a.blue<=b.blue && a.red <= b.red && a.green <= b.green && a.brown <= b.brown);
    }
    public static bool operator< (EnergyBundle a, EnergyBundle b){
		if (a is null || b is null){
            return false;
        }
        return (a.blue < b.blue && a.red < b.red && a.green < b.green && a.brown < b.brown);
    }
}
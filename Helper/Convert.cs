namespace wxDCS_Injector.Helper
{
    static class Convert
    {
        internal static float KtsToMps(int kts) => kts * 0.514444F;

        internal static float InHgToMmHg(float inHg) => inHg * 25.399999F;

        internal static float FtToM(float ft) => ft / 3.280839F;

        internal static float MiToM(float mi) => mi * 1609.347219F;
    }
}

/// <summary>
/// Hash-based "noise" function implemented by Squirrel Eiseroh's GDC talk "Noise-Based RNG"
/// Convenience functions are provided to allow calls with up to 4 coordinates, and retrieve values in different ranges
/// </summary>
public static class SquirrelNoise {

    /// <summary>
    /// Fast 1D Noise Function
    /// </summary>
    /// <param name="position">Position along the noise stream for the given seed. Provided as an int for convenience, is reinterpreted as uint</param>
    /// <param name="seed">Seed of the noise stream, this should not change</param>
    /// <returns>Random value from 0 to 2^32 - 1</returns>
    public static uint Get1DNoiseUint(int position, uint seed = 0) {
        const uint BIT_NOISE1 = 0x68E31DA4;
        const uint BIT_NOISE2 = 0x85297A4D;
        const uint BIT_NOISE3 = 0x1B56C4E9;

        unchecked {
            uint mangledBits = (uint) position;
            mangledBits *= BIT_NOISE1;
            mangledBits += seed;
            mangledBits ^= (mangledBits >> 8);
            mangledBits += BIT_NOISE2;
            mangledBits ^= (mangledBits << 8);
            mangledBits *= BIT_NOISE3;
            mangledBits ^= (mangledBits >> 8);

            return mangledBits;
        }
    }

    /// <summary>
    /// Fast 2D Noise Function for convinience, combines X and Y
    /// </summary>
    /// <returns>Random value from 0 to 2^32 - 1</returns>
    public static uint Get2DNoiseUint(int posX, int posY, uint seed = 0) {
        return unchecked(Get1DNoiseUint(posX + posY * 27742151, seed));
    }

    /// <summary>
    /// Fast 3D Noise Function for convinience, combines X and Y
    /// </summary>
    /// <returns>Random value from 0 to 2^32 - 1</returns>
    public static uint Get3DNoiseUint(int posX, int posY, int posZ, uint seed = 0) {
        return unchecked(Get1DNoiseUint(posX + posY * 27833021 + posZ * 317130731, seed));
    }

    /// <summary>
    /// Fast 4D Noise Function for convinience, combines X and Y
    /// </summary>
    /// <returns>Random value from 0 to 2^32 - 1</returns>
    public static uint Get4DNoiseUint(int posX, int posY, int posZ, int posW, uint seed = 0) {
        return unchecked(Get1DNoiseUint(posX + posY * 29399999 + posZ * 325767523 + posW * 1495052261, seed));
    }

    public static float Get1DNoiseZeroToOne(int position, uint seed = 0) {
        return unchecked((Get1DNoiseUint(position, seed) - 1f) / (uint.MaxValue));
    }

    public static float Get2DNoiseZeroToOne(int posX, int posY, uint seed = 0) {
        return unchecked(Get1DNoiseUint(posX + posY * 27742151, seed) / (uint.MaxValue + 1f));
    }

    public static float Get3DNoiseZeroToOne(int posX, int posY, int posZ, uint seed = 0) {
        return unchecked(Get1DNoiseUint(posX + posY * 27833021 + posZ * 317130731, seed) / (uint.MaxValue + 1f));
    }

    public static float Get4DNoiseZeroToOne(int posX, int posY, int posZ, int posW, uint seed = 0) {
        return unchecked(Get1DNoiseUint(posX + posY * 29399999 + posZ * 325767523 + posW * 1495052261, seed) / (uint.MaxValue + 1f));
    }
    public static float Get1DNoiseNegativeOneToOne(int position, uint seed = 0) {
        return unchecked((Get1DNoiseUint(position, seed) / (float) uint.MaxValue) * 2 - 1);
    }

    public static float Get2DNoiseNegativeToOne(int posX, int posY, uint seed = 0) {
        return unchecked((Get1DNoiseUint(posX + posY * 27742151, seed) / (float)uint.MaxValue) * 2 - 1);
    }

    public static float Get3DNoiseNegativeToOne(int posX, int posY, int posZ, uint seed = 0) {
        return unchecked((Get1DNoiseUint(posX + posY * 27833021 + posZ * 317130731, seed) / (float)uint.MaxValue) * 2 - 1);
    }

    public static float Get4DNoiseNegativeToOne(int posX, int posY, int posZ, int posW, uint seed = 0) {
        return unchecked((Get1DNoiseUint(posX + posY * 29399999 + posZ * 325767523 + posW * 1495052261, seed) / (float)uint.MaxValue) * 2 - 1);
    }

    public static int Get1DNoiseInt(int position, uint seed = 0) {
        return unchecked((int) Get1DNoiseUint(position, seed));
    }

    public static int Get2DNoiseInt(int posX, int posY, uint seed = 0) {
        return unchecked((int)Get1DNoiseUint(posX + posY * 27742151, seed));
    }
    public static int Get3DNoiseInt(int posX, int posY, int posZ, uint seed = 0) {
        return unchecked((int)Get1DNoiseUint(posX + posY * 27833021 + posZ * 317130731, seed));
    }
    public static int Get4DNoiseInt(int posX, int posY, int posZ, int posW, uint seed = 0) {
        return unchecked((int)Get1DNoiseUint(posX + posY * 29399999 + posZ * 325767523 + posW * 1495052261, seed));
    }

}
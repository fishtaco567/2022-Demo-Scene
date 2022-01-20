using System;
using UnityEngine;

/// <summary>
/// Custom random class build on Squirrel Eiselroh's noise-based RNG
/// </summary>
public class SRandom {

    private uint seed;
    private int position;

    /// <summary>
    /// Initialize a new SRandom
    /// </summary>
    /// <param name="seed">Seed from which to generate the random stream</param>
    /// <param name="position">Initial position along the stream</param>
    public SRandom(uint seed, int position = 0) {
        this.seed = seed;
        this.position = position;
    }

    /// <summary>
    /// Reset the random to another seed
    /// </summary>
    /// <param name="seed">New seed</param>
    /// <param name="position">New position</param>
    public void ResetSeed(uint seed, int position = 0) {
        this.seed = seed;
        this.position = position;
    }

    /// <returns>The seed of this random generator</returns>
    public uint GetSeed() {
        return seed;
    }

    /// <summary>
    /// Sets the current position of the random generator. This can be used to rewind or fast-forward random sequences
    /// </summary>
    /// <param name="position">The new position</param>
    public void SetCurrentPosition(int position) {
        this.position = position;
    }

    /// <returns>The current position of the random generator</returns>
    public int GetCurrentPosition() {
        return position;
    }

    /// <summary>
    /// Generates next random uint
    /// </summary>
    /// <returns>Random integer from 0 - 2^32 - 1</returns>
    public uint RandomUInt() {
        return SquirrelNoise.Get1DNoiseUint(position++, seed);
    }

    /// <summary>
    /// Generates next random int
    /// </summary>
    /// <returns>Random integer from -2^31 - 2^31 - 1</returns>
    public int RandomInt() {
        return unchecked((int) SquirrelNoise.Get1DNoiseUint(position++, seed));
    }

    /// <summary>
    /// Generates next random integer less than a given value (exclusive)
    /// </summary>
    /// <param name="lessThan">The upper limit of the random integer (exclusive)</param>
    /// <returns>A random integer from 0 - (lessThan - 1)</returns>
    public int RandomIntLessThan(int lessThan) {
        if(lessThan == 0) {
            return 0;
        }
        int thing = (int)Math.Floor(SquirrelNoise.Get1DNoiseZeroToOne(position++, seed) * lessThan);
        return thing % lessThan;
    }

    /// <summary>
    /// Generates next random integer between a lower limit (inclusive) and an upper limit (inclusive)
    /// </summary>
    /// <param name="lowerInclusive">The lower limit (inclusive)</param>
    /// <param name="upperInclusive">The upper limit (inclusive)</param>
    /// <returns>Random integer between -lowerInclusive - upperInclusive</returns>
    public int RandomIntInRange(int lowerInclusive, int upperInclusive) {
        return (int)Math.Floor(SquirrelNoise.Get1DNoiseZeroToOne(position++, seed) * (upperInclusive - lowerInclusive + 1) + lowerInclusive);
    }

    /// <returns>Random number from 0 - 1</returns>
    public float RandomFloatZeroToOne() {
        return SquirrelNoise.Get1DNoiseZeroToOne(position++, seed);
    }

    /// <returns>Random number from -1 - 1</returns>
    public float RandomFloatNegativeOneToOne() {
        return SquirrelNoise.Get1DNoiseNegativeOneToOne(position++, seed);
    }

    /// <summary>
    /// Generates next random float between a lower limit (inclusive) and an upper limit (inclusive)
    /// </summary>
    /// <param name="lowerInclusive">The lower limit (inclusive)</param>
    /// <param name="upperInclusive">The upper limit (inclusive)</param>
    /// <returns></returns>
    public float RandomFloatInRange(float lowerInclusive, float upperInclusive) {
        return (SquirrelNoise.Get1DNoiseNegativeOneToOne(position++, seed) + 1) * ((upperInclusive - lowerInclusive) / 2) + lowerInclusive;
    }

    /// <summary>
    /// Rolls random chance for a given probability
    /// </summary>
    /// <param name="probabilityTrue">The probability that the roll is true</param>
    /// <returns>The outcome of the roll (true for success)</returns>
    public bool RandomChance(float probabilityTrue) {
        return SquirrelNoise.Get1DNoiseZeroToOne(position++, seed) < probabilityTrue;
    }

    /// <summary>
    /// Generates next random direction evenly distributed, with trig functions
    /// </summary>
    /// <returns>A random direction</returns>
    public Vector2 RandomDirection2D() {
        var theta = RandomFloatInRange(0, 2 * (float) Math.PI);
        return new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
    }

    /// <summary>
    /// Generates next random coordinates in a circle using rejection sampling
    /// </summary>
    /// <param name="radius">The radius of the circle</param>
    /// <returns>A random point within a circle of radius centered at 0, 0</returns>
    public Vector2 RandomInCircle(float radius) {
        float x = RandomFloatInRange(-radius, radius);
        float y = RandomFloatInRange(-radius, radius);
        float r2 = radius * radius;

        while(x * x + y * y > r2) {
            x = RandomFloatInRange(-radius, radius);
            y = RandomFloatInRange(-radius, radius);
        }

        return new Vector2(x, y);
    }

}

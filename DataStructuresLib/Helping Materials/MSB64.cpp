/**
 * Implementation of the highestOneBitIn function for 64-bit integers.
 *
 * At a high-level, this algorithm works by decomposing a 64-bit integer aprt
 * into a number of smaller pieces called blocks. A block is a consecutive
 * sequence of eight bits (a byte), but since this algorithm is a special case
 * of a more general algorithm that works on arbitrary word sizes, we retain
 * the terminology "block" in this discussion.
 *
 * There are several key subroutines that we'll be making use of in the course
 * of this algorithm. First, there's a "parallel comparison" operation. Imagine
 * we have a list of up to 8 pairs of 7-bit integers (a1, b1), (b1, b2), ...
 * , (h1, h2) that are packed into 64-bit machine words. We can perform a
 * constant number of machine word operations to determine which number in each
 * pair is larger. To do so, we'll form two 64-bit integers, as shown here:
 *
 *   1aaaaaaa1bbbbbbb1ccccccc1ddddddd1eeeeeee1fffffff1ggggggg1hhhhhhh
 *   0aaaaaaa0bbbbbbb0ccccccc0ddddddd0eeeeeee0fffffff0ggggggg0hhhhhhh
 *
 * Here, the top number represents the first element of each pair with ones
 * interspersed, and the bottom number represents the second element of each
 * pair with zeros interspersed. If we subtract this second number from the
 * first, think about what will happen for any pair of numbers. If the top
 * number is larger than the bottom number, then the subtraction will not need
 * to do any borrowing past the beginning of the number, and the 1 bit set up
 * at the front of the top number will be preserved. On the other hand, if the
 * bottom number is larger than the top, then the subtraction will need to
 * borrow from the 1 bit in front of the first number, stopping the subtraction
 * and clearing the 1 bit as a marker. As a result, the final number will have
 * the form
 *
 *   AxxxxxxxBxxxxxxxCxxxxxxxDxxxxxxxExxxxxxxFxxxxxxxGxxxxxxxHxxxxxxx
 *
 * Where A, B, C, ..., and H represent the result of each of the comparisons
 * made, and the x's are the leftover bits from the numbers. By then using
 * appropriate bitmasking, we can clear the x bits to form the number
 *
 *   A0000000B0000000C0000000D0000000E0000000F0000000G0000000H0000000,
 *
 * which consists of the results of all the comparisons with 0s interspersed.
 * We call this a "flag integer," since it consists of a set of spread out
 * flag values.
 *
 * In actual code, the way to form this value is to compute something of the
 * form
 *
 *                      ((First | OneMask) - Second) & OneMask
 *
 * which takes only a constant number of operations and is essentially a very
 * well-disguised set of parallel comparisons.
 *
 * Once we have this flag integer, we can perform several different operations
 * on it in order to get some useful property of those bits. There are two
 * different operations we'll use here: sketching, which compacts the flag
 * bits into a single block, and summing, which returns the sum of those bits.
 * Both of these techniques are based on the idea of stacking many copies of
 * the flag integer with different offsets and summing them up, which happens
 * to correspond to performing a multiplication with some other integer (wow!)
 *
 * To perform a sketch, we want to transform the 64-bit integer
 *
 *   A0000000B0000000C0000000D0000000E0000000F0000000G0000000H0000000
 *
 * into the 8-bit integer
 *
 *   ABCDEFGH
 *
 * To do this, we'll stack together several staggered copies of the integer as
 * shown here, then add them together. (For simplicity, we've only shown four
 * of the shifts)
 *
 *                             A0000000B0000000 ... G0000000H0000000
 *                      A0000000B0000000 ... G0000000H0000000
 *                           ...
 *          A0000000B0000000 ... G0000000H0000000 
 *   A0000000B0000000 ... G0000000H0000000
 *                             ^^^^ <-- Look here
 *   
 * Notice that at the indicated point, if we sum all these numbers up, we end
 * up with the bitsequence ABCDEFGH written out consecutively. If we therefore
 * add up these numbers, mask out all the remaining bits, and shift what's
 * left back to the start of the number, we'll end up with our bit pattern.
 *
 * The strategy shown above looks like it performs a lot of separate additions
 * and shifts, but we can combine this together into a single multiplication.
 * Notice that the top number is the original number, the one below that is the
 * original number shifted by 7 positions, the one after that the original
 * number shifted by 14 positions, etc. Since shifting by k positions is
 * equivalent to multiplying by 2^k, we can encode this series of shifts and
 * adds as multiplication by (2^0 + 2^7 + 2^14 + ... 2^49), which is a single
 * constant! In code, this looks like
 *
 *                       ((flags * multiplier) & mask) >> offset
 *
 * and corresponds to crunching all the bits together.
 *
 * The remaining operation, summing, uses a similar idea, but instead of
 * shifting the bits so that they all nicely settle into separate columns, we
 * instead stack them so that they overlap with one another and add into a
 * total. This looks like this:
 * 
 *                              A0000000B0000000 ... G0000000H0000000
 *                      A0000000B0000000 ... G0000000H0000000
 *                            ...
 *         A0000000B0000000 ... G0000000H0000000 
 * A0000000B0000000 ... G0000000H0000000
 *                           ^^^^ <-- Look here
 *
 * Notice that the bits A + B + C + D + E + F + G + H all stack at this point,
 * so the three indicated bits will contain their (4-bit) numerical sum.
 *
 * The above approach has the problem that the product would actually get
 * placed before the start of the 64-bit value, but this is easily corrected
 * by pulling off the A bit to leave behind seven 8-bit blocks and then adding
 * the A bit back in at the end. As above, this series of shifted additions can
 * be encoded in a single multiplication step (with 2^0 + 2^8 + ... + 2^48),
 * and we also get the nice property that the sum only takes up three bits, not
 * four. In code, this looks like this:
 *
 *                ((flags * multiplier) & mask) >> offset
 *
 * This is the same code pattern as before, just with different multipliers and
 * masks.
 *
 * The algorithm uses three subroutines - parallel comparison, sketching, and
 * summing - and their variants to extract all sorts of useful properties of
 * various integers.
 */

#include "MSB64.h"
using namespace std;

namespace {
  /* Given a 64-bit number with a flag bit at the start of each of the blocks,
   * returns an 8-bit number composed of those flag bits, in order.
   *
   * More specifically, given a 64-bit value
   *
   *  A0000000B0000000C0000000D0000000E0000000F0000000G0000000H0000000,
   *
   * the value returned is
   *
   *    ABCDEFGH
   *
   * As mentioned above, this uses a multiplication to shift several copies of
   * the bits into the proper place, then adds them together.
   */
  uint8_t sketchOf(uint64_t value) {
    /* Use a bit-spreading technique to push all of the result bits into the high
     * byte. Specifically, we want to add
     *
     *                      A0000000B0000000C0000000D0000000...
     *               A0000000B0000000C0000000D0000000...
     *        A0000000B0000000C0000000D0000000...
     * A0000000B0000000C0000000D0000000...
     *
     *                      ^^^^^^^^ <-- The value we want appears here.
     *
     * This means that the first block is in the right place, the second block
     * needs to be shifted by 7 bits, the third by 14 bits, etc. We can do this
     * by performing a multiplication with a number that has bits 0, 7, 14, 21,
     * etc. all set to 1. Once we're done, we can then mask off everything but
     * the high byte and shift everything else away.
     */
    const uint64_t kMultiplier = 0b0000000000000010000001000000100000010000001000000100000010000001;
    const uint64_t kMask       = 0b1111111100000000000000000000000000000000000000000000000000000000;
    const uint8_t  kShift      = 64 - 8;
    
    return ((value * kMultiplier) & kMask) >> kShift;
  }

  /* Given a 64-bit flag value, returns the number of flags set.
   *
   * More specifically, given a 64-bit value
   *
   *  A0000000B0000000C0000000D0000000E0000000F0000000G0000000H0000000,
   *
   * the value returned is
   *
   *    A + B + C + D + E + F + G + H
   */
  uint8_t sumOf(uint64_t value) {
    /* We're going to use another shift-based technique to superimpose copies of
     * the lower seven flags on top of one another. (The eighth flag we'll handle
     * as a special case.) This will have the effect of summing those flags up.
     *
     * The general shift pattern looks like this:
     *
     *                    B0000000C0000000D0000000...
     *            B0000000C0000000D0000000...
     *    B0000000C0000000D0000000...
     *                  ...
     *                  ^^^ <-- The flags all add together here.
     *                     
     * This means that we'll place a 0-shifted copy, an 8-shifted copy, a 16-
     * shifted copy, etc., which corresponds to a nice multiplication.
     *
     * We leave off the top bit because we want the product to end up fitting
     * at the top of a 64-bit machine word. We could avoid this special-casing
     * by splitting the product across two machine words (have the top half of
     * the flags get added in one word and the bottom half in another), but
     * the special casing makes things a bit cleaner.
     */
    const uint64_t kMultiplier = 0b0000000000000001000000010000000100000001000000010000000100000001;
    
    /* The bits will collide in the three bits starting at the start of the
     * second block. We'll put our mask there.
     */
    const uint64_t kMask       = 0b0000001110000000000000000000000000000000000000000000000000000000;
    
    /* Our shift value is the position of that mask, right before the start of
     * the second block.
     */
    const uint8_t  kShift      = 64 - 8 - 1;
    
    /* We left out the top bit, so we have to add it back in at the end. The
     * first value here is the parallel addition.
     */
    return (((value * kMultiplier) & kMask) >> kShift) + (value >> 63u);
  }

  /* Returns a bitmask where each block's high bit is 1 if that block contains a
   * 1 bit and is 0 otherwise. All remaining bits are 0.
   *
   * Stated differently, given the input
   *
   *   aaaaaaaabbbbbbbbccccccccddddddddeeeeeeeeffffffffgggggggghhhhhhhh
   *
   * We'll return a 64-bit flag integer
   *
   *   A0000000B0000000C0000000D0000000E0000000F0000000G0000000H0000000
   *
   * where each letter is 1 if any of the bits in the block were set and is
   * 0 otherwise.
   */
  uint64_t usedBlocksIn(uint64_t value) {
    /* Every block with a 1 bit set in it either
     *
     *  1. has its highest bit set, or
     *  2. when that bit is cleared, has a numeric value of 1 or greater.
     *
     * We can check for ths first part by using a bitmask to extract the high bits
     * from each of the blocks. The remainder can be identified by using the
     * parallel comparison technique of comparing each block against 1.
     */
     
    /* Positions of all the high bits within each block. */
    const uint64_t kHighBits = 0b1000000010000000100000001000000010000000100000001000000010000000;
    uint64_t highBitsSet = value & kHighBits;
    
    /* Now, do a parallel comparison on the 7-bit remainders of each block to
     * identify all the blocks with a nonzero bit set in them.
     *
     * The parallel comparison works as follows. We begin by reshaping the blocks
     * so that each block starts with a 1:
     *
     *   1aaaaaaa1bbbbbbb1ccccccc1ddddddd1eeeeeee1fffffff1ggggggg1hhhhhhh
     *
     * Now, subtract out the value with 1's at the bottom of each block:
     *
     *   1aaaaaaa 1bbbbbbb 1ccccccc 1ddddddd 1eeeeeee 1fffffff 1ggggggg 1hhhhhhh
     * - 00000001 00000001 00000001 00000001 00000001 00000001 00000001 00000001
     *
     * If a block is nonempty, then the subtraction will stop before hitting the
     * special 1 bit we placed at the front of the block. That 1 bit then means
     * "yes, there was some bit set here." Otherwise, if the block is empty, then
     * the subtraction within that block will be forced to borrow the 1 bit from
     * the flag, which means that the resulting 0 bit means "no, there was no bit
     * set here."
     *
     * We can therefore perform the subtraction, mask off all the bits except for
     * the flags, and we end up with what we're looking for.
     */
    const uint64_t kLowBits =  0b0000000100000001000000010000000100000001000000010000000100000001;
    uint64_t lowBitsSet  = ((value | kHighBits) - kLowBits) & kHighBits;
    
    /* Combine them together to find nonempty blocks. */
    return highBitsSet | lowBitsSet;
  }

  /* Given an 8-bit value, returns the index of the highest 1 bit within that
   * value.
   *
   * This subroutine is where much of the magic happens with regards to the
   * overall algorithm. The idea is that if we can get down to an eight-bit
   * number, we can manually check each power of two that could serve as the
   * most-significant bit. This is actually done using a clever parallel
   * comparison step, describe below.
   */
  uint8_t highestBitSetIn_8(uint8_t value) {
    /* We will again use the parallel comparison technique. To get everything to
     * fit cleanly into a machine word, we'll treat the 8-bit value as actually
     * being 7 bits, since if the top bit is set we immediately know the answer.
     *
     * As a result, our first step is to simply check if the highest bit is set
     * and immediately return the answer if it is.
     */
    if (value & 0b10000000) return 7;
    
    /* The main observation here is that the MSB of an integer is equal to the
     * number of powers of two less than or equal to it, minus one. As an
     * example, the number 00110110 has six powers of two less than or equal to
     * it (each of the powers of two that are less than or equal to 00100000).
     * Therefore, if we can count up how many powers of two are less than or
     * equal to our number, we can quickly determine the most-significant bit.
     *
     * Since we have a seven-bit number at this point, there are only seven
     * powers of two that we need to test, and we can test them all in parallel
     * using our lovely parallel comparison technique! We'll compare against
     * the numbers 1000000, 100000, 10000, 1000, 100, 10, and 1 all at the same
     * time by performing this subtraction, which is a parallel compare:
     *
     *   1aaaaaaa1aaaaaaa1aaaaaaa1aaaaaaa1aaaaaaa1aaaaaaa1aaaaaaa
     * - 01000000001000000001000000001000000001000000001000000001
     *
     * That bottom string is the concatenation of all the powers of two listed
     * above, padded with zeros betweeh the elements.
     */
    const uint64_t kComparator = 0b0000000001000000001000000001000000001000000001000000001000000001;
    
    /* We need to spray out seven copies of the value so that we can compare
     * multiple copies of it in parallel. To do this, we essentially want to make
     * a bunch of shifts and add them all together:
     *
     *                                                     aaaaaaaa
     *                                             aaaaaaaa
     *                                     aaaaaaaa
     *                             aaaaaaaa
     *                     aaaaaaaa
     *             aaaaaaaa
     *     aaaaaaaa
     *  + ---------------------------------------------------------
     *
     * This corresponds to a multiplication by 2^0 + 2^8 + 2^16 + 2^24 + ...
     */
    const uint64_t kSpreader   = 0b0000000000000001000000010000000100000001000000010000000100000001;
    
    /* As before, to make a parallel comparison, we're going to force a 1 bit at
     * the start of each block. (This is why we special-cased away the top bit of
     * the byte - we need to recycle that bit for other purposes.)
     */
    const uint64_t kMask       = 0b0000000010000000100000001000000010000000100000001000000010000000;
    
    /* Perform the parallel comparison:
     *
     *  1. Spray out multiple copies of the value.
     *  2. Put 1 bits at the front of each block.
     *  3. Do the subtraction with the comparator to see which powers of two
     *     we're bigger than.
     *  4. Mask off everything except the flag bits.
     */
    const uint64_t comparison = (((kSpreader * value) | kMask) - kComparator) & kMask;
    
    /* We now have a flag integer holding bits indicating which powers of two
     * are smaller than us. Summing up those flags using a summation operation
     * gives us the number we want!
     */
    return sumOf(comparison) - 1;
  }

  /* Given a 64-bit integer, returns the index of the block within that integer
   * that contains a 1 bit.
   *
   * Stated differently, given a 64-bit integer
   *
   *  aaaaaaaabbbbbbbbccccccccddddddddeeeeeeeeffffffffhhhhhhhh
   *
   * the algorithm will treat each block of eight bits as a unit and return the
   * index of the highest nonzero block.
   *
   * This is used as the first step in the algorithm, since once we know which
   * block to look inside of, we just need to find the MSB within that block.
   * Fortunately, we have an algorithm that, given a block, finds its MSB, and
   * so that doesn't leave much work for us to do!
   */
  uint8_t highestBlockSetIn(uint64_t value) {
    return highestBitSetIn_8(sketchOf(usedBlocksIn(value)));
  }
}

uint8_t highestOneBitIn(uint64_t value) {
  /* Step 1: Identify the index of the highest block with a 1 bit in it. */
  uint8_t highBlockIndex = highestBlockSetIn(value);
  
  /* Step 2: Identify the highest bit within that block. To do so, we're going
   * to shift that block down to the proper position and mask out the other
   * bits.
   */
  uint8_t highBlock = value >> (highBlockIndex * 8);
  return highBlockIndex * 8 + highestBitSetIn_8(highBlock);
}
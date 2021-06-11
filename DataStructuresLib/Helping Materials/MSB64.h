/**
 * File: MSB64.h
 * Author: Keith Schwarz (htiek@cs.stanford.edu)
 *
 * An implementation of Fredman and Willard's O(1)-time algorithm for finding
 * the most significant bit set in a machine word. This family of algorithms
 * works for any machine word size and has a runtime that is completely
 * independent of the word size.
 *
 * At a high level, the algorithm works as follows. Let w denote the number
 * of bits in a word (the word size). We first devise an algorithm that can
 * find the most-significant bit of a sqrt(w)-bit number. Then, we use it
 * as follows:
 *
 *   1. Break the input apart into sqrt(w) blocks of size sqrt(w) bits each.
 *   2. Find the index of the highest block containing at least one 1 bit.
 *   3. Look within that block, which has size sqrt(w), to find the high 1 bit.
 *   4. Combine the information from steps (2) and (3) together.
 *
 * The individual steps in the algorithm involve a number of very creative uses
 * of word-level parallelism (performing multiple comparisons in a single
 * subtraction, using a multiplication step to shift bits into specific places
 * or to add them together, etc.) that are detailed in the implementation file.
 *
 * This file contains an implementation of the algorithm for 64-bit words,
 * which have the nice property that they can be split apart into 8 blocks of
 * size 8 bits each.
 */

#ifndef MostSignificantBit_Included
#define MostSignificantBit_Included

#include <cstdint>

/**
 * Given a 64-bit value, returns the index of the highest-order bit in that
 * value. If the input is 0, the result is unspecified.
 *
 * @param value A 64-bit number.
 * @return The index of the highest-order bit.
 * @complexity: O(1).
 */
std::uint8_t highestOneBitIn(std::uint64_t value);

#endif

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaDecoder
{
    class RotorList : IList<Rotor>
    {
        public const int RotorCount = 3;

        readonly Rotor[] rotors;
        int count;
        
        public RotorList() { rotors = new Rotor[RotorCount]; count = 0; }
        public RotorList(Rotor[] rotors)
        {
            if (rotors.Length == RotorCount) { this.rotors = rotors; count = RotorCount; }
            else throw new ArgumentOutOfRangeException("Enigma machines must have ");
        }

        public Rotor this[int index]
        {
            get => rotors[index];
            set => rotors[index] = value;
        }

        public int Count => count;
        public bool IsReadOnly => false;
        public bool IsCompleted => count == RotorCount;

        public void Add(Rotor item)
        {
            if (IsCompleted) throw new NotSupportedException("Maximum amount of rotors reached.");
            else
            {
                rotors[count] = item;
                count++;
            }
        }

        public void Clear() => count = 0;

        public bool Contains(Rotor item)
        {
            for (int i = 0; i < count; i++)
                if (rotors[i].Equals(item)) return true;
            return false;
        }

        public void CopyTo(Rotor[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Transforms the input <see cref="char"/> to another <see cref="char"/> using the <see cref="Rotor"/>'s.
        /// </summary>
        /// <param name="input">The <see cref="char"/> to transform.</param>
        /// <returns>The resultant <see cref="char"/>.</returns>
        /// <exception cref="MissingFieldException">
        /// Throw when the <see cref="RotorList"/> is not <see cref="IsCompleted"/>;
        /// </exception>
        public char Encrypt(char input)
        {
            if (IsCompleted)
            {
                for (int i = 0; i < RotorCount; i++)
                {
                    input = rotors[i].Encrypt(input);
                    if (rotors[i].RotateNext)
                    {
                        if (i == RotorCount - 1) rotors[0].Rotate();
                        else rotors[i + 1].Rotate();
                    }
                    rotors[i].Rotate();
                }
                return input;
            }
            else throw new MissingFieldException("The list still needs additional rotors");
        }

        public IEnumerator<Rotor> GetEnumerator()
        {
            foreach (Rotor rotor in rotors)
               yield return rotor;
        }

        public int IndexOf(Rotor item)
        {
            for (int i = 0; i < count; i++)
                if (rotors[i].Equals(item)) return i;
            return -1;
        }

        public void Insert(int index, Rotor item) => this[index] = item;

        public bool Remove(Rotor item)
        {
            int index = IndexOf(item);
            if (index == -1) return false;
            else
            {
                RemoveAt(index);
                return true;
            }
        }

        public void RemoveAt(int index)
        {
            for (int a = index; a >= 0; a--)
                rotors[a] = rotors[a + 1];
            count--;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    struct Rotor
    {
        public const int MIN = 1;
        public const int MAX = 26;

        int position;
        int nugetPosition;

        public Rotor(int position, int nugetPosition)
        {
            if (position >= MIN && position <= MAX)
            {
                this.position = position;
                if (nugetPosition >= MIN && nugetPosition <= MAX) this.nugetPosition = nugetPosition;
                else throw new ArgumentException("The Rotor nuget position was outside the posible range.");
            }
            else throw new ArgumentException("The Rotor position was outside the posible range.");
        }

        /// <summary>
        /// Gets <see langword="true"/> if the next <see cref="Rotor"/> has to be rotated;
        /// otherwise, <see langword="false"/>.
        /// </summary>
        public bool RotateNext { get => position == nugetPosition; }
        public int Position { get => position; set => position = value; }
        public int NugetPosition { get => nugetPosition; set => nugetPosition = value; }

        /// <summary>
        /// Rotates the <see cref="Rotor"/> one position.
        /// </summary>
        public void Rotate() { position = position == MAX ? MIN : position + 1; }
        /// <summary>
        /// Transforms the input <see cref="char"/> to another <see cref="char"/>.
        /// </summary>
        /// <param name="input">The <see cref="char"/> to transform.</param>
        /// <returns>The resultant <see cref="char"/>.</returns>
        public char Encrypt(char input) => Int2Char(Char2Int(input) + position);

        /// <summary>
        /// Transforms a <see cref="Rotor"/> <see cref="int"/>to a readable <see cref="char"/>.
        /// <para>The result will be lowecase.</para>
        /// </summary>
        /// <param name="value">The <see cref="int"/> value to transform.</param>
        /// <returns>The resultant <see cref="char"/>.</returns>
        public static char Int2Char(int value) => (char)(value + 96);
        /// <summary>
        /// Transforms a readable <see cref="char"/> to a <see cref="Rotor"/> <see cref="int"/>.
        /// </summary>
        /// <param name="value">The readable <see cref="char"/> lowercase.</param>
        /// <returns>The resultant <see cref="int"/> value.</returns>
        public static int Char2Int(char value) => value - 96;
    }
}

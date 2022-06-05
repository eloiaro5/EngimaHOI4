using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaDecoder
{
    /// <summary>
    /// Class on which all enigma machines have to be based.
    /// </summary>
    abstract class BaseEnigma
    {
        protected RotorList rotors;
        protected Plugboard board;

        public Rotor this[int index] { get => rotors[index]; }
        public RotorList Rotors { get => rotors; }
        public Plugboard Board { get => board; }

        /// <summary>
        /// Transforms an input <see cref="char"/> to another <see cref="char"/>.
        /// </summary>
        /// <param name="input">The <see cref="char"/> to transform.</param>
        /// <returns>The resultant <see cref="char"/>.</returns>
        public abstract char Encrypt(char input);
    }
}

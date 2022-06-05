using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaDecoder
{
    /// <summary>
    /// Class to handle the Enigma simulation and emulate the general Enigma behaviour found in
    /// <see href="https://www.youtube.com/watch?v=ybkkiGtJmkM"/>.
    /// For more in depth simulations, please create the correspondandt Enigma class
    /// </summary>
    class EnigmaX : BaseEnigma
    {
        public EnigmaX(Plugboard board, RotorList rotors) { this.board = board; this.rotors = rotors; }

        public override char Encrypt(char input)
        {
            input = board.Transform(input);
            input = rotors.Encrypt(input);
            input = board.Transform(input);
            return input;
        }
    }
}

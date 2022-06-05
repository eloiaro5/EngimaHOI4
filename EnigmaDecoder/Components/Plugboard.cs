using System.Collections.Generic;

namespace EnigmaDecoder
{
    class Plugboard
    {
        List<Connection> connections;

        public Plugboard() { connections = new List<Connection>(); }
        public Plugboard(List<Connection> connections) { this.connections = connections; }

        public List<Connection> Connections { get => connections; set => connections = value; }
        public Connection this[int index] { get => connections[index]; set => connections[index] = value; }

        /// <summary>
        /// Search a <see cref="Connection"/> in the <see cref="Plugboard"/> regarding the input <see cref="char"/>.
        /// </summary>
        /// <param name="input">The <see cref="char"/> to search for.</param>
        /// <returns>
        /// <see langword="true"/> if has a connection with the input <see cref="char"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool HasConnection(char input) => connections.Exists(c => c.Input == input);
        /// <summary>
        /// Transforms the input <see cref="char"/> to another <see cref="char"/>
        /// using the correspondant <see cref="Connection"/>.
        /// </summary>
        /// <param name="input">The <see cref="char"/> to transform.</param>
        /// <returns>The resultant <see cref="char"/>.</returns>
        public char Transform(char input)
        {
            if (HasConnection(input)) return connections.Find(c => c.Input == input).Output;
            else return input;
        }
    }

    struct Connection
    {
        public char Input { get; set; }
        public char Output { get; set; }

        public Connection(char input, char output) { Input = input; Output = output; }
    }
}

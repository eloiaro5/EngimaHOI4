using System.Windows;

namespace EnigmaDecoder
{
    public partial class MainWindow : Window
    {
        BaseEnigma enigma;

        public MainWindow()
        {
            InitializeComponent();

            Init();
        }

        public void Init()
        {
            enigma = CreateInstanceX();
            WreckItRalph();
        }

        public void WreckItRalph()
        {
            WriteDebugAsync();
        }

        public void WriteDebugAsync()
        {
            Dispatcher.Invoke(() =>
            {
                //TODO: Write enigma settings
                if (enigma is EnigmaX) txtDebug.Text += "\t- New EnigmaX iteration\n";
                txtDebug.Text += "[Settings]\n";
                for (int i = 0; i < enigma.Rotors.Count; i++)
                {
                    txtDebug.Text += "Rotor " + i + ":\n";
                    txtDebug.Text += " · Position: " + enigma.Rotors[i].Position;
                    txtDebug.Text += " · Nuget Position: " + enigma.Rotors[i].NugetPosition;
                }
            });
        }

        /// <summary>
        /// Creates a new <see cref="EnigmaX"/> machine from a previous <see cref="EnigmaX"/> machine.
        /// </summary>
        /// <param name="previous">The <see cref="EnigmaX"/> machine to reorganize.</param>
        /// <returns>A new reorganized <see cref="EnigmaX"/> machine.</returns>
        EnigmaX CreateInstanceX(EnigmaX previous = null)
        {
            if (previous is null)
            {
                //Create a base Enigma machine with all the setting at base value
                RotorList rotors = new RotorList(
                    new Rotor[] {
                        new Rotor(Rotor.MIN, Rotor.MIN),
                        new Rotor(Rotor.MIN, Rotor.MIN),
                        new Rotor(Rotor.MIN, Rotor.MIN)
                    });
                return new EnigmaX(new Plugboard(), rotors);
            }
            else
            {
                //TODO: Control if rotors can't be advanced.
                //Advance the rotors. If rotors can't be advanced, set rotors to base value and advance the PlugBoard.
                Plugboard board = previous.Board;
                RotorList rotors = new RotorList();

                foreach (Rotor rotor in previous.Rotors)
                {
                    if (rotor.Position == Rotor.MAX) rotors.Add(new Rotor(Rotor.MIN, rotor.NugetPosition + 1));
                    else rotors.Add(new Rotor(rotor.Position + 1, rotor.NugetPosition));
                }

                return new EnigmaX(board, rotors);
            }
        }
    }
}

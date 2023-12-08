using BomberMansTCPFormsLibrary;
using BomberMansTCPFormsLibrary.GameObjects;

namespace BomberManClient
{
    public partial class ClientForm : Form
    {
        Random rnd = new Random();
        const string PlayerName = "RUs18Tam";

        public ClientForm()
        {
            InitializeComponent();

            var c = new Client();

            c.UpdatePlayersInfo = (info) =>
            {
                Invoke(() => BomberMansTCPHelper.UpdatePlayersListBox(
                    playersListBox, info));
            };

            c.Visualize = (map) =>
            {
                BomberMansTCPHelper.DrawMap(pictureBox, map, PlayerName, 24);
            };

            c.SendPlayerAction = DoWork;

            c.Connect("192.168.0.1:9000", PlayerName);
        }

        public PlayerAction DoWork(GameObject[,]? map)
        {
            return PlayerAction.Top;
        }
    }
}
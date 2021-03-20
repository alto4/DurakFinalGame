using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CardLib;

namespace CardBox
{
    ///
    public partial class CardBox: UserControl
    {
        // FIELDS AND PROPERTIES
        private PlayingCard myCard;
        public PlayingCard Card
        {
            set
            {
                myCard = value;
                pbMyPictureBox.Image = myCard.GetCardImage();
                UpdateCardImage();
            }
            get { return myCard;  }
        }

        public CardSuit Suit
        {
            set
            {
                Card.Suit = value;
                UpdateCardImage();
            }
            get { return Card.Suit; }
        }

        public CardRank Rank
        {
            set
            {
                Card.Rank = value;
                UpdateCardImage();
            }
            get { return Card.Rank; }
        }

        public bool FaceUp
        {
            set
            {
                if (myCard.FaceUp != value)
                {
                    myCard.FaceUp = value;

                    UpdateCardImage();
                    if (CardFlipped != null)
                    {
                        CardFlipped(this, new EventArgs());
                    }
                }
            }
            get { return Card.FaceUp; }
        }

        private Orientation myOrientation;
        public Orientation CardOrientation
        {
            set
            {
                if (myOrientation != value)
                {
                    myOrientation = value;
                    this.Size = new Size(Size.Height, Size.Width);
                    UpdateCardImage();
                }
            }
            get { return myOrientation; }
        }

        private void UpdateCardImage()
        {
            pbMyPictureBox.Image = myCard.GetCardImage();

            if (myOrientation == Orientation.Horizontal)
            {
                pbMyPictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
        }

        public CardBox()
        {
            InitializeComponent();
            myOrientation = Orientation.Vertical;
            myCard = new PlayingCard();
        }

        public CardBox(PlayingCard card, Orientation orientation = Orientation.Vertical)
        {
            InitializeComponent();
            myOrientation = orientation;
            myCard = new PlayingCard();
        }

        /// <summary>
        /// Return string summarizing the card's properties
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return myCard.ToString();
        }

        /// <summary>
        /// Initial load state of application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardBox_Load(object sender, EventArgs e)
        {
            UpdateCardImage();
        }

        public event EventHandler CardFlipped;
        /// <summary>
        /// Event delegation for card picture box click event
        /// </summary>
        new public event EventHandler Click;

        /// <summary>
        /// Event handler for when user clicks on the card picturebox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbMyPictureBox_Click(object sender, EventArgs e)
        {
            if (Click != null)
            {
                Click(this, e);
            }
        }
    }
}

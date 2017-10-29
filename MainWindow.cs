using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WearDataHelper
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            foreach (PictureBox pbPart in gbPhotos.Controls)
            {
                pbPart.Tag = new PumpAttributes();

                pbPart.AllowDrop = true;
                pbPart.SizeMode = PictureBoxSizeMode.StretchImage;
                pbPart.DragEnter += PbPart_DragEnter;
                pbPart.DragDrop += PbPart_DragDrop;
                pbPart.Click += pbPart_Click;
            }

            ((PumpAttributes)pbPart1.Tag).PartName = "VOLUTE LINER FRONT";
        }

        private void PbPart_DragDrop(object sender, DragEventArgs e)
        {
            PictureBox pbPart = sender as PictureBox;
            var files = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            PumpAttributes attributes = pbPart.Tag as PumpAttributes;
            attributes.ImageFilePath = files[0];
            pbPart.Image = Image.FromFile(attributes.ImageFilePath);
        }

        private void PbPart_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) ||
                e.Data.GetDataPresent(DataFormats.Bitmap, false) ||
                e.Data.GetDataPresent(DataFormats.Text, false))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void pbPart_Click(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            pb.BorderStyle = BorderStyle.Fixed3D;
            pgAttributes.SelectedObject = pb.Tag as PumpAttributes;
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            foreach (PictureBox pbPart in gbPhotos.Controls)
            {
                PumpAttributes attributes = pbPart.Tag as PumpAttributes;

                // 201705 Group 01-1 IMPELLER FRONT.JPG
                string fileNameNew = dtpOH.Value.ToString("yyyyMM") + " " + txtAssetNum.Text + ((PumpAttributes)pbPart.Tag).PartName;
            }
        }
    }
}
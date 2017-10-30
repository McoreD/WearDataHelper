using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WearDataHelper
{
    public partial class MainWindow : Form
    {
        private List<PumpAttributes> listAttributes = new List<PumpAttributes>();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            listAttributes.Add(new PumpAttributes() { PartName = "VOLUTE LINER FRONT" });
            listAttributes.Add(new PumpAttributes() { PartName = "VOLUTE LINER CUTWATER" });
            listAttributes.Add(new PumpAttributes() { PartName = "IMPELLER FRONT" });
            listAttributes.Add(new PumpAttributes() { PartName = "IMPELLER SIDE" });
            listAttributes.Add(new PumpAttributes() { PartName = "THROATBUSH" });
            listAttributes.Add(new PumpAttributes() { PartName = "FRAME PLATE LINER INSERT" });
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            LoadData();

            int i = 0;
            foreach (PictureBox pbPart in gbPhotos.Controls)
            {
                pbPart.Tag = listAttributes[i++];
                pbPart.AllowDrop = true;
                pbPart.SizeMode = PictureBoxSizeMode.Zoom;
                pbPart.DragEnter += PbPart_DragEnter;
                pbPart.DragDrop += PbPart_DragDrop;
                pbPart.Click += pbPart_Click;
            }
        }

        private void PbPart_DragDrop(object sender, DragEventArgs e)
        {
            PictureBox pbPart = sender as PictureBox;
            var files = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            PumpAttributes attributes = pbPart.Tag as PumpAttributes;

            attributes.ImageFilePath = files[0];
            if (string.IsNullOrEmpty(txtAssetNum.Text))
            {
                txtAssetNum.Text = Path.GetFileName(Path.GetDirectoryName(files[0]));
            }
            using (var img = Image.FromFile(attributes.ImageFilePath))
            {
                pbPart.Image = new Bitmap(img);
            }
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
            // error checking
            if (string.IsNullOrEmpty(txtAssetNum.Text))
            {
                MessageBox.Show("Asset # is empty.");
                return;
            }

            foreach (PictureBox pbPart in gbPhotos.Controls)
            {
                PumpAttributes attrib = pbPart.Tag as PumpAttributes;

                // 201705 Group 01-1 IMPELLER FRONT.JPG
                if (File.Exists(attrib.ImageFilePath))
                {
                    string fnNew = $"{dtpOH.Value.ToString("yyyyMM")} {txtAssetNum.Text.Trim()} {((PumpAttributes)pbPart.Tag).PartName}";
                    string fpNew = $"{Path.Combine(Path.GetDirectoryName(attrib.ImageFilePath), fnNew)}{Path.GetExtension(attrib.ImageFilePath)}";
                    MessageBox.Show(fpNew);
                    File.Move(attrib.ImageFilePath, fpNew);
                }
            }
        }
    }
}